using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Games;
using HnefataflAI.Games.Engine;
using HnefataflAI.Games.Engine.Impl;
using HnefataflAI.Pieces;
using System.Collections.Generic;

namespace HnefataflAI.AI.RuleEngine
{
    class BotRuleEngine
    {
        public Board Board { get; private set; }
        internal IRuleEngine RuleEngine;
        public BotRuleEngine(Board board)
        {
            this.Board = board;
            this.RuleEngine = new RuleEngineImpl();
        }
        public void UpdateBoardState(Board board)
        {
            this.Board = board;
        }
        public List<Move> GetAvailableMovesByColor(PieceColors pieceColor)
        {
            List<Move> AvailableMoves = new List<Move>();
            List<IPiece> pieces = this.Board.GetPiecesByColor(pieceColor);

            foreach (IPiece piece in pieces)
            {
                AvailableMoves.AddRange(GetMovesForPiece(piece));
            }

            return AvailableMoves;
        }
        private List<Move> GetMovesForPiece(IPiece piece)
        {
            List<Move> availableMoves = new List<Move>();
            // moves up
            MovesByDirection(Directions.UP, piece, availableMoves);
            // moves down
            MovesByDirection(Directions.DOWN, piece, availableMoves);
            // moves right
            MovesByDirection(Directions.RIGHT, piece, availableMoves);
            // moves left
            MovesByDirection(Directions.LEFT, piece, availableMoves);
            return availableMoves;
        }
        private void MovesByDirection(Directions direction, IPiece piece, List<Move> availableMoves)
        {
            Position moved = piece.Position.MoveTo(direction);
            while (this.RuleEngine.IsPositionUpdateValid(moved, direction, this.Board.TotalRows, this.Board.TotalCols))
            {
                if (this.Board.At(moved) != null)
                {
                    break;
                }
                if (this.RuleEngine.CanMoveToPosition(piece, moved, this.Board.TotalRows, this.Board.TotalCols))
                {
                    Move move = new Move(piece, moved);
                    availableMoves.Add(move);
                }
                moved = moved.MoveTo(direction);
            }
        }
    }
}
