using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Rules;
using HnefataflAI.Player;

namespace HnefataflAI.AI.Bots
{
    /// <summary>
    /// Interface for the ITaflBot
    /// </summary>
    public interface ITaflBot : IPlayer
    {
        /// <summary>
        /// The rule type for the bot
        /// </summary>
        IRule Rule { get; }
        /// <summary>
        /// Get a move as a user input
        /// </summary>
        /// <param name="board">The board</param>
        /// <param name="moves">The list of all possible moves</param>
        /// <returns>A move as a user input</returns>
        string[] GetMove(Board board);
    }
}
