using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Defaults;
using HnefataflAI.Games.Boards;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System.Collections.Generic;

namespace HnefataflAI.Commons.Utils
{
    public static class BoardUtils
    {
        public static void UpdatePieceFromMove(IPiece piece, Position position, Board board)
        {
            board.RemovePiece(piece);
            piece.UpdatePosition(position);
            board.AddPiece(piece);
        }
        public static void CheckBoardDimensions(int totalRows, int totalCols)
        {
            if (totalRows > DefaultValues.MAX_ROWS || totalCols > DefaultValues.MAX_COLS)
            {
                throw new System.Exception(ErrorMessages.INVALID_BOARD);
            }
        }
        public static int GetArrayRow(int numRows, int row)
        {
            return numRows - row;
        }
        public static int GetArrayCol(int col)
        {
            return col - DefaultValues.FIRST_COLUMN;
        }
        public static string GetBoardColumnsChars(int totalCols)
        {
            string ret = "";
            for (int i = 0; i < totalCols; i++)
            {
                ret += string.Format(" {0} ", (char)(System.Convert.ToUInt16(DefaultValues.UPPER_FIRST_COLUMN) + i));
            }
            return ret;
        }
        public static bool IsPositionUpdateValid(Position position, Directions direction, Board board)
        {
            switch (direction)
            {
                case Directions.UP:
                    return position.Row < board.TotalRows;
                case Directions.DOWN:
                    return position.Row > 1;
                case Directions.LEFT:
                    return position.Col > DefaultValues.FIRST_COLUMN;
                case Directions.RIGHT:
                    return position.Col < DefaultValues.FIRST_COLUMN + board.TotalCols - 1;
                default:
                    return false;
            }
        }
        public static bool IsPositionValid(Position position, Board board)
        {
            return (position.Row <= board.TotalRows
                &&
                position.Row > 0
                &&
                position.Col >= DefaultValues.FIRST_COLUMN
                &&
                position.Col < DefaultValues.FIRST_COLUMN + board.TotalCols);
        }
        public static bool IsOnBoardCorner(Position position, Board board)
        {
            bool isOnBoardCorner = false;
            foreach (Position corner in GetCornersPositions(board))
            {
                isOnBoardCorner |= position.Equals(corner);
            }
            return isOnBoardCorner;
        }
        public static List<Position> GetCornersPositions(Board board)
        {
            return new List<Position>
            {
                new Position(1, DefaultValues.FIRST_COLUMN),
                new Position(board.TotalRows, DefaultValues.FIRST_COLUMN),
                new Position(1, (char)(DefaultValues.FIRST_COLUMN + board.TotalCols - 1)),
                new Position(board.TotalRows, (char)(DefaultValues.FIRST_COLUMN + board.TotalCols - 1))
            };
        }
        public static bool IsOnThrone(Position position, Board board)
        {
            return position.Equals(GetThronePosition(board));
        }
        public static Position GetThronePosition(Board board)
        {
            return new Position(board.TotalRows / 2 + 1, (char)(DefaultValues.FIRST_COLUMN + (board.TotalCols / 2)));
        }
        public static bool CanMoveToPosition(IPiece piece, Position position, Board board)
        {
            return (piece is King) || !(IsOnBoardCorner(position, board) || IsOnThrone(position, board));
        }
    }
}
