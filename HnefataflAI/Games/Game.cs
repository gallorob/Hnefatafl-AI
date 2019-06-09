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
            bool gameOver = false;
            Console.WriteLine("\t\t\tGAME OF HNEFATAFL");

            while (!gameOver)
            {
                PlayPlayer(this.Player1);
                PlayPlayer(this.Player2);
            }
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
            Console.WriteLine(this.Board);
            string[] playerMove;
            if (player is HumanPlayer)
                playerMove = player.getMove();
            else
                playerMove = ((TaflBot)player).getMove(this.Board.GetBoardCopy(), this.BotRuleEngine.GetAvailableMovesByColor(player.PieceColors));
            Move actualMove = this.GameEngine.ProcessPlayerMove(playerMove, this.Board);
            this.GameEngine.ApplyMove(actualMove, this.Board, player.PieceColors);
            Console.Clear();
            Console.WriteLine(this.Board);
        }
    }
}
