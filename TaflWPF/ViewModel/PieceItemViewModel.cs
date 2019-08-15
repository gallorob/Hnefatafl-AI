using HnefataflAI.Commons.Positions;
using HnefataflAI.Pieces;
using System.Windows;
using TaflWPF.Model.Piece;
using TaflWPF.Utils;

namespace TaflWPF.ViewModel
{
    /// <summary>
    /// View Model class for the game piece
    /// </summary>
    public class PieceItemViewModel : BaseViewModel
    {
        /// <summary>
        /// The actual piece
        /// </summary>
        private IPiece m_Piece;
        public IPiece Piece
        {
            get { return m_Piece; }
            set { m_Piece = value; NotifyPropertyChanged(nameof(Piece)); }
        }
		private bool m_Selected;
		public bool Selected
		{
			get { return m_Selected; }
			set { m_Selected = value; NotifyPropertyChanged(nameof(Selected)); }
		}
        private bool m_IsPossibleMove;
		public bool IsPossibleMove
		{
			get { return m_IsPossibleMove; }
			set { m_IsPossibleMove = value; NotifyPropertyChanged(nameof(IsPossibleMove)); }
		}
        private bool m_IsCaptured;
        public bool IsCaptured
        {
            get { return m_IsCaptured; }
            set { m_IsCaptured = value; NotifyPropertyChanged(nameof(IsCaptured)); }
        }
        private bool m_IsThreatened;
        public bool IsThreatened
        {
            get { return m_IsThreatened; }
            set { m_IsThreatened = value; NotifyPropertyChanged(nameof(IsThreatened)); }
        }
        private int m_Index;
        public int Index
        {
            get { return m_Index; }
            set { m_Index = value; NotifyPropertyChanged(nameof(Index)); }
        }
        private Position m_Position;
        public Position Position
        {
            get { return m_Position; }
            set { m_Position = value; NotifyPropertyChanged(nameof(Position)); }
        }
        /// <summary>
        /// The piece type
        /// </summary>
        public PieceType PieceType { get { return PieceUtils.GetPieceType(this.Piece); } }
        /// <summary>
        /// The piece informations
        /// </summary>
        public string PieceInfo { get { return this.Piece.ToString(); } }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="piece">The actual piece</param>
        public PieceItemViewModel(IPiece piece, int index)
        {
            this.Piece = piece;
            this.Index = index;
        }
    }
}
