using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;

namespace HnefataflAI.Pieces.Impl
{
    public abstract class Pawn : IPiece
    {
        public PieceColors PieceColors { get; internal set; }
        public Position Position { get; internal set; }
        public virtual void UpdatePosition(Position newPosition)
        {
            this.Position = newPosition;
        }
    }
}
