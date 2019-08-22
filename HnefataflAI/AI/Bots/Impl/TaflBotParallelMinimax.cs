using HnefataflAI.Commons;
using HnefataflAI.Commons.Logs;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Defaults;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Engine;
using HnefataflAI.Games.Engine.Impl;
using HnefataflAI.Games.GameState;
using HnefataflAI.Games.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HnefataflAI.AI.Bots.Impl
{
    public class TaflBotParallelMinimax : ATaflBot
    {
        /// <summary>
        /// The internal BoardEvaluator
        /// </summary>
        private readonly BoardEvaluator MovesEvaluator = new BoardEvaluator();
        /// <summary>
        /// The list of moves played by the bot
        /// </summary>
        private readonly List<Move> BotMoves = new List<Move>();


        //private readonly IGameEngine GameEngine;

        /// <summary>
        /// Constructor for the TaflBotMinimax
        /// </summary>
        /// <param name="pieceColors">The piece color</param>
        public TaflBotParallelMinimax(PieceColors pieceColors, RuleTypes ruleType)
        {
            this.PieceColors = pieceColors;
            this.Rule = RuleUtils.GetRule(ruleType);
            this.BotType = BotTypes.MINIMAX;
            //this.GameEngine = new GameEngineImpl(ruleType);
            //temporary
            this.PlayerName = "Miny";
            this.AdditionalInfo = new List<String> { "A standard minimax player bot" };
        }
        /// <summary>
        /// Get the best move as a user input
        /// </summary>
        /// <param name="board">The board</param>
        /// <param name="moves">The list of all possible moves</param>
        /// <returns>The best move as a user input</returns>
        public override string[] GetMove(Board board)
        {
            IGameEngine GameEngine = new GameEngineImpl(Rule.RuleType);
            List<Move> moves = GameEngine.GetMovesByColor(PieceColors, board);
            MovesLogger.Log(string.Format("Started decision making for {0}...", PieceColors));
            MoveValue bestMove = ComputeBestMoveMinimax(DefaultValues.MINIMAX_DEPTH, board, moves, true, this.PieceColors);
            MovesLogger.LogMove(bestMove.Move, bestMove.Value);
            this.BotMoves.Add(bestMove.Move);
            return MoveUtils.MoveAsInput(bestMove.Move);
        }
        /// <summary>
        /// Compute the best move using the basic Minimax algorithm
        /// </summary>
        /// <remarks>
        /// This version of the minimax algorithm is much faster than its vanilla version.
        /// The game is playable with this bot.
        /// </remarks>
        /// <param name="depth">The lookahead level</param>
        /// <param name="board">The current board state</param>
        /// <param name="moves">The list of all possible moves</param>
        /// <param name="isMaximizing">>If it's maximizing or minimizing</param>
        /// <returns>The best move in the current turn</returns>
        private MoveValue ComputeBestMoveMinimax(int depth, Board board, List<Move> moves, bool isMaximizing, PieceColors pieceColors)
        {
            IGameEngine GameEngine = new GameEngineImpl(Rule.RuleType);
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
            // randomize list so it's not always the same if no best move is found
            ListUtils.ShuffleList(moves);
            Parallel.ForEach(moves, (move, state) =>
            {
                Board newBoard = BoardUtils.DuplicateBoard(board);
                // update board with the move
                GameEngine.ApplyMove(move, newBoard, pieceColors, true);
                // check if it reached an endgame point
                GameStatus gameStatus = GameEngine.GetGameStatus(move, newBoard);
                if (!gameStatus.IsGameOver)
                {
                    gameStatus.IsGameOver = MoveUtils.IsRepeatedMove(this.BotMoves, move, this.Rule);
                    gameStatus.Status = Status.LOSS;
                }
                // recursive call for the move's sub-tree
                int moveValue = ComputeBestMoveMinimax(depth - 1,
                    newBoard,
                    GameEngine.GetMovesByColor(PieceColorsUtils.GetOppositePieceColor(pieceColors), newBoard),
                    gameStatus.NextPlayer.Equals(this.PieceColors),
                    gameStatus.NextPlayer
                    ).Value;
                // revert board's state
                GameEngine.UndoMove(move, newBoard, pieceColors, true);
                GameEngine.UndoCaptures(newBoard, gameStatus.CapturedPieces);
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
                                moveValue = PieceValues.LossValue / (DefaultValues.MINIMAX_DEPTH - depth + 1);
                                break;
                            case Status.WIN:
                                moveValue = PieceValues.WinValue / (DefaultValues.MINIMAX_DEPTH - depth + 1);
                                break;
                        }
                        MovesLogger.LogEvent(pieceColors, gameStatus.Status, isMaximizing);
                    }
                    if (moveValue > bestMoveValue.Value)
                    {
                        bestMoveValue.Value = moveValue;
                        bestMoveValue.Move = move;
                    }
                }
                else
                {
                    // if an endgame situation is reached, force the pruning
                    // depth difference is used in order to choose the move that ends the game asap
                    if (gameStatus.IsGameOver)
                    {
                        switch (gameStatus.Status)
                        {
                            case Status.LOSS:
                                moveValue = PieceValues.WinValue / (DefaultValues.MINIMAX_DEPTH - depth + 1);
                                break;
                            case Status.WIN:
                                moveValue = PieceValues.LossValue / (DefaultValues.MINIMAX_DEPTH - depth + 1);
                                break;
                        }
                        MovesLogger.LogEvent(pieceColors, gameStatus.Status, isMaximizing);
                    }
                    if (moveValue < bestMoveValue.Value)
                    {
                        bestMoveValue.Value = moveValue;
                        bestMoveValue.Move = move;
                    }
                }
                MovesLogger.LogMove(pieceColors, depth, bestMoveValue.Move, bestMoveValue.Value, isMaximizing, move, moveValue);
                // force to pick a move if none is chosen
                if (bestMoveValue.Move is null)
                {
                    bestMoveValue.Move = moves[0];
                }
            });
            //foreach (Move move in moves)
            //{
            //    // update board with the move
            //    GameEngine.ApplyMove(move, board, pieceColors);
            //    // check if it reached an endgame point
            //    GameStatus gameStatus = GameEngine.GetGameStatus(move.Piece, board);
            //    if (!gameStatus.IsGameOver)
            //    {
            //        gameStatus.IsGameOver = MoveUtils.IsRepeatedMove(this.BotMoves, move, this.Rule);
            //        gameStatus.Status = Status.LOSS;
            //    }
            //    // recursive call for the move's sub-tree
            //    int moveValue = ComputeBestMoveMinimax(depth - 1,
            //        board,
            //        GameEngine.GetMovesByColor(PieceColorsUtils.GetOppositePieceColor(pieceColors), board),
            //        gameStatus.NextPlayer.Equals(this.PieceColors),
            //        gameStatus.NextPlayer
            //        ).Value;
            //    // revert board's state
            //    GameEngine.UndoMove(move, board, pieceColors);
            //    GameEngine.UndoCaptures(board, gameStatus.CapturedPieces);
            //    // two different cases if we're simulating our turn or the opponent's
            //    if (isMaximizing)
            //    {
            //        // if an endgame situation is reached, force the pruning
            //        // depth difference is used in order to choose the move that ends the game asap
            //        if (gameStatus.IsGameOver)
            //        {
            //            switch (gameStatus.Status)
            //            {
            //                case Status.LOSS:
            //                    moveValue = PieceValues.LossValue / (DefaultValues.MINIMAX_DEPTH - depth + 1);
            //                    break;
            //                case Status.WIN:
            //                    moveValue = PieceValues.WinValue / (DefaultValues.MINIMAX_DEPTH - depth + 1);
            //                    break;
            //            }
            //            MovesLogger.LogEvent(pieceColors, gameStatus.Status, isMaximizing);
            //        }
            //        if (moveValue > bestMoveValue.Value)
            //        {
            //            bestMoveValue.Value = moveValue;
            //            bestMoveValue.Move = move;
            //        }
            //    }
            //    else
            //    {
            //        // if an endgame situation is reached, force the pruning
            //        // depth difference is used in order to choose the move that ends the game asap
            //        if (gameStatus.IsGameOver)
            //        {
            //            switch (gameStatus.Status)
            //            {
            //                case Status.LOSS:
            //                    moveValue = PieceValues.WinValue / (DefaultValues.MINIMAX_DEPTH - depth + 1);
            //                    break;
            //                case Status.WIN:
            //                    moveValue = PieceValues.LossValue / (DefaultValues.MINIMAX_DEPTH - depth + 1);
            //                    break;
            //            }
            //            MovesLogger.LogEvent(pieceColors, gameStatus.Status, isMaximizing);
            //        }
            //        if (moveValue < bestMoveValue.Value)
            //        {
            //            bestMoveValue.Value = moveValue;
            //            bestMoveValue.Move = move;
            //        }
            //    }
            //    MovesLogger.LogMove(pieceColors, depth, bestMoveValue.Move, bestMoveValue.Value, isMaximizing, move, moveValue);
            //    // force to pick a move if none is chosen
            //    if (bestMoveValue.Move is null)
            //    {
            //        bestMoveValue.Move = moves[0];
            //    }
            //}
            return bestMoveValue;
        }
    }
}
