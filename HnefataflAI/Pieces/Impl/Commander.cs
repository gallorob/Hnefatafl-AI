using HnefataflAI.Commons.Positions;

namespace HnefataflAI.Pieces.Impl
{
    /// <summary>
    /// The Commander piece
    /// </summary>
    public class Commander : Attacker
    {
        /// <summary>
        /// Create a new Commander in the given position
        /// </summary>
        /// <param name="position">The Commander's position</param>
        public Commander(Position position) : base(position)
        {
        }
        /// <summary>
        /// Get the representing string for the piece
        /// </summary>
        /// <returns>The representing string for the piece</returns>
        public override string PieceRepresentation()
        {
            return " C ";
        }
        /// <summary>
        /// Human readable object representation
        /// </summary>
        /// <returns>The King's representation as string</returns>
        public override string ToString()
        {
            return System.String.Format("Commander in {0}", this.Position.ToString());
        }
    }
}
