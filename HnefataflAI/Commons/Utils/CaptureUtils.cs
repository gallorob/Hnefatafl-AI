using HnefataflAI.Commons.Positions;
using HnefataflAI.Games.Boards;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System;

namespace HnefataflAI.Commons.Utils
{
    public static class CaptureUtils
    {
        /// <summary>
        /// Checks if a piece is threatened.
        /// A piece is threatened if it's next to an opponent piece, a corner tile (if in the rule), a throne tile (if in the rule) or in a shieldwall (if in the rule)
        /// </summary>
        /// <param name="piece">The piece to check if threatened</param>
        /// <param name="board">The board</param>
        /// <param name="isCornerHostileToPieces">If the corner tile is hostile to pieces</param>
        /// <param name="isThroneHostileToPieces">If the throne is hostile to pieces</param>
        /// <param name="isShieldWallAllowed">If the variant allows shieldwall captures</param>
        /// <param name="isKingArmed">If the king is armed OR can take part in an active capture</param>
        /// <returns>If the piece is threatened of capture</returns>
        public static bool IsPieceThreatened(IPiece piece, Board board, bool isCornerHostileToPieces, bool isThroneHostileToPieces, bool isShieldWallAllowed, bool isKingArmed)
        {
            bool threatened = false;
            foreach (Directions direction in Enum.GetValues(typeof(Directions)))
            {
                if (BoardUtils.IsPositionUpdateValid(piece.Position, direction, board))
                {
                    // piece is inside the board
                    Position adjacentPosition = piece.Position.MoveTo(direction);
                    IPiece adjacentPiece = board.At(adjacentPosition);
                    if (!(adjacentPiece is null))
                    {
                        // if the adjacentPiece is a king, piece is threatened if the king is armed and the piece is not a defender
                        if (adjacentPiece is King)
                        {
                            threatened |= isKingArmed && !piece.PieceColors.Equals(adjacentPiece.PieceColors);
                        }
                        // otherwise the piece is threatened if the adjacentPiece is the opponent's
                        else
                        {
                            threatened |= !piece.PieceColors.Equals(adjacentPiece.PieceColors);
                        }
                    }
                    // if piece is null, it could still be either the throne or the corner tiles
                    else threatened |= (isThroneHostileToPieces && BoardUtils.IsOnThrone(adjacentPosition, board))
                            ||
                            (isCornerHostileToPieces && BoardUtils.IsOnBoardCorner(adjacentPosition, board));                            
                }
                // if new position is not valid, the piece is on the edge of the board
                else
                {
                    threatened |= isShieldWallAllowed && IsInShieldWall(piece, board, isCornerHostileToPieces);
                }
                // exit loop if it's already threatened
                if (threatened)
                {
                    break;
                }
            }
            return threatened;
        }

        private static bool IsInShieldWall(IPiece piece, Board board, bool isCornerHostileToPieces)
        {
            // TODO: Implement the method
            return false;
        }
    }
}
