using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Games;
using HnefataflAI.Games.Engine;
using HnefataflAI.Games.Engine.Impl;
using HnefataflAI.Pieces;
using System;
using System.Collections.Generic;

namespace HnefataflAI.AI.RuleEngine
{
    class BotEngine
    {
        internal IRuleEngine RuleEngine;
        public BotEngine()
        {
            this.RuleEngine = new RuleEngineImpl();
        }
        public List<Move> GetAvailableMovesByColor(PieceColors pieceColor, Board board)
        {
            List<Move> AvailableMoves = new List<Move>();
            List<IPiece> pieces = board.GetPiecesByColor(pieceColor);

            foreach (IPiece piece in pieces)
            {
                AvailableMoves.AddRange(GetMovesForPiece(piece, board));
            }

            return AvailableMoves;
        }
        private List<Move> GetMovesForPiece(IPiece piece, Board board)
        {
            List<Move> availableMoves = new List<Move>();
            foreach (Directions direction in Enum.GetValues(typeof(Directions)))
            {
                MovesByDirection(direction, piece, availableMoves, board);
            }
            return availableMoves;
        }
        private void MovesByDirection(Directions direction, IPiece piece, List<Move> availableMoves, Board board)
        {
            Position moved = piece.Position;
            while (this.RuleEngine.IsPositionUpdateValid(moved, direction, board.TotalRows, board.TotalCols))
            {
                moved = moved.MoveTo(direction);
                if (board.At(moved) != null)
                {
                    break;
                }
                if (this.RuleEngine.CanMoveToPosition(piece, moved, board.TotalRows, board.TotalCols))
                {
                    Move move = new Move(piece, moved);
                    availableMoves.Add(move);
                }
            }
        }
    }
}
