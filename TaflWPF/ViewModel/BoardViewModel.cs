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
    }
}
