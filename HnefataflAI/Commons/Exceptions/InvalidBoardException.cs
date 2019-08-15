using System;

namespace HnefataflAI.Commons.Exceptions
{
    class InvalidBoardException : Exception
    {
        public InvalidBoardException(string cause)
            : base(String.Format("Invalid board: {0}", cause)) { }
    }
}
