using HnefataflAI.Commons;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games;
using HnefataflAI.Player;
using System;
using System.Collections.Generic;

namespace HnefataflAI.AI
{
    class TaflBotRandom : IPlayer
    {
        public PieceColors PieceColors { get; private set; }

        public TaflBotRandom(PieceColors pieceColors)
        {
            this.PieceColors = pieceColors;
        }
        public string[] getMove()
        {
            throw new System.NotImplementedException();
        }
        public string[] getMove(Board board, List<Move> moves)
        {
            Random rnd = new Random();
            int index = rnd.Next(moves.Count);
            return MoveUtils.MoveAsInput(moves[index]);
        }
    }
}
