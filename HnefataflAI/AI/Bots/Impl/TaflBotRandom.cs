using HnefataflAI.Commons;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Rules;
using System;
using System.Collections.Generic;

namespace HnefataflAI.AI.Bots.Impl
{
    /// <summary>
    /// A Hnefatafl bot that plays random valid moves
    /// </summary>
    class TaflBotRandom : ITaflBot
    {
        /// <summary>
        /// The rule type for the bot
        /// </summary>
        public RuleTypes RuleType { get; private set; }
        /// <summary>
        /// The piece color
        /// </summary>
        public PieceColors PieceColors { get; private set; }
        /// <summary>
        /// Constructor for the TaflBotRandom
        /// </summary>
        /// <param name="pieceColors">The piece color</param>
        /// <param name="ruleType">The rule type</param>
        public TaflBotRandom(PieceColors pieceColors, RuleTypes ruleType)
        {
            this.PieceColors = pieceColors;
            this.RuleType = ruleType;
        }
        /// <summary>
        /// Only for implementation
        /// </summary>
        /// <returns>Nothing; throws NotImplementedException</returns>
        public string[] GetMove()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Get a random move as a user input
        /// </summary>
        /// <param name="board">The board</param>
        /// <param name="moves">The list of all possible moves</param>
        /// <returns>A move as a user input</returns>
        public string[] GetMove(Board board, List<Move> moves)
        {
            Random rnd = new Random();
            int index = rnd.Next(moves.Count);
            return MoveUtils.MoveAsInput(moves[index]);
        }
    }
}
