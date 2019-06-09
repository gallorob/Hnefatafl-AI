using HnefataflAI.Commons;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Pieces;
using System;
using System.Collections.Generic;

namespace HnefataflAI.Games.Engine.Impl
{
    public class HnefataflGameEngine : IGameEngine
    {
        internal List<Move> WhiteMoves = new List<Move>();
        internal List<Move> BlakcMoves = new List<Move>();
        internal IRuleEngine RuleEngine = new RuleEngineImpl();
        public Move ProcessPlayerMove(string[] playerMove, Board board)
        {
            string[] inputFrom = playerMove[0].Split(':');
            Position from = PositionUtils.ValidateAndReturnInputPosition(inputFrom);
            ValidatePosition(from, board);
            string[] inputTo = playerMove[1].Split(':');
            Position to = PositionUtils.ValidateAndReturnInputPosition(inputTo);
            ValidatePosition(to, board);
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
                this.BlakcMoves.Add(move);
            }
            else
            {
                this.WhiteMoves.Add(move);
            }

            board.RemovePiece(move.Piece, move.From);
            board.AddPiece(move.Piece, move.To);
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
    }
}
