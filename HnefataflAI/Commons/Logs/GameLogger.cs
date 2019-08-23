using HnefataflAI.Commons.Utils;
using HnefataflAI.Defaults;
using HnefataflAI.Games.Boards;
using HnefataflAI.Pieces;
using System;
using System.Collections.Generic;
using System.IO;

namespace HnefataflAI.Commons.Logs
{
    public class GameLogger
    {
        public static readonly string FilePath = String.Format(@"./game_{0}.log", DateTime.Now.ToString("MMddyyyyHHmm"));
        internal static void LogBoard(Board board)
        {
            if (DefaultValues.LOG_GAME && DefaultValues.LOG_BOARD)
            {
                string toLog = LoggingUtils.LogBoard(board);
                Log(toLog + "\n");
            }
        }
        public static void LogMove(int turn, PieceColors pieceColor, Move move)
        {
            if (DefaultValues.LOG_GAME)
            {
                string toLog = LoggingUtils.LogMove(turn, pieceColor, move);
                Log(toLog + "\n");
            }
        }
        public static void LogPiecesCaptures(HashSet<IPiece> capturedPieces)
        {
            if (DefaultValues.LOG_GAME && capturedPieces.Count > 0)
            {
                String toLog = LoggingUtils.LogPiecesCaptures(capturedPieces);
                Log(toLog);
            }
        }
        public static void LogDuration(TimeSpan timeSpan)
        {
            if (DefaultValues.LOG_GAME)
            {
                string toLog = LoggingUtils.LogDuration(timeSpan);
                Log(toLog + "\n");
            }
        }
        public static void LogCyningstanStyle(Move move, HashSet<IPiece> capturedPieces, bool isSuicidal, bool isWinning, bool isGameOver)
        {
            if (DefaultValues.LOG_CYNINGSTAN_STYLE)
            {
                string toLog = LoggingUtils.LogCyningstanStyle(move, capturedPieces, isSuicidal, isWinning, isGameOver);
                Log("\n" + toLog);
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
