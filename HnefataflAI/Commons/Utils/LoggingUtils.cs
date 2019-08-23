using HnefataflAI.Games.Boards;
using HnefataflAI.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HnefataflAI.Commons.Utils
{
    public static class LoggingUtils
    {
        public static String LogBoard(Board board)
        {
            return String.Format("{0}",
                board.ToString());
        }
        public static String LogMove(int turn, PieceColors pieceColor, Move move)
        {
            return String.Format("[{0}] - {1} moves {2} from {3} to {4}.",
                turn,
                PieceColorsUtils.GetRoleFromPieceColor(pieceColor),
                "",//move.Piece.ToString(),
                move.From,
                move.To);
        }
        public static String LogPiecesCaptures(HashSet<IPiece> capturedPieces)
        {    
            return String.Format("Captures {0}", String.Join(", ", capturedPieces.Select(capturedPiece => capturedPiece.ToString()).ToArray()));
        }
        public static String LogDuration(TimeSpan timeSpan)
        {
            return String.Format("Game lasted {0:00}:{1:00}:{2:00}.{3:00}",
                timeSpan.Hours,
                timeSpan.Minutes,
                timeSpan.Seconds,
                timeSpan.Milliseconds / 10);
        }
        public static String LogCyningstanStyle(Move move, HashSet<IPiece> capturedPieces, bool isSuicidal, bool isWinning, bool isGameOver)
        {
            string toLog = String.Format("{0}-{1}",
                move.From,
                move.To);
            if (capturedPieces.Count > 0)
            {
                toLog += String.Format("x{0}", String.Join("/", capturedPieces.Select(capturedPiece => capturedPiece.Position.ToString()).ToArray()));
            }
            if (isSuicidal)
            {
                toLog += "--";
            }
            else if (isWinning)
            {
                toLog += "+";
            }
            else if (isGameOver)
            {
                toLog += "++";
            }
            return toLog;
        }
    }
}
