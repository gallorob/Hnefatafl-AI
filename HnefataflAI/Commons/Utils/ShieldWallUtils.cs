using HnefataflAI.Commons.Positions;
using HnefataflAI.Defaults;
using HnefataflAI.Games.Boards;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System;
using System.Collections.Generic;

namespace HnefataflAI.Commons.Utils
{
    public static class ShieldWallUtils
    {
        public static List<IPiece> GetShieldWallBrackets(IPiece pivot, IPiece moved, Board board, Directions edgeDirection, CaptureRuleSet captureRuleSet)
        {
            List<IPiece> bracketingPieces = new List<IPiece>();
            if (CheckAhead(pivot, board, edgeDirection, captureRuleSet))
            {
                IPiece bracket1 = CheckDirection(pivot, moved, board, edgeDirection, captureRuleSet, PositionUtils.GetClockWiseDirection);
                IPiece bracket2 = CheckDirection(pivot, moved, board, edgeDirection, captureRuleSet, PositionUtils.GetCounterClockWiseDirection);
                bracketingPieces.Add(bracket1);
                bracketingPieces.Add(bracket2);
            }
            return bracketingPieces;
        }
        public static bool CheckAhead(IPiece piece, Board board, Directions edgeDirection, CaptureRuleSet captureRuleSet)
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
                        if (!CheckAhead(checkingPiece, board, edgeDirection, captureRuleSet))
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

        public static bool IsShieldWallThreatened(List<IPiece> bracketingPieces, IPiece piece, Board board, Directions edgeDirection, CaptureRuleSet captureRuleSet)
        {
            if (!bracketingPieces.Contains(null))
            {
                return false;
            }
            else
            {
                bool underThreat = false;
                IPiece bracket = bracketingPieces[0] ?? bracketingPieces[1];
                // handle both bracketing pieces null
                if (bracket == null)
                {
                    return false;
                }
                if (bracket.PieceColors.Equals(piece.PieceColors))
                {
                    return false;
                }
                Position otherBracket = GetLastPositionBracket(bracket.Position, bracket.PieceColors, board, edgeDirection);
                foreach (Directions checkingDirection in PositionUtils.GetClockWiseDirections())
                {
                    // get the first piece in range
                    IPiece checkingPiece = BoardUtils.GetFirstPieceFromPosition(board, checkingDirection, otherBracket);
                    if (checkingPiece != null && bracket.PieceColors.Equals(checkingPiece.PieceColors))
                    {
                        bool reaches = CaptureUtils.DoesPieceReachPosition(checkingPiece, otherBracket, board, captureRuleSet);
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

        private static Position GetLastPositionBracket(Position bracketPosition, PieceColors bracketColor, Board board, Directions edgeDirection)
        {
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
    }
}
