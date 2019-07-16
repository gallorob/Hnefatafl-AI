using HnefataflAI.Commons;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.GameState;
using HnefataflAI.Pieces;
using System.Collections.Generic;

namespace HnefataflAI.Games.Engine
{
    public interface IGameEngine
    {
        Move ProcessPlayerMove(string[] playerMove, Board board);
        GameStatus GetGameStatus(IPiece movedPiece, Board board);
        List<Move> GetMovesByColor(PieceColors pieceColor, Board board);
        void ApplyMove(Move move, Board board, PieceColors playerColor);
        void UndoMove(Move move, Board board, PieceColors playerColor);
        void UndoCaptures(Board board);
    }
}
