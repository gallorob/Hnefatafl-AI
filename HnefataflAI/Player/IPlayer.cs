using HnefataflAI.Commons;
using System;
using System.Collections.Generic;

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
        String PlayerName { get; }
        List<String> AdditionalInfo { get; }
        /// <summary>
        /// Get a player's move
        /// </summary>
        /// <returns>The player's move as an array of strings</returns>
        string[] GetMove();
    }
}
