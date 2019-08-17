using HnefataflAI.Commons.Positions;
using System.Collections.Generic;

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
        public static readonly int MAX_ROWS = 19;
        /// <summary>
        /// Maximum number or columns allowed for a board
        /// </summary>
        public static readonly int MAX_COLS = 19;
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
        public static readonly bool LOG_GAME = false;
        /// <summary>
        /// Boolean value to log the board or not
        /// </summary>
        public static readonly bool LOG_BOARD = false;
        /// <summary>
        /// Boolean value to log the moves or not
        /// </summary>
        public static readonly bool LOG_MOVES = false;
        /// <summary>
        /// Boolean value to log the moves' evaluation results or not
        /// </summary>
        public static readonly bool LOG_MOVES_EVAL = false;
        /// <summary>
        /// Boolean value to log the game with Cyningstan notation
        /// </summary>
        public static readonly bool LOG_CYNINGSTAN_STYLE = false;
        /// <summary>
        /// A default position that will never be in any board
        /// </summary>
        public static readonly Position DefaultPosition = new Position(MAX_ROWS + 1, (char)(FIRST_COLUMN + MAX_COLS + 1));
        public static readonly Dictionary<int, List<int>> POSITIONS_DICT = new Dictionary<int, List<int>>()
            {
                { 2, new List<int>() { 10, 5 } },
                { 3, new List<int>() { 14, 7, 11 } },
                { 4, new List<int>() { 8, 4, 2, 1 } }
            };
    }
}
