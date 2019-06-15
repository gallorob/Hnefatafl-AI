using HnefataflAI.Commons;
using HnefataflAI.Commons.DataTypes;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
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
    /// Its algorithm is a simple board evaluation in its current turn.
    /// </remarks>
    /// </summary>
    class TaflBotBasic : IHnefataflBot
    {
        /// <summary>
        /// The piece color
        /// </summary>
        public PieceColors PieceColors { get; private set; }
        /// <summary>
        /// The internal GameEngine
        /// </summary>
        private IGameEngine GameEngine = new HnefataflGameEngine();
        /// <summary>
        /// The internal MovesEvaluator
        /// </summary>
        private MovesEvaluator MovesEvaluator = new MovesEvaluator();
        /// <summary>
        /// Constructor for the TaflBotBasic
        /// </summary>
        /// <param name="pieceColors">The piece color</param>
        public TaflBotBasic(PieceColors pieceColors)
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
                // keep a copy of the piece's original position
                Position originalPosition = move.From;
                // copy the board so the evaluation doesn't alter the original one
                Board boardCopy = new Board(new Matrix<IPiece>(board.GetCurrentBoard().GetMatrixCopy()));
                // update board with the move
                this.GameEngine.ApplyMove(move, boardCopy, PieceColors);
                this.GameEngine.GetGameStatus(move.Piece, boardCopy);
                // evaluate the new board status
                int moveValue = this.MovesEvaluator.EvaluateBoard(boardCopy, PieceColors);
                if (moveValue > bestMoveValue)
                {
                    bestMove = move;
                    bestMoveValue = moveValue;
                }
                // reset the moved piece's position to its original value
                move.Piece.UpdatePosition(originalPosition);
            }
            return bestMove;
        }
    }
}
