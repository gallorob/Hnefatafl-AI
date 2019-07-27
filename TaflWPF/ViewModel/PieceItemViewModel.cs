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
