using HnefataflAI.AI.Bots;
using HnefataflAI.AI.RuleEngine;
using HnefataflAI.Commons;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Games.Engine;
using HnefataflAI.Games.Engine.Impl;
using HnefataflAI.Player;
using HnefataflAI.Player.Impl;
using System;

namespace HnefataflAI.Games
{
    public class Game
    {
        private bool GameOver = false;
        private PieceColors CurrentlyPlaying = PieceColors.BLACK;
        internal Board Board { get; private set; }
        internal IPlayer Player1 { get; private set; }
        internal IPlayer Player2 { get; private set; }
        internal IGameEngine GameEngine { get; private set; }
        internal BotRuleEngine BotRuleEngine { get; private set; }
        public Game(Board board, IPlayer player1, IPlayer player2)
        {
            this.Board = board;
            this.Player1 = player1;
            this.Player2 = player2;
            this.GameEngine = new HnefataflGameEngine();
            this.BotRuleEngine = new BotRuleEngine(board);
        }
        public void StartGame()
        {
            Console.WriteLine("\t\t\tGAME OF HNEFATAFL");
            while (!this.GameOver)
            {
                PlayPlayer(this.Player1);
                this.CurrentlyPlaying = Player1.PieceColors;
                if (this.GameOver) break;
                PlayPlayer(this.Player2);
                this.CurrentlyPlaying = Player2.PieceColors;
                if (this.GameOver) break;
            }
            DisplayGameOver(CurrentlyPlaying);
        }
        private void DisplayGameOver(PieceColors pieceColor)
        {
            string winner = "";
            switch (pieceColor)
            {
                case PieceColors.BLACK:
                    winner = "Attacker";
                    break;
                case PieceColors.WHITE:
                    winner = "Defender";
                    break;
            }
            Console.WriteLine(String.Format(Messages.GAME_OVER, winner));
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
                playerMove = ((IHnefataflBot)player).GetMove(this.Board.GetBoardCopy(), this.BotRuleEngine.GetAvailableMovesByColor(player.PieceColors));
            Move actualMove = this.GameEngine.ProcessPlayerMove(playerMove, this.Board);
            this.GameEngine.ApplyMove(actualMove, this.Board, player.PieceColors);
            this.GameOver = this.GameEngine.GetGameStatus(actualMove.Piece, this.Board);
        }
    }
}
