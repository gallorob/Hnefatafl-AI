using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;

namespace HnefataflAI.Pieces.Impl
{
    /// <summary>
    /// The Attacker piece
    /// </summary>
    public class Attacker : Pawn
    {
        /// <summary>
        /// Create a new Attacker in the given position
        /// </summary>
        /// <param name="position">The King's position</param>
        public Attacker(Position position)
        {
            this.Position = position;
            this.PieceColors = PieceColors.BLACK;
        }
        /// <summary>
        /// Get the representing string for the piece
        /// </summary>
        /// <returns>The representing string for the piece</returns>
        public override string PieceRepresentation()
        {
            return " A ";
        }
        /// <summary>
        /// Human readable object representation
        /// </summary>
        /// <returns>The King's representation as string</returns>
        public override string ToString()
        {
            return System.String.Format("Attacker in {0}", this.Position.ToString());
        }
    }
}
