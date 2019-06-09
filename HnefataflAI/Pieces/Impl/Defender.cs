using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;

namespace HnefataflAI.Pieces.Impl
{
    public class Defender : Pawn
    {
        public Defender(Position position)
        {
            this.Position = position;
            this.PieceColors = PieceColors.WHITE;
        }
        public override string ToString()
        {
            return System.String.Format("Defender ({0}) in {1}", this.PieceColors, this.Position.ToString());
        }
    }
}
