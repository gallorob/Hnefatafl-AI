using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;

namespace HnefataflAI.Pieces.Impl
{
    /// <summary>
    /// The Defender piece
    /// </summary>
    public class Defender : Pawn
    {
        /// <summary>
        /// Create a new Defender in the given position
        /// </summary>
        /// <param name="position">The King's position</param>
        public Defender(Position position)
        {
            this.Position = position;
            this.PieceColors = PieceColors.WHITE;
        }/// <summary>
         /// Get the representing string for the piece
         /// </summary>
         /// <returns>The representing string for the piece</returns>
        public override string PieceRepresentation()
        {
            return " D ";
        }
        /// <summary>
        /// Human readable object representation
        /// </summary>
        /// <returns>The King's representation as string</returns>
        public override string ToString()
        {
            return System.String.Format("Defender in {0}", this.Position.ToString());
        }
    }
}
