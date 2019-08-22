using HnefataflAI.Commons;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Player;
using System;

namespace HnefataflAI.Games
{
    public class ConsoleGameRunner
    {
        private readonly Game Game;
        public ConsoleGameRunner(Game game)
        {
            this.Game = game;
        }
        public void StartGame()
        {
            while (!Game.GameStatus.IsGameOver)
            {
                if (Game.Player1.PieceColors.Equals(Game.CurrentlyPlaying))
                {
                    PlayerPlay(Game.Player1);
                }
                else
                {
                    PlayerPlay(Game.Player2);
                }
                if (Game.GameStatus.IsGameOver)
                {
                    break;
                }
                Game.CurrentlyPlaying = Game.GameStatus.NextPlayer;
            }
            DisplayGameOver();
        }
        public void PlayerPlay(IPlayer player)
        {
            //try
            //{
                switch (player.PieceColors)
                {
                    case PieceColors.BLACK:
                        Console.WriteLine(Messages.BLACK_TURN);
                        break;
                    case PieceColors.WHITE:
                        Console.WriteLine(Messages.WHITE_TURN);
                        break;
                }
                Console.WriteLine(Game.Board);
                Game.PlayTurn(player);
                Console.Clear();
            //}
            //catch (Exception e) when (e is InvalidMoveException || e is InvalidInputException || e is CustomGenericException)
            //{
            //    Console.WriteLine(e.Message);
            //    Console.WriteLine(Messages.PRESS_TO_CONTINUE);
            //    Console.ReadKey();
            //    Console.Clear();
            //    PlayerPlay(player);
            //}
        }
        private void DisplayGameOver()
        {
            Console.WriteLine(Game.GameOver());
            Console.WriteLine(Messages.PRESS_TO_CONTINUE);
            Console.ReadKey();
        }
    }
}
