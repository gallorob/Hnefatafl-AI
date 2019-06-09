using HnefataflAI.Commons.Positions;
using System;

namespace HnefataflAI.Commons.Exceptions
{
    class InvalidMoveException : Exception
    {
        public InvalidMoveException(Move move, string cause)
            : base(String.Format("Invalid move: {0} - {1}", cause, move)) { }
        public InvalidMoveException(Position position, string cause)
            : base(String.Format("Invalid move: {0} - {1}", cause, position)) { }
    }
}
