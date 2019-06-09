using HnefataflAI.Commons.Positions;
using HnefataflAI.Games;
using HnefataflAI.Pieces.Impl;

namespace HnefataflAI.Defaults
{
    public class DefaultValues
    {
        public static int MAX_ROWS = 11;
        public static int MAX_COLS = 11;
        public static Board GetDefaultHnefataflTable()
        {
            Board board = new Board(11, 11);

            int[] whiteRows = { 8, 7, 7, 7, 6, 6, 6, 6, 5, 5, 5, 4 };
            char[] whiteCols = { 'f', 'e', 'f', 'g', 'd', 'e', 'g', 'h', 'e', 'f', 'g', 'f' };

            int[] blackRows = { 11, 11, 11, 11, 11, 10, 8, 8, 7, 7, 6, 6, 6, 6, 5, 5, 4, 4, 2, 1, 1, 1, 1, 1 };
            char[] blackCols = { 'd', 'e', 'f', 'g', 'h', 'f', 'a', 'k', 'a', 'k', 'a', 'b', 'j', 'k', 'a', 'k', 'a', 'k', 'f', 'd', 'e', 'f', 'g', 'h' };

            // add white pieces
            King king = new King(new Position(6, 'f'));
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
        public static Board GetMovesTestTable()
        {
            Board board = new Board(11, 11);

            int[] whiteRows = { 7, 7, 7 };
            char[] whiteCols = { 'e', 'f', 'g' };

            int[] blackRows = { 11, 11, 11, 11, 11, 8, 8, 7, 7, 6, 6, 5, 5, 4, 4, 1, 1, 1, 1, 1 };
            char[] blackCols = { 'd', 'e', 'f', 'g', 'h', 'a', 'k', 'a', 'k', 'a', 'k', 'a', 'k', 'a', 'k', 'd', 'e', 'f', 'g', 'h' };

            King king = new King(new Position(4, 'f'));
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
    }
}
