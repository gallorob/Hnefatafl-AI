using HnefataflAI.Commons;
using HnefataflAI.Pieces;
using System.Collections.Generic;

namespace HnefataflAI.Games.GameState
{
    /// <summary>
    /// The game status object
    /// </summary>
    public class GameStatus
    {
        /// <summary>
        /// Boolean representation of a gameover status
        /// </summary>
        public bool IsGameOver { get; set; }
        /// <summary>
        /// The endgame status
        /// </summary>
        public Status Status { get; set; }

        public string Reason { get; set; }

        /// <summary>
        /// The captured pieces in the game status
        /// </summary>
        public List<IPiece> CapturedPieces = new List<IPiece>();
        /// <summary>
        /// The next player's color
        /// </summary>
        public PieceColors NextPlayer;
        /// <summary>
        /// Constructor for the object
        /// </summary>
        /// <param name="isGameOver">Game over</param>
        public GameStatus(bool isGameOver)
        {
            this.IsGameOver = isGameOver;
        }
    }
}
