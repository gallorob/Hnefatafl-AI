using HnefataflAI.Commons;

namespace HnefataflAI.Player
{
    /// <summary>
    /// The interface for a player type
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// The player's pieces' color
        /// </summary>
        PieceColors PieceColors { get; }
        /// <summary>
        /// Get a player's move
        /// </summary>
        /// <returns>The player's move as an array of strings</returns>
        string[] GetMove();
    }
}
