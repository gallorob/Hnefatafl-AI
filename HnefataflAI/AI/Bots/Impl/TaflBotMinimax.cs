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

namespace HnefataflAI.AI.Bots.Impl
{
    /// <summary>
    /// A Hnefatafl bot that plays the best move possible
    /// <remarks>
    /// Its algorithm is a vanilla minimax algorithm.
    /// </remarks>
    /// </summary>
    class TaflBotMinimax : IHnefataflBot
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
        /// Constructor for the TaflBotBasic
        /// </summary>
        /// <param name="pieceColors">The piece color</param>
        public TaflBotMinimax(PieceColors pieceColors)
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
            Move bestMove = ComputeBestMoveMinimax(DefaultValues.MINIMAX_DEPTH, board, moves, true).Move;
            return MoveUtils.MoveAsInput(bestMove);
        }
        /// <summary>
        /// Compute the best move using the Minimax algorithm
        /// </summary>
        /// <remarks>
        /// This vanilla version of the minimax algorithm is VERY time consuming and resources intensive.
        /// It's pretty much impossible to play with this Bot since it takes too long to make a move.
        /// </remarks>
        /// <param name="depth">The lookahead level</param>
        /// <param name="board">The current board state</param>
        /// <param name="moves">The list of all possible moves</param>
        /// <param name="isMaximizing">If it's maximizing or minimizing</param>
        /// <returns>The best move in the current turn</returns>
        private MoveValue ComputeBestMoveMinimax(int depth, Board board, List<Move> moves, bool isMaximizing)
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
                int moveValue = ComputeBestMoveMinimax(depth - 1, boardCopy, new BotRuleEngine(boardCopy).GetAvailableMovesByColor(PieceColors), !isMaximizing).Value;
                // two different cases if we're simulating our turn or the opponent's
                if (isMaximizing)
                {
                    if (moveValue > bestMoveValue.Value)
                    {
                        bestMoveValue.Value = moveValue;
                        bestMoveValue.Move = move;
                    }
                }
                else
                {
                    if (moveValue < bestMoveValue.Value)
                    {
                        bestMoveValue.Value = moveValue;
                        bestMoveValue.Move = move;
                    }
                }
                // reset the moved piece's position to its original value
                move.Piece.UpdatePosition(originalPosition);
            }
           return bestMoveValue;
        }
    }
}
