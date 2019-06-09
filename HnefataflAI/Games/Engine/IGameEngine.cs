using HnefataflAI.Commons;

namespace HnefataflAI.Games.Engine
{
    public interface IGameEngine
    {
        Move ProcessPlayerMove(string[] playerMove, Board board);
        void ApplyMove(Move move, Board board, PieceColors playerColor);
    }
}
