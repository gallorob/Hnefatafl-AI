using HnefataflAI.Commons.Positions;
using HnefataflAI.Games;
using HnefataflAI.Pieces.Impl;

namespace HnefataflAI.Defaults
{
    /// <summary>
    /// Class containing the program's default values
    /// </summary>
    public class DefaultValues
    {
        /// <summary>
        /// Maximum number or rows allowed for a board
        /// </summary>
        public static readonly int MAX_ROWS = 13;
        /// <summary>
        /// Maximum number or columns allowed for a board
        /// </summary>
        public static readonly int MAX_COLS = 13;
        /// <summary>
        /// First column as char
        /// </summary>
        public static readonly char FIRST_COLUMN = 'a';
        /// <summary>
        /// First column as char (uppercase)
        /// </summary>
        public static readonly char UPPER_FIRST_COLUMN = 'A';
        /// <summary>
        /// Depth the minimax algorithm can reach before stopping
        /// </summary>
        public static readonly int MINIMAX_DEPTH = 4;
        /// <summary>
        /// Boolean value to log the game or not
        /// </summary>
        public static readonly bool LOG_GAME = true;
        /// <summary>
        /// Boolean value to log the board or not
        /// </summary>
        public static readonly bool LOG_BOARD = false;
        /// <summary>
        /// Boolean value to log the moves or not
        /// </summary>
        public static readonly bool LOG_MOVES = true;
        /// <summary>
        /// Boolean value to log the moves' evaluation results or not
        /// </summary>
        public static readonly bool LOG_MOVES_EVAL = false;
        /// <summary>
        /// Boolean value to log the game with Cyningstan notation
        /// </summary>
        public static readonly bool LOG_CYNINGSTAN_STYLE = true;
    }
}
