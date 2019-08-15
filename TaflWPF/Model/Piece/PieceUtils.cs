using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;

namespace TaflWPF.Model.Piece
{
    /// <summary>
    /// Helper class for the pieces
    /// </summary>
    public class PieceUtils
    {
        /// <summary>
        /// Get the piece type given a piece
        /// </summary>
        /// <param name="piece">The piece</param>
        /// <returns>The piece type</returns>
        public static PieceType GetPieceType(IPiece piece)
        {
            if (piece is King)
            {
                return PieceType.KING;
            }
            else if (piece is EliteGuard)
            {
                return PieceType.ELITEGUARD;
            }
            else if (piece is Defender)
            {
                return PieceType.DEFENDER;
            }
            else if (piece is Commander)
            {
                return PieceType.COMMANDER;
            }
            else if (piece is Attacker)
            {
                return PieceType.ATTACKER;
            }
            else return PieceType.EMPTY;
        }
    }
}
