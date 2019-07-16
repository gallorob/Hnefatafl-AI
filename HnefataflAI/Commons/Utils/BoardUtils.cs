﻿using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Defaults;
using HnefataflAI.Games.Boards;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;

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
        public static bool IsPositionUpdateValid(Position position, Directions direction, int totalRows, int totalCols)
        {
            switch (direction)
            {
                case Directions.UP:
                    return position.Row < totalRows;
                case Directions.DOWN:
                    return position.Row > 1;
                case Directions.LEFT:
                    return position.Col > DefaultValues.FIRST_COLUMN;
                case Directions.RIGHT:
                    return position.Col < DefaultValues.FIRST_COLUMN + totalCols - 1;
                default:
                    return false;
            }
        }
        public static bool IsPositionValid(Position position, int totalRows, int totalCols)
        {
            return (position.Row <= totalRows
                &&
                position.Row > 0
                &&
                position.Col >= DefaultValues.FIRST_COLUMN
                &&
                position.Col < DefaultValues.FIRST_COLUMN + totalCols);
        }
        public static bool IsOnBoardCorner(Position position, int totalRows, int totalCols)
        {
            return (
                (position.Col == DefaultValues.FIRST_COLUMN && position.Row == 1)
                ||
                (position.Col == DefaultValues.FIRST_COLUMN && position.Row == totalRows)
                ||
                (position.Col == DefaultValues.FIRST_COLUMN + totalCols - 1 && position.Row == 1)
                ||
                (position.Col == DefaultValues.FIRST_COLUMN + totalCols - 1 && position.Row == totalRows)
                );
        }
        public static bool IsOnThrone(Position position, int totalRows, int totalCols)
        {
            return (position.Col - DefaultValues.FIRST_COLUMN == totalCols / 2
                &&
                position.Row == totalRows / 2 + 1);
        }
        public static bool CanMoveToPosition(IPiece piece, Position position, int totalRows, int totalCols)
        {
            return (piece is King) || !(IsOnBoardCorner(position, totalRows, totalCols) || IsOnThrone(position, totalRows, totalCols));
        }
    }
}
