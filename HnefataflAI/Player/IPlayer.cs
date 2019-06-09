namespace HnefataflAI.Player
{
    using HnefataflAI.Commons;
    public interface IPlayer
    {
        PieceColors PieceColors { get; }
        string[] getMove();
    }
}
