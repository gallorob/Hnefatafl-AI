using HnefataflAI.Commons;
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
        public int TurnNumber = 1;
        public GameStatus GameStatus = new GameStatus(false);
        public PieceColors CurrentlyPlaying = PieceColors.BLACK;
        internal Board Board { get; private set; }
        internal BoardTypes BoardType { get; private set; }
        public IPlayer Player1 { get; private set; }
        public IPlayer Player2 { get; private set; }
        internal IGameEngine GameEngine { get; private set; }
        public Game(BoardTypes boardType, IPlayer player1, IPlayer player2, RuleTypes ruleType)
        {
            this.BoardType = boardType;
            this.Board = BoardBuilder.GetBoard(boardType);
            this.Player1 = player1;
            this.Player2 = player2;
            this.GameEngine = new GameEngineImpl(ruleType);
            this.StopWatch.Start();
        }
        public void PlayTurn(IPlayer player)
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
            this.TurnNumber++;
        }
        public string GameOver()
        {
            this.StopWatch.Stop();
            string message = "";
            switch (this.GameStatus.Status)
            {
                case Status.WIN:
                    message = String.Format(Messages.GAME_OVER, PieceColorsUtils.GetRoleFromPieceColor(this.CurrentlyPlaying));
                    break;
                case Status.LOSS:
                    message = String.Format(Messages.GAME_OVER, PieceColorsUtils.GetOppositePieceColor(this.CurrentlyPlaying));
                    break;
                case Status.DRAW:
                    message = Messages.GAME_OVER_DRAW;
                    break;
            }
            GameLogger.LogBoard(this.Board);
            GameLogger.Log(message);
            TimeSpan timeSpan = this.StopWatch.Elapsed;
            GameLogger.LogDuration(timeSpan);
            return message;
        }
        ///
        ///     FOR FRONT-END
        ///
        public Game(Board board, IPlayer player1, IPlayer player2, IGameEngine gameEngine)
        {
            this.Board = board;
            this.Player1 = player1;
            this.Player2 = player2;
            this.GameEngine = gameEngine;
            this.StopWatch.Start();
        }
        public Move PlayTurn(IPlayer player, string rawMove)
        {
            string[] playerMove = rawMove.Split('-');
            Move actualMove = this.GameEngine.ProcessPlayerMove(playerMove, this.Board);
            GameLogger.LogMove(this.TurnNumber, player.PieceColors, actualMove);
            GameLogger.LogBoard(this.Board);
            this.GameEngine.ApplyMove(actualMove, this.Board, player.PieceColors);
            this.GameStatus = this.GameEngine.GetGameStatus(actualMove.Piece, this.Board);
            GameLogger.LogPiecesCaptures(this.GameStatus.CapturedPieces);
            this.TurnNumber++;
            return actualMove;
        }
    }
}
