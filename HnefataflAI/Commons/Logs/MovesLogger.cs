using HnefataflAI.Commons.Utils;
using HnefataflAI.Defaults;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.GameState;
using System;
using System.Collections.Generic;
using System.IO;

namespace HnefataflAI.Commons.Logs
{
    public class MovesLogger
    {
        public static readonly string FilePath = String.Format(@"./moves_{0}.log", DateTime.Now.ToString("MMddyyyyHHmm"));
        public static void LogMove(PieceColors player, int depth, Move bestMove, int bestMoveValue, bool isMaximizing, Move move, int moveValue)
        {
            if (DefaultValues.LOG_MOVES_EVAL)
            {
                string toLog = String.Format("{6} - {0} at depth {1} - Best move: {2} ({3}) - Evaluated move: {4} ({5})",
                    isMaximizing ? "Max." : "Min",
                    depth,
                    bestMove is null ? "null" : bestMove.ToString(),
                    bestMoveValue,
                    move.ToString(),
                    moveValue,
                    player.ToString());
                Log(toLog);
            }
        }
        public static void LogMove(PieceColors player, int depth, Move bestMove, int bestMoveValue, bool isMaximizing, Move move, int moveValue, Board board)
        {
            if (DefaultValues.LOG_MOVES_EVAL)
            {
                string toLog = String.Format("{7}\n{6} - {0} at depth {1} - Best move: {2} ({3}) - Evaluated move: {4} ({5})",
                    isMaximizing ? "Max." : "Min",
                    depth,
                    bestMove is null ? "null" : bestMove.ToString(),
                    bestMoveValue,
                    move.ToString(),
                    moveValue,
                    player.ToString(),
                    board.ToString());
                Log(toLog);
            }
        }
        public static void LogMove(Move move, int value)
        {
            if (DefaultValues.LOG_MOVES)
            {
                string toLog = String.Format("Move {0} with value {1}.",
                    move.ToString(),
                    value);
                Log(toLog);
            }
        }
        public static void LogEvent(PieceColors pieceColors, Status status, bool isMaximizing)
        {
            if (DefaultValues.LOG_MOVES_EVAL)
            {
                string toLog = String.Format("{0}; {1} for {2}",
                    isMaximizing ? "Max." : "Min.",
                    status.Equals(Status.LOSS) ? "loss" : "win",
                    PieceColorsUtils.GetRoleFromPieceColor(pieceColors));
                Log(toLog);
            }
        }
        public static void LogPruning(int alpha, int beta)
        {
            if (DefaultValues.LOG_MOVES_EVAL)
            {
                string toLog = String.Format("Prune: Alpha {0}; Beta {1}",
                    alpha,
                    beta);
                Log(toLog);
            }
        }
        public static void Log(string message)
        {
            using (StreamWriter streamwriter = new StreamWriter(FilePath, true))
            {
                streamwriter.WriteLine(message);
                streamwriter.Close();
            }
        }
    }
}
