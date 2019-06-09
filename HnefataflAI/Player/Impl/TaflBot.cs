﻿using HnefataflAI.Commons;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games;
using System;
using System.Collections.Generic;

namespace HnefataflAI.Player.Impl
{
    class TaflBot : IPlayer
    {
        public PieceColors PieceColors { get; private set; }

        public TaflBot(PieceColors pieceColors)
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
