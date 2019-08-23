using HnefataflAI.Commons;
using HnefataflAI.Commons.DataTypes;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Defaults;
using HnefataflAI.Pieces;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        }

        public Position GetCenterPosition()
        {
            return new Position(this.TotalRows / 2 + 1, (char)(DefaultValues.FIRST_COLUMN + (this.TotalCols / 2)));
        }

        public void FinalizeCreation()
        {
            BoardMapper.InitializeTable(TotalRows, TotalCols);
            AddBaseCamps();
        }

        /// <summary>
        /// Add the default 4 corners to the board
        /// </summary>
        public void AddCornerTiles()
        {
            BoardMapper.AddEntry(new Position(1, DefaultValues.FIRST_COLUMN), TileTypes.CORNER);
            BoardMapper.AddEntry(new Position(this.TotalRows, DefaultValues.FIRST_COLUMN), TileTypes.CORNER);
            BoardMapper.AddEntry(new Position(1, (char)(DefaultValues.FIRST_COLUMN + this.TotalCols - 1)), TileTypes.CORNER);
            BoardMapper.AddEntry(new Position(this.TotalRows, (char)(DefaultValues.FIRST_COLUMN + this.TotalCols - 1)), TileTypes.CORNER);
        }
        /// <summary>
        /// Add the given corners to the board
        /// </summary>
        /// <param name="corners">The corners to add</param>
        public void AddCornerTiles(HashSet<Position> corners)
        {
            for (int i = 0; i < corners.Count; i++)
            {
                BoardMapper.AddEntry(corners.ElementAt(i), TileTypes.CORNER);
            }
        }
        /// <summary>
        /// Add the default throne tile to the board
        /// </summary>
        public void AddThroneTiles()
        {
            BoardMapper.AddEntry(GetCenterPosition(), TileTypes.THRONE);
        }
        /// <summary>
        /// Add the throne tiles to the board
        /// </summary>
        /// <param name="thrones">The thrones to add</param>
        public void AddThroneTiles(HashSet<Position> thrones)
        {
            for (int i = 0; i < thrones.Count; i++)
            {
                BoardMapper.AddEntry(thrones.ElementAt(i), TileTypes.THRONE);
            }
        }
        /// <summary>
        /// Add the default base camps for a player to the board.
        /// </summary>
		public void AddBaseCamps()
        {
            Position[] positions = GetPiecesByColor(PieceColors.BLACK).Select(piece => piece.Position).ToArray();
            for (int i = 0; i < positions.Length; i++)
            {
                BoardMapper.AddEntry(positions.ElementAt(i), TileTypes.BASECAMP);
            }
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
            StringBuilder result = new StringBuilder();
            result.AppendFormat("\t\t{0}\n\r", BoardUtils.GetBoardColumnsChars(this.TotalCols));
			for (int i = 0; i < this.TotalRows; i++)
			{
				result.AppendFormat("{0}\t", this.TotalRows - i + "\t");
				for (int j = 0; j < this.TotalCols; j++)
				{
					IPiece piece = this.board.At(i, j);
                    result.Append(piece == null ? BoardUtils.GetPositionBoardRepresentation(i, j, this) : piece.PieceRepresentation());
				}
                result.Append("\n\r");
			}
			return result.ToString();
		}

        public string AsString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(IPiece piece in GetPiecesWithNull())
            {
                if (piece is null)
                {
                    stringBuilder.Append(".");
                }
                else
                {
                    stringBuilder.Append(piece.PieceRepresentation());
                }
            }
            return stringBuilder.ToString().Replace(" ", "");
        }

		#endregion
	}
}
