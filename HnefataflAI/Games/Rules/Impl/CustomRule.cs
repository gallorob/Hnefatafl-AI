using System;
using System.Collections.Generic;
using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Pieces;

namespace HnefataflAI.Games.Rules.Impl
{
    class CustomRule : IRule
    {
        public RuleTypes RuleType { get; private set; }
        public CustomRule()
        {
            this.RuleType = RuleTypes.CUSTOM;
        }

        public List<IPiece> CheckIfCaptures(Move move, Board board)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfHasRepeatedMoves(List<Move> moves)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfKingIsCaptured(IPiece king, Board board)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfMoveIsValid(IPiece piece, Position to)
        {
            throw new NotImplementedException();
        }

        public List<IPiece> CheckIfCaptures(IPiece piece, Board board)
        {
            throw new NotImplementedException();
        }
    }
}
