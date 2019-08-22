using HnefataflAI.Commons.Positions;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Engine;
using HnefataflAI.Games.Engine.Impl;
using HnefataflAI.Games.Rules.Impl;
using HnefataflAI.Pieces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaflWPF.Utils;

namespace TaflWPF.ViewModel
{
	public class BoardViewModel : BaseViewModel
	{
		public ObservableCollection<PieceItemViewModel> Pieces { get; set; }
		public int RowCount { get; set; }
		public int ColumnCount { get; set; }
        public List<char> Columns { get; set; }
        public List<int> Rows { get; set; }
        public bool ShowCorners { get; set; }
        public List<Position> Corners { get; set; }
        public List<Position> Thrones { get; set; }
        public List<Position> AttackerBaseCamps { get; set; }
        public List<Position> DefenderBaseCamps { get; set; }
        internal Board Board { get; set; }
        internal IRuleEngine RuleEngine { get; set; }
		public BoardViewModel(Board board)
		{
            this.Pieces = new ObservableCollection<PieceItemViewModel>();
            this.RowCount = board.TotalRows;
			this.ColumnCount = board.TotalCols;
            this.Columns = GridUtils.GetLetters(this.ColumnCount);
            this.Rows = GridUtils.GetNumbers(this.RowCount);

            this.Corners = board.CornerTiles;
            this.Thrones = board.ThroneTiles;
            this.AttackerBaseCamps = board.AttackerBaseCamps;
            this.DefenderBaseCamps = board.DefenderBaseCamps;

            this.Board = board;
            this.RuleEngine = new RuleEngineImpl(new HnefataflRule());
            AddPieceVMs();
        }
        public void AddPieceVMs()
        {
            this.Pieces.Clear();
            var pieces = this.Board.GetPiecesWithNull();
            for (int i = 0; i < pieces.Count; i++)
            {
                IPiece piece = pieces[i];
                PieceItemViewModel pieceVM = new PieceItemViewModel(pieces[i], i)
                {
                    Position = piece != null ? piece.Position : GridUtils.GetPositionFromIndex(i, ColumnCount),
                    IsThreatened = piece != null ? piece.IsThreatened : false
                };
				pieceVM.PositionType = Board.CornerTiles.Contains(pieceVM.Position) ? PositionType.CORNER : Board.ThroneTiles.Contains(pieceVM.Position) ? PositionType.THRONE : PositionType.DEFAULT;
				this.Pieces.Add(pieceVM);
            }
        }
        public void RefreshBoard()
        {
            ObservableCollection<PieceItemViewModel> newPieces = new ObservableCollection<PieceItemViewModel>();
            var pieces = this.Board.GetPiecesWithNull();
            for (int i = 0; i < pieces.Count; i++)
            {
                IPiece piece = pieces[i];
                PieceItemViewModel newPieceVM = new PieceItemViewModel(pieces[i], i)
                {
                    Position = piece != null ? piece.Position : GridUtils.GetPositionFromIndex(i, ColumnCount),
                    IsThreatened = piece != null ? piece.IsThreatened : false
                };
                newPieces.Add(newPieceVM);
            }
            Pieces.Clear();
            foreach (PieceItemViewModel pieceVM in newPieces)
            {
                Pieces.Add(pieceVM);
            }
        }
	}
}
