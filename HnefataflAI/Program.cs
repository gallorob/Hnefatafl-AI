using HnefataflAI.Commons;
using HnefataflAI.Games;
using HnefataflAI.Games.Engine.Impl;
using HnefataflAI.Player;
using HnefataflAI.Player.Impl;
using System;
using System.Collections.Generic;

namespace HnefataflAI
{
    class Program
    {
        static void Main(string[] args)
        {
            RunPvPGame();
            //RunPvPCGame();
            //RunMovesTest();
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
            IPlayer player2 = new TaflBot(PieceColors.WHITE);

            Game game = new Game(board, player1, player2);
            game.StartGame();
        }
        static void RunMovesTest()
        {
            Board board = Defaults.DefaultValues.GetMovesTestTable();
            IPlayer player1 = new HumanPlayer(PieceColors.BLACK);
            IPlayer player2 = new TaflBot(PieceColors.WHITE);
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
