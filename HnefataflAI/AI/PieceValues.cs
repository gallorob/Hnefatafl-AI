using HnefataflAI.Pieces.Impl;
using System.Collections.Generic;

namespace HnefataflAI.AI
{
    public class PieceValues
    {
        public readonly Dictionary<string, int> pieceValues = new Dictionary<string, int>();
        public PieceValues()
        {
            this.pieceValues.Add("King", 10);
            this.pieceValues.Add("Pawn", 1);
        }
    }
}
