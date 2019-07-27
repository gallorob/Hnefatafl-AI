using HnefataflAI.Commons;
using HnefataflAI.Commons.DataTypes;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace HnefataflAI.Games.Boards
{
	public class Board
	{
		#region Properties
		private readonly Matrix<IPiece> board;
		public int TotalRows { get; private set; }
		public int TotalCols { get; private set; }
		#endregion
		#region Constructors
		public Board(int numRows, int numCols)
		{
			this.TotalRows = numRows;
			this.TotalCols = numCols;
			this.board = new Matrix<IPiece>(numRows, numCols);
		}
		public Board(Matrix<IPiece> newBoard)
		{
			this.board = newBoard;
			this.TotalRows = newBoard.RowsNumber();
			this.TotalCols = newBoard.ColumnsNumber();
		}
		#endregion
		#region Getters
		public Matrix<IPiece> GetCurrentBoard()
		{
			return this.board;
		}
		#endregion
		#region Piece handling
		public void AddPiece(IPiece piece)
		{
			this.Set(piece, piece.Position);
		}
		public void RemovePiece(IPiece piece)
		{
			IPiece currentPiece = this.At(piece.Position);

			if (currentPiece.Equals(piece))
			{
				this.Set(null, piece.Position);
			}
		}
		private void Set(IPiece piece, Position position)
		{
			int arrRow = BoardUtils.GetArrayRow(this.TotalRows, position.Row);
			int arrCol = BoardUtils.GetArrayCol(position.Col);

			this.board.Set(arrRow, arrCol, piece);
		}
		public IPiece At(Position position)
		{
			int arrRow = BoardUtils.GetArrayRow(this.TotalRows, position.Row);
			int arrCol = BoardUtils.GetArrayCol(position.Col);

			return this.board.At(arrRow, arrCol);
		}
		public List<IPiece> GetPieces()
		{
            return this.GetPiecesWithNull().Where(piece => piece != null).ToList();
		}
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
		public List<IPiece> GetPiecesByColor(PieceColors pieceColor)
		{
			return this.GetPieces().Where(piece => piece.PieceColors.Equals(pieceColor)).ToList();
		}
		#endregion
		#region Other
		public override string ToString()
		{
			string result = System.String.Format("\t{0}\n\r", BoardUtils.GetBoardColumnsChars(this.TotalCols));
			for (int i = 0; i < this.TotalRows; i++)
			{
				result += this.TotalRows - i + "\t";
				for (int j = 0; j < this.TotalCols; j++)
				{
					IPiece piece = this.board.At(i, j);
					result += piece == null ? " . " : piece.PieceRepresentation();
				}
				result += "\n\r";
			}
			return result;
		}
		#endregion
	}
}
