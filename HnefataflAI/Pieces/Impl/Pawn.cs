using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;

namespace HnefataflAI.Pieces.Impl
{
    /// <summary>
    /// The pawn
    /// </summary>
    public abstract class Pawn : IPiece
    {
        /// <summary>
        /// The Piece's color
        /// </summary>
        public PieceColors PieceColors { get; internal set; }
        /// <summary>
        /// The piece's position
        /// </summary>
        public Position Position { get; internal set; }
        public bool IsThreatened { get; set; }
        /// <summary>
        /// Update the piece's position
        /// </summary>
        /// <param name="newPosition">The piece's new position</param>
        public virtual void UpdatePosition(Position newPosition)
        {
            this.Position = newPosition;
        }
        /// <summary>
        /// Get the representing string for the piece
        /// </summary>
        /// <returns>The representing string for the piece</returns>
        public virtual string PieceRepresentation()
        {
            return " P ";
        }
        public override bool Equals(object obj)
        {
            if (obj is Pawn other)
            {
                return this.PieceColors.Equals(other.PieceColors)
                    &&
                    this.Position.Equals(other.Position);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
