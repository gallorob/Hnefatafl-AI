using HnefataflAI.Commons;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Logs;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games.Engine;
using HnefataflAI.Games.Engine.Impl;
using HnefataflAI.Games.GameState;
using HnefataflAI.Player;
using System;
using System.Diagnostics;
using HnefataflAI.Games.Rules;
using HnefataflAI.Player.Impl;
using HnefataflAI.AI.Bots;
using HnefataflAI.Games.Boards;

namespace HnefataflAI.Games
{
    public class Game
    {
        private readonly Stopwatch StopWatch = new Stopwatch();
        private int TurnNumber = 0;
        private GameStatus GameStatus = new GameStatus(false);
        private PieceColors CurrentlyPlaying = PieceColors.BLACK;
        internal Board Board { get; private set; }
        internal BoardTypes BoardType { get; private set; }
        internal IPlayer Player1 { get; private set; }
        internal IPlayer Player2 { get; private set; }
        internal IGameEngine GameEngine { get; private set; }
        public Game(BoardTypes boardType, IPlayer player1, IPlayer player2, RuleTypes ruleType)
        {
            this.BoardType = boardType;
            this.Board = BoardBuilder.GetBoard(boardType);
            this.Player1 = player1;
            this.Player2 = player2;
            this.GameEngine = new GameEngineImpl(ruleType);
        }
        public void StartGame()
        {
            this.StopWatch.Start();
            while (!this.GameStatus.IsGameOver)
            {
                this.TurnNumber++;
                if (this.Player1.PieceColors.Equals(this.CurrentlyPlaying))
                {
                    PlayPlayer(this.Player1);
                }
                else
                {
                    PlayPlayer(this.Player2);
                }
                if (this.GameStatus.IsGameOver)
                {
                    break;
                }
                this.CurrentlyPlaying = this.GameStatus.NextPlayer;
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
            GameLogger.LogBoard(this.Board);
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
            string[] playerMove;
            if (player is HumanPlayer)
                playerMove = player.GetMove();
            else
                playerMove = ((ITaflBot)player).GetMove(this.Board, this.GameEngine.GetMovesByColor(player.PieceColors, this.Board));
            Move actualMove = this.GameEngine.ProcessPlayerMove(playerMove, this.Board);
            GameLogger.LogMove(this.TurnNumber, player.PieceColors, actualMove);
            GameLogger.LogBoard(this.Board);
            this.GameEngine.ApplyMove(actualMove, this.Board, player.PieceColors);
            this.GameStatus = this.GameEngine.GetGameStatus(actualMove.Piece, this.Board);
            GameLogger.LogPiecesCaptures(this.GameStatus.CapturedPieces);
        }
    }
}
