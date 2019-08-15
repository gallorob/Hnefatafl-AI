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
    class TaflBotMinimaxAB : ITaflBot
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
        /// The internal BoardEvaluator
        /// </summary>
        private readonly BoardEvaluator MovesEvaluator = new BoardEvaluator();
        /// <summary>
        /// The list of moves played by the bot
        /// </summary>
        private readonly List<Move> BotMoves = new List<Move>();
        public String PlayerName { get; private set; }
        public List<String> AdditionalInfo { get; private set; }
        /// <summary>
        /// Constructor for the TaflBotMinimaxAB
        /// </summary>
        /// <param name="pieceColors">The piece color</param>
        public TaflBotMinimaxAB(PieceColors pieceColors, RuleTypes ruleType)
        {
            this.PieceColors = pieceColors;
            this.RuleType = ruleType;
            //temporary
            this.PlayerName = "Minyab";
            this.AdditionalInfo = new List<String> { "A standard minimax with alpha-beta pruning player bot" };
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
                IGameEngine gameEngine = new GameEngineImpl(this.RuleType);
                // update board with the move
                gameEngine.ApplyMove(move, board, pieceColors);
                // check if it reached an endgame point
                GameStatus gameStatus = gameEngine.GetGameStatus(move.Piece, board);
                if (!gameStatus.IsGameOver)
                {
                    gameStatus.IsGameOver = MoveUtils.IsRepeatedMove(this.BotMoves, move, RuleUtils.GetRule(this.RuleType));
                    gameStatus.Status = Status.LOSS;
                }
                // recursive call for the move's sub-tree
                int moveValue = ComputeBestMoveMinimaxAB(depth - 1,
                    board,
                    new GameEngineImpl(this.RuleType).GetMovesByColor(PieceColorsUtils.GetOppositePieceColor(pieceColors), board),
                    alpha,
                    beta,
                    !isMaximizing,
                    PieceColorsUtils.GetOppositePieceColor(pieceColors)
                    ).Value;
                // revert board's state
                gameEngine.UndoMove(move, board, pieceColors);
                gameEngine.UndoCaptures(board);
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
