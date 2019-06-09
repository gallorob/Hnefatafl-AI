using HnefataflAI.Pieces;
using HnefataflAI.Commons.Positions;

namespace HnefataflAI.Commons
{
    public class Move
    {
        public IPiece Piece { get; private set; }
        public Position From { get; private set; }
        public Position To { get; private set; }
        public Move(IPiece piece, Position from, Position to)
        {
            this.Piece = piece;
            this.From = from;
            this.To = to;
        }
        public Move(IPiece piece, Position to)
        {
            this.Piece = piece;
            this.From = piece.Position;
            this.To = to;
        }
        public override string ToString()
        {
            return System.String.Format("Move for {0} to {1}", Piece.ToString(), To.ToString());
        }
    }
}
