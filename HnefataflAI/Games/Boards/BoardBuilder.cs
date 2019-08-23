using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Defaults;
using HnefataflAI.Pieces.Impl;
using System.Collections.Generic;

namespace HnefataflAI.Games.Boards
{
    public static class BoardBuilder
    {
        /// <summary>
        /// Get a predefined board
        /// </summary>
        /// <param name="boardType">The board type</param>
        /// <returns>The board</returns>
        public static Board GetBoard(BoardTypes boardType)
        {
            switch (boardType)
            {
                // 7 x 7
                case BoardTypes.ARDRDI_7X7:
                    return GetArdRi7x7Board();
                case BoardTypes.BRANDUBH_7X7:
                    return GetBrandubh7x7Board();
                case BoardTypes.CELTICROYALCHESS_7X7:
                    return GetCelticRoyalChess7x7Board();
                case BoardTypes.FITCHNEAL_7X7:
                    return GetFitchneal7x7Board();
                case BoardTypes.GWYDDBWYLL_7X7:
                    return GetGwyddbwyll7x7Board();
                case BoardTypes.MAGPIE_7X7:
                    return GetMagpie7x7Board();
                case BoardTypes.NINETEENPIECE_7X7:
                    return GetNineteenPiece7x7Board();
                case BoardTypes.SMALLVIKINGSIEGE_7X7:
                    return GetSmallVikingSiege7x7Board();
                // 9 x 9
                case BoardTypes.LINNE_9X9:
                    return GetLinne9x9Board();
                // 11 x 11
                case BoardTypes.BELL_11X11:
                    return GetBell11x11Board();
                case BoardTypes.DARDELL_11X11:
                    return GetDardell11x11Board();
                case BoardTypes.TAFL_11x11:
                    return GetTafl11x11Board();
                case BoardTypes.BERSERK_11X11:
                    return GetBerserkTafl11x11Board();
                case BoardTypes.SIMPLETAFL_11X11:
                    return GetSimpleTafl11x11Board();
                // 13 x 13
                case BoardTypes.PARLETT1_13X13:
                    return GetParlett1_13x13Board();
                case BoardTypes.PARLETT2_13X13:
                    return GetParlett2_13x13Board();
                case BoardTypes.SERIFFCROSSNOGAPS_13X13:
                    return GetSeriffCrossNoGaps_13x13Board();
                case BoardTypes.TAFL_13X13:
                    return GetTafl_13x13Board();
                case BoardTypes.TAFL2_13X13:
                    return GetTafl2_13x13Board();
                // 15 x 15
                case BoardTypes.WALKER_15X15:
                    return GetWalker_15x15Board();
                // 19 x 19
                case BoardTypes.ALEAEVANGELII_19X19:
                    return GetAleaEvangelii_19x19Board();
                // Other
                default:
                    return null;
            }
        }

