using HnefataflAI.Commons.Positions;
using HnefataflAI.Pieces.Impl;
using System.Collections.Generic;

namespace HnefataflAI.Games.Boards
{
    public static class BoardBuilder
    {
        public static Board GetBoard(BoardTypes boardType)
        {
            switch (boardType)
            {
                case BoardTypes.COPENHAGEN_HNEFATAFL_11X11:
                    return GetCopenhagenHnefataflTable();
                case BoardTypes.HISTORICAL_HNEFATAFL_7X7:
                    return GetHistoricalHnefatafl7Table();
                case BoardTypes.HISTORICAL_HNEFATAFL_9X9:
                    return GetHistoricalHnefatafl9Table();
                case BoardTypes.HISTORICAL_HNEFATAFL_11X11:
                    return GetHistoricalHnefatafl11Table();
                case BoardTypes.HISTORICAL_HNEFATAFL_13X13:
                    return GetHistoricalHnefatafl13Table();
                case BoardTypes.SEABATTLE_9X9:
                    return GetHistoricalHnefatafl9Table();
                case BoardTypes.SEABATTLE_11X11:
                    return GetSeaBattleTable();
                case BoardTypes.SEABATTLE_13X13:
                    return GetHistoricalHnefatafl13Table();
                default:
                    return GetTestingTable();
            }
        }
        #region All possible boards
        /// <summary>
        /// Get the Copenhagen Hnefatafl table
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
        /// <returns>The Copenhagen Hnefatafl table</returns>
        public static Board GetCopenhagenHnefataflTable()
        {
            int boardRows = 11;
            int boardCols = 11;

            Position kingPosition = new Position(6, 'f');

            int[] whiteRows = { 8, 7, 7, 7, 6, 6, 6, 6, 5, 5, 5, 4 };
            char[] whiteCols = { 'f', 'e', 'f', 'g', 'd', 'e', 'g', 'h', 'e', 'f', 'g', 'f' };

            int[] blackRows = { 11, 11, 11, 11, 11, 10, 8, 8, 7, 7, 6, 6, 6, 6, 5, 5, 4, 4, 2, 1, 1, 1, 1, 1 };
            char[] blackCols = { 'd', 'e', 'f', 'g', 'h', 'f', 'a', 'k', 'a', 'k', 'a', 'b', 'j', 'k', 'a', 'k', 'a', 'k', 'f', 'd', 'e', 'f', 'g', 'h' };

            return TempBuildBoard(boardRows, boardCols, whiteRows, whiteCols, blackRows, blackCols, kingPosition);
        }
        /// <summary>
        /// Get the Historical Hnefatafl 7x7 table
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
        /// <returns>The Historical Hnefatafl 7x7 table</returns>
        public static Board GetHistoricalHnefatafl7Table()
        {
            int boardRows = 7;
            int boardCols = 7;

            Position kingPosition = new Position(4, 'd');

            int[] whiteRows = { 5, 4, 4, 3 };
            char[] whiteCols = { 'd', 'c', 'e', 'd' };

            int[] blackRows = { 7, 6, 4, 4, 4, 4, 2, 1 };
            char[] blackCols = { 'd', 'd', 'a', 'b', 'f', 'g', 'd', 'd' };

            return TempBuildBoard(boardRows, boardCols, whiteRows, whiteCols, blackRows, blackCols, kingPosition);
        }
        /// <summary>
        /// Get the Historical Hnefatafl 9x9 table
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G  H  I
        /// 9        .  .  .  A  A  A  .  .  .
        /// 8        .  .  .  .  A  .  .  .  .
        /// 7        .  .  .  .  D  .  .  .  .
        /// 6        A  .  .  .  D  .  .  .  A
        /// 5        A  A  D  D  K  D  D  A  A
        /// 4        A  .  .  .  D  .  .  .  A
        /// 3        .  .  .  .  D  .  .  .  .
        /// 2        .  .  .  .  A  .  .  .  .
        /// 1        .  .  .  A  A  A  .  .  .
        /// </remarks>
        /// <returns>The Historical Hnefatafl 9x9 table</returns>
        public static Board GetHistoricalHnefatafl9Table()
        {
            int boardRows = 9;
            int boardCols = 9;

            Position kingPosition = new Position(5, 'e');

            int[] whiteRows = { 7, 6, 5, 5, 5, 5, 4, 3 };
            char[] whiteCols = { 'e', 'e', 'c', 'd', 'f', 'g', 'e', 'e' };

            int[] blackRows = { 9, 9, 9, 8, 6, 6, 5, 5, 5, 5, 4, 4, 2, 1, 1, 1 };
            char[] blackCols = { 'd', 'e', 'f', 'e', 'a', 'i', 'a', 'b', 'h', 'i', 'a', 'i', 'e', 'd', 'e', 'f' };

            return TempBuildBoard(boardRows, boardCols, whiteRows, whiteCols, blackRows, blackCols, kingPosition);
        }
        /// <summary>
        /// Get the Historical Hnefatafl 11x11 table
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G  H  I  J  K
        /// 11       .  .  .  .  A  A  A  .  .  .  .
        /// 10       .  .  .  .  A  .  A  .  .  .  .
        /// 9        .  .  .  .  .  A  .  .  .  .  .
        /// 8        .  .  .  .  .  D  .  .  .  .  .
        /// 7        A  A  .  .  D  D  D  .  .  A  A
        /// 6        A  .  A  D  D  K  D  D  A  .  A
        /// 5        A  A  .  .  D  D  D  .  .  A  A
        /// 4        .  .  .  .  .  D  .  .  .  .  .
        /// 3        .  .  .  .  .  A  .  .  .  .  .
        /// 2        .  .  .  .  A  .  A  .  .  .  .
        /// 1        .  .  .  .  A  A  A  .  .  .  .
        /// </remarks>
        /// <returns>The Historical Hnefatafl 11x11 table</returns>
        public static Board GetHistoricalHnefatafl11Table()
        {
            int boardRows = 11;
            int boardCols = 11;

            Position kingPosition = new Position(6, 'f');

            int[] whiteRows = { 8, 7, 7, 7, 6, 6, 6, 6, 5, 5, 5, 4 };
            char[] whiteCols = { 'f', 'e', 'f', 'g', 'd', 'e', 'g', 'h', 'e', 'f', 'g', 'f' };

            int[] blackRows = { 11, 11, 11, 10, 10, 9, 7, 7, 7, 7, 6, 6, 6, 6, 5, 5, 5, 5, 3, 2, 2, 1, 1, 1 };
            char[] blackCols = { 'e', 'f', 'g', 'e', 'g', 'f', 'a', 'b', 'j', 'k', 'a', 'c', 'i', 'k', 'a', 'b', 'j', 'k', 'f', 'e', 'g', 'e', 'f', 'g' };

            return TempBuildBoard(boardRows, boardCols, whiteRows, whiteCols, blackRows, blackCols, kingPosition);
        }
        /// <summary>
        /// Get the Sea Battle 9x9 table
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G  H  I  J  K
        /// 11       .  .  .  A  A  A  A  A  .  .  .
        /// 10       .  .  .  .  .  A  .  .  .  .  .
        /// 9        .  .  .  .  .  D  .  .  .  .  .
        /// 8        A  .  .  .  .  D  .  .  .  .  A
        /// 7        A  .  .  .  .  D  .  .  .  .  A
        /// 6        A  A  D  D  D  K  D  D  D  A  A
        /// 5        A  .  .  .  .  D  .  .  .  .  A
        /// 4        A  .  .  .  .  D  .  .  .  .  A
        /// 3        .  .  .  .  .  D  .  .  .  .  .
        /// 2        .  .  .  .  .  A  .  .  .  .  .
        /// 1        .  .  .  A  A  A  A  A  .  .  .
        /// </remarks>
        /// <returns>The Sea Battle 9x9 table</returns>
        public static Board GetSeaBattleTable()
        {
            int boardRows = 11;
            int boardCols = 11;

            Position kingPosition = new Position(6, 'f');

            int[] whiteRows = { 9, 8, 7, 6, 6, 6, 6, 6, 6, 5, 4, 3 };
            char[] whiteCols = { 'f', 'f', 'f', 'c', 'd', 'e', 'g', 'h', 'i', 'f', 'f', 'f' };

            int[] blackRows = { 11, 11, 11, 11, 11, 10, 8, 8, 7, 7, 6, 6, 6, 6, 5, 5, 4, 4, 2, 1, 1, 1, 1, 1 };
            char[] blackCols = { 'd', 'e', 'f', 'g', 'h', 'f', 'a', 'k', 'a', 'k', 'a', 'b', 'j', 'k', 'a', 'k', 'a', 'k', 'f', 'd', 'e', 'f', 'g', 'h' };

            return TempBuildBoard(boardRows, boardCols, whiteRows, whiteCols, blackRows, blackCols, kingPosition);
        }
        /// <summary>
        /// Get the Historical Hnefatafl 13x13 table
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G  H  I  J  K  L  M
        /// 13       .  .  .  .  A  A  A  A  A  .  .  .  .
        /// 12       .  .  .  .  .  A  .  A  .  .  .  .  .
        /// 11       .  .  .  .  .  .  A  .  .  .  .  .  .
        /// 10       .  .  .  .  .  .  D  .  .  .  .  .  .
        /// 9        A  .  .  .  D  .  .  .  D  .  .  .  A
        /// 8        A  A  .  .  .  D  D  D  .  .  .  A  A
        /// 7        A  .  A  D  .  D  K  D  .  D  A  .  A
        /// 6        A  A  .  .  .  D  D  D  .  .  .  A  A
        /// 5        A  .  .  .  D  .  .  .  D  .  .  .  A
        /// 4        .  .  .  .  .  .  D  .  .  .  .  .  .
        /// 3        .  .  .  .  .  .  A  .  .  .  .  .  .
        /// 2        .  .  .  .  .  A  .  A  .  .  .  .  .
        /// 1        .  .  .  .  A  A  A  A  A  .  .  .  .
        /// </remarks>
        /// <returns>The Historical Hnefatafl 13x13 table</returns>
        public static Board GetHistoricalHnefatafl13Table()
        {
            int boardRows = 13;
            int boardCols = 13;

            Position kingPosition = new Position(7, 'g');
           
            int[] whiteRows = { 10, 9, 9, 8, 8, 8, 7, 7, 7, 7, 6, 6, 6, 5, 5, 4 };
            char[] whiteCols = { 'g', 'e', 'i', 'f', 'g', 'h', 'd', 'f', 'h', 'j', 'f', 'g', 'h', 'e', 'i', 'g' };

            int[] blackRows = { 13, 13, 13, 13, 13, 12, 12, 11, 9, 9, 8, 8, 8, 8, 7, 7, 7, 7, 6, 6, 6, 6, 5, 5, 3, 2, 2, 1, 1, 1, 1, 1 };
            char[] blackCols = { 'e', 'f', 'g', 'h', 'i', 'f', 'h', 'g', 'a', 'm', 'a', 'b', 'l', 'm', 'a', 'c', 'k', 'm', 'a', 'b', 'l', 'm', 'a', 'm', 'g', 'f', 'h', 'e', 'f', 'g', 'h', 'i' };

            return TempBuildBoard(boardRows, boardCols, whiteRows, whiteCols, blackRows, blackCols, kingPosition);
        }
        #endregion
        #region Builder
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
        private static Board TempBuildBoard(int boardRows, int boardCols, int[] whiteRows, char[] whiteCols, int[] blackRows, char[] blackCols, Position kingPosition)
        {
            List<Position> defenders = new List<Position>();
            List<Position> attackers = new List<Position>();
            for (int i = 0; i < whiteRows.Length; i++)
            {
                defenders.Add(new Position(whiteRows[i], whiteCols[i]));
            }
            // add black pieces
            for (int i = 0; i < blackRows.Length; i++)
            {
                attackers.Add(new Position(blackRows[i], blackCols[i]));
            }
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }
        /// <summary>
        /// Build the board with specified parameters
        /// </summary>
        /// <param name="boardRows">Number of board's rows</param>
        /// <param name="boardCols">Number of board's columns</param>
        /// <param name="kingPosition">King's position</param>
        /// <param name="defenders">Positions for the defenders</param>
        /// <param name="attackers">Positions for the attackers</param>
        /// <returns>The populated board</returns>
        public static Board BuildBoard(int boardRows, int boardCols, Position kingPosition, List<Position> defenders, List<Position> attackers)
        {
            Board board = new Board(boardRows, boardCols);
            // add defenders pieces
            King king = new King(kingPosition);
            board.AddPiece(king);
            foreach (Position defender in defenders)
            {
                board.AddPiece(new Defender(defender));
            }
            // add attackers pieces
            foreach (Position attacker in attackers)
            {
                board.AddPiece(new Attacker(attacker));
            }
            return board;
        }
        #endregion
        #region Other
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

            return TempBuildBoard(boardRows, boardCols, whiteRows, whiteCols, blackRows, blackCols, kingPosition);
        }
        #endregion
    }
}
