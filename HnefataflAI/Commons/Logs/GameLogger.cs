using HnefataflAI.Commons.Utils;
using HnefataflAI.Defaults;
using HnefataflAI.Pieces;
using System;
using System.IO;

namespace HnefataflAI.Commons.Logs
{
    class GameLogger
    {
        public static readonly string FilePath = String.Format(@"./game_{0}.log", DateTime.Now.ToString("MMddyyyyhhmm"));
        public static void LogMove(int turn, PieceColors pieceColor, Move move)
        {
            if (DefaultValues.LOG_GAME)
            {
                string toLog = String.Format("[{0}] - {1} moves {2} from {3} to {4}.",
                    turn,
                    PieceColorsUtils.GetRoleFromPieceColor(pieceColor),
                    move.Piece.ToString(),
                    move.From,
                    move.To);
                Log(toLog);
            }
        }
        public static void LogPieceCapture(PieceColors pieceColor, IPiece captured)
        {
            if (DefaultValues.LOG_GAME)
            {
                string toLog = String.Format("{0} captures piece {1}.",
                    PieceColorsUtils.GetRoleFromPieceColor(pieceColor),
                    captured.ToString());
                Log(toLog);
            }
        }
        internal static void LogDuration(TimeSpan timeSpan)
        {
            if (DefaultValues.LOG_GAME)
            {
                string toLog = String.Format("Game lasted {0:00}:{1:00}:{2:00}.{3:00}",
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
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
        }
    }
}
