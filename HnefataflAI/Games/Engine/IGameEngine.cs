using HnefataflAI.Commons;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.GameState;
using HnefataflAI.Pieces;
using System.Collections.Generic;

namespace HnefataflAI.Games.Engine
{
    public interface IGameEngine
    {
        IRuleEngine RuleEngine { get; }
        Move ProcessPlayerMove(string[] playerMove, Board board);
        GameStatus GetGameStatus(Move move, Board board);
        List<Move> GetMovesByColor(PieceColors pieceColor, Board board);
        void ApplyMove(Move move, Board board, PieceColors playerColor, bool fromBot = false);
        void UndoMove(Move move, Board board, PieceColors playerColor, bool fromBot = false);
        void UndoCaptures(Board board);
        void UndoCaptures(Board board, List<IPiece> captures);
    }
}
