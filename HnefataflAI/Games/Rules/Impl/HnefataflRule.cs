using HnefataflAI.Defaults;
using System;

namespace HnefataflAI.Games.Rules.Impl
{
    public class HnefataflRule : AbstractRule
    {
        public HnefataflRule()
        {
            RuleType = RuleTypes.HNEFATAFL;

            PawnMovesLimiter = Math.Max(DefaultValues.MAX_COLS, DefaultValues.MAX_ROWS);
            KingMovesLimiter = PawnMovesLimiter;
            CanKingLandOnThrone = true;
            CanKingLandOnCorner = true;
            CanKingLandOnEnemyBaseCamps = true;
            CanPawnLandOnThrone = false;
            CanPawnLandOnCorner = false;
            CanPawnLandOnEnemyBaseCamps = true;
            CanPawnLandBackOnOwnBaseCamp = true;
            CanKingTraverseThrone = true;
            CanPawnTraverseThrone = false;
            CanKingTraverseEnemyBaseCamps = true;
            CanPawnTraverseEnemyBaseCamps = true;

            PiecesForPawn = 2;
            PiecesForKingOnThrone = 4;
            PiecesForKingNextToThrone = 4;
            PiecesForKingNextToCorner = 4;
            PiecesForKingOnEdge = 4;
            PiecesForKingOnBoard = 4;
            IsCornerHostileToPawns = true;
            IsCornerHostileToKing = true;
            IsThroneHostileToPawns = true;
            IsThroneHostileToKing = true;
            IsEdgeHostileToPawns = false;
            IsEdgeHostileToKing = true;
            IsKingArmed = false;
            IsShieldWallAllowed = true;
            IsKingDefeatedInShieldWall = false;

            IsEdgeEscape = false;
            IsCornerEscape = true;

            MovesRepetition = 3;
        }
    }
}
