using HnefataflAI.AI.Bots;
using HnefataflAI.AI.RuleEngine;
using HnefataflAI.Commons;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Logs;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games.Engine;
using HnefataflAI.Games.Engine.Impl;
using HnefataflAI.Games.GameState;
using HnefataflAI.Pieces;
using HnefataflAI.Player;
using HnefataflAI.Player.Impl;
using System;
using System.Diagnostics;

namespace HnefataflAI.Games
{
    public class Game
    {
        private Stopwatch StopWatch = new Stopwatch();
        private int TurnNumber = 0;
        private GameStatus GameStatus = new GameStatus(false);
        private PieceColors CurrentlyPlaying = PieceColors.BLACK;
        internal Board Board { get; private set; }
        internal IPlayer Player1 { get; private set; }
        internal IPlayer Player2 { get; private set; }
        internal IGameEngine GameEngine { get; private set; }
        internal BotEngine BotRuleEngine { get; private set; }
        public Game(Board board, IPlayer player1, IPlayer player2)
        {
            this.Board = board;
            this.Player1 = player1;
            this.Player2 = player2;
            this.GameEngine = new HnefataflGameEngine();
            this.BotRuleEngine = new BotEngine();
        }
        public void StartGame()
        {
            this.StopWatch.Start();
            GameLogger.Log(this.Board.ToString());
            while (!this.GameStatus.IsGameOver)
            {
                PlayPlayer(this.Player1);
                this.CurrentlyPlaying = Player1.PieceColors;
                if (this.GameStatus.IsGameOver) break;
                PlayPlayer(this.Player2);
                this.CurrentlyPlaying = Player2.PieceColors;
                if (this.GameStatus.IsGameOver) break;
            }
            this.StopWatch.Stop();
            DisplayGameOver(CurrentlyPlaying, this.GameStatus.Status);
        }
        private void DisplayGameOver(PieceColors pieceColor, Status status)
        {
            string winner = "";
            switch (status)
            {
                case Status.WIN:
                    winner = PieceColorsUtils.GetRoleFromPieceColor(pieceColor);
                    break;
                case Status.LOSS:
                    winner = PieceColorsUtils.GetRoleFromPieceColor(PieceColorsUtils.GetOppositePieceColor(pieceColor));
                    break;
            }
            Console.WriteLine(String.Format(Messages.GAME_OVER, winner));
            GameLogger.Log(this.Board.ToString());
            GameLogger.Log(String.Format(Messages.GAME_OVER, winner));
            TimeSpan timeSpan = this.StopWatch.Elapsed;
            GameLogger.LogDuration(timeSpan);
            Console.WriteLine(Messages.PRESS_TO_CONTINUE);
            Console.ReadKey();
        }
        private void PlayPlayer(IPlayer player)
        {
            try
            {
                switch (player.PieceColors)
                {
                    case PieceColors.BLACK:
                        Console.WriteLine(Messages.BLACK_TURN);
                        break;
                    case PieceColors.WHITE:
                        Console.WriteLine(Messages.WHITE_TURN);
                        break;
                }
                Console.WriteLine(this.Board);
                PlayTurn(player);
                Console.Clear();
            }
            catch (Exception e) when (e is InvalidMoveException || e is InvalidInputException)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(Messages.PRESS_TO_CONTINUE);
                Console.ReadKey();
                Console.Clear();
                PlayPlayer(player);
            }
        }
        private void PlayTurn(IPlayer player)
        {
            this.TurnNumber++;
            string[] playerMove;
            if (player is HumanPlayer)
                playerMove = player.GetMove();
            else
                playerMove = ((IHnefataflBot)player).GetMove(this.Board, this.BotRuleEngine.GetAvailableMovesByColor(player.PieceColors, this.Board));
            Move actualMove = this.GameEngine.ProcessPlayerMove(playerMove, this.Board);
            GameLogger.LogMove(this.TurnNumber, player.PieceColors, actualMove);
            GameLogger.Log(this.Board.ToString());
            this.GameEngine.ApplyMove(actualMove, this.Board, player.PieceColors);
            this.GameStatus = this.GameEngine.GetGameStatus(actualMove.Piece, this.Board);
            foreach(IPiece piece in this.GameStatus.CapturedPieces)
            {
                GameLogger.LogPieceCapture(player.PieceColors, piece);
            }
        }
    }
}
