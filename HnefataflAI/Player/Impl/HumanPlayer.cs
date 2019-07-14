using HnefataflAI.Commons;
using HnefataflAI.Commons.Utils;
using System;

namespace HnefataflAI.Player.Impl
{
    /// <summary>
    /// The human player
    /// </summary>
    class HumanPlayer : IPlayer
    {
        /// <summary>
        /// The player's pieces' color
        /// </summary>
        public PieceColors PieceColors { get; private set; }
        /// <summary>
        /// Human player constructor
        /// </summary>
        /// <param name="pieceColors">The player's pieces' color</param>
        public HumanPlayer(PieceColors pieceColors)
        {
            this.PieceColors = pieceColors;
        }
        /// <summary>
        /// Get a player's move
        /// </summary>
        /// <returns>The player's move as an array of strings</returns>
        public string[] GetMove()
        {
            Console.Write(Messages.ENTER_MOVE);
            return MoveUtils.MoveFromInput(Console.ReadLine());
        }
    }
}
