using HnefataflAI.Commons.DataTypes;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Defaults;
using HnefataflAI.Games.Boards;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HnefataflAI.Commons.Utils
{
    /// <summary>
    /// Utils class for the board
    /// </summary>
    public static class BoardUtils
    {
        /// <summary>
        /// Update a piece in the board
        /// </summary>
        /// <param name="piece">The piece to be updated</param>
        /// <param name="position">The new piece position</param>
        /// <param name="board">The board</param>
        public static void UpdatePieceFromMove(IPiece piece, Position position, Board board)
        {
            board.RemovePiece(piece);
            piece.UpdatePosition(position);
            board.AddPiece(piece);
        }

        public static void UpdatePieceFromMove(Position from, Position to, Board board)
        {
            IPiece piece = board.At(from);
            board.RemovePiece(piece);
            piece.UpdatePosition(to);
            board.AddPiece(piece);
        }

        /// <summary>
        /// Check if the board dimensions are valid
        /// </summary>
        /// <param name="totalRows">The number of rows</param>
        /// <param name="totalCols">The number of columns</param>
        public static void CheckBoardDimensions(int totalRows, int totalCols)
        {
            if (totalRows < 5 || totalRows > DefaultValues.MAX_ROWS || (totalRows % 2) == 0
                ||
                totalCols < 5 || totalCols > DefaultValues.MAX_COLS || (totalCols % 2) == 0)
            {
                throw new CustomGenericException(typeof(BoardUtils).Name, MethodBase.GetCurrentMethod().Name, ErrorMessages.INVALID_BOARD_DIMENSION);
            }
        }
        /// <summary>
        /// Get the array value of a row
        /// </summary>
        /// <param name="numRows">The total number of rows</param>
        /// <param name="row">The current row</param>
        /// <returns>The array value of a row</returns>
        public static int GetArrayRow(int numRows, int row)
        {
            return numRows - row;
        }
        /// <summary>
        /// Get the array value of a column
        /// </summary>
        /// <param name="col">The column</param>
        /// <returns>The array value of a column</returns>
        public static int GetArrayCol(int col)
        {
            return col - DefaultValues.FIRST_COLUMN;
        }
        public static Position GetPositionFromArrayValues(int row, int col)
        {
            return new Position(row + 1, (char)(col + DefaultValues.FIRST_COLUMN));
        }
        /// <summary>
        /// Get the string representation of the board's columns
        /// </summary>
        /// <param name="totalCols">The number of columns</param>
        /// <returns>The string representation of the board's columns</returns>
        public static string GetBoardColumnsChars(int totalCols)
        {
            StringBuilder ret = new StringBuilder("");
            for (int i = 0; i < totalCols; i++)
            {
                ret.AppendFormat(" {0} ", (char)(i + DefaultValues.UPPER_FIRST_COLUMN));
            }
            return ret.ToString();
        }
        /// <summary>
        /// Check if an update to a position to a given direction is valid in the board
        /// </summary>
        /// <param name="position">The starting position</param>
        /// <param name="direction">The direction to move to</param>
        /// <param name="board">The board</param>
        /// <returns>If a position update is valid</returns>
        public static bool IsPositionMoveValid(Position position, Directions direction, Board board)
        {
            return IsPositionValid(position.MoveTo(direction), board);
        }
        /// <summary>
        /// Check if a given position is valid in the board
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <param name="board">The board</param>
        /// <returns>If the position is valid</returns>
        public static bool IsPositionValid(Position position, Board board)
        {
            return (
                position.Row <= board.TotalRows
                &&
                position.Row > 0
                &&
                position.Col >= DefaultValues.FIRST_COLUMN
                &&
                position.Col < DefaultValues.FIRST_COLUMN + board.TotalCols
                );
        }
        /// <summary>
        /// Check if a given position is on an edge of the board
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <param name="board">The board</param>
        /// <returns>If a given position is on an edge</returns>
        public static bool IsOnEdge(Position position, Board board)
        {
            return (
                position.Row == board.TotalRows
                ||
                position.Row == 1
                ||
                position.Col == DefaultValues.FIRST_COLUMN
                ||
                position.Col == DefaultValues.FIRST_COLUMN + board.TotalCols - 1
                );
        }
        /// <summary>
        /// Check if a position is on a board's corner
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <param name="board">The board</param>
        /// <returns>If a position is on a board's corner</returns>
        public static bool IsOnCorner(Position position, Board board)
		{
			return position.Row == 0 && position.Col == 0
				|| position.Row == 0 && position.Col == board.TotalCols
				|| position.Row == board.TotalRows && position.Col == 0
				|| position.Row == board.TotalRows && position.Col == board.TotalCols;

			//return BoardMapper.LookUpTable[position].Equals(TileTypes.CORNER);

			//bool isOnBoardCorner = false;
			//foreach (Position corner in board.CornerTiles)
			//{
			//    isOnBoardCorner |= position.Equals(corner);
			//}
			//return isOnBoardCorner;
		}
        /// <summary>
        /// Check if a position is on a board's throne
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <param name="board">The board</param>
        /// <returns>If a position is on a board's throne</returns>
        public static bool IsOnThrone(Position position, Board board)
        {
			return position.Row == board.TotalRows / 2 - 1 && position.Col == board.TotalCols / 2 - 1;

            //return BoardMapper.LookUpTable[position].Equals(TileTypes.THRONE);

            //bool isOnThrone = false;
            //foreach (Position throne in board.ThroneTiles)
            //{
            //    isOnThrone |= position.Equals(throne);
            //}
            //return isOnThrone;
        }
        /// <summary>
        /// Check if a position is an enemy's base camp
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <param name="board">The board</param>
        /// <returns>If a position is an enemy's base camp</returns>
        public static bool IsOnEnemyCamp(Position position, Board board)
        {
            return BoardMapper.LookUpTable[position].Equals(TileTypes.BASECAMP);
            //bool isOnEnemyBaseCamp = false;
            //foreach (Position baseCamp in board.AttackerBaseCamps)
            //{
            //    isOnEnemyBaseCamp |= position.Equals(baseCamp);
            //}
            //return isOnEnemyBaseCamp;
        }
        /// <summary>
        /// Get the first piece in a direction from the given position
        /// </summary>
        /// <param name="board">The board</param>
        /// <param name="direction">The direction to check</param>
        /// <param name="position">The starting position</param>
        /// <returns>The first piece if found, else null</returns>
        public static IPiece GetFirstPieceFromPosition(Board board, Directions direction, Position position)
        {
            List<IPiece> pieces = new List<IPiece>();
            Matrix<IPiece> matrix = board.GetCurrentBoard();
            int arrRow = board.TotalRows - position.Row;
            int arrCol = position.Col - DefaultValues.FIRST_COLUMN;
            switch (direction)
            {
                case Directions.DOWN:
                    pieces.AddRange(matrix.GetCol(arrCol, board.TotalRows - arrRow - 1, arrRow + 1));
                    break;
                case Directions.LEFT:
                    pieces.AddRange(matrix.GetRow(arrRow, arrCol, 0));
                    break;
                case Directions.RIGHT:
                    pieces.AddRange(matrix.GetRow(arrRow, board.TotalCols - arrCol - 1, arrCol + 1));
                    break;
                case Directions.UP:
                    pieces.AddRange(matrix.GetCol(arrCol, arrRow, 0));
                    break;
            }
            // reverse list to get the first piece closest to the position
            if (direction == Directions.UP || direction == Directions.LEFT)
            {
                pieces.Reverse();
            }
            // return the first non-null piece if it exists
            foreach (IPiece piece in pieces)
            {
                if (piece != null)
                {
                    return piece;
                }
            }
            return null;
        }
        /// <summary>
        /// Return the first piece found in a given direction at a row/column N
        /// </summary>
        /// <param name="board">The board</param>
        /// <param name="direction">The direction in which to check</param>
        /// <param name="n">The row/column number</param>
        /// <returns>The first piece if found, else null</returns>
        public static IPiece GetFirstPieceInDirection(Board board, Directions direction, int n)
        {
            List<IPiece> pieces = new List<IPiece>();
            Matrix<IPiece> matrix = board.GetCurrentBoard();
            switch (direction)
            {
                case Directions.UP:
                case Directions.DOWN:
                    pieces.AddRange(matrix.GetCol(n));
                    break;
                case Directions.RIGHT:
                case Directions.LEFT:
                    pieces.AddRange(matrix.GetRow(n));
                    break;
            }
            // reverse list to get the first piece closest to the position
            if (direction == Directions.UP || direction == Directions.LEFT)
            {
                pieces.Reverse();
            }
            // return the first non-null piece if it exists
            foreach (IPiece piece in pieces)
            {
                if (piece != null)
                {
                    return piece;
                }
            }
            return null;
        }
        //public static List<IPiece> GetPieceNeighbouringPieces(IPiece piece, Board board)
        //{
        //    List<IPiece> neighbours = new List<IPiece>();
        //    foreach (Directions direction in PositionUtils.GetAllClockWiseDirections())
        //    {
        //        if (IsPositionMoveValid(piece.Position, direction, board))
        //        {
        //            Position newPosition = piece.Position.MoveTo(direction);
        //            IPiece neighbour = board.At(newPosition);
        //            neighbours.Add(neighbour);
        //        }
        //    }
        //    return neighbours;
        //}
        public static string GetPositionBoardRepresentation(int row, int col, Board board)
        {
            Position toCheck = GetPositionFromArrayValues(row, col);
            TileTypes tile = BoardMapper.LookUpTable[toCheck];
            switch (tile)
            {
                case TileTypes.SIMPLE:
                    return " . ";
                case TileTypes.THRONE:
                    return " x ";
                case TileTypes.CORNER:
                    return " * ";
                case TileTypes.BASECAMP:
                    return " a ";
                default:
                    throw new CustomGenericException(typeof(BoardUtils).Name, MethodBase.GetCurrentMethod().Name, string.Format("Unrecognized tile type {0}", tile));
            }
            //if (board.ThroneTiles.Contains(toCheck))
            //{
            //    return " x ";
            //}
            //if (board.CornerTiles.Contains(toCheck))
            //{
            //    return " * ";
            //}
            //if (board.AttackerBaseCamps.Contains(toCheck))
            //{
            //    return " a ";
            //}
            //return " . ";
        }
        public static Board DuplicateBoard(Board board)
        {
            int boardCols = board.TotalCols;
            Board newBoard = new Board(board.TotalRows, boardCols);
            //newBoard.AddThroneTiles(board.ThroneTiles);
            //newBoard.AddCornerTiles(board.CornerTiles);
            //newBoard.AddBaseCamps(board.AttackerBaseCamps, PieceColors.BLACK);
            char[] oldBoard = board.AsString().ToCharArray();
            for (int i = 0; i < oldBoard.Length; i++)
            {
                char c = oldBoard[i];
                if (c != '.')
                {
                    if (c == 'K')
                    {
                        newBoard.AddPiece(new King(GetPositionFromIndex(i, boardCols)));
                    }
                    else if (c == 'D')
                    {
                        newBoard.AddPiece(new Defender(GetPositionFromIndex(i, boardCols)));
                    }
                    else if (c == 'A')
                    {
                        newBoard.AddPiece(new Attacker(GetPositionFromIndex(i, boardCols)));
                    }
                    else if (c == 'G')
                    {
                        newBoard.AddPiece(new EliteGuard(GetPositionFromIndex(i, boardCols)));
                    }
                    else if (c == 'C')
                    {
                        newBoard.AddPiece(new Commander(GetPositionFromIndex(i, boardCols)));
                    }
                }
            }
            return newBoard;
        }
        private static Position GetPositionFromIndex(int index, int boardCols)
        {
            int actualCol = index % boardCols;
            int actualRow = boardCols - (index / boardCols);
            return new Position(actualRow, (char)(DefaultValues.FIRST_COLUMN + actualCol));
        }
    }
}
