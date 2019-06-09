using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;

namespace HnefataflAI.Pieces.Impl
{
    public class Attacker : Pawn
    {
        public Attacker(Position position)
        {
            this.Position = position;
            this.PieceColors = PieceColors.BLACK;
        }
        public override string ToString()
        {
            return System.String.Format("Attacker ({0}) in {1}", this.PieceColors, this.Position.ToString());
        }
    }
}
