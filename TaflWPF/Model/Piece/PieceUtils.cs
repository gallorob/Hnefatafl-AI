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
            else if (piece is Defender)
            {
                return PieceType.DEFENDER;
            }
            else return PieceType.ATTACKER;
        }
    }
}
