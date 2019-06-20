using HnefataflAI.Commons;
using HnefataflAI.Games.GameState;
using HnefataflAI.Pieces;

namespace HnefataflAI.Games.Engine
{
    public interface IGameEngine
    {
        Move ProcessPlayerMove(string[] playerMove, Board board);
        void ApplyMove(Move move, Board board, PieceColors playerColor);
        void UndoMove(Move move, Board board, PieceColors playerColor);
        GameStatus GetGameStatus(IPiece movedPiece, Board board);
        void UndoCaptures(Board board);
    }
}
