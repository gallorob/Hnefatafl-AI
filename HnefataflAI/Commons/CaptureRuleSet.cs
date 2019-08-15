namespace HnefataflAI.Commons
{
    public class CaptureRuleSet
    {
        public int piecesForPawn = 2;
        public int piecesForKingOnThrone = 4;
        public int piecesForKingNextToThrone = 4;
        public int piecesForKingNextToCorner = 4;
        public int piecesForKingOnEdge = 4;
        public int piecesForKingOnBoard = 4;
        public bool isCornerHostileToPawns = true;
        public bool isCornerHostileToKing = true;
        public bool isThroneHostileToPawns = true;
        public bool isThroneHostileToKing = true;
        public bool isEdgeHostileToKing = true;
        public bool isEdgeHostileToPawns = false;
        public bool isShieldWallAllowed = false;
        public bool isKingDefeatedInShieldWall = false;
        public bool isKingArmed = false;
        public int kingMovesLimiter = 11;
        public int pawnMovesLimiter = 11;
        public bool canPawnLandOnThrone = false;
        public bool canKingLandOnThrone = true;
        public bool canPawnLandOnCorner = false;
        public bool canKingLandOnCorner = true;
        public bool canPawnTraverseThrone = true;
        public bool canKingTraverseThrone = true;

        public CaptureRuleSet() { }

    }
}
