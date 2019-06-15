using HnefataflAI.AI.RuleEngine;
using HnefataflAI.Commons;
using HnefataflAI.Commons.DataTypes;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Defaults;
using HnefataflAI.Games;
using HnefataflAI.Games.Engine;
using HnefataflAI.Games.Engine.Impl;
using HnefataflAI.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HnefataflAI.AI.Bots.Impl
{
    class TaflBotMinimaxAB : IHnefataflBot
    {
        /// <summary>
        /// The piece color
        /// </summary>
        public PieceColors PieceColors { get; private set; }
        /// <summary>
        /// The internal GameEngine
        /// </summary>
        private readonly IGameEngine GameEngine = new HnefataflGameEngine();
        /// <summary>
        /// The internal MovesEvaluator
        /// </summary>
        private readonly MovesEvaluator MovesEvaluator = new MovesEvaluator();
        /// <summary>
        /// Constructor for the TaflBotMinimax
        /// </summary>
        /// <param name="pieceColors">The piece color</param>
        public TaflBotMinimaxAB(PieceColors pieceColors)
        {
            this.PieceColors = pieceColors;
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
            Move bestMove = ComputeBestMoveMinimaxAB(DefaultValues.MINIMAX_DEPTH, board, moves, int.MinValue, int.MaxValue, true).Move;
            return MoveUtils.MoveAsInput(bestMove);
        }
        /// <summary>
        /// Compute the best move using the Minimax algorithm with Alpha-Beta Pruning
        /// </summary>
        /// <remarks>
        /// This version of the minimax algorithm is much faster than its vanilla version.
        /// The game is playable with this bot.
        /// </remarks>
        /// <param name="depth">The lookahead level</param>
        /// <param name="board">The current board state</param>
        /// <param name="moves">The list of all possible moves</param>
        /// <param name="alpha">The Alpha value</param>
        /// <param name="beta">The Beta value</param>
        /// <param name="isMaximizing">>If it's maximizing or minimizing</param>
        /// <returns>The best move in the current turn</returns>
        private MoveValue ComputeBestMoveMinimaxAB(int depth, Board board, List<Move> moves, int alpha, int beta, bool isMaximizing)
        {
            // reached the end of the branch, evaluate the board
            if (depth == 0)
            {
                int boardValue = this.MovesEvaluator.EvaluateBoard(board, PieceColors);
                return new MoveValue(null, boardValue);
            }
            // we need to keep track of both the best move and its value during each iteration
            Move bestMove = null;
            int bestValue = isMaximizing ? int.MinValue : int.MaxValue;
            MoveValue bestMoveValue = new MoveValue(bestMove, bestValue);
            // randomize list so it's not always the same if no best move is found
            ListUtils.ShuffleList(moves);
            foreach (Move move in moves)
            {
                // keep a copy of the piece's original position
                Position originalPosition = move.From;
                // copy the board so the evaluation doesn't alter the original one
                Board boardCopy = new Board(new Matrix<IPiece>(board.GetCurrentBoard().GetMatrixCopy()));
                // update board with the move
                this.GameEngine.ApplyMove(move, boardCopy, PieceColors);
                this.GameEngine.GetGameStatus(move.Piece, boardCopy);
                // recursive call for the move's sub-tree
                int moveValue = ComputeBestMoveMinimaxAB(depth - 1, boardCopy, new BotRuleEngine(boardCopy).GetAvailableMovesByColor(PieceColors), alpha, beta, !isMaximizing).Value;
                // two different cases if we're simulating our turn or the opponent's
                if (isMaximizing)
                {
                    if (moveValue > bestMoveValue.Value)
                    {
                        bestMoveValue.Value = moveValue;
                        bestMoveValue.Move = move;
                    }
                    alpha = Math.Max(alpha, moveValue);
                }
                else
                {
                    if (moveValue < bestMoveValue.Value)
                    {
                        bestMoveValue.Value = moveValue;
                        bestMoveValue.Move = move;
                    }
                    beta = Math.Min(beta, moveValue);
                }
                // reset the moved piece's position to its original value
                move.Piece.UpdatePosition(originalPosition);
                if (beta <= alpha)
                {
                    break;
                }
            }
            return bestMoveValue;
        }
    }
}
