using System.Collections.Generic;
using HnefataflAI.Commons;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games.Boards;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;

namespace HnefataflAI.Games.Rules
{
    public abstract class AbstractRule : IRule
    {
        #region Properties
        public RuleTypes RuleType { get; protected set; }
        // moves restrictions
        public int PawnMovesLimiter { get; protected set; }
        public int KingMovesLimiter { get; protected set; }
            // landing
        public bool CanKingLandOnThrone { get; protected set; }
        public bool CanKingLandOnCorner { get; protected set; }
        public bool CanKingLandOnEnemyBaseCamps { get; protected set; }
        public bool CanPawnLandOnThrone { get; protected set; }
        public bool CanPawnLandOnCorner { get; protected set; }
        public bool CanPawnLandOnEnemyBaseCamps { get; protected set; }
        public bool CanPawnLandBackOnOwnBaseCamp { get; protected set; }
        // capturing
        public int PiecesForPawn { get; protected set; }
        public int PiecesForKingOnThrone { get; protected set; }
        public int PiecesForKingNextToThrone { get; protected set; }
        public int PiecesForKingNextToCorner { get; protected set; }
        public int PiecesForKingOnEdge { get; protected set; }
        public int PiecesForKingOnBoard { get; protected set; }
            // corner behaviour
        public bool IsCornerHostileToPawns { get; protected set; }
        public bool IsCornerHostileToKing { get; protected set; }
            // throne behaviour
        public bool IsThroneHostileToPawns { get; protected set; }
        public bool IsThroneHostileToKing { get; protected set; }
            // edge behaviour
        public bool IsEdgeHostileToKing { get; protected set; }
        public bool IsEdgeHostileToPawns { get; protected set; }
            // armed king
        public bool IsKingArmed { get; protected set; }
            // shieldwall rules
        public bool IsShieldWallAllowed { get; protected set; }
        public bool IsKingDefeatedInShieldWall { get; protected set; }
        // end game for defender
        public bool IsEdgeEscape { get; protected set; }
        public bool IsCornerEscape { get; protected set; }
        // losing situation
        public int MovesRepetition { get; protected set; }
        // other
        public bool AllowEncirclement { get; protected set; }
        public bool AllowDrawForts { get; protected set; }
        public bool AllowExitForts { get; protected set; }
        #endregion

        public virtual HashSet<IPiece> CheckIfCaptures(IPiece piece, Board board)
        {
            HashSet<IPiece> capturedPieces = new HashSet<IPiece>();
            bool isKingArmed = false;
            if ((piece is King && isKingArmed) || !(piece is King))
            {
                foreach (Directions direction in PositionUtils.GetClockWiseDirections())
                {
                    SetUtils<IPiece>.AddIfNotNull(capturedPieces, HasCapturedPiece(piece, board, direction));
                }
            }
            return capturedPieces;
        }

        public virtual HashSet<IPiece> HasCapturedPiece(IPiece piece, Board board, Directions direction)
        {
            HashSet<IPiece> captures = new HashSet<IPiece>();
            CaptureRuleSet captureRuleSet = GetCaptureRuleSet();
            if (BoardUtils.IsPositionMoveValid(piece.Position, direction, board))
            {
                IPiece otherPiece = board.At(piece.Position.MoveTo(direction));
                if (otherPiece != null && CaptureUtils.IsPieceCaptured(otherPiece, piece, board, captureRuleSet))
                {
                    // SHIELDWALL CAPTURE
                    if (BoardUtils.IsOnEdge(otherPiece.Position, board)
                        &&
                        captureRuleSet.isShieldWallAllowed
                        &&
                        ShieldWallUtils.GetShieldWallBrackets(otherPiece, piece, board, PositionUtils.GetEdgeDirection(otherPiece.Position, board), captureRuleSet).Count == 2)
                    {
                        List<IPiece> anchors = CaptureUtils.IsShieldWallComplete(otherPiece, piece, board, PositionUtils.GetEdgeDirection(otherPiece.Position, board), captureRuleSet);
                        List<Position> range = PositionUtils.GetPositionsRange(anchors[0].Position, anchors[1].Position);
                        foreach (Position p in range)
                        {
                            IPiece capturedPiece = board.At(p);
                            if (capturedPiece is King && captureRuleSet.isKingDefeatedInShieldWall)
                            {
                                captures.Add(capturedPiece);
                            }
                            captures.Add(capturedPiece);
                        }
                    }
                    // NORMAL (CUSTODIAN) CAPTURES
                    else
                    {
                        captures.Add(otherPiece);
                    }
                }
            }
            return captures;
        }

