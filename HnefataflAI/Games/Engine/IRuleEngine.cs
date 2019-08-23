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
        HashSet<IPiece> GetCapturedPieces(IPiece piece, Board board);
        HashSet<Move> GetAvailableMoves(PieceColors playerColor, Board board);
        HashSet<Move> GetAvailableMoves(IPiece piece, Board board);
        GameStatus GetGameStatus(Move move, Board board, List<Move> whiteMoves, List<Move> blackMoves);
        bool CanMove(PieceColors playerColor, Board board);
        void UpdatePiecesThreatLevel(Board board);
    }
}
