using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System.Collections.Generic;

namespace HnefataflAI.Games.Engine.Impl
{
    public class RuleEngineImpl : IRuleEngine
    {
        public bool CanMoveToPosition(IPiece piece, Position position, int totalRows, int totalCols)
        {
            return (piece is King) || !(IsMoveOnBoardCorner(position, totalRows, totalCols) || IsMoveOnThrone(position, totalRows, totalCols));
        }
        public bool IsPositionUpdateValid(Position moved, Directions direction, int totalRows, int totalCols)
        {
            return BoardUtils.IsPositionUpdateValid(moved, direction, totalRows, totalCols);
        }
        public bool IsPositionValid(Position position, int totalRows, int totalCols)
        {
            return BoardUtils.IsPositionValid(position, totalRows, totalCols);
        }
        public bool IsMoveOnBoardCorner(Position move, int totalRows, int totalCols)
        {
            return BoardUtils.IsOnBoardCorner(move, totalRows, totalCols);
        }
        public bool IsMoveOnThrone(Position move, int totalRows, int totalCols)
        {
            return BoardUtils.IsOnThrone(move, totalRows, totalCols);
        }
    }
}
