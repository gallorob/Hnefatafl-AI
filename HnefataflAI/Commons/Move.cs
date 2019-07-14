using HnefataflAI.Pieces;
using HnefataflAI.Commons.Positions;
using System.Collections.Generic;

namespace HnefataflAI.Commons
{
    public class Move
    {
        public IPiece Piece { get; private set; }
        public Position From { get; private set; }
        public Position To { get; private set; }
        public bool DoesNotCapture { get; set; }
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
        public string MoveRepresentation()
        {
            return System.String.Format("Move for {0} to {1}", Piece.ToString(), To.ToString());
        }
        public override string ToString()
        {
            return System.String.Format("{0}-{1}", From.ToString(), To.ToString());
        }
        public override bool Equals(object obj)
        {
            var otherMove = obj as Move;
            return obj == null ? false : this.From.Equals(otherMove.From) && this.To.Equals(otherMove.To);
        }
        public override int GetHashCode()
        {
            var hashCode = 932772886;
            hashCode = hashCode * -1521134295 + EqualityComparer<IPiece>.Default.GetHashCode(Piece);
            hashCode = hashCode * -1521134295 + EqualityComparer<Position>.Default.GetHashCode(From);
            hashCode = hashCode * -1521134295 + EqualityComparer<Position>.Default.GetHashCode(To);
            return hashCode;
        }
    }
}
