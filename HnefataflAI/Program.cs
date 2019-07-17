﻿using HnefataflAI.Commons;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games;
using HnefataflAI.Player;
using HnefataflAI.Player.Impl;
using System;
using System.Collections.Generic;
using HnefataflAI.Games.Rules;
using HnefataflAI.AI.Bots;
using HnefataflAI.AI.Bots.Impl;
using HnefataflAI.Games.Engine.Impl;
using HnefataflAI.Games.Engine;
using HnefataflAI.AI;
using HnefataflAI.Games.Boards;

namespace HnefataflAI
{
    class Program
    {
        static void Main()
        {
            RunPvPGame();
            //RunPvPCGame();
            //RunPCvPCGame();
            //RunMovesTest();
            //TestBoardEvaluator();
        }

        private static void RunPCvPCGame()
        {
            RuleTypes ruleType = RuleTypes.HNEFATAFL;
            BoardTypes boardType = BoardTypes.COPENHAGEN_HNEFATAFL_11X11;
            IPlayer player1 = new TaflBotMinimaxAB(PieceColors.BLACK, ruleType);
            ITaflBot player2 = new TaflBotMinimaxAB(PieceColors.WHITE, ruleType);

            Game game = new Game(boardType, player1, player2, ruleType);
            game.StartGame();
        }
        static void RunPvPGame()
        {
            RuleTypes ruleType = RuleTypes.HNEFATAFL;
            BoardTypes boardType = BoardTypes.COPENHAGEN_HNEFATAFL_11X11;
            IPlayer player1 = new HumanPlayer(PieceColors.BLACK);
            IPlayer player2 = new HumanPlayer(PieceColors.WHITE);

            Game game = new Game(boardType, player1, player2, ruleType);
            game.StartGame();
        }
        static void RunPvPCGame()
        {
            RuleTypes ruleType = RuleTypes.HNEFATAFL;
            BoardTypes boardType = BoardTypes.COPENHAGEN_HNEFATAFL_11X11;
            IPlayer player1 = new HumanPlayer(PieceColors.BLACK);
            ITaflBot player2 = new TaflBotMinimaxAB(PieceColors.WHITE, ruleType);

            Game game = new Game(boardType, player1, player2, ruleType);
            game.StartGame();
        }
        private static void TestBoardEvaluator()
        {
            BoardEvaluator movesEvaluator = new BoardEvaluator();
            BoardTypes boardType = BoardTypes.HISTORICAL_HNEFATAFL_7X7;
            Board board = BoardBuilder.GetBoard(boardType);
            int bv = movesEvaluator.EvaluateBoard(board, PieceColors.BLACK);
            int wv = movesEvaluator.EvaluateBoard(board, PieceColors.WHITE);

            Console.Out.Write(String.Format("Board state:\n{0}", board));
            Console.Out.Write(String.Format("Attacker board value: {0}\nDefender board value: {1}", bv, wv));
            Console.In.Read();
        }
        static void RunMovesTest()
        {
            RuleTypes ruleType = RuleTypes.HNEFATAFL;
            Board board = BoardBuilder.GetTestingTable();
            IPlayer player1 = new HumanPlayer(PieceColors.BLACK);
            ITaflBot player2 = new TaflBotRandom(PieceColors.WHITE, ruleType);
            IGameEngine gameEngine = new GameEngineImpl(ruleType);

            List<Move> wmoves = gameEngine.GetMovesByColor(player2.PieceColors, board);
            List<Move> bmoves = gameEngine.GetMovesByColor(player1.PieceColors, board);

            Console.Out.Write(String.Format("Board state:\n{0}", board));
            Console.Out.Write("Moves for white:\n");
            foreach (Move move in wmoves)
            {
                Console.Out.Write(String.Format("{0}\n", move));
            }
            Console.Out.Write("Moves for black:\n");
            foreach (Move move in bmoves)
            {
                Console.Out.Write(String.Format("{0}\n", move));
            }

            MoveUtils.OrderMovesByCapture(wmoves, board, RuleTypes.HNEFATAFL);
            Console.Out.Write("Moves for white:\n");
            foreach (Move move in wmoves)
            {
                Console.Out.Write(String.Format("{0}\n", move));
            }
            MoveUtils.OrderMovesByCapture(bmoves, board, RuleTypes.HNEFATAFL);
            Console.Out.Write("Moves for black:\n");
            foreach (Move move in bmoves)
            {
                Console.Out.Write(String.Format("{0}\n", move));
            }
            Console.In.Read();
        }
    }
}
