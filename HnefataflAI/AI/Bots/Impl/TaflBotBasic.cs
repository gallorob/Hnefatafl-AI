using HnefataflAI.Commons;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Engine;
using HnefataflAI.Games.Engine.Impl;
using HnefataflAI.Games.GameState;
using HnefataflAI.Games.Rules;
using System.Collections.Generic;

namespace HnefataflAI.AI.Bots.Impl
{
    /// <summary>
    /// A Hnefatafl bot that plays the best move possible
    /// <remarks>
    /// Its algorithm is a simple board evaluation in its current turn.
    /// </remarks>
    /// </summary>
    class TaflBotBasic : ATaflBot
    {
        /// <summary>
        /// The internal GameEngine
        /// </summary>
        private readonly IGameEngine GameEngine;
        /// <summary>
        /// The internal BoardEvaluator
        /// </summary>
        private readonly BoardEvaluator MovesEvaluator = new BoardEvaluator();
        /// <summary>
        /// Constructor for the TaflBotBasic
        /// </summary>
        /// <param name="pieceColors">The piece color</param>
        /// <param name="ruleType">The rule type</param>
        public TaflBotBasic(PieceColors pieceColors, RuleTypes ruleType)
        {
            this.PieceColors = pieceColors;
            this.Rule = RuleUtils.GetRule(ruleType);
            this.GameEngine = new GameEngineImpl(ruleType);
            this.BotType = BotTypes.BASIC;
            //temporary
            this.PlayerName = "Bassy";
            this.AdditionalInfo = new List<string> { "Just a basic player bot" };
        }
        /// <summary>
        /// Get the best move as a user input
        /// </summary>
        /// <param name="board">The board</param>
        /// <param name="moves">The list of all possible moves</param>
        /// <returns>The best move as a user input</returns>
        public override string[] GetMove(Board board)
        {
            Move bestMove = ComputeBestMove(board);
            return MoveUtils.MoveAsInput(bestMove);
        }
        /// <summary>
        /// Compute the best move
        /// </summary>
        /// <param name="board">The current board state</param>
        /// <param name="moves">The list of all possible moves</param>
        /// <returns>The best move in the current turn</returns>
        public Move ComputeBestMove(Board board)
        {
            Move bestMove = null;
            int bestMoveValue = int.MinValue;
            HashSet<Move> moves = GameEngine.GetMovesByColor(PieceColors, board);
            foreach (Move move in moves)
            {
                // update board with the move
                this.GameEngine.ApplyMove(move, board, PieceColors);
                GameStatus gameStatus = this.GameEngine.GetGameStatus(move, board);
                // evaluate the new board status
                int moveValue = this.MovesEvaluator.EvaluateBoard(board, PieceColors);
                if (gameStatus.IsGameOver && gameStatus.Status is Status.WIN)
                {
                    moveValue = PieceValues.WinValue;
                }
                else if (gameStatus.IsGameOver && gameStatus.Status is Status.LOSS)
                {
                    moveValue = PieceValues.LossValue;
                }
                if (moveValue > bestMoveValue)
                {
                    bestMove = move;
                    bestMoveValue = moveValue;
                }
                // revert board's state
                this.GameEngine.UndoMove(move, board, PieceColors);
                this.GameEngine.UndoCaptures(board);
            }
            return bestMove;
        }
    }
}
