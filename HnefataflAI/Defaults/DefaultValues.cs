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
        public static readonly bool LOG_BOARD = true;
        /// <summary>
        /// Boolean value to log the moves or not
        /// </summary>
        public static readonly bool LOG_MOVES = true;
        /// <summary>
        /// Boolean value to log the moves' evaluation results or not
        /// </summary>
        public static readonly bool LOG_MOVES_EVAL = false;
        /// <summary>
        /// Get the default Hnefatafl table
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G  H  I  J  K
        /// 11       .  .  .  A  A  A  A  A  .  .  .
        /// 10       .  .  .  .  .  A  .  .  .  .  .
        /// 9        .  .  .  .  .  .  .  .  .  .  .
        /// 8        A  .  .  .  .  D  .  .  .  .  A
        /// 7        A  .  .  .  D  D  D  .  .  .  A
        /// 6        A  A  .  D  D  K  D  D  .  A  A
        /// 5        A  .  .  .  D  D  D  .  .  .  A
        /// 4        A  .  .  .  .  D  .  .  .  .  A
        /// 3        .  .  .  .  .  .  .  .  .  .  .
        /// 2        .  .  .  .  .  A  .  .  .  .  .
        /// 1        .  .  .  A  A  A  A  A  .  .  .
        /// </remarks>
        /// <returns>The default Hnefatafl table</returns>
        public static Board GetDefaultHnefataflTable()
        {
            int boardRows = 11;
            int boardCols = 11;

            Position kingPosition = new Position(6, 'f');

            int[] whiteRows = { 8, 7, 7, 7, 6, 6, 6, 6, 5, 5, 5, 4 };
            char[] whiteCols = { 'f', 'e', 'f', 'g', 'd', 'e', 'g', 'h', 'e', 'f', 'g', 'f' };

            int[] blackRows = { 11, 11, 11, 11, 11, 10, 8, 8, 7, 7, 6, 6, 6, 6, 5, 5, 4, 4, 2, 1, 1, 1, 1, 1 };
            char[] blackCols = { 'd', 'e', 'f', 'g', 'h', 'f', 'a', 'k', 'a', 'k', 'a', 'b', 'j', 'k', 'a', 'k', 'a', 'k', 'f', 'd', 'e', 'f', 'g', 'h' };

            return BuildBoard(boardRows, boardCols, whiteRows, whiteCols, blackRows, blackCols, kingPosition);
        }
        /// <summary>
        /// Get the default Brandubh table
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G
        /// 7        .  .  .  A  .  .  .
        /// 6        .  .  .  A  .  .  .
        /// 5        .  .  .  D  .  .  .
        /// 4        A  A  D  K  D  A  A
        /// 3        .  .  .  D  .  .  .
        /// 2        .  .  .  A  .  .  .
        /// 1        .  .  .  A  .  .  .
        /// </remarks>
        /// <returns>The default Brandubh table</returns>
        public static Board GetDefaultBrandubhTable()
        {
            int boardRows = 7;
            int boardCols = 7;

            Position kingPosition = new Position(4, 'd');

            int[] whiteRows = { 5, 4, 4, 3 };
            char[] whiteCols = { 'd', 'c', 'e', 'd' };

            int[] blackRows = { 7, 6, 4, 4, 4, 4, 2, 1 };
            char[] blackCols = { 'd', 'd', 'a', 'b', 'f', 'g', 'd', 'd' };

            return BuildBoard(boardRows, boardCols, whiteRows, whiteCols, blackRows, blackCols, kingPosition);
        }
        /// <summary>
        /// Build the board with specified parameters
        /// </summary>
        /// <param name="boardRows">Number of board's rows</param>
        /// <param name="boardCols">Number of board's columns</param>
        /// <param name="whiteRows">White's pieces rows</param>
        /// <param name="whiteCols">White's pieces columns</param>
        /// <param name="blackRows">Black's pieces rows</param>
        /// <param name="blackCols">Black's pieces columns</param>
        /// <param name="kingPosition">King's position</param>
        /// <returns>The populated board</returns>
        private static Board BuildBoard(int boardRows, int boardCols, int[] whiteRows, char[] whiteCols, int[] blackRows, char[] blackCols, Position kingPosition)
        {
            Board board = new Board(boardRows, boardCols);
            // add white pieces
            King king = new King(kingPosition);
            board.AddPiece(king, king.Position);
            for (int i = 0; i < whiteRows.Length; i++)
            {
                Defender defender = new Defender(new Position(whiteRows[i], whiteCols[i]));
                board.AddPiece(defender, defender.Position);
            }
            // add black pieces
            for (int i = 0; i < blackRows.Length; i++)
            {
                Attacker attacker = new Attacker(new Position(blackRows[i], blackCols[i]));
                board.AddPiece(attacker, attacker.Position);
            }
            return board;
        }
        /// <summary>
        /// Prepares a testing board for testing purposes
        /// </summary>
        /// <returns>A testing board for testing purposes</returns>
        public static Board GetTestingTable()
        {
            int boardRows = 5;
            int boardCols = 5;

            Position kingPosition = new Position(3, 'c');

            int[] whiteRows = { 2 };
            char[] whiteCols = { 'c' };

            int[] blackRows = { 1, 1 };
            char[] blackCols = { 'b', 'c' };

            return BuildBoard(boardRows, boardCols, whiteRows, whiteCols, blackRows, blackCols, kingPosition);
        }
    }
}