        #region All possible boards
        /// <summary>
        /// Get the 7 x 7 Ard Ri board
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G
        /// 7        .  .  A  A  A  .  .
        /// 6        .  .  .  A  .  .  .
        /// 5        A  .  D  D  D  .  A
        /// 4        A  A  D  K  D  A  A
        /// 3        A  .  D  D  D  .  A
        /// 2        .  .  .  A  .  .  .
        /// 1        .  .  A  A  A  .  .
        /// </remarks>
        /// <returns>The 7 x 7 Ard Ri board</returns>
        private static Board GetArdRi7x7Board()
        {
            int boardRows = 7;
            int boardCols = 7;
            Position kingPosition = new Position(4, 'd');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(5, 'c'),
                new Position(5, 'd'),
                new Position(5, 'e'),
                new Position(4, 'c'),
                new Position(4, 'e'),
                new Position(3, 'c'),
                new Position(3, 'd'),
                new Position(3, 'e')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(7, 'c'),
                new Position(7, 'd'),
                new Position(7, 'e'),
                new Position(6, 'd'),
                new Position(5, 'a'),
                new Position(5, 'g'),
                new Position(4, 'a'),
                new Position(4, 'b'),
                new Position(4, 'f'),
                new Position(4, 'g'),
                new Position(3, 'a'),
                new Position(3, 'g'),
                new Position(2, 'd'),
                new Position(1, 'c'),
                new Position(1, 'd'),
                new Position(1, 'e')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 7 x 7 Brandubh board
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
        /// <returns>The 7 x 7 Brandubh board</returns>
        private static Board GetBrandubh7x7Board()
        {
            int boardRows = 7;
            int boardCols = 7;
            Position kingPosition = new Position(4, 'd');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(5, 'd'),
                new Position(4, 'c'),
                new Position(4, 'e'),
                new Position(3, 'd')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(7, 'd'),
                new Position(6, 'd'),
                new Position(4, 'a'),
                new Position(4, 'b'),
                new Position(4, 'f'),
                new Position(4, 'g'),
                new Position(2, 'd'),
                new Position(1, 'd')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 7 x 7 Brandubh board
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G
        /// 7        .  A  .  .  .  A  .
        /// 6        A  .  .  .  .  .  A
        /// 5        .  .  D  .  D  .  .
        /// 4        .  .  .  K  .  .  .
        /// 3        .  .  D  .  D  .  .
        /// 2        A  .  .  .  .  .  A
        /// 1        .  A  .  .  .  A  .
        /// </remarks>
        /// <returns>The 7 x 7 Brandubh board</returns>
        private static Board GetCelticRoyalChess7x7Board()
        {
            int boardRows = 7;
            int boardCols = 7;
            Position kingPosition = new Position(4, 'd');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(5, 'c'),
                new Position(5, 'e'),
                new Position(3, 'c'),
                new Position(3, 'e')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(7, 'b'),
                new Position(7, 'f'),
                new Position(6, 'a'),
                new Position(6, 'g'),
                new Position(2, 'a'),
                new Position(2, 'g'),
                new Position(1, 'b'),
                new Position(1, 'f'),
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 7 x 7 Fitchneal board
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G
        /// 7        A  .  A  A  A  .  A
        /// 6        .  .  .  D  .  .  .
        /// 5        A  .  .  D  .  .  A
        /// 4        A  D  D  K  D  D  A
        /// 3        A  .  .  D  .  .  A
        /// 2        .  .  .  D  .  .  .
        /// 1        A  .  A  A  A  .  A
        /// </remarks>
        /// <returns>The 7 x 7 Fitchneal board</returns>
        private static Board GetFitchneal7x7Board()
        {
            int boardRows = 7;
            int boardCols = 7;
            Position kingPosition = new Position(4, 'd');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(6, 'd'),
                new Position(5, 'd'),
                new Position(4, 'b'),
                new Position(4, 'c'),
                new Position(4, 'e'),
                new Position(4, 'f'),
                new Position(3, 'd'),
                new Position(2, 'd')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(7, 'a'),
                new Position(7, 'c'),
                new Position(7, 'd'),
                new Position(7, 'e'),
                new Position(7, 'g'),
                new Position(5, 'a'),
                new Position(5, 'g'),
                new Position(4, 'a'),
                new Position(4, 'g'),
                new Position(3, 'a'),
                new Position(3, 'g'),
                new Position(1, 'a'),
                new Position(1, 'c'),
                new Position(1, 'd'),
                new Position(1, 'e'),
                new Position(1, 'g'),
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 7 x 7 Gwyddbwyll board
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G
        /// 7        .  .  A  .  A  .  .
        /// 6        .  .  .  .  .  .  .
        /// 5        A  .  .  D  .  .  A
        /// 4        .  .  D  K  D  .  .
        /// 3        A  .  .  D  .  .  A
        /// 2        .  .  .  .  .  .  .
        /// 1        .  .  A  .  A  .  .
        /// </remarks>
        /// <returns>The 7 x 7 Gwyddbwyll board</returns>
        private static Board GetGwyddbwyll7x7Board()
        {
            int boardRows = 7;
            int boardCols = 7;
            Position kingPosition = new Position(4, 'd');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(5, 'd'),
                new Position(4, 'c'),
                new Position(4, 'e'),
                new Position(3, 'd')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(7, 'c'),
                new Position(7, 'e'),
                new Position(5, 'a'),
                new Position(5, 'g'),
                new Position(3, 'a'),
                new Position(3, 'g'),
                new Position(1, 'c'),
                new Position(1, 'e')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 7 x 7 Magpie board
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G
        /// 7        .  .  .  A  .  .  .
        /// 6        .  A  .  .  .  A  .
        /// 5        .  .  .  D  .  .  .
        /// 4        A  .  D  K  D  .  A
        /// 3        .  .  .  D  .  .  .
        /// 2        .  A  .  .  .  A  .
        /// 1        .  .  .  A  .  .  .
        /// </remarks>
        /// <returns>The 7 x 7 Magpie board</returns>
        private static Board GetMagpie7x7Board()
        {
            int boardRows = 7;
            int boardCols = 7;
            Position kingPosition = new Position(4, 'd');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(5, 'd'),
                new Position(4, 'c'),
                new Position(4, 'e'),
                new Position(3, 'd')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(7, 'd'),
                new Position(6, 'b'),
                new Position(6, 'f'),
                new Position(4, 'a'),
                new Position(4, 'g'),
                new Position(2, 'b'),
                new Position(2, 'f'),
                new Position(1, 'd')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 7 x 7 Nineteen piece board
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G
        /// 7        .  .  A  A  .  .  .
        /// 6        .  .  A  D  .  .  .
        /// 5        .  .  .  D  .  A  A
        /// 4        A  .  D  K  D  .  A
        /// 3        A  A  .  D  .  .  .
        /// 2        .  .  .  D  A  .  .
        /// 1        .  .  .  A  A  .  .
        /// </remarks>
        /// <returns>The 7 x 7 Nineteen piece board</returns>
        private static Board GetNineteenPiece7x7Board()
        {
            int boardRows = 7;
            int boardCols = 7;
            Position kingPosition = new Position(4, 'd');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(6, 'd'),
                new Position(5, 'd'),
                new Position(4, 'c'),
                new Position(4, 'e'),
                new Position(3, 'd'),
                new Position(2, 'd')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(7, 'c'),
                new Position(7, 'd'),
                new Position(6, 'c'),
                new Position(5, 'f'),
                new Position(5, 'g'),
                new Position(4, 'a'),
                new Position(4, 'g'),
                new Position(3, 'a'),
                new Position(3, 'b'),
                new Position(2, 'e'),
                new Position(1, 'd'),
                new Position(1, 'e'),
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 7 x 7 small Viking Siege board
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G
        /// 7        .  A  A  .  A  A  .
        /// 6        A  .  .  D  .  .  A
        /// 5        A  .  .  D  .  .  A
        /// 4        .  D  D  K  D  D  .
        /// 3        A  .  .  D  .  .  A
        /// 2        A  .  .  D  .  .  A
        /// 1        .  A  A  .  A  A  .
        /// </remarks>
        /// <returns>The 7 x 7 small Viking Siege board</returns>
        private static Board GetSmallVikingSiege7x7Board()
        {
            int boardRows = 7;
            int boardCols = 7;
            Position kingPosition = new Position(4, 'd');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(6, 'd'),
                new Position(5, 'd'),
                new Position(4, 'b'),
                new Position(4, 'c'),
                new Position(4, 'e'),
                new Position(4, 'f'),
                new Position(3, 'd'),
                new Position(2, 'd')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(7, 'b'),
                new Position(7, 'c'),
                new Position(7, 'e'),
                new Position(7, 'f'),
                new Position(6, 'a'),
                new Position(6, 'g'),
                new Position(5, 'a'),
                new Position(5, 'g'),
                new Position(3, 'a'),
                new Position(3, 'g'),
                new Position(2, 'a'),
                new Position(2, 'g'),
                new Position(1, 'b'),
                new Position(1, 'c'),
                new Position(1, 'e'),
                new Position(1, 'f')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 9 x 9 Linné board
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
        /// <returns>The 9 x 9 Linné board</returns>
        private static Board GetLinne9x9Board()
        {
            int boardRows = 9;
            int boardCols = 9;
            Position kingPosition = new Position(5, 'e');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(7, 'e'),
                new Position(6, 'e'),
                new Position(5, 'c'),
                new Position(5, 'd'),
                new Position(5, 'f'),
                new Position(5, 'g'),
                new Position(4, 'e'),
                new Position(3, 'e')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(9, 'd'),
                new Position(9, 'e'),
                new Position(9, 'f'),
                new Position(8, 'e'),
                new Position(6, 'a'),
                new Position(6, 'i'),
                new Position(5, 'a'),
                new Position(5, 'b'),
                new Position(5, 'h'),
                new Position(5, 'i'),
                new Position(4, 'a'),
                new Position(4, 'i'),
                new Position(2, 'e'),
                new Position(1, 'd'),
                new Position(1, 'e'),
                new Position(1, 'f')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 11 x 11 Bell board
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
        /// <returns>The 11 x 11 Bell board</returns>
        private static Board GetBell11x11Board()
        {
            int boardRows = 11;
            int boardCols = 11;
            Position kingPosition = new Position(6, 'f');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(8, 'f'),
                new Position(7, 'e'),
                new Position(7, 'f'),
                new Position(7, 'g'),
                new Position(6, 'd'),
                new Position(6, 'e'),
                new Position(6, 'g'),
                new Position(6, 'h'),
                new Position(5, 'e'),
                new Position(5, 'f'),
                new Position(5, 'g'),
                new Position(4, 'f')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(11, 'e'),
                new Position(11, 'f'),
                new Position(11, 'g'),
                new Position(10, 'e'),
                new Position(10, 'g'),
                new Position(9, 'f'),
                new Position(7, 'a'),
                new Position(7, 'b'),
                new Position(7, 'j'),
                new Position(7, 'k'),
                new Position(6, 'a'),
                new Position(6, 'c'),
                new Position(6, 'i'),
                new Position(6, 'k'),
                new Position(5, 'a'),
                new Position(5, 'b'),
                new Position(5, 'j'),
                new Position(5, 'k'),
                new Position(3, 'f'),
                new Position(2, 'e'),
                new Position(2, 'g'),
                new Position(1, 'e'),
                new Position(1, 'f'),
                new Position(1, 'g')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 11 x 11 Dardell board
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
        /// <returns>The 11 x 11 Dardell board</returns>
        private static Board GetDardell11x11Board()
        {
            int boardRows = 11;
            int boardCols = 11;
            Position kingPosition = new Position(6, 'f');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(9, 'f'),
                new Position(8, 'f'),
                new Position(7, 'f'),
                new Position(6, 'c'),
                new Position(6, 'd'),
                new Position(6, 'e'),
                new Position(6, 'g'),
                new Position(6, 'h'),
                new Position(6, 'i'),
                new Position(5, 'f'),
                new Position(4, 'f'),
                new Position(3, 'f')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(11, 'd'),
                new Position(11, 'e'),
                new Position(11, 'f'),
                new Position(11, 'g'),
                new Position(11, 'h'),
                new Position(10, 'f'),
                new Position(8, 'a'),
                new Position(8, 'k'),
                new Position(7, 'a'),
                new Position(7, 'k'),
                new Position(6, 'a'),
                new Position(6, 'b'),
                new Position(6, 'j'),
                new Position(6, 'k'),
                new Position(5, 'a'),
                new Position(5, 'k'),
                new Position(4, 'a'),
                new Position(4, 'k'),
                new Position(2, 'f'),
                new Position(1, 'd'),
                new Position(1, 'e'),
                new Position(1, 'f'),
                new Position(1, 'g'),
                new Position(1, 'h')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 11 x 11 Hnefatafl board
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
        /// <returns>The 11 x 11 Hnefatafl board</returns>
        private static Board GetTafl11x11Board()
        {
            int boardRows = 11;
            int boardCols = 11;
            Position kingPosition = new Position(6, 'f');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(8, 'f'),
                new Position(7, 'e'),
                new Position(7, 'f'),
                new Position(7, 'g'),
                new Position(6, 'd'),
                new Position(6, 'e'),
                new Position(6, 'g'),
                new Position(6, 'h'),
                new Position(5, 'e'),
                new Position(5, 'f'),
                new Position(5, 'g'),
                new Position(4, 'f')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(11, 'd'),
                new Position(11, 'e'),
                new Position(11, 'f'),
                new Position(11, 'g'),
                new Position(11, 'h'),
                new Position(10, 'f'),
                new Position(8, 'a'),
                new Position(8, 'k'),
                new Position(7, 'a'),
                new Position(7, 'k'),
                new Position(6, 'a'),
                new Position(6, 'b'),
                new Position(6, 'j'),
                new Position(6, 'k'),
                new Position(5, 'a'),
                new Position(5, 'k'),
                new Position(4, 'a'),
                new Position(4, 'k'),
                new Position(2, 'f'),
                new Position(1, 'd'),
                new Position(1, 'e'),
                new Position(1, 'f'),
                new Position(1, 'g'),
                new Position(1, 'h')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 11 x 11 Berserk Hnefatafl board
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G  H  I  J  K
        /// 11       .  .  .  A  A  A  A  A  .  .  .
        /// 10       .  .  .  .  .  C  .  .  .  .  .
        /// 9        .  .  .  .  .  .  .  .  .  .  .
        /// 8        A  .  .  .  .  D  .  .  .  .  A
        /// 7        A  .  .  .  G  D  D  .  .  .  A
        /// 6        A  C  .  D  D  K  D  D  .  C  A
        /// 5        A  .  .  .  D  D  D  .  .  .  A
        /// 4        A  .  .  .  .  D  .  .  .  .  A
        /// 3        .  .  .  .  .  .  .  .  .  .  .
        /// 2        .  .  .  .  .  C  .  .  .  .  .
        /// 1        .  .  .  A  A  A  A  A  .  .  .
        /// </remarks>
        /// <returns>The 11 x 11 Berserk Hnefatafl board</returns>
        private static Board GetBerserkTafl11x11Board()
        {
            int boardRows = 11;
            int boardCols = 11;
            Position kingPosition = new Position(6, 'f');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(8, 'f'),
                new Position(7, 'f'),
                new Position(7, 'g'),
                new Position(6, 'd'),
                new Position(6, 'e'),
                new Position(6, 'g'),
                new Position(6, 'h'),
                new Position(5, 'e'),
                new Position(5, 'f'),
                new Position(5, 'g'),
                new Position(4, 'f')
            };
            HashSet<Position> eliteGuards = new HashSet<Position>
            {
                new Position(7, 'e')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(11, 'd'),
                new Position(11, 'e'),
                new Position(11, 'f'),
                new Position(11, 'g'),
                new Position(11, 'h'),
                new Position(8, 'a'),
                new Position(8, 'k'),
                new Position(7, 'a'),
                new Position(7, 'k'),
                new Position(6, 'a'),
                new Position(6, 'k'),
                new Position(5, 'a'),
                new Position(5, 'k'),
                new Position(4, 'a'),
                new Position(4, 'k'),
                new Position(1, 'd'),
                new Position(1, 'e'),
                new Position(1, 'f'),
                new Position(1, 'g'),
                new Position(1, 'h')
            };
            HashSet<Position> commanders = new HashSet<Position>
            {
                new Position(10, 'f'),
                new Position(6, 'b'),
                new Position(6, 'j'),
                new Position(2, 'f')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers, eliteGuards, commanders);
        }

        /// <summary>
        /// Get the 11 x 11 Simple Tafl board
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G  H  I  J  K
        /// 11       .  .  .  .  A  A  A  .  .  .  .
        /// 10       .  .  .  .  A  A  A  .  .  .  .
        /// 9        .  .  .  .  .  D  .  .  .  .  .
        /// 8        .  .  .  .  .  D  .  .  .  .  .
        /// 7        A  A  .  .  .  D  .  .  .  A  A
        /// 6        A  A  D  D  D  K  D  D  D  A  A
        /// 5        A  A  .  .  .  D  .  .  .  A  A
        /// 4        .  .  .  .  .  D  .  .  .  .  .
        /// 3        .  .  .  .  .  D  .  .  .  .  .
        /// 2        .  .  .  .  A  A  A  .  .  .  .
        /// 1        .  .  .  .  A  A  A  .  .  .  .
        /// </remarks>
        /// <returns>The 11 x 11 Simple Tafl board</returns>
        private static Board GetSimpleTafl11x11Board()
        {
            int boardRows = 11;
            int boardCols = 11;
            Position kingPosition = new Position(6, 'f');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(9, 'f'),
                new Position(8, 'f'),
                new Position(7, 'f'),
                new Position(6, 'c'),
                new Position(6, 'd'),
                new Position(6, 'e'),
                new Position(6, 'g'),
                new Position(6, 'h'),
                new Position(6, 'i'),
                new Position(5, 'f'),
                new Position(4, 'f'),
                new Position(3, 'f')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(11, 'e'),
                new Position(11, 'f'),
                new Position(11, 'g'),
                new Position(10, 'e'),
                new Position(10, 'f'),
                new Position(10, 'g'),
                new Position(7, 'a'),
                new Position(7, 'b'),
                new Position(7, 'j'),
                new Position(7, 'k'),
                new Position(6, 'a'),
                new Position(6, 'b'),
                new Position(6, 'j'),
                new Position(6, 'k'),
                new Position(5, 'a'),
                new Position(5, 'b'),
                new Position(5, 'j'),
                new Position(5, 'k'),
                new Position(2, 'e'),
                new Position(2, 'f'),
                new Position(2, 'g'),
                new Position(1, 'e'),
                new Position(1, 'f'),
                new Position(1, 'g')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 13 x 13 Parlett (1) board
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
        /// <returns>The 13 x 13 Parlett (1) board</returns>
        private static Board GetParlett1_13x13Board()
        {
            int boardRows = 13;
            int boardCols = 13;
            Position kingPosition = new Position(7, 'g');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(10, 'g'),
                new Position(9, 'e'),
                new Position(9, 'i'),
                new Position(8, 'f'),
                new Position(8, 'g'),
                new Position(8, 'h'),
                new Position(7, 'd'),
                new Position(7, 'f'),
                new Position(7, 'h'),
                new Position(7, 'j'),
                new Position(6, 'f'),
                new Position(6, 'g'),
                new Position(6, 'h'),
                new Position(5, 'e'),
                new Position(5, 'i'),
                new Position(4, 'g')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(13, 'e'),
                new Position(13, 'f'),
                new Position(13, 'g'),
                new Position(13, 'h'),
                new Position(13, 'i'),
                new Position(12, 'f'),
                new Position(12, 'h'),
                new Position(11, 'g'),
                new Position(9, 'a'),
                new Position(9, 'm'),
                new Position(8, 'a'),
                new Position(8, 'b'),
                new Position(8, 'l'),
                new Position(8, 'm'),
                new Position(7, 'a'),
                new Position(7, 'c'),
                new Position(7, 'k'),
                new Position(7, 'm'),
                new Position(6, 'a'),
                new Position(6, 'b'),
                new Position(6, 'l'),
                new Position(6, 'm'),
                new Position(5, 'a'),
                new Position(5, 'm'),
                new Position(3, 'g'),
                new Position(2, 'f'),
                new Position(2, 'h'),
                new Position(1, 'e'),
                new Position(1, 'f'),
                new Position(1, 'g'),
                new Position(1, 'h'),
                new Position(1, 'i')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 13 x 13 Parlett (2) board
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G  H  I  J  K  L  M
        /// 13       .  .  .  .  A  A  .  A  A  .  .  .  .
        /// 12       .  .  .  .  .  A  A  A  .  .  .  .  .
        /// 11       .  .  .  .  .  .  A  .  .  .  .  .  .
        /// 10       .  .  .  .  .  .  D  .  .  .  .  .  .
        /// 9        A  .  .  .  D  .  .  .  D  .  .  .  A
        /// 8        A  A  .  .  .  D  D  D  .  .  .  A  A
        /// 7        .  A  A  D  .  D  K  D  .  D  A  A  .
        /// 6        A  A  .  .  .  D  D  D  .  .  .  A  A
        /// 5        A  .  .  .  D  .  .  .  D  .  .  .  A
        /// 4        .  .  .  .  .  .  D  .  .  .  .  .  .
        /// 3        .  .  .  .  .  .  A  .  .  .  .  .  .
        /// 2        .  .  .  .  .  A  A  A  .  .  .  .  .
        /// 1        .  .  .  .  A  A  .  A  A  .  .  .  .
        /// </remarks>
        /// <returns>The 13 x 13 Parlett (2) board</returns>
        private static Board GetParlett2_13x13Board()
        {
            int boardRows = 13;
            int boardCols = 13;
            Position kingPosition = new Position(7, 'g');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(10, 'g'),
                new Position(9, 'e'),
                new Position(9, 'i'),
                new Position(8, 'f'),
                new Position(8, 'g'),
                new Position(8, 'h'),
                new Position(7, 'd'),
                new Position(7, 'f'),
                new Position(7, 'h'),
                new Position(7, 'j'),
                new Position(6, 'f'),
                new Position(6, 'g'),
                new Position(6, 'h'),
                new Position(5, 'e'),
                new Position(5, 'i'),
                new Position(4, 'g')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(13, 'e'),
                new Position(13, 'f'),
                new Position(13, 'h'),
                new Position(13, 'i'),
                new Position(12, 'f'),
                new Position(12, 'g'),
                new Position(12, 'h'),
                new Position(11, 'g'),
                new Position(9, 'a'),
                new Position(9, 'm'),
                new Position(8, 'a'),
                new Position(8, 'b'),
                new Position(8, 'l'),
                new Position(8, 'm'),
                new Position(7, 'b'),
                new Position(7, 'c'),
                new Position(7, 'k'),
                new Position(7, 'l'),
                new Position(6, 'a'),
                new Position(6, 'b'),
                new Position(6, 'l'),
                new Position(6, 'm'),
                new Position(5, 'a'),
                new Position(5, 'm'),
                new Position(3, 'g'),
                new Position(2, 'f'),
                new Position(2, 'g'),
                new Position(2, 'h'),
                new Position(1, 'e'),
                new Position(1, 'f'),
                new Position(1, 'h'),
                new Position(1, 'i')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 13 x 13 Seriff Cross with no gaps board
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G  H  I  J  K  L  M
        /// 13       .  .  .  A  A  A  A  A  A  A  .  .  .
        /// 12       .  .  .  .  .  .  A  .  .  .  .  .  .
        /// 11       .  .  .  .  .  .  D  .  .  .  .  .  .
        /// 10       A  .  .  .  .  .  D  .  .  .  .  .  A
        /// 9        A  .  .  .  .  .  D  .  .  .  .  .  A
        /// 8        A  .  .  .  .  .  D  .  .  .  .  .  A
        /// 7        A  A  D  D  D  D  K  D  D  D  D  A  A
        /// 6        A  .  .  .  .  .  D  .  .  .  .  .  A
        /// 5        A  .  .  .  .  .  D  .  .  .  .  .  A
        /// 4        A  .  .  .  .  .  D  .  .  .  .  .  A
        /// 3        .  .  .  .  .  .  D  .  .  .  .  .  .
        /// 2        .  .  .  .  .  .  A  .  .  .  .  .  .
        /// 1        .  .  .  A  A  A  A  A  A  A  .  .  .
        /// </remarks>
        /// <returns>The 13 x 13 Seriff Cross with no gaps board</returns>
        private static Board GetSeriffCrossNoGaps_13x13Board()
        {
            int boardRows = 13;
            int boardCols = 13;
            Position kingPosition = new Position(7, 'g');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(11, 'g'),
                new Position(10, 'g'),
                new Position(9, 'g'),
                new Position(8, 'g'),
                new Position(7, 'c'),
                new Position(7, 'd'),
                new Position(7, 'e'),
                new Position(7, 'f'),
                new Position(7, 'h'),
                new Position(7, 'i'),
                new Position(7, 'j'),
                new Position(7, 'k'),
                new Position(6, 'g'),
                new Position(5, 'g'),
                new Position(4, 'g'),
                new Position(3, 'g')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(13, 'd'),
                new Position(13, 'e'),
                new Position(13, 'f'),
                new Position(13, 'g'),
                new Position(13, 'h'),
                new Position(13, 'i'),
                new Position(13, 'j'),
                new Position(12, 'g'),
                new Position(10, 'a'),
                new Position(10, 'm'),
                new Position(9, 'a'),
                new Position(9, 'm'),
                new Position(8, 'a'),
                new Position(8, 'm'),
                new Position(7, 'a'),
                new Position(7, 'b'),
                new Position(7, 'l'),
                new Position(7, 'm'),
                new Position(6, 'a'),
                new Position(6, 'm'),
                new Position(5, 'a'),
                new Position(5, 'm'),
                new Position(4, 'a'),
                new Position(4, 'm'),
                new Position(2, 'g'),
                new Position(1, 'd'),
                new Position(1, 'e'),
                new Position(1, 'f'),
                new Position(1, 'g'),
                new Position(1, 'h'),
                new Position(1, 'i'),
                new Position(1, 'j')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 13 x 13 Hnefatafl board
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G  H  I  J  K  L  M
        /// 13       .  .  .  .  A  A  A  A  A  .  .  .  .
        /// 12       .  .  .  .  .  .  A  .  .  .  .  .  .
        /// 11       .  .  .  .  .  .  .  .  .  .  .  .  .
        /// 10       .  .  .  .  .  .  D  .  .  .  .  .  .
        /// 9        A  .  .  .  .  .  D  .  .  .  .  .  A
        /// 8        A  .  .  .  .  .  D  .  .  .  .  .  A
        /// 7        A  A  .  D  D  D  K  D  D  D  .  A  A
        /// 6        A  .  .  .  .  .  D  .  .  .  .  .  A
        /// 5        A  .  .  .  .  .  D  .  .  .  .  .  A
        /// 4        .  .  .  .  .  .  D  .  .  .  .  .  .
        /// 3        .  .  .  .  .  .  .  .  .  .  .  .  .
        /// 2        .  .  .  .  .  .  A  .  .  .  .  .  .
        /// 1        .  .  .  .  A  A  A  A  A  .  .  .  .
        /// </remarks>
        /// <returns>The 13 x 13 Hnefatafl board</returns>
        private static Board GetTafl_13x13Board()
        {
            int boardRows = 13;
            int boardCols = 13;
            Position kingPosition = new Position(7, 'g');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(10, 'g'),
                new Position(9, 'g'),
                new Position(8, 'g'),
                new Position(7, 'd'),
                new Position(7, 'e'),
                new Position(7, 'f'),
                new Position(7, 'h'),
                new Position(7, 'i'),
                new Position(7, 'j'),
                new Position(6, 'g'),
                new Position(5, 'g'),
                new Position(4, 'g')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(13, 'e'),
                new Position(13, 'f'),
                new Position(13, 'g'),
                new Position(13, 'h'),
                new Position(13, 'i'),
                new Position(12, 'g'),
                new Position(9, 'a'),
                new Position(9, 'm'),
                new Position(8, 'a'),
                new Position(8, 'm'),
                new Position(7, 'a'),
                new Position(7, 'b'),
                new Position(7, 'l'),
                new Position(7, 'm'),
                new Position(6, 'a'),
                new Position(6, 'm'),
                new Position(5, 'a'),
                new Position(5, 'm'),
                new Position(2, 'g'),
                new Position(1, 'e'),
                new Position(1, 'f'),
                new Position(1, 'g'),
                new Position(1, 'h'),
                new Position(1, 'i')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 13 x 13 Hnefatafl board
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G  H  I  J  K  L  M
        /// 13       .  .  .  .  A  A  A  A  A  .  .  .  .
        /// 12       .  .  .  .  .  A  A  A  .  .  .  .  .
        /// 11       .  .  .  .  .  .  .  .  .  .  .  .  .
        /// 10       .  .  .  .  .  .  D  .  .  .  .  .  .
        /// 9        A  .  .  .  .  .  D  .  .  .  .  .  A
        /// 8        A  A  .  .  .  D  D  D  .  .  .  A  A
        /// 7        A  A  .  D  D  D  K  D  D  D  .  A  A
        /// 6        A  A  .  .  .  D  D  D  .  .  .  A  A
        /// 5        A  .  .  .  .  .  D  .  .  .  .  .  A
        /// 4        .  .  .  .  .  .  D  .  .  .  .  .  .
        /// 3        .  .  .  .  .  .  .  .  .  .  .  .  .
        /// 2        .  .  .  .  .  A  A  A  .  .  .  .  .
        /// 1        .  .  .  .  A  A  A  A  A  .  .  .  .
        /// </remarks>
        /// <returns>The 13 x 13 Hnefatafl board</returns>
        private static Board GetTafl2_13x13Board()
        {
            int boardRows = 13;
            int boardCols = 13;
            Position kingPosition = new Position(7, 'g');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(10, 'g'),
                new Position(9, 'g'),
                new Position(8, 'f'),
                new Position(8, 'g'),
                new Position(8, 'h'),
                new Position(7, 'd'),
                new Position(7, 'e'),
                new Position(7, 'f'),
                new Position(7, 'h'),
                new Position(7, 'i'),
                new Position(7, 'j'),
                new Position(6, 'f'),
                new Position(6, 'g'),
                new Position(6, 'h'),
                new Position(5, 'g'),
                new Position(4, 'g')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(13, 'e'),
                new Position(13, 'f'),
                new Position(13, 'g'),
                new Position(13, 'h'),
                new Position(13, 'i'),
                new Position(12, 'f'),
                new Position(12, 'g'),
                new Position(12, 'h'),
                new Position(9, 'a'),
                new Position(9, 'm'),
                new Position(8, 'a'),
                new Position(8, 'b'),
                new Position(8, 'l'),
                new Position(8, 'm'),
                new Position(7, 'a'),
                new Position(7, 'b'),
                new Position(7, 'l'),
                new Position(7, 'm'),
                new Position(6, 'a'),
                new Position(6, 'b'),
                new Position(6, 'l'),
                new Position(6, 'm'),
                new Position(5, 'a'),
                new Position(5, 'm'),
                new Position(2, 'f'),
                new Position(2, 'g'),
                new Position(2, 'h'),
                new Position(1, 'e'),
                new Position(1, 'f'),
                new Position(1, 'g'),
                new Position(1, 'h'),
                new Position(1, 'i')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 15 x 15 Damian Walker board
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G  H  I  J  K  L  M  N  O
        /// 15       .  .  .  .  .  A  A  A  A  A  .  .  .  .  .
        /// 14       .  .  .  .  .  .  A  A  A  .  .  .  .  .  .
        /// 13       .  .  .  .  .  .  .  A  .  .  .  .  .  .  .
        /// 12       .  .  .  .  .  .  .  A  .  .  .  .  .  .  .
        /// 11       .  .  .  .  .  .  .  D  .  .  .  .  .  .  .
        /// 10       A  .  .  .  .  .  D  D  D  .  .  .  .  .  A
        /// 9        A  A  .  .  .  D  .  D  .  D  .  .  .  A  A
        /// 8        A  A  A  A  D  D  D  K  D  D  D  A  A  A  A
        /// 7        A  A  .  .  .  D  .  D  .  D  .  .  .  A  A
        /// 6        A  .  .  .  .  .  D  D  D  .  .  .  .  .  A
        /// 5        .  .  .  .  .  .  .  D  .  .  .  .  .  .  .
        /// 4        .  .  .  .  .  .  .  A  .  .  .  .  .  .  .
        /// 3        .  .  .  .  .  .  .  A  .  .  .  .  .  .  .
        /// 2        .  .  .  .  .  .  A  A  A  .  .  .  .  .  .
        /// 1        .  .  .  .  .  A  A  A  A  A  .  .  .  .  .
        /// </remarks>
        /// <returns>The 15 x 15 Damian Walker board</returns>
        private static Board GetWalker_15x15Board()
        {
            int boardRows = 15;
            int boardCols = 15;
            Position kingPosition = new Position(8, 'h');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(11, 'h'),
                new Position(10, 'g'),
                new Position(10, 'h'),
                new Position(10, 'i'),
                new Position(9, 'f'),
                new Position(9, 'h'),
                new Position(9, 'j'),
                new Position(8, 'e'),
                new Position(8, 'f'),
                new Position(8, 'g'),
                new Position(8, 'i'),
                new Position(8, 'j'),
                new Position(8, 'k'),
                new Position(7, 'f'),
                new Position(7, 'h'),
                new Position(7, 'j'),
                new Position(6, 'g'),
                new Position(6, 'h'),
                new Position(6, 'i'),
                new Position(5, 'h')
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(15, 'f'),
                new Position(15, 'g'),
                new Position(15, 'h'),
                new Position(15, 'i'),
                new Position(15, 'j'),
                new Position(14, 'g'),
                new Position(14, 'h'),
                new Position(14, 'i'),
                new Position(13, 'h'),
                new Position(12, 'h'),
                new Position(10, 'a'),
                new Position(10, 'o'),
                new Position(9, 'a'),
                new Position(9, 'b'),
                new Position(9, 'n'),
                new Position(9, 'o'),
                new Position(8, 'a'),
                new Position(8, 'b'),
                new Position(8, 'c'),
                new Position(8, 'd'),
                new Position(8, 'l'),
                new Position(8, 'm'),
                new Position(8, 'n'),
                new Position(8, 'o'),
                new Position(7, 'a'),
                new Position(7, 'b'),
                new Position(7, 'n'),
                new Position(7, 'o'),
                new Position(6, 'a'),
                new Position(6, 'o'),
                new Position(4, 'h'),
                new Position(3, 'h'),
                new Position(2, 'g'),
                new Position(2, 'h'),
                new Position(2, 'i'),
                new Position(1, 'f'),
                new Position(1, 'g'),
                new Position(1, 'h'),
                new Position(1, 'i'),
                new Position(1, 'j')
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers);
        }

        /// <summary>
        /// Get the 19 x 19 Alea Evangelii board
        /// </summary>
        /// <remarks>
        ///          A  B  C  D  E  F  G  H  I  J  K  L  M  N  O  P  Q  R  S
        /// 19       .  .  A  .  .  A  .  .  .  .  .  .  .  A  .  .  A  .  .
        /// 18       .  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .
        /// 17       A  .  .  .  .  A  .  .  .  .  .  .  .  A  .  .  .  .  A
        /// 16       .  .  .  .  .  .  .  A  .  A  .  A  .  .  .  .  .  .  .
        /// 15       .  .  .  .  .  .  A  .  D  .  D  .  A  .  .  .  .  .  .
        /// 14       A  .  A  .  .  A  .  .  .  .  .  .  .  A  .  .  A  .  A
        /// 13       .  .  .  .  A  .  .  .  .  D  .  .  .  .  A  .  .  .  .
        /// 12       .  .  .  A  .  .  .  .  D  .  D  .  .  .  .  A  .  .  .
        /// 11       .  .  .  .  D  .  .  D  .  G  .  D  .  .  D  .  .  .  .
        /// 10       .  .  .  A  .  .  D  .  G  K  G  .  D  .  .  A  .  .  .
        /// 9        .  .  .  .  D  .  .  D  .  G  .  D  .  .  D  .  .  .  .
        /// 8        .  .  .  A  .  .  .  .  D  .  D  .  .  .  .  A  .  .  .
        /// 7        .  .  .  .  A  .  .  .  .  D  .  .  .  .  A  .  .  .  .
        /// 6        A  .  A  .  .  A  .  .  .  .  .  .  .  A  .  .  A  .  A
        /// 5        .  .  .  .  .  .  A  .  D  .  D  .  A  .  .  .  .  .  .
        /// 4        .  .  .  .  .  .  .  A  .  A  .  A  .  .  .  .  .  .  .
        /// 3        A  .  .  .  .  A  .  .  .  .  .  .  .  A  .  .  .  .  A
        /// 2        .  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .
        /// 1        .  .  A  .  .  A  .  .  .  .  .  .  .  A  .  .  A  .  .
        /// </remarks>
        /// <returns>The 19 x 19 Alea Evangelii board</returns>
        private static Board GetAleaEvangelii_19x19Board()
        {
            int boardRows = 19;
            int boardCols = 19;
            Position kingPosition = new Position(10, 'j');
            HashSet<Position> defenders = new HashSet<Position>{
                new Position(15, 'i'),
                new Position(15, 'k'),
                new Position(13, 'j'),
                new Position(12, 'i'),
                new Position(12, 'k'),
                new Position(11, 'e'),
                new Position(11, 'h'),
                new Position(11, 'l'),
                new Position(11, 'o'),
                new Position(10, 'g'),
                new Position(10, 'm'),
                new Position(9, 'e'),
                new Position(9, 'h'),
                new Position(9, 'l'),
                new Position(9, 'o'),
                new Position(8, 'i'),
                new Position(8, 'k'),
                new Position(7, 'j'),
                new Position(5, 'i'),
                new Position(5, 'k')
            };
            HashSet<Position> eliteGuards = new HashSet<Position>
            {
                new Position(11, 'j'),
                new Position(10, 'i'),
                new Position(10, 'k'),
                new Position(9, 'j'),
            };
            HashSet<Position> attackers = new HashSet<Position>{
                new Position(19, 'c'),
                new Position(19, 'f'),
                new Position(19, 'n'),
                new Position(19, 'q'),
                new Position(17, 'a'),
                new Position(17, 'f'),
                new Position(17, 'n'),
                new Position(17, 's'),
                new Position(16, 'h'),
                new Position(16, 'j'),
                new Position(16, 'l'),
                new Position(15, 'g'),
                new Position(15, 'm'),
                new Position(14, 'a'),
                new Position(14, 'c'),
                new Position(14, 'f'),
                new Position(14, 'n'),
                new Position(14, 'q'),
                new Position(14, 's'),
                new Position(13, 'e'),
                new Position(13, 'o'),
                new Position(12, 'd'),
                new Position(12, 'p'),
                new Position(10, 'd'),
                new Position(10, 'p'),
                new Position(8, 'd'),
                new Position(8, 'p'),
                new Position(7, 'e'),
                new Position(7, 'o'),
                new Position(6, 'a'),
                new Position(6, 'c'),
                new Position(6, 'f'),
                new Position(6, 'n'),
                new Position(6, 'q'),
                new Position(6, 's'),
                new Position(5, 'g'),
                new Position(5, 'm'),
                new Position(4, 'h'),
                new Position(4, 'j'),
                new Position(4, 'l'),
                new Position(3, 'a'),
                new Position(3, 'f'),
                new Position(3, 'n'),
                new Position(3, 's'),
                new Position(1, 'c'),
                new Position(1, 'f'),
                new Position(1, 'n'),
                new Position(1, 'q'),
            };
            HashSet<Position> corners = new HashSet<Position>{
                new Position(19, 'a'),
                new Position(19, 'b'),
                new Position(19, 'r'),
                new Position(19, 's'),
                new Position(18, 'a'),
                new Position(18, 'b'),
                new Position(18, 'r'),
                new Position(18, 's'),
                new Position(2, 'a'),
                new Position(2, 'b'),
                new Position(2, 'r'),
                new Position(2, 's'),
                new Position(1, 'a'),
                new Position(1, 'b'),
                new Position(1, 'r'),
                new Position(1, 's'),
            };
            return BuildBoard(boardRows, boardCols, kingPosition, defenders, attackers, eliteGuards, null, null, corners);
        }
        #endregion

        #region Builder
        /// <summary>
        /// Build the board with specified parameters
        /// </summary>
        /// <param name="boardRows">Number of board's rows</param>
        /// <param name="boardCols">Number of board's columns</param>
        /// <param name="kingPosition">King's position</param>
        /// <param name="defenders">Positions for the defenders</param>
        /// <param name="attackers">Positions for the attackers</param>
        /// <param name="thrones">The thrones, if different than the default one</param>
        /// <param name="corners">The corners, if different than the default four</param>
        /// <returns>The populated board</returns>
        private static Board BuildBoard(int boardRows, int boardCols, Position kingPosition, HashSet<Position> defenders, HashSet<Position> attackers, HashSet<Position> eliteGuards = null, HashSet<Position> commanders = null, HashSet<Position> thrones = null, HashSet<Position> corners = null)
        {
            #region Validate the board
            // The board should be at least 5x5 and at most MAX_ROWSxMAX_COLS and should always be odd
            BoardUtils.CheckBoardDimensions(boardRows, boardCols);
            // can't have a game without king
            if (kingPosition.Equals(DefaultValues.DefaultPosition))
            {
                throw new InvalidBoardException(ErrorMessages.MISSING_KING);
            }
            // can't have a game without defenders
            if (defenders.Count == 0)
            {
                throw new InvalidBoardException(ErrorMessages.MISSING_DEFENDERS);
            }
            // can't have a game without attackers
            if (attackers.Count == 0)
            {
                throw new InvalidBoardException(ErrorMessages.MISSING_ATTACKERS);
            }
            int attackersNum = attackers.Count;
            attackersNum += commanders != null ? commanders.Count : 0;
            int defendersNum = defenders.Count;
            defendersNum += eliteGuards != null ? eliteGuards.Count : 0;
            // attackers should always be at least twice the amount of the defenders
            if (attackersNum < defendersNum * 2)
            {
                throw new InvalidBoardException(ErrorMessages.INVALID_A_TO_D_RATIO);
            }
            #endregion

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
            // add elite guards
            if (eliteGuards != null && eliteGuards.Count > 0)
            {
                foreach (Position eliteGuard in eliteGuards)
                {
                    board.AddPiece(new EliteGuard(eliteGuard));
                }
            }
            // add commanders
            if (commanders != null && commanders.Count > 0)
            {
                foreach (Position commander in commanders)
                {
                    board.AddPiece(new Commander(commander));
                }
            }
            // add throne/s
            if (thrones != null && thrones.Count > 0)
            {
                board.AddThroneTiles(thrones);
            }
            else
            {
                board.AddThroneTiles();
            }
            // add corners
            if (corners != null && corners.Count > 0)
            {
                board.AddCornerTiles(corners);
            }
            else
            {
                board.AddCornerTiles();
            }
            // add base camps
            board.AddBaseCamps();
            return board;
        }
        #endregion
    }
}
