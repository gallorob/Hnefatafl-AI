using HnefataflAI.Commons;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games.GameState;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System.Collections.Generic;

namespace HnefataflAI.Games.Engine.Impl
{
    public class HnefataflGameEngine : IGameEngine
    {
        internal List<Move> WhiteMoves = new List<Move>();
        internal List<Move> BlackMoves = new List<Move>();
        internal IRuleEngine RuleEngine = new RuleEngineImpl();
        internal List<IPiece> CapturedPieces = new List<IPiece>();
        public Move ProcessPlayerMove(string[] playerMove, Board board)
        {
            string[] inputFrom = playerMove[0].Split(':');
            Position from = PositionUtils.ValidateAndReturnInputPosition(inputFrom);
            ValidatePosition(from, board);
            string[] inputTo = playerMove[1].Split(':');
            Position to = PositionUtils.ValidateAndReturnInputPosition(inputTo);
            ValidatePosition(to, board);
            MoveUtils.ValidateMove(from, to);
            IPiece movedPiece = board.At(from);
            if (movedPiece == null)
            {
                throw new InvalidMoveException(from, ErrorMessages.NULL_PIECE);
            }
            return new Move(movedPiece, from, to);
        }
        public void ApplyMove(Move move, Board board, PieceColors playerColor)
        {
            ValidateMove(move, board, playerColor);
            if (move.Piece.PieceColors.Equals(PieceColors.BLACK))
            {
                this.BlackMoves.Add(move);
            }
            else
            {
                this.WhiteMoves.Add(move);
            }
            board.RemovePiece(move.Piece);
            move.Piece.UpdatePosition(move.To);
            board.AddPiece(move.Piece);
        }
        public void UndoMove(Move move, Board board, PieceColors playerColor)
        {
            if (move.Piece.PieceColors.Equals(PieceColors.BLACK))
            {
                this.BlackMoves.Remove(move);
            }
            else
            {
                this.WhiteMoves.Remove(move);
            }
            board.RemovePiece(move.Piece);
            move.Piece.UpdatePosition(move.From);
            board.AddPiece(move.Piece);
        }
        private void ValidateMove(Move move, Board board, PieceColors playerColor)
        {

            if (!this.RuleEngine.IsPositionValid(move.From, board.TotalRows, board.TotalCols))
            {
                throw new InvalidMoveException(move.From, ErrorMessages.POSITION_OUT_OF_BOARD);
            }
            if (!move.Piece.PieceColors.Equals(playerColor))
            {
                throw new InvalidMoveException(move, ErrorMessages.OPPONENT_PIECE);
            }
            if (board.At(move.To) != null)
            {
                throw new InvalidMoveException(move, ErrorMessages.INVALID_DESTINATION_PIECE);
            }
            if (!this.RuleEngine.CanMoveToPosition(move.Piece, move.To, board.TotalRows, board.TotalCols))
            {
                throw new InvalidMoveException(move, ErrorMessages.INVALID_DESTINATION_POSITION);
            }
        }
        private void ValidatePosition(Position position, Board board)
        {
            if (!this.RuleEngine.IsPositionValid(position, board.TotalRows, board.TotalCols))
            {
                throw new InvalidMoveException(position, ErrorMessages.POSITION_OUT_OF_BOARD);
            }
        }
        public GameStatus GetGameStatus(IPiece movedPiece, Board board)
        {
            GameStatus gameStatus = new GameStatus(false);
            if (!(movedPiece is King))
            {
                List<IPiece> capturedPieces = this.RuleEngine.CheckIfHasCaptured(movedPiece, board);
                foreach (IPiece piece in capturedPieces)
                {
                    if (piece is King && !gameStatus.IsGameOver)
                    {
                        // winning condition for Attacker
                        gameStatus.IsGameOver = this.RuleEngine.CheckIfKingIsCaptured(piece, board);
                        gameStatus.Status = Status.WIN;
                        if (gameStatus.IsGameOver)
                        {
                            board.RemovePiece(piece);
                            this.CapturedPieces.Add(piece);
                            gameStatus.CapturedPieces.Add(piece);
                        }
                    }
                    else
                    {
                        board.RemovePiece(piece);
                        this.CapturedPieces.Add(piece);
                        gameStatus.CapturedPieces.Add(piece);
                    }
                }
            }
            if (!gameStatus.IsGameOver)
            {
                // winning condition for Defender
                gameStatus.IsGameOver = movedPiece is King && this.RuleEngine.IsMoveOnBoardCorner(movedPiece.Position, board.TotalRows, board.TotalCols);
                gameStatus.Status = Status.WIN;
            }
            if (!gameStatus.IsGameOver)
            {
                // winning condition for Defender
                gameStatus.IsGameOver = board.GetPiecesByColor(PieceColors.BLACK).Count == 0;
                gameStatus.Status = Status.WIN;
            }
            if (!gameStatus.IsGameOver)
            {
                // losing condition for Attacker
                gameStatus.IsGameOver = this.RuleEngine.HasRepeatedMoves(this.BlackMoves);
                gameStatus.Status = Status.LOSS;
            }
            if (!gameStatus.IsGameOver)
            {
                // losing condition for Defender
                gameStatus.IsGameOver = this.RuleEngine.HasRepeatedMoves(this.WhiteMoves);
                gameStatus.Status = Status.LOSS;
            }
            return gameStatus;
        }
        public void UndoCaptures(Board board)
        {
            foreach(IPiece piece in this.CapturedPieces)
            {
                //this.CapturedPieces.Remove(piece);
                board.AddPiece(piece);
            }
            this.CapturedPieces.Clear();
        }
    }
}
