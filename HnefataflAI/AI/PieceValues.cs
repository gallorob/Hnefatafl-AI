namespace HnefataflAI.AI
{
    public static class PieceValues
    {
        public static readonly int PawnValue = 10;
        public static readonly int OpenLineValue = 20;
        public static readonly int KingValue = 100;
        public static readonly int PawnPositionMultiplier = 2;
        public static readonly int KingPositionMultiplier = 100;
        public static readonly int PawnUnderAttackMultiplier = -5;
        public static readonly int KingUnderAttackMultiplier = -7;
        public static readonly int PawnOnOpenLine = 3;
        public static readonly int KingOnOpenLine = 5;
        public static readonly int WinValue = 999999;
        public static readonly int LossValue = -999999;
    }
}
