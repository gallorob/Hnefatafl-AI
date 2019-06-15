using HnefataflAI.Commons;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games;
using System;
using System.Collections.Generic;

namespace HnefataflAI.AI.Bots.Impl
{
    class TaflBotRandom : IHnefataflBot
    {
        public PieceColors PieceColors { get; private set; }

        public TaflBotRandom(PieceColors pieceColors)
        {
            this.PieceColors = pieceColors;
        }
        public string[] GetMove()
        {
            throw new System.NotImplementedException();
        }
        public string[] GetMove(Board board, List<Move> moves)
        {
            Random rnd = new Random();
            int index = rnd.Next(moves.Count);
            return MoveUtils.MoveAsInput(moves[index]);
        }
    }
}
