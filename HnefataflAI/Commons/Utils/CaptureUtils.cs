using HnefataflAI.Commons.Positions;
using HnefataflAI.Defaults;
using HnefataflAI.Games.Boards;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System;
using System.Collections.Generic;

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
                        IPiece checkingPiece = BoardUtils.GetFirstPiece(board, checkingDirection, adjacentPosition);
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
            if (captureRuleSet.isShieldWallAllowed && BoardUtils.IsOnEdge(piece.Position, board) && CheckAhead(piece, board, PositionUtils.GetEdgeDirection(piece.Position, board), captureRuleSet))
            {
                Directions edge = PositionUtils.GetEdgeDirection(piece.Position, board);
                List<IPiece> bracketingPieces = IsInShieldWall(piece, board, edge, captureRuleSet);
                return IsShieldWallThreatened(bracketingPieces, piece, board, edge, captureRuleSet);
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

        private static bool IsShieldWallThreatened(List<IPiece> bracketingPieces, IPiece piece, Board board, Directions edgeDirection, CaptureRuleSet captureRuleSet)
        {
            if (!bracketingPieces.Contains(null))
            {
                return false;
            }
            else
            {
                bool underThreat = false;
                IPiece bracket = bracketingPieces[0] != null ? bracketingPieces[0] : bracketingPieces[1];
                if (bracket.PieceColors.Equals(piece.PieceColors))
                {
                    return false;
                }
                Position otherBracket = GetLastPositionBracket(bracket.Position, bracket.PieceColors, board, edgeDirection);
                foreach (Directions checkingDirection in PositionUtils.GetClockWiseDirections())
                {
                    // get the first piece in range
                    IPiece checkingPiece = BoardUtils.GetFirstPiece(board, checkingDirection, otherBracket);
                    if (checkingPiece != null && bracket.PieceColors.Equals(checkingPiece.PieceColors))
                    {
                        bool reaches = DoesPieceReachPosition(checkingPiece, otherBracket, board, captureRuleSet);
                        if (checkingPiece is King)
                        {
                            // a piece can be captured by the king only if it's armed
                            underThreat |= captureRuleSet.isKingArmed && reaches;
                        }
                        else
                        {
                            underThreat |= reaches;
                        }
                    }
                    // quick exit from foreach loop
                    if (underThreat)
                    {
                        break;
                    }
                }
                return underThreat;
            }
        }

        private static Position GetLastPositionBracket(Position bracketPosition, PieceColors bracketColor, Board board, Directions edgeDirection) {
            IPiece piece = board.At(bracketPosition.MoveTo(PositionUtils.GetClockWiseDirection(edgeDirection)));
            if (piece != null && piece.PieceColors.Equals(bracketColor))
            {
                // check ccw
                Directions moving = PositionUtils.GetCounterClockWiseDirection(edgeDirection);
                Position checking = bracketPosition.MoveTo(moving);
                while (board.At(checking) != null)
                {
                    checking = checking.MoveTo(moving);
                }
                return checking;
            }
            else
            {
                // check cw
                Directions moving = PositionUtils.GetClockWiseDirection(edgeDirection);
                Position checking = bracketPosition.MoveTo(moving);
                while (board.At(checking) != null)
                {
                    checking = checking.MoveTo(moving);
                }
                return checking;
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
            if (captureRuleSet.isShieldWallAllowed && BoardUtils.IsOnEdge(checkingPiece.Position, board) && CheckAhead(checkingPiece, board, PositionUtils.GetEdgeDirection(checkingPiece.Position, board), captureRuleSet))
            {
                List<IPiece> bracketingPieces = IsShieldWallComplete(checkingPiece, movedPiece, board, PositionUtils.GetEdgeDirection(checkingPiece.Position, board), captureRuleSet);
                return bracketingPieces.Contains(movedPiece) && !bracketingPieces.Contains(null) && bracketingPieces[0].PieceColors.Equals(movedPiece.PieceColors) && bracketingPieces[1].PieceColors.Equals(movedPiece.PieceColors);
            }
            bool[] sides = new bool[4];
            int i = 0;
            foreach (Directions direction in PositionUtils.GetClockWiseDirections())
            {
                sides[i] = IsPieceUnderThreat(checkingPiece, board, direction, captureRuleSet);
                i++;
            }
            if (checkingPiece is King)
            {
                return IsCaptured(sides, 4);
            }
            else
            {
                return IsCaptured(sides, 2);
            }
        }

        #region Shieldwall routines

        public static List<IPiece> IsInShieldWall(IPiece piece, Board board, Directions edgeDirection, CaptureRuleSet captureRuleSet)
        {
            List<IPiece> bracketingPiece = new List<IPiece>();
            bool facing = CheckAhead(piece, board, edgeDirection, captureRuleSet);
            IPiece clockWise = CheckDirection(piece, piece, board, edgeDirection, captureRuleSet, PositionUtils.GetClockWiseDirection);
            IPiece counterClockWise = CheckDirection(piece, piece, board, edgeDirection, captureRuleSet, PositionUtils.GetCounterClockWiseDirection);
            if (facing)
            {
                bracketingPiece.Add(clockWise);
                bracketingPiece.Add(counterClockWise);
            }
            return bracketingPiece;
        }

        private static bool CheckAhead(IPiece piece, Board board, Directions edgeDirection, CaptureRuleSet captureRuleSet)
        {
            bool facing = false;
            IPiece inFront = board.At(piece.Position.MoveTo(PositionUtils.GetOppositeDirection(edgeDirection)));
            if (inFront != null)
            {
                facing = !inFront.PieceColors.Equals(piece.PieceColors) && (((inFront is King) && captureRuleSet.isKingArmed) || !(piece is King));
            }
            return facing;
        }

        private static IPiece CheckDirection(IPiece piece, IPiece movedPiece, Board board, Directions edgeDirection, CaptureRuleSet captureRuleSet, Func<Directions, Directions> directionMethod)
        {
            bool reached = false;
            IPiece bracket = null;
            Position checkingPosition = piece.Position;
            while (!reached && BoardUtils.IsPositionMoveValid(checkingPosition, directionMethod.Invoke(edgeDirection), board))
            {
                Position newPosition = checkingPosition.MoveTo(directionMethod.Invoke(edgeDirection));
                IPiece checkingPiece = board.At(newPosition);
                if (checkingPiece != null)
                {
                    // reached the end of the line
                    if (!piece.PieceColors.Equals(checkingPiece.PieceColors))
                    {
                        if (checkingPiece is King)
                        {
                            if (captureRuleSet.isKingArmed)
                            {
                                bracket = checkingPiece;
                            }
                            else
                            {
                                bracket = null;
                            }
                        }
                        else
                        {
                            bracket = checkingPiece;
                        }
                        reached = true;
                    }
                    else
                    {
                        if(!CheckAhead(checkingPiece, board, edgeDirection, captureRuleSet))
                        {
                            reached = true;
                            bracket = checkingPiece;
                        }
                    }
                }
                else
                {
                    if (BoardUtils.IsOnCorner(newPosition, board))
                    {
                        if (captureRuleSet.isCornerHostileToPawns)
                        {
                            bracket = GetCornerPiece(movedPiece.PieceColors);
                        }
                    }
                    else
                    {
                        reached = true;
                        bracket = null;
                    }
                }
                // update position
                checkingPosition = newPosition;
            }
            return bracket;
        }

        private static IPiece GetCornerPiece(PieceColors pieceColors)
        {
            if (pieceColors.Equals(PieceColors.BLACK))
            {
                return new Attacker(DefaultValues.DefaultPosition);
            }
            else
            {
                return new Defender(DefaultValues.DefaultPosition);
            }
        }

        /// <summary>
        /// Check if the shieldwall is complete
        /// </summary>
        /// <param name="pivot">The pivot piece</param>
        /// <param name="board">The board</param>
        /// <returns>If the shieldwall is complete</returns>
        public static List<IPiece> IsShieldWallComplete(IPiece pivot, IPiece movedPiece, Board board, Directions edgeDirection, CaptureRuleSet captureRuleSet)
        {
            List<IPiece> bracketingPieces = new List<IPiece>();
            bool facing = CheckAhead(pivot, board, edgeDirection, captureRuleSet);
            IPiece clockWise = CheckDirection(pivot, movedPiece, board, edgeDirection, captureRuleSet, PositionUtils.GetClockWiseDirection);
            IPiece counterClockWise = CheckDirection(pivot, movedPiece, board, edgeDirection, captureRuleSet, PositionUtils.GetCounterClockWiseDirection);
            if (facing)
            {
                ListUtils.AddIfNotNull(clockWise, bracketingPieces);
                ListUtils.AddIfNotNull(counterClockWise, bracketingPieces);
            }
            return bracketingPieces;
        }

        #endregion

        /// <summary>
        /// Check if a piece is captured
        /// </summary>
        /// <param name="sides">Sides (true -> enemy, false -> empty tile)</param>
        /// <param name="n">The number of sides needed to be blocked</param>
        /// <returns>If a piece is captured</returns>
        static bool IsCaptured(bool[] sides, int n)
        {
            switch (n)
            {
                // never used atm
                case 1:
                    return sides[0] || sides[1] || sides[2] || sides[3];
                // custodian capture
                case 2:
                    return (sides[0] && sides[2]) || (sides[1] && sides[3]);
                // king next to edge or throne
                case 3:
                    return (sides[0] && sides[1] && sides[2]) || (sides[1] && sides[2] && sides[3]) || (sides[2] && sides[3] && sides[0]);
                // roaming king
                case 4:
                    return sides[0] && sides[1] && sides[2] && sides[3];
                // uncapturable piece (any n>4)
                default:
                    return false;
            }
        }
        static bool IsThreatened(bool[] immediateThreat, bool[] nextMoveThreat, int n)
        {
            switch (n)
            {
                // never used atm
                case 1:
                    return immediateThreat[0] || immediateThreat[1] || immediateThreat[2] || immediateThreat[3]
                        ||
                        immediateThreat[0] || immediateThreat[1] || immediateThreat[2] || immediateThreat[3];
                // custodian capture threat
                case 2:
                    return (immediateThreat[0] && nextMoveThreat[2])
                        ||
                        (immediateThreat[1] && nextMoveThreat[3])
                        ||
                        (immediateThreat[2] && nextMoveThreat[0])
                        ||
                        (immediateThreat[3] && nextMoveThreat[1]);
                // king next to edge or throne
                case 3:
                    return (immediateThreat[0] && immediateThreat[1] && nextMoveThreat[2])
                        ||
                        (nextMoveThreat[1] && immediateThreat[2] && immediateThreat[3])
                        ||
                        (immediateThreat[1] && nextMoveThreat[2] && immediateThreat[3])
                        ||
                        (immediateThreat[1] && immediateThreat[2] && nextMoveThreat[3])
                        ||
                        (nextMoveThreat[1] && immediateThreat[2] && immediateThreat[3])
                        ||
                        (immediateThreat[1] && nextMoveThreat[2] && immediateThreat[3])
                        ||
                        (immediateThreat[2] && immediateThreat[3] && nextMoveThreat[0])
                        ||
                        (immediateThreat[2] && immediateThreat[3] && nextMoveThreat[0])
                        ||
                        (nextMoveThreat[2] && immediateThreat[3] && immediateThreat[0])
                        ||
                        (immediateThreat[2] && nextMoveThreat[3] && immediateThreat[0]);
                // roaming king
                case 4:
                    return (immediateThreat[0] && immediateThreat[1] && immediateThreat[2] && nextMoveThreat[3])
                        ||
                        (immediateThreat[0] && immediateThreat[1] && nextMoveThreat[2] && immediateThreat[3])
                        ||
                        (immediateThreat[0] && nextMoveThreat[1] && immediateThreat[2] && immediateThreat[3])
                        ||
                        (nextMoveThreat[0] && immediateThreat[1] && immediateThreat[2] && immediateThreat[3]);
                default:
                    return false;
            }
        }
        static int GetOppositeIndex(int i)
        {
            switch (i)
            {
                case 0:
                    return 2;
                case 1:
                    return 3;
                case 2:
                    return 0;
                case 3:
                    return 1;
                default:
                    throw new Exception("Unexpected index reached");
            }
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

        private static bool DoesPieceReachPosition(IPiece piece, Position position, Board board, CaptureRuleSet captureRuleSet)
        {
            bool canLand = true;
            bool canTraverse = true;
            if (BoardUtils.IsOnThrone(position, board))
            {
                if (piece is King)
                {
                    canLand = captureRuleSet.canKingLandOnThrone;
                }
                else
                {
                    canLand = captureRuleSet.canPawnLandOnThrone;
                }
            }
            else if (BoardUtils.IsOnCorner(position, board))
            {
                if (piece is King)
                {
                    canLand = captureRuleSet.canKingLandOnCorner;
                }
                else
                {
                    canLand = captureRuleSet.canPawnLandOnCorner;
                }
            }
            if (canLand)
            {
                List<Position> rangePosition = PositionUtils.GetPositionsRange(piece.Position, position);
                foreach (Position checking in rangePosition)
                {
                    if (BoardUtils.IsOnThrone(position, board))
                    {
                        if (piece is King)
                        {
                            canTraverse = captureRuleSet.canKingTraverseThrone;
                        }
                        else
                        {
                            canTraverse = captureRuleSet.canPawnTraverseThrone;
                        }
                    }
                }
            }
            if (piece is King)
            {
                return canLand && canTraverse && piece.Position.Subtract(position) <= captureRuleSet.kingMovesLimiter;
            }
            // if piece is commander
            // if piece is elite guard
            else
            {
                return canLand && canTraverse && piece.Position.Subtract(position) <= captureRuleSet.pawnMovesLimiter;
            }
        }
    }
}
