using HnefataflAI.Games.Boards;
using System.Collections.ObjectModel;
using System.Linq;

namespace TaflWPF.ViewModel
{
	public class BoardViewModel : BaseViewModel
	{
		public ObservableCollection<PieceItemViewModel> Pieces { get; set; }
		public int RowCount { get; set; }
		public int ColumnCount { get; set; }
		public BoardViewModel(Board board)
		{
			this.Pieces = new ObservableCollection<PieceItemViewModel>(board.GetPiecesWithNull().Select(piece => new PieceItemViewModel(piece)));
			this.RowCount = board.TotalRows;
			this.ColumnCount = board.TotalCols;
		}

		public void CalculatePossibleMoves(PieceItemViewModel item)
		{
			foreach (var piece in Pieces)
			{
				piece.IsPossibleMove = false;
			}

			// var moveslikejagger = board.getpossiblemoves(item);
			// foreach(var move in moveslikejagger)
			// look through the pieceitemviewmodels and mark them

			// TODO: old, do not keep me
			if (Pieces.FirstOrDefault(c => c?.Piece?.Position.Col == item.Piece.Position.Col - 1 && c?.Piece?.Position.Row == item.Piece.Position.Row) is PieceItemViewModel movablePiece)
				movablePiece.IsPossibleMove = true;
		}
	}
}
