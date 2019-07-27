using HnefataflAI.Pieces;
using System.ComponentModel;
using TaflWPF.Model.Piece;

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
        public PieceItemViewModel(IPiece piece)
        {
            this.Piece = piece;
        }
    }
}
