using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Pieces;
using System.Collections.Generic;

namespace HnefataflAI.Games.Engine
{
    public interface IRuleEngine
    {
        bool CanMoveToPosition(IPiece piece, Position position, int totalRows, int totalCols);
        bool IsPositionUpdateValid(Position moved, Directions direction, int totalRows, int totalCols);
        bool IsPositionValid(Position position, int totalRows, int totalCols);
        bool IsMoveOnBoardCorner(Position move, int totalRows, int totalCols);
        bool IsMoveOnThrone(Position move, int totalRows, int totalCols);
    }
}
