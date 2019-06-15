using HnefataflAI.AI;
using HnefataflAI.AI.Bots;
using HnefataflAI.AI.Bots.Impl;
using HnefataflAI.AI.RuleEngine;
using HnefataflAI.Commons;
using HnefataflAI.Commons.DataTypes;
using HnefataflAI.Games;
using HnefataflAI.Player;
using HnefataflAI.Player.Impl;
using System;

namespace HnefataflAI
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunPvPGame();
            RunPvPCGame();
            //RunMovesTest();

            //testMatrix();
            //testMovesEvaluator();
        }

        private static void testMovesEvaluator()
        {
            MovesEvaluator movesEvaluator = new MovesEvaluator();
            int bv = movesEvaluator.EvaluateBoard(Defaults.DefaultValues.GetDefaultHnefataflTable(), PieceColors.BLACK);
            int wv = movesEvaluator.EvaluateBoard(Defaults.DefaultValues.GetDefaultHnefataflTable(), PieceColors.WHITE);

            Console.Out.Write(String.Format("Attacker board value: {0}\nDefender board value: {1}", bv, wv));
            Console.In.Read();
        }

        static void testMatrix()
        {
            Matrix<Int32> m = new Matrix<Int32>(5, 5);
            m.Set(0, 0, 0);
            m.Set(0, 1, 1);
            m.Set(0, 2, 2);
            m.Set(0, 3, 3);
            m.Set(0, 4, 4);
            m.Set(1, 1, 2);
            m.Set(2, 1, 3);
            m.Set(3, 1, 4);
            m.Set(4, 1, 5);

            Console.Out.Write("Matrix:\n");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Out.Write(String.Format(" {0} ", m.At(i,j)));
                }
                Console.Out.Write("\n");
            }

            int[] row0 = m.GetRow(0, 2, 1);
            int[] col1 = m.GetCol(1, 2, 2);
            int[,] sq = m.GetRange(1, 0, 4, 2);

            Console.Out.Write("Row 0:\n");
            foreach (Int32 rowVal in row0)
            {
                Console.Out.Write(String.Format(" {0} ", rowVal));
            }

            Console.Out.Write("\nCol 1:\n");
            foreach (Int32 colVal in col1)
            {
                Console.Out.Write(String.Format(" {0} ", colVal));
            }

            Console.Out.Write("\n4 cols x 2 rows submatrix from row 1 - col 0:\n");
            for (int i = 0; i < sq.GetLength(0); i++)
            {
                for (int j = 0; j < sq.GetLength(1); j++)
                {
                    Console.Out.Write(String.Format(" {0} ", sq[i,j]));
                }
                Console.Out.Write("\n");
            }
            Console.In.Read();

        }


        static void RunPvPGame()
        {
            Board board = Defaults.DefaultValues.GetDefaultHnefataflTable();
            IPlayer player1 = new HumanPlayer(PieceColors.BLACK);
            IPlayer player2 = new HumanPlayer(PieceColors.WHITE);

            Game game = new Game(board, player1, player2);
            game.StartGame();
        }
        static void RunPvPCGame()
        {
            Board board = Defaults.DefaultValues.GetDefaultHnefataflTable();
            IPlayer player1 = new HumanPlayer(PieceColors.BLACK);
            IHnefataflBot player2 = new TaflBotBasic(PieceColors.WHITE);

            Game game = new Game(board, player1, player2);
            game.StartGame();
        }
        static void RunMovesTest()
        {
            Board board = Defaults.DefaultValues.GetMovesTestTable();
            IPlayer player1 = new HumanPlayer(PieceColors.BLACK);
            IHnefataflBot player2 = new TaflBotRandom(PieceColors.WHITE);
            BotRuleEngine ruleEngine = new BotRuleEngine(board);

            Console.Out.Write(String.Format("Board state:\n{0}", board));
            Console.Out.Write("Moves for white:\n");
            foreach(Move move in ruleEngine.GetAvailableMovesByColor(player2.PieceColors))
            {
                Console.Out.Write(String.Format("{0}\n", move));
            }
            Console.Out.Write("Moves for black:\n");
            foreach (Move move in ruleEngine.GetAvailableMovesByColor(player1.PieceColors))
            {
                Console.Out.Write(String.Format("{0}\n", move));
            }
            Console.In.Read();
        }
    }
}
