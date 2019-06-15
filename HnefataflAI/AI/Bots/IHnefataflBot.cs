using HnefataflAI.Commons;
using HnefataflAI.Games;
using HnefataflAI.Player;
using System.Collections.Generic;

namespace HnefataflAI.AI.Bots
{
    /// <summary>
    /// Interface for the HnefataflBot
    /// </summary>
    public interface IHnefataflBot : IPlayer
    {
        /// <summary>
        /// Get a move as a user input
        /// </summary>
        /// <param name="board">The board</param>
        /// <param name="moves">The list of all possible moves</param>
        /// <returns>A move as a user input</returns>
        string[] GetMove(Board board, List<Move> moves);
    }
}
