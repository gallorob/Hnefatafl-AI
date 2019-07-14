using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Pieces;
using System.Collections.Generic;

namespace HnefataflAI.Games.Rules
{
    public interface IRule
    {
        RuleTypes RuleType { get; }
        bool CheckIfKingIsCaptured(IPiece king, Board board);
        List<IPiece> CheckIfCaptures(IPiece piece, Board board);
        bool CheckIfMoveIsValid(IPiece piece, Position to);
        bool CheckIfHasRepeatedMoves(List<Move> moves);
    }
}
