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
        public static readonly bool LOG_GAME = true;
        /// <summary>
        /// Boolean value to log the board or not
        /// </summary>
        public static readonly bool LOG_BOARD = true;
        /// <summary>
        /// Boolean value to log the moves or not
        /// </summary>
        public static readonly bool LOG_MOVES = true;
        /// <summary>
        /// Boolean value to log the moves' evaluation results or not
        /// </summary>
        public static readonly bool LOG_MOVES_EVAL = true;
        /// <summary>
        /// Boolean value to log the game with Cyningstan notation
        /// </summary>
        public static readonly bool LOG_CYNINGSTAN_STYLE = true;
        /// <summary>
        /// A default position that will never be in any board
        /// </summary>
        public static readonly Position DefaultPosition = new Position(MAX_ROWS + 1, (char)(FIRST_COLUMN + MAX_COLS + 1));
        /// <summary>
        /// A dictionary that, for the number of sides, has the required decimal representation of
        /// the piece's surrounings to determine if it can be captured
        /// 
        ///   0
        /// 3 P 1
        ///   2
        /// 
        /// 2:
        /// 1010 1000	168
        /// 1010 0010	162
        /// 0101 0100	84
        /// 0101 0001	81
        /// 3:   
        /// 1110 1000	232
        /// 1110 0100	228
        /// 1110 0010	226
        /// 1011 1000	184
        /// 1011 0010	178
        /// 1011 0001	177
        /// 1101 1000	216
        /// 1101 0100	212
        /// 1101 0001	209
        /// 0111 0100	116
        /// 0111 0010	114
        /// 0111 0001	113
        /// 4:   
        /// 1111 0001	241
        /// 1111 0010	242
        /// 1111 0100	244
        /// 1111 1000	248
        /// </summary>
        public static readonly Dictionary<int, List<int>> CAPTURES_DICT = new Dictionary<int, List<int>>()
            {
                { 2, new List<int>() { 168, 162, 84, 81 } },
                { 3, new List<int>() { 232, 228, 226, 184, 178, 177, 216, 212, 209, 116, 114, 113 } },
                { 4, new List<int>() { 241, 242, 244, 248 } }
            };
        /// <summary>
        /// A dictionary that, for the number of sides, has the required decimal representation of
        /// the piece's immediate surrounings and possible next move to determine if it is threatened
        /// 
        ///   0
        /// 3 P 1
        ///   2
        /// 
        /// 2:
        /// 1000 0010	130
        /// 0100 0001	65
        /// 0010 1000	40
        /// 0001 0100	20
        /// 3:   
        /// 1100 0010	194
        /// 1100 0001	193
        /// 0110 1000	104
        /// 0110 0001	97
        /// 0011 1000	56
        /// 0011 0100	52
        /// 4:   
        /// 1110 0001	225
        /// 1101 0010	210
        /// 1011 0100	180
        /// 0111 1000	120
        /// </summary>
        public static readonly Dictionary<int, List<int>> THREATS_DICT = new Dictionary<int, List<int>>()
            {
                { 2, new List<int>() { 130, 65, 40, 20 } },
                { 3, new List<int>() { 194, 193, 104, 97, 56, 52 } },
                { 4, new List<int>() { 225, 210, 120, 180 } }
            };
        /// <summary>
        /// A dictionary that, for the number of sides, has the required decimal representation of
        /// the piece's surroundings for at-infinity captures combinations
        /// 
        /// 7 0 1
        /// 6 P 2
        /// 5 4 3
        /// 
        /// 2:
        /// 10001000	136
        /// 10001001	137
        /// 11001000	200
        /// 10011000	152
        /// 10001100	140
        /// 10001101	141
        /// 11001100	204
        /// 10011001	153
        /// 11011000	216
        /// 00100010	34
        /// 01100010	98
        /// 00110010	50
        /// 00100011	38
        /// 00100110	35
        /// 01100110	102
        /// 00110110	54
        /// 01100011	99
        /// 00110011	51
        /// 
        /// </summary>
        public static readonly Dictionary<int, List<int>> UNSAFE_DICT = new Dictionary<int, List<int>>()
        {
            {2, new List<int>() { 34, 35, 38, 50, 51, 54, 98, 99, 102, 136, 137, 140, 141, 152, 153, 200, 204, 216 } }
        };
    }
}
