using HnefataflAI.Commons.Positions;

namespace HnefataflAI.Pieces.Impl
{
    /// <summary>
    /// The King piece
    /// </summary>
    public class King : Defender
    {
        /// <summary>
        /// Create a new King in the given position
        /// </summary>
        /// <param name="position">The King's position</param>
        public King(Position position)
            : base(position)
        {
        }/// <summary>
         /// Get the representing string for the piece
         /// </summary>
         /// <returns>The representing string for the piece</returns>
        public override string PieceRepresentation()
        {
            return " K ";
        }
        /// <summary>
        /// Human readable object representation
        /// </summary>
        /// <returns>The King's representation as string</returns>
        public override string ToString()
        {
            return System.String.Format("King in {0}", this.Position.ToString());
        }
    }
}
