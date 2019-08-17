using HnefataflAI.Commons.Exceptions;
using System;
using System.Reflection;

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
                    throw new CustomGenericException(typeof(PieceColorsUtils).Name, MethodBase.GetCurrentMethod().Name, string.Format("Invalid piece color: {0}", pieceColors));
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
                    throw new CustomGenericException(typeof(PieceColorsUtils).Name, MethodBase.GetCurrentMethod().Name, string.Format("Invalid piece color: {0}", pieceColors));
            }
        }
    }
}
