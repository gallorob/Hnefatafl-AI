using HnefataflAI.Commons;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System;
using System.Collections.Generic;

namespace HnefataflAI.Games.Engine.Impl
{
    public class HnefataflGameEngine : IGameEngine
    {
        internal List<Move> WhiteMoves = new List<Move>();
        internal List<Move> BlackMoves = new List<Move>();
        internal IRuleEngine RuleEngine = new RuleEngineImpl();
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
        public bool GetGameStatus(IPiece movedPiece, Board board)
        {
            bool isGameOver = false;
            List<IPiece> capturedPieces = this.RuleEngine.CheckIfHasCaptured(movedPiece, board);
            foreach (IPiece piece in capturedPieces)
            {
                if (piece is King && !isGameOver)
                {
                    // winning condition for Attacker
                    isGameOver = this.RuleEngine.CheckIfKingIsCaptured(piece, board);
                }
                else
                {
                    board.RemovePiece(piece);
                }
            }
            if (!isGameOver)
            {
                // winning condition for Defender
                isGameOver = movedPiece is King && this.RuleEngine.IsMoveOnBoardCorner(movedPiece.Position, board.TotalRows, board.TotalCols);
            }
            if (!isGameOver)
            {
                // losing condition for Attacker
                isGameOver = HasRepeatedMoves(this.BlackMoves);
            }
            if (!isGameOver)
            {
                // losing condition for Defender
                isGameOver = HasRepeatedMoves(this.WhiteMoves);
            }
            return isGameOver;
        }
        private bool HasRepeatedMoves(List<Move> moves)
        {
            if (moves.Count > 5)
            {
                return moves[moves.Count - 1].Equals(moves[moves.Count - 3])
                    &&
                    moves[moves.Count - 3].Equals(moves[moves.Count - 5]);
            }
            return false;
        }
    }
}
