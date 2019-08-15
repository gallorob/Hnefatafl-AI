using HnefataflAI.Commons.Positions;

namespace HnefataflAI.Pieces.Impl
{
    /// <summary>
    /// The Elite Guard piece
    /// </summary>
    public class EliteGuard : Defender
    {
        /// <summary>
        /// Create a new Elite Guard in the given position
        /// </summary>
        /// <param name="position">The Elite Guard's position</param>
        public EliteGuard(Position position) : base(position)
        {
        }
        /// <summary>
        /// Get the representing string for the piece
        /// </summary>
        /// <returns>The representing string for the piece</returns>
        public override string PieceRepresentation()
        {
            return " G ";
        }
        /// <summary>
        /// Human readable object representation
        /// </summary>
        /// <returns>The Elite Guard's representation as string</returns>
        public override string ToString()
        {
            return System.String.Format("Elite Guard in {0}", this.Position.ToString());
        }
    }
}
