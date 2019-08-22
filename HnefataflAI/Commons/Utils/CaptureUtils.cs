using HnefataflAI.Commons.Converter;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Defaults;
using HnefataflAI.Games.Boards;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HnefataflAI.Commons.Utils
{
    public static class CaptureUtils
    {
        /// <summary>
        /// Checks if a piece is under threat in a given direction.
        /// A piece is under threat if it's next to an opponent piece, a corner tile (if in the rule), a throne tile (if in the rule) or in a shieldwall (if in the rule)
        /// </summary>
        /// <param name="piece">The piece to check if under threat</param>
        /// <param name="board">The board</param>
        /// <param name="direction">The direction to check</param>
        /// <param name="isCornerHostileToPieces">If the corner tile is hostile to pieces</param>
        /// <param name="isThroneHostileToPieces">If the throne is hostile to pieces</param>
        /// <param name="isShieldWallAllowed">If the variant allows shieldwall captures</param>
        /// <param name="isKingArmed">If the king is armed OR can take part in an active capture</param>
        /// <param name="IsEdgeCaptureAllowed">If the edge counts as enemy tile for capturing the king</param>
        /// <returns>If the piece is under threat of capture</returns>
        static bool IsPieceUnderThreat(IPiece piece, Board board, Directions direction, CaptureRuleSet captureRuleSet)
        {
            if (BoardUtils.IsPositionMoveValid(piece.Position, direction, board))
            {
                // piece is inside the board
                Position adjacentPosition = piece.Position.MoveTo(direction);
                IPiece adjacentPiece = board.At(adjacentPosition);
                if (adjacentPiece != null)
                {
                    // if the adjacentPiece is a king, piece is threatened if the king is armed and the piece is not a defender
                    if (adjacentPiece is King)
                    {
                        return captureRuleSet.isKingArmed && !piece.PieceColors.Equals(adjacentPiece.PieceColors);
                    }
                    // otherwise the piece is threatened if the adjacentPiece is the opponent's
                    else
                    {
                        return !piece.PieceColors.Equals(adjacentPiece.PieceColors);
                    }
                }
                // if piece is null, it could still be either the throne or the corner tiles
                if (piece is King)
                {
                    return (captureRuleSet.isThroneHostileToKing && BoardUtils.IsOnThrone(adjacentPosition, board))
                        ||
                        (captureRuleSet.isCornerHostileToKing && BoardUtils.IsOnCorner(adjacentPosition, board));
                }
                else return (captureRuleSet.isThroneHostileToPawns && BoardUtils.IsOnThrone(adjacentPosition, board))
                        ||
                        (captureRuleSet.isCornerHostileToPawns && BoardUtils.IsOnCorner(adjacentPosition, board));
            }
            // if new position is not valid, the piece is on the edge of the board
            else
            {
                if (piece is King)
                {
                    // if the piece is king, it is threatened if it's next to the edge and edge capture is allowed
                    return captureRuleSet.isEdgeHostileToKing;
                }
                else
                {
                    // any other piece is threatened if they're in a shieldwall
                    return captureRuleSet.isEdgeHostileToPawns;
                }
            }
        }

        /// <summary>
        /// Checks if a piece can be captured by the opponent in one move.
        /// </summary>
        /// <param name="piece">The piece to check if can be captured</param>
        /// <param name="board">The board</param>
        /// <param name="direction">The direction in which to check</param>
        /// <returns>If the piece can be captured by the opponent</returns>
        static bool CanPieceBeCaptured(IPiece piece, Board board, Directions direction, CaptureRuleSet captureRuleSet)
        {
            bool underCapture = false;
            if (BoardUtils.IsPositionMoveValid(piece.Position, direction, board))
            {
                // checking in the board
                Position adjacentPosition = piece.Position.MoveTo(direction);
                IPiece adjacentPiece = board.At(adjacentPosition);
                // only check if other piece doesn't exist
                if (adjacentPiece == null)
                {
                    foreach (Directions checkingDirection in PositionUtils.GetClockWiseDirections())
                    {
                        // get the first piece in range
                        IPiece checkingPiece = BoardUtils.GetFirstPieceFromPosition(board, checkingDirection, adjacentPosition);
                        if (checkingPiece != null && !piece.PieceColors.Equals(checkingPiece.PieceColors))
                        {
                            bool reaches = DoesPieceReachPosition(checkingPiece, adjacentPosition, board, captureRuleSet);
                            if (checkingPiece is King)
                            {
                                // a piece can be captured by the king only if it's armed
                                underCapture |= captureRuleSet.isKingArmed && reaches;
                            }
                            else
                            {
                                underCapture |= reaches;
                            }
                        }
                        // quick exit from foreach loop
                        if (underCapture)
                        {
                            break;
                        }
                    }
                }
            }
            return underCapture;
        }

        /// <summary>
        /// Check if a piece is threatened
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="board"></param>
        /// <param name="isCornerHostileToPieces"></param>
        /// <param name="isThroneHostileToPieces"></param>
        /// <param name="isShieldWallAllowed"></param>
        /// <param name="isKingArmed"></param>
        /// <returns></returns>
        public static bool IsPieceThreatened(IPiece piece, Board board, CaptureRuleSet captureRuleSet)
        {
            if (captureRuleSet.isShieldWallAllowed && BoardUtils.IsOnEdge(piece.Position, board) && ShieldWallUtils.CheckAhead(piece, board, PositionUtils.GetEdgeDirection(piece.Position, board), captureRuleSet))
            {
                Directions edge = PositionUtils.GetEdgeDirection(piece.Position, board);
                List<IPiece> bracketingPieces = IsInShieldWall(piece, board, edge, captureRuleSet);
                return ShieldWallUtils.IsShieldWallThreatened(bracketingPieces, piece, board, edge, captureRuleSet);
            }
            bool[] immediateThreat = new bool[4];
            bool[] nextMoveThreat = new bool[4];
            List<Directions> directions = PositionUtils.GetClockWiseDirections();
            for (int i = 0; i < directions.Count; i++)
            {
                bool threatened = IsPieceUnderThreat(piece, board, directions[i], captureRuleSet);
                if (threatened)
                {
                    immediateThreat[i] = threatened;
                    Directions oppositeDirection = PositionUtils.GetOppositeDirection(directions[i]);
                    if (BoardUtils.IsPositionMoveValid(piece.Position, oppositeDirection, board))
                    {
                        Position adjacentPosition = piece.Position.MoveTo(oppositeDirection);
                        IPiece adjacentPiece = board.At(adjacentPosition);
                        if (adjacentPiece == null)
                        {
                            nextMoveThreat[GetOppositeIndex(i)] = CanPieceBeCaptured(piece, board, oppositeDirection, captureRuleSet);
                        }
                    }
                }
            }
            if (piece is King)
            {
                return IsThreatened(immediateThreat, nextMoveThreat, GetNumberOfPiecesForKing(piece.Position, board, captureRuleSet));
            }
            else
            {
                return IsThreatened(immediateThreat, nextMoveThreat, captureRuleSet.piecesForPawn);
            }
        }    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="board"></param>
        /// <param name="isCornerHostileToPieces"></param>
        /// <param name="isThroneHostileToPieces"></param>
        /// <param name="isShieldWallAllowed"></param>
        /// <param name="isKingArmed"></param>
        /// <param name="IsEdgeCaptureAllowed"></param>
        /// <returns></returns>
        public static bool IsPieceCaptured(IPiece checkingPiece, IPiece movedPiece, Board board, CaptureRuleSet captureRuleSet)
        {
            if (captureRuleSet.isShieldWallAllowed && BoardUtils.IsOnEdge(checkingPiece.Position, board) && ShieldWallUtils.CheckAhead(checkingPiece, board, PositionUtils.GetEdgeDirection(checkingPiece.Position, board), captureRuleSet))
            {
                List<IPiece> bracketingPieces = IsShieldWallComplete(checkingPiece, movedPiece, board, PositionUtils.GetEdgeDirection(checkingPiece.Position, board), captureRuleSet);
                return bracketingPieces.Contains(movedPiece) && !bracketingPieces.Contains(null) && bracketingPieces[0].PieceColors.Equals(movedPiece.PieceColors) && bracketingPieces[1].PieceColors.Equals(movedPiece.PieceColors);
            }
            bool[] sides = new bool[4];
            bool[] moved = new bool[4];
            int i = 0;
            foreach (Directions direction in PositionUtils.GetClockWiseDirections())
            {
                sides[i] = IsPieceUnderThreat(checkingPiece, board, direction, captureRuleSet);
                // keep memory of where the moved piece is
                if (BoardUtils.IsPositionMoveValid(checkingPiece.Position, direction, board))
                {
                    Position adjacentPosition = checkingPiece.Position.MoveTo(direction);
                    IPiece adjacentPiece = board.At(adjacentPosition);
                    if (adjacentPiece != null && adjacentPiece.Equals(movedPiece))
                    {
                        moved[i] = true;
                    }
                }

                i++;
            }
            if (checkingPiece is King)
            {
                return IsCaptured(sides, moved, 4);
            }
            else
            {
                return IsCaptured(sides, moved, 2);
            }
        }

        #region Shieldwall routines

        public static List<IPiece> IsInShieldWall(IPiece piece, Board board, Directions edgeDirection, CaptureRuleSet captureRuleSet)
        {
            return ShieldWallUtils.GetShieldWallBrackets(piece, piece, board, edgeDirection, captureRuleSet);
        }

        /// <summary>
        /// Check if the shieldwall is complete
        /// </summary>
        /// <param name="pivot">The pivot piece</param>
        /// <param name="board">The board</param>
        /// <returns>If the shieldwall is complete</returns>
        public static List<IPiece> IsShieldWallComplete(IPiece pivot, IPiece movedPiece, Board board, Directions edgeDirection, CaptureRuleSet captureRuleSet)
        {
            return ShieldWallUtils.GetShieldWallBrackets(pivot, movedPiece, board, edgeDirection, captureRuleSet);
        }

        #endregion

        /// <summary>
        /// Check if a piece is captured
        /// </summary>
        /// <param name="sides">Sides (true -> enemy, false -> empty tile)</param>
        /// <param name="moved"></param>
        /// <param name="n">The number of sides needed to be blocked</param>
        /// <returns>If a piece is captured</returns>
        static bool IsCaptured(bool[] sides, bool[] moved, int n)
        {
            int side = ArrayToSingleValueConverter.Convert(sides, moved);
            if (DefaultValues.CAPTURES_DICT.ContainsKey(n))
            {
                foreach (int num in DefaultValues.CAPTURES_DICT[n])
                {
                    if ((side & num) == num)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        static bool IsThreatened(bool[] immediateThreat, bool[] nextMoveThreat, int n)
        {
            int effectiveValue = ArrayToSingleValueConverter.Convert(immediateThreat, nextMoveThreat);
            if (DefaultValues.THREATS_DICT.ContainsKey(n))
            {
                foreach (int num in DefaultValues.THREATS_DICT[n])
                {
                    if ((effectiveValue & num) == num)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Get the opposite index for the cross checking (0-3 only)
        /// </summary>
        /// <remarks>
        /// 0 (00) -> 2 (10)
        /// 1 (01) -> 3 (11)
        /// 2 (10) -> 0 (10)
        /// 3 (11) -> 1 (01)
        /// </remarks>
        /// <param name="idx">The index</param>
        /// <returns></returns>
        static int GetOppositeIndex(int idx)
        {
            if (idx > 3)
            {
                throw new CustomGenericException(typeof(CaptureUtils).Name, MethodBase.GetCurrentMethod().Name, string.Format("Unexpected index reached: {0}", idx));
            }
            return (((idx >> 1) ^ 1) << 1) | (idx & 1);
        }
        private static int GetNumberOfPiecesForKing(Position kingPosition, Board board, CaptureRuleSet captureRuleSet)
        {
            // on throne
            if (BoardUtils.IsOnThrone(kingPosition, board))
            {
                return captureRuleSet.piecesForKingOnThrone;
            }
            // next to...
            foreach (Directions direction in PositionUtils.GetClockWiseDirections())
            {
                Position newPosition = kingPosition.MoveTo(direction);
                // ... throne
                if (BoardUtils.IsOnThrone(newPosition, board))
                {
                    return captureRuleSet.piecesForKingNextToThrone;
                }
                // ... corner
                if (BoardUtils.IsOnCorner(newPosition, board))
                {
                    return captureRuleSet.piecesForKingNextToCorner;
                }
            }
            // on edge
            if (BoardUtils.IsOnEdge(kingPosition, board))
            {
                return captureRuleSet.piecesForKingOnEdge;
            }
            // else, on the board
            return captureRuleSet.piecesForKingOnBoard;
        }

        public static bool DoesPieceReachPosition(IPiece piece, Position position, Board board, CaptureRuleSet captureRuleSet)
        {
            Directions direction = PositionUtils.GetPositionsDirection(piece.Position, position);
            List<Move> moves = MoveUtils.GetMovesForPiece(piece, board, direction, captureRuleSet.moveRuleSet);
            Move validMove = moves.Where(move => move.To.Equals(position)).FirstOrDefault();
            return validMove != null;
        }
    }
}
