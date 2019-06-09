using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;

namespace HnefataflAI.Pieces
{
    public interface IPiece
    {
        PieceColors PieceColors { get; }
        Position Position { get; }

        void updatePosition(Position newPosition);
    }
}
