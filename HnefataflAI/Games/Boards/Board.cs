using HnefataflAI.Commons;
using HnefataflAI.Commons.DataTypes;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Defaults;
using HnefataflAI.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace HnefataflAI.Games.Boards
{
    /// <summary>
    /// The board object
    /// </summary>
	public class Board
	{
		#region Properties
        /// <summary>
        /// The matrix of the board
        /// </summary>
		private readonly Matrix<IPiece> board;
        /// <summary>
        /// The number of total rows in the board
        /// </summary>
        public int TotalRows { get; private set; }
        /// <summary>
        /// The number of total columns in the board
        /// </summary>
		public int TotalCols { get; private set; }
        /// <summary>
        /// The board corners
        /// </summary>
        public List<Position> CornerTiles { get; private set; }
        /// <summary>
        /// The board anvils
        /// </summary>
        public List<Position> ThroneTiles { get; private set; }
        /// <summary>
        /// The base camps for the attacker
        /// </summary>
        public List<Position> AttackerBaseCamps { get; private set; }
        /// <summary>
        /// The base camps for the defender
        /// </summary>
        public List<Position> DefenderBaseCamps { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Create an empty board
        /// </summary>
        /// <param name="numRows">The number of rows</param>
        /// <param name="numCols">The number of columns</param>
        public Board(int numRows, int numCols)
		{
			this.TotalRows = numRows;
			this.TotalCols = numCols;
			this.board = new Matrix<IPiece>(numRows, numCols);
            this.AttackerBaseCamps = new List<Position>();
            this.DefenderBaseCamps = new List<Position>();
            this.CornerTiles = new List<Position>();
            this.ThroneTiles = new List<Position>();
        }
        /// <summary>
        /// Add the default 4 corners to the board
        /// </summary>
        public void AddCornerTiles()
        {
            List<Position> corners = new List<Position>
            {
                new Position(1, DefaultValues.FIRST_COLUMN),
                new Position(this.TotalRows, DefaultValues.FIRST_COLUMN),
                new Position(1, (char)(DefaultValues.FIRST_COLUMN + this.TotalCols - 1)),
                new Position(this.TotalRows, (char)(DefaultValues.FIRST_COLUMN + this.TotalCols - 1))
            };
            this.AddCornerTiles(corners);
        }
        /// <summary>
        /// Add the given corners to the board
        /// </summary>
        /// <param name="corners">The corners to add</param>
        public void AddCornerTiles(List<Position> corners)
        {
            this.CornerTiles.AddRange(corners);
        }
        /// <summary>
        /// Add the default throne tile to the board
        /// </summary>
        public void AddThroneTiles()
        {
            List<Position> thrones = new List<Position>
            {
                new Position(this.TotalRows / 2 + 1, (char)(DefaultValues.FIRST_COLUMN + (this.TotalCols / 2)))
            };
            this.AddThroneTiles(thrones);
        }
        /// <summary>
        /// Add the throne tiles to the board
        /// </summary>
        /// <param name="thrones">The thrones to add</param>
        public void AddThroneTiles(List<Position> thrones)
        {
            this.ThroneTiles.AddRange(thrones);
        }
        /// <summary>
        /// Add the default base camps for a player to the board.
        /// </summary>
		public void AddBaseCamps()
        {
            this.AddBaseCamps(this.AttackerBaseCamps, PieceColors.BLACK);
            this.AddBaseCamps(this.DefenderBaseCamps, PieceColors.WHITE);
        }
        /// <summary>
        /// Add the base camps for a player to the board.
        /// </summary>
        /// <param name="baseCamps">The empty list of basecamps</param>
        /// <param name="pieceColor">The pieces color</param>
        public void AddBaseCamps(List<Position> baseCamps, PieceColors pieceColor)
        {
            baseCamps.AddRange(this.GetPiecesByColor(pieceColor).Select(piece => piece.Position).ToList());
        }
        #endregion

        #region Getters
        /// <summary>
        /// Get the board's matrix
        /// </summary>
        /// <returns>The board's matrix</returns>
        public Matrix<IPiece> GetCurrentBoard()
		{
			return this.board;
		}
		#endregion

		#region Piece handling
        /// <summary>
        /// Add a piece to the board
        /// </summary>
        /// <param name="piece">The piece to be added</param>
		public void AddPiece(IPiece piece)
		{
			this.Set(piece, piece.Position);
		}
        /// <summary>
        /// Remove a piece from the board
        /// </summary>
        /// <param name="piece">The piece to be removed</param>
		public void RemovePiece(IPiece piece)
		{
			IPiece currentPiece = this.At(piece.Position);
			if (currentPiece.Equals(piece))
			{
                // removing a piece means setting the matrix entry to null
				this.Set(null, piece.Position);
			}
		}
        /// <summary>
        /// Set a piece in the position of the board
        /// </summary>
        /// <param name="piece">The piece to be set</param>
        /// <param name="position">The position in which to set the piece</param>
		private void Set(IPiece piece, Position position)
		{
			int arrRow = BoardUtils.GetArrayRow(this.TotalRows, position.Row);
			int arrCol = BoardUtils.GetArrayCol(position.Col);
			this.board.Set(arrRow, arrCol, piece);
		}
        /// <summary>
        /// Get the piece at the given position
        /// </summary>
        /// <param name="position">The position</param>
        /// <returns>The piece at the given position</returns>
		public IPiece At(Position position)
		{
			int arrRow = BoardUtils.GetArrayRow(this.TotalRows, position.Row);
			int arrCol = BoardUtils.GetArrayCol(position.Col);
			return this.board.At(arrRow, arrCol);
		}
        /// <summary>
        /// Get all the pieces in the board
        /// </summary>
        /// <returns>All the pieces in the board</returns>
		public List<IPiece> GetPieces()
		{
            return this.GetPiecesWithNull().Where(piece => piece != null).ToList();
		}
        /// <summary>
        /// Get all the pieces (null included) in the board
        /// </summary>
        /// <returns>All the pieces (null included) in the board</returns>
		public List<IPiece> GetPiecesWithNull()
		{
			List<IPiece> pieces = new List<IPiece>();
			for (int i = 0; i < this.TotalRows; i++)
            {
				for (int j = 0; j < this.TotalCols; j++)
                {
                    pieces.Add(this.board.At(i, j));
                }
            }
			return pieces;
		}
        /// <summary>
        /// Get all the pieces in the board of the given color
        /// </summary>
        /// <param name="pieceColor"></param>
        /// <returns>All the pieces in the board of the given color</returns>
		public List<IPiece> GetPiecesByColor(PieceColors pieceColor)
		{
			return this.GetPieces().Where(piece => piece.PieceColors.Equals(pieceColor)).ToList();
		}
		#endregion

		#region Other
        /// <summary>
        /// The overridden ToString method
        /// </summary>
        /// <returns>The string representation of the board</returns>
		public override string ToString()
		{
			string result = System.String.Format("\t{0}\n\r", BoardUtils.GetBoardColumnsChars(this.TotalCols));
			for (int i = 0; i < this.TotalRows; i++)
			{
				result += this.TotalRows - i + "\t";
				for (int j = 0; j < this.TotalCols; j++)
				{
					IPiece piece = this.board.At(i, j);
					result += piece == null ? BoardUtils.GetPositionBoardRepresentation(i, j, this) : piece.PieceRepresentation();
				}
				result += "\n\r";
			}
			return result;
		}
		#endregion
	}
}
