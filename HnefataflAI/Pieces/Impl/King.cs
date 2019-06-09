using HnefataflAI.Commons.Positions;

namespace HnefataflAI.Pieces.Impl
{
    public class King : Defender
    {
        public King(Position position)
            : base(position)
        {
        }
        public override string ToString()
        {
            return System.String.Format("King ({0}) in {1}", this.PieceColors, this.Position.ToString());
        }
    }
}
