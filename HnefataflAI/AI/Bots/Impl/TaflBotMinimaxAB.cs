using HnefataflAI.Commons;
using HnefataflAI.Commons.Logs;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Defaults;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Engine;
using HnefataflAI.Games.Engine.Impl;
using HnefataflAI.Games.GameState;
using HnefataflAI.Games.Rules;
using HnefataflAI.Player;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HnefataflAI.AI.Bots.Impl
{
    class TaflBotMinimaxAB : ATaflBot
    {
        /// <summary>
        /// The internal BoardEvaluator
        /// </summary>
        private readonly BoardEvaluator MovesEvaluator = new BoardEvaluator();
        private readonly IGameEngine GameEngine;
        private readonly List<Move> BotMoves = new List<Move>();
        /// <summary>
        /// Constructor for the TaflBotMinimaxAB
        /// </summary>
        /// <param name="pieceColors">The piece color</param>
        public TaflBotMinimaxAB(PieceColors pieceColors, RuleTypes ruleType)
        {
            this.PieceColors = pieceColors;
            this.Rule = RuleUtils.GetRule(ruleType);
            this.BotType = BotTypes.MINIMAXAB;
            this.GameEngine = new GameEngineImpl(ruleType);
            //temporary
            this.PlayerName = "Minyab";
            this.AdditionalInfo = new List<String> { "A standard minimax with alpha-beta pruning player bot" };
        }
        /// <summary>
        /// Get the best move as a user input
        /// </summary>
        /// <param name="board">The board</param>
        /// <param name="moves">The list of all possible moves</param>
        /// <returns>The best move as a user input</returns>
        public override string[] GetMove(Board board)
        {
            List<Move> moves = GameEngine.GetMovesByColor(PieceColors, board);
            MovesLogger.Log(string.Format("Started decision making for {0}...", PieceColors));
            MoveValue bestMove = ComputeBestMoveMinimaxAB(DefaultValues.MINIMAX_DEPTH, board, moves, int.MinValue, int.MaxValue, true, this.PieceColors);
            MovesLogger.LogMove(bestMove.Move, bestMove.Value);
            this.BotMoves.Add(bestMove.Move);
            return MoveUtils.MoveAsInput(bestMove.Move);
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
        private MoveValue ComputeBestMoveMinimaxAB(int depth, Board board, List<Move> moves, int alpha, int beta, bool isMaximizing, PieceColors pieceColors)
        {
            // reached the end of the branch, evaluate the board
            if (depth == 0)
            {
                int boardValue = this.MovesEvaluator.EvaluateBoard(board, pieceColors);
                return new MoveValue(null, boardValue);
            }
            // we need to keep track of both the best move and its value during each iteration
            Move bestMove = null;
            int bestValue = isMaximizing ? int.MinValue : int.MaxValue;
            MoveValue bestMoveValue = new MoveValue(bestMove, bestValue);
            // order moves in order to reduce number of nodes visited
            OrderMoves(moves, board);
            foreach (Move move in moves)
            {
                // update board with the move
                GameEngine.ApplyMove(move, board, pieceColors);
                // check if it reached an endgame point
                GameStatus gameStatus = GameEngine.GetGameStatus(move.Piece, board);
                if (!gameStatus.IsGameOver)
                {
                    gameStatus.IsGameOver = MoveUtils.IsRepeatedMove(this.BotMoves, move, Rule);
                    gameStatus.Status = Status.LOSS;
                }
                // recursive call for the move's sub-tree
                int moveValue = ComputeBestMoveMinimaxAB(depth - 1,
                    board,
                    GameEngine.GetMovesByColor(PieceColorsUtils.GetOppositePieceColor(pieceColors), board),
                    alpha,
                    beta,
                    gameStatus.NextPlayer.Equals(this.PieceColors),
                    gameStatus.NextPlayer
                    ).Value;
                // revert board's state
                GameEngine.UndoMove(move, board, pieceColors);
                GameEngine.UndoCaptures(board, gameStatus.CapturedPieces);
                // two different cases if we're simulating our turn or the opponent's
                if (isMaximizing)
                {
                    // if an endgame situation is reached, force the pruning
                    // depth difference is used in order to choose the move that ends the game asap
                    if (gameStatus.IsGameOver)
                    {
                        switch (gameStatus.Status)
                        {
                            case Status.LOSS:
                                moveValue += PieceValues.LossValue / (DefaultValues.MINIMAX_DEPTH - depth + 1);
                                break;
                            case Status.WIN:
                                moveValue += PieceValues.WinValue / (DefaultValues.MINIMAX_DEPTH - depth + 1);
                                break;
                        }
                        MovesLogger.LogEvent(pieceColors, gameStatus.Status, isMaximizing);
                    }
                    if (moveValue > bestMoveValue.Value)
                    {
                        bestMoveValue.Value = moveValue;
                        bestMoveValue.Move = move;
                    }
                    // compute the alpha
                    alpha = Math.Max(alpha, bestMoveValue.Value);
                }
                else
                {
                    // if an endgame situation is reached, force the pruning
                    // depth difference is used in order to choose the move that ends the game asap
                    if (gameStatus.IsGameOver)// && depth == DefaultValues.MINIMAX_DEPTH)
                    {
                        switch (gameStatus.Status)
                        {
                            case Status.LOSS:
                                moveValue += PieceValues.WinValue / (DefaultValues.MINIMAX_DEPTH - depth + 1);
                                break;
                            case Status.WIN:
                                moveValue += PieceValues.LossValue / (DefaultValues.MINIMAX_DEPTH - depth + 1);
                                break;
                        }
                        MovesLogger.LogEvent(pieceColors, gameStatus.Status, isMaximizing);
                    }
                    if (moveValue < bestMoveValue.Value)
                    {
                        bestMoveValue.Value = moveValue;
                        bestMoveValue.Move = move;
                    }
                    // compute the beta
                    beta = Math.Min(beta, bestMoveValue.Value);
                }
                MovesLogger.LogMove(this.PieceColors, depth, bestMoveValue.Move, bestMoveValue.Value, isMaximizing, move, moveValue);
                if (beta <= alpha)
                {
                    MovesLogger.LogPruning(alpha, beta);
                    break;
                }
                // force to pick a move if none is chosen
                if (bestMoveValue.Move is null)
                {
                    bestMoveValue.Move = moves[0];
                }
            }
            return bestMoveValue;
        }
        private void OrderMoves(List<Move> moves, Board board)
        {
            HashSet<Move> orderedMoves = new HashSet<Move>();
            //orderedMoves.UnionWith(MoveUtils.GetCapturingMoves(moves, board, this.RuleType));
            //orderedMoves.UnionWith(MoveUtils.GetEscapingMoves(moves, board, this.RuleType));
            orderedMoves.UnionWith(moves);
            moves.Clear();
            moves.AddRange(orderedMoves.ToList());
        }
    }
}
