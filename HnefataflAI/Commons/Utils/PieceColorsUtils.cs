using System;

namespace HnefataflAI.Commons.Utils
{
    public static class PieceColorsUtils
    {
        public static PieceColors GetOppositePieceColor(PieceColors pieceColors)
        {
            switch (pieceColors)
            {
                case PieceColors.BLACK:
                    return PieceColors.WHITE;
                case PieceColors.WHITE:
                    return PieceColors.BLACK;
                default:
                    throw new ArgumentException("Invalid piece color");
            }
        }
        public static string GetRoleFromPieceColor(PieceColors pieceColors)
        {
            switch (pieceColors)
            {
                case PieceColors.BLACK:
                    return "Attacker";
                case PieceColors.WHITE:
                    return "Defender"; 
                default:
                    throw new ArgumentException("Invalid piece color");
            }
        }
    }
}
