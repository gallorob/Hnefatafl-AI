using HnefataflAI.AI;
using HnefataflAI.AI.Bots;
using HnefataflAI.AI.Bots.Impl;
using HnefataflAI.AI.RuleEngine;
using HnefataflAI.Commons;
using HnefataflAI.Games;
using HnefataflAI.Player;
using HnefataflAI.Player.Impl;
using System;

namespace HnefataflAI
{
    class Program
    {
        static void Main()
        {
            //RunPvPGame();
            //RunPvPCGame();
            RunPCvPCGame();
            //RunMovesTest();
            //TestBoardEvaluator();
        }

        private static void RunPCvPCGame()
        {
            Board board = Defaults.DefaultValues.GetDefaultBrandubhTable();
            IPlayer player1 = new TaflBotMinimaxAB(PieceColors.BLACK);
            IPlayer player2 = new TaflBotMinimaxAB(PieceColors.WHITE);

            Game game = new Game(board, player1, player2);
            game.StartGame();
        }
        static void RunPvPGame()
        {
            Board board = Defaults.DefaultValues.GetTestingTable();
            IPlayer player1 = new HumanPlayer(PieceColors.BLACK);
            IPlayer player2 = new HumanPlayer(PieceColors.WHITE);

            Game game = new Game(board, player1, player2);
            game.StartGame();
        }
        static void RunPvPCGame()
        {
            Board board = Defaults.DefaultValues.GetDefaultBrandubhTable();
            IPlayer player1 = new HumanPlayer(PieceColors.BLACK);
            IHnefataflBot player2 = new TaflBotMinimaxAB(PieceColors.WHITE);

            Game game = new Game(board, player1, player2);
            game.StartGame();
        }
        private static void TestBoardEvaluator()
        {
            BoardEvaluator movesEvaluator = new BoardEvaluator();
            Board board = Defaults.DefaultValues.GetDefaultBrandubhTable();
            int bv = movesEvaluator.EvaluateBoard(board, PieceColors.BLACK);
            int wv = movesEvaluator.EvaluateBoard(board, PieceColors.WHITE);

            Console.Out.Write(String.Format("Board state:\n{0}", board));
            Console.Out.Write(String.Format("Attacker board value: {0}\nDefender board value: {1}", bv, wv));
            Console.In.Read();
        }
        static void RunMovesTest()
        {
            Board board = Defaults.DefaultValues.GetDefaultBrandubhTable();
            IPlayer player1 = new HumanPlayer(PieceColors.BLACK);
            IHnefataflBot player2 = new TaflBotRandom(PieceColors.WHITE);
            BotEngine ruleEngine = new BotEngine();

            Console.Out.Write(String.Format("Board state:\n{0}", board));
            Console.Out.Write("Moves for white:\n");
            foreach(Move move in ruleEngine.GetAvailableMovesByColor(player2.PieceColors, board))
            {
                Console.Out.Write(String.Format("{0}\n", move));
            }
            Console.Out.Write("Moves for black:\n");
            foreach (Move move in ruleEngine.GetAvailableMovesByColor(player1.PieceColors, board))
            {
                Console.Out.Write(String.Format("{0}\n", move));
            }
            Console.In.Read();
        }
    }
}
