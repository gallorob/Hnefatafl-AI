using HnefataflAI.Commons;
using HnefataflAI.Commons.Utils;
using System;

namespace HnefataflAI.Player.Impl
{
    class HumanPlayer : IPlayer
    {
        public PieceColors PieceColors { get; private set; }

        public HumanPlayer(PieceColors pieceColors)
        {
            this.PieceColors = pieceColors;
        }
        public string[] getMove()
        {
            Console.Write(Messages.ENTER_MOVE);
            return MoveUtils.MoveFromInput(Console.ReadLine());
        }
    }
}
