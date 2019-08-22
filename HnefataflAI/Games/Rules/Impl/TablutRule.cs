using HnefataflAI.Defaults;
using System;

namespace HnefataflAI.Games.Rules.Impl
{
    public class TablutRule : AbstractRule
    {
        public TablutRule()
        {
            RuleType = RuleTypes.TABLUT;

            PawnMovesLimiter = Math.Max(DefaultValues.MAX_COLS, DefaultValues.MAX_ROWS);
            KingMovesLimiter = PawnMovesLimiter;
            CanKingLandOnThrone = true;
            CanKingLandOnCorner = true;
            CanKingLandOnEnemyBaseCamps = true;
            CanPawnLandOnThrone = true;
            CanPawnLandOnCorner = true;
            CanPawnLandOnEnemyBaseCamps = true;
            CanPawnLandBackOnOwnBaseCamp = true;

            PiecesForPawn = 2;
            PiecesForKingOnThrone = 4;
            PiecesForKingNextToThrone = 4;
            PiecesForKingNextToCorner = 2;
            PiecesForKingOnEdge = 5;
            PiecesForKingOnBoard = 2;
            IsCornerHostileToPawns = true;
            IsCornerHostileToKing = true;
            IsThroneHostileToPawns = true;
            IsThroneHostileToKing = true;
            IsEdgeHostileToPawns = false;
            IsEdgeHostileToKing = true;
            IsKingArmed = false;
            IsShieldWallAllowed = false;
            IsKingDefeatedInShieldWall = false;

            IsEdgeEscape = true;
            IsCornerEscape = false;

            MovesRepetition = 3;

            AllowEncirclement = false;
            AllowDrawForts = false;
            AllowExitForts = false;
        }
    }
}
