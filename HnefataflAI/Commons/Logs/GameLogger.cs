using HnefataflAI.Commons.Utils;
using HnefataflAI.Defaults;
using HnefataflAI.Games;
using HnefataflAI.Pieces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HnefataflAI.Commons.Logs
{
    class GameLogger
    {
        public static readonly string FilePath = String.Format(@"./game_{0}.log", DateTime.Now.ToString("MMddyyyyHHmm"));
        internal static void LogBoard(Board board)
        {
            if (DefaultValues.LOG_GAME && DefaultValues.LOG_BOARD)
            {
                string toLog = String.Format("{0}\n",
                    board.ToString());
                Log(toLog);
            }
        }
        public static void LogMove(int turn, PieceColors pieceColor, Move move)
        {
            if (DefaultValues.LOG_GAME)
            {
                string toLog;
                if (DefaultValues.LOG_CYNINGSTAN_STYLE)
                {
                    toLog = String.Format("\n{0}-{1}",
                        move.From,
                        move.To);
                }
                else
                {
                    toLog = String.Format("\n[{0}] - {1} moves {2} from {3} to {4}.\n",
                        turn,
                        PieceColorsUtils.GetRoleFromPieceColor(pieceColor),
                        move.Piece.ToString(),
                        move.From,
                        move.To);
                }
                Log(toLog);
            }
        }
        public static void LogPiecesCaptures(List<IPiece> capturedPieces)
        {
            if (DefaultValues.LOG_GAME && capturedPieces.Count > 0)
            {
                String toLog = " captures ";
                String separator = ", ";
                if (DefaultValues.LOG_CYNINGSTAN_STYLE)
                {
                    toLog = "x";
                    separator = "/";
                }
                toLog += String.Join(separator, capturedPieces.Select(capturedPiece => GetCapturedPieceDescription(capturedPiece)).ToArray());
                Log(toLog);
            }
        }
        private static string GetCapturedPieceDescription(IPiece captured)
        {
            return DefaultValues.LOG_CYNINGSTAN_STYLE ? captured.Position.ToString() : captured.ToString();
        }
        internal static void LogDuration(TimeSpan timeSpan)
        {
            if (DefaultValues.LOG_GAME)
            {
                string toLog = String.Format("Game lasted {0:00}:{1:00}:{2:00}.{3:00}\n",
                    timeSpan.Hours,
                    timeSpan.Minutes,
                    timeSpan.Seconds,
                    timeSpan.Milliseconds / 10);
                Log(toLog);
            }
        }
        public static void Log(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(FilePath, true))
            {
                streamWriter.Write(message);
                streamWriter.Close();
            }
        }
    }
}
