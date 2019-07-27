using HnefataflAI.Commons;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.GameState;
using HnefataflAI.Games.Rules;
using HnefataflAI.Pieces;
using System.Collections.Generic;

namespace HnefataflAI.Games.Engine
{
    public interface IRuleEngine
    {
        IRule Rule { get; }
        List<IPiece> GetCapturedPieces(IPiece piece, Board board);
        List<Move> GetAvailableMoves(PieceColors playerColor, Board board);
        List<Move> GetAvailableMoves(IPiece piece, Board board);
        GameStatus GetGameStatus(IPiece movedPiece, Board board, List<Move> whiteMoves, List<Move> blackMoves);
        bool CanMove(PieceColors playerColor, Board board);
    }
}