        public virtual bool CheckIfHasRepeatedMoves(List<Move> moves)
        {
            return RuleUtils.CheckIfHasRepeatedMoves(moves, this.MovesRepetition);
        }

        public virtual bool CheckIfMoveIsValid(IPiece piece, Position to)
        {
            try
            {
                MoveUtils.ValidateMove(piece.Position, to);
            }
            catch (InvalidInputException)
            {
                return false;
            }
            return true;
        }
        
        public virtual bool CheckIfUnderThreat(IPiece piece, Board board)
        {
            return CaptureUtils.IsPieceThreatened(piece, board, GetCaptureRuleSet());
        }

        public virtual HashSet<Move> GetMovesForPiece(IPiece piece, Board board)
        {
            HashSet<Move> availableMoves = new HashSet<Move>();
            foreach (Directions direction in PositionUtils.GetClockWiseDirections())
            {
                SetUtils<Move>.AddRange(availableMoves, MoveUtils.GetMovesForPiece(piece, board, direction, GetMoveRuleSet()));
            }
            return availableMoves;
        }

        public virtual CaptureRuleSet GetCaptureRuleSet()
        {
            CaptureRuleSet captureRuleSet = new CaptureRuleSet
            {
                isCornerHostileToKing = this.IsCornerHostileToKing,
                isCornerHostileToPawns = this.IsCornerHostileToPawns,
                isEdgeHostileToKing = this.IsEdgeHostileToKing,
                isEdgeHostileToPawns = this.IsEdgeHostileToPawns,
                isKingArmed = this.IsKingArmed,
                isShieldWallAllowed = this.IsShieldWallAllowed,
                isKingDefeatedInShieldWall = this.IsKingDefeatedInShieldWall,
                isThroneHostileToKing = this.IsThroneHostileToKing,
                isThroneHostileToPawns = this.IsThroneHostileToPawns,
                piecesForPawn = this.PiecesForPawn,
                piecesForKingNextToCorner = this.PiecesForKingNextToCorner,
                piecesForKingNextToThrone = this.PiecesForKingNextToThrone,
                piecesForKingOnBoard = this.PiecesForKingOnBoard,
                piecesForKingOnEdge = this.PiecesForKingOnEdge,
                piecesForKingOnThrone = this.PiecesForKingOnThrone,
                moveRuleSet = this.GetMoveRuleSet()
            };
            return captureRuleSet;
        }
        public virtual MoveRuleSet GetMoveRuleSet()
        {
            MoveRuleSet moveRuleSet = new MoveRuleSet
            {
                canKingLandOnCorner = this.CanKingLandOnCorner,
                canKingLandOnThrone = this.CanKingLandOnThrone,
                canPawnLandOnCorner = this.CanPawnLandOnCorner,
                canPawnLandOnThrone = this.CanPawnLandOnThrone,
                kingMovesLimiter = this.KingMovesLimiter,
                pawnMovesLimiter = this.PawnMovesLimiter,
                canKingLandOnEnemyBaseCamps = this.CanKingLandOnEnemyBaseCamps,
                canPawnLandOnEnemyBaseCamps = this.CanPawnLandOnEnemyBaseCamps,
                canPawnLandBackOnOwnBaseCamp = this.CanPawnLandBackOnOwnBaseCamp
            };
            return moveRuleSet;
        }
    }
}
