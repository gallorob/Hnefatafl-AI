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
    class TaflBotBasic : ITaflBot
    {
        /// <summary>
        /// The rule type for the bot
        /// </summary>
        public RuleTypes RuleType { get; private set; }
        /// <summary>
        /// The piece color
        /// </summary>
        public PieceColors PieceColors { get; private set; }
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
            this.RuleType = ruleType;
            this.GameEngine = new GameEngineImpl(ruleType);
        }
        /// <summary>
        /// Only for implementation
        /// </summary>
        /// <returns>Nothing; throws NotImplementedException</returns>
        public string[] GetMove()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Get the best move as a user input
        /// </summary>
        /// <param name="board">The board</param>
        /// <param name="moves">The list of all possible moves</param>
        /// <returns>The best move as a user input</returns>
        public string[] GetMove(Board board, List<Move> moves)
        {
            Move bestMove = ComputeBestMove(board, moves);
            return MoveUtils.MoveAsInput(bestMove);
        }
        /// <summary>
        /// Compute the best move
        /// </summary>
        /// <param name="board">The current board state</param>
        /// <param name="moves">The list of all possible moves</param>
        /// <returns>The best move in the current turn</returns>
        public Move ComputeBestMove(Board board, List<Move> moves)
        {
            Move bestMove = null;
            int bestMoveValue = int.MinValue;
            // randomize list so it's not always the same if no best move is found
            ListUtils.ShuffleList(moves);
            foreach (Move move in moves)
            {
                // update board with the move
                this.GameEngine.ApplyMove(move, board, PieceColors);
                GameStatus gameStatus = this.GameEngine.GetGameStatus(move.Piece, board);
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
                // reset the moved piece's position to its original value
                move.Piece.UpdatePosition(move.From);
            }
            return bestMove;
        }
    }
}
