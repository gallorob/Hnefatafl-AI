namespace HnefataflAI.Commons
{
    public class MoveRuleSet
    {
        public int kingMovesLimiter = 11;
        public int pawnMovesLimiter = 11;
        public bool canPawnLandOnThrone = false;
        public bool canKingLandOnThrone = true;
        public bool canPawnLandOnCorner = false;
        public bool canKingLandOnCorner = true;
        public bool canKingLandOnEnemyBaseCamps = true;
        public bool canPawnLandOnEnemyBaseCamps = true;
        public bool canPawnLandBackOnOwnBaseCamp = true;

        public MoveRuleSet() { }
    }
}
