using HnefataflAI.Commons;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.GameState;
using HnefataflAI.Games.Rules;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System.Collections.Generic;

namespace HnefataflAI.Games.Engine.Impl
{
    public class GameEngineImpl : IGameEngine
    {
        public IRuleEngine RuleEngine { get; private set; }
        internal List<Move> WhiteMoves;
        internal List<Move> BlackMoves;
        internal List<IPiece> CapturedPieces;
        public GameEngineImpl(RuleTypes ruleType)
        {
            this.RuleEngine = new RuleEngineImpl(RuleUtils.GetRule(ruleType));
            this.WhiteMoves = new List<Move>();
            this.BlackMoves = new List<Move>();
            this.CapturedPieces = new List<IPiece>();
        }
        public Move ProcessPlayerMove(string[] playerMove, Board board)
        {
            string inputFrom = playerMove[0];
            Position from = PositionUtils.ValidateAndReturnInputPosition(inputFrom);
            ValidatePosition(from, board);
            string inputTo = playerMove[1];
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
            ValidateMove(move, board, playerColor, RuleEngine.Rule.GetMoveRuleSet());
            switch (move.Piece.PieceColors)
            {
                case PieceColors.BLACK:
                    this.BlackMoves.Add(move);
                    break;
                case PieceColors.WHITE:
                    this.WhiteMoves.Add(move);
                    break;
            }
            BoardUtils.UpdatePieceFromMove(move.Piece, move.To, board);
        }
        public void UndoMove(Move move, Board board, PieceColors playerColor)
        {
            switch (playerColor)
            {
                case PieceColors.BLACK:
                    this.BlackMoves.Remove(move);
                    break;
                case PieceColors.WHITE:
                    this.WhiteMoves.Remove(move);
                    break;
            }
            BoardUtils.UpdatePieceFromMove(move.Piece, move.From, board);
        }
        private void ValidateMove(Move move, Board board, PieceColors playerColor, MoveRuleSet moveRuleSet)
        {
            if (!BoardUtils.IsPositionValid(move.From, board))
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
            if (!MoveUtils.CanMoveToPosition(move.Piece, move.To, board, moveRuleSet))
            {
                throw new InvalidMoveException(move, ErrorMessages.INVALID_DESTINATION_POSITION);
            }
        }
        private void ValidatePosition(Position position, Board board)
        {
            if (!BoardUtils.IsPositionValid(position, board))
            {
                throw new InvalidMoveException(position, ErrorMessages.POSITION_OUT_OF_BOARD);
            }
        }
        private void ApplyCapture(IPiece piece, Board board)
        {
            board.RemovePiece(piece);
            this.CapturedPieces.Add(piece);
        }
        public void UndoCaptures(Board board)
        {
            foreach(IPiece piece in this.CapturedPieces)
            {
                board.AddPiece(piece);
            }
            this.CapturedPieces.Clear();
        }
        // todo: test if it works with AI
        public void UndoCaptures(Board board, List<IPiece> captures)
        {
            foreach (IPiece piece in captures)
            {
                board.AddPiece(piece);
                this.CapturedPieces.Remove(piece);
            }
        }
        public GameStatus GetGameStatus(IPiece movedPiece, Board board)
        {
            GameStatus gameStatus = this.RuleEngine.GetGameStatus(movedPiece, board, this.WhiteMoves, this.BlackMoves);
            foreach (IPiece capturedPiece in gameStatus.CapturedPieces)
            {
                if (!(capturedPiece is King) || (capturedPiece is King && gameStatus.IsGameOver))
                {
                    ApplyCapture(capturedPiece, board);
                }
            }
            // todo: move to rule engine
            if (!gameStatus.IsGameOver)
            {
                if (!this.RuleEngine.CanMove(gameStatus.NextPlayer, board))
                {
                    gameStatus.Status = Status.WIN;
                    gameStatus.IsGameOver = true;
                }
            }
            return gameStatus;
        }

        public List<Move> GetMovesByColor(PieceColors pieceColor, Board board)
        {
            return this.RuleEngine.GetAvailableMoves(pieceColor, board);
        }
    }
}
