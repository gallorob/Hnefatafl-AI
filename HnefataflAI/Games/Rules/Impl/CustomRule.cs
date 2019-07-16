using System;
using System.Collections.Generic;
using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Defaults;
using HnefataflAI.Games.Boards;
using HnefataflAI.Pieces;

namespace HnefataflAI.Games.Rules.Impl
{
    class CustomRule : IRule
    {
        /// <summary>
        /// The Rule type
        /// </summary>
        public RuleTypes RuleType { get; private set; }
        /// <summary>
        /// How many tiles can a pawn move
        /// </summary>
        public int PawnMoveLimiter { get; private set; }
        /// <summary>
        /// How many tiles can the king move
        /// </summary>
        public int KingMoveLimiter { get; private set; }
        /// <summary>
        /// How many pieces are needed to capture the king
        /// </summary>
        public int KingPiecesForCapture { get; private set; }
        /// <summary>
        /// Does the throne count as a capture piece for king as well?
        /// </summary>
        public bool IsThroneHostileToAll { get; private set; }
        /// <summary>
        /// Does the corner count as a capture piece for king as well?
        /// </summary>
        public bool IsCornerHostileToAll { get; private set; }
        /// <summary>
        /// How many moves it takes to repeat before losing the game
        /// </summary>
        public int MovesRepetition { get; private set; }
        public CustomRule()
        {
            this.RuleType = RuleTypes.HNEFATAFL;
            this.PawnMoveLimiter = Math.Max(DefaultValues.MAX_COLS, DefaultValues.MAX_ROWS);
            this.KingMoveLimiter = this.PawnMoveLimiter;
            this.KingPiecesForCapture = 4;
            this.IsThroneHostileToAll = true;
            this.IsCornerHostileToAll = true;
            this.MovesRepetition = 3;
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
