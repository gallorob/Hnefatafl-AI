using HnefataflAI.Commons;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games;
using System;
using System.Collections.Generic;

namespace HnefataflAI.AI.Bots.Impl
{
    /// <summary>
    /// A Hnefatafl bot that plays random valid moves
    /// </summary>
    class TaflBotRandom : IHnefataflBot
    {
        /// <summary>
        /// The piece color
        /// </summary>
        public PieceColors PieceColors { get; private set; }
        /// <summary>
        /// Constructor for the TaflBotRandom
        /// </summary>
        /// <param name="pieceColors">The piece color</param>
        public TaflBotRandom(PieceColors pieceColors)
        {
            this.PieceColors = pieceColors;
        }
        /// <summary>
        /// Only for implementation
        /// </summary>
        /// <returns>Nothing; throws NotImplementedException</returns>
        public string[] GetMove()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Get a random move as a user input
        /// </summary>
        /// <param name="board">The board</param>
        /// <param name="moves">The list of all possible moves</param>
        /// <returns>A move as a user input</returns>
        public string[] GetMove(Board board, List<Move> moves)
        {
            Random rnd = new Random();
            int index = rnd.Next(moves.Count);
            return MoveUtils.MoveAsInput(moves[index]);
        }
    }
}
