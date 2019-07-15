using HnefataflAI.Games.Rules;
using HnefataflAI.Games.Rules.Impl;
using System.Collections.Generic;

namespace HnefataflAI.Commons.Utils
{
    public class RuleUtils
    {
        /// <summary>
        /// Get the default rule given its type
        /// </summary>
        /// <param name="ruleType">The rule type</param>
        /// <returns>The default rule</returns>
        public static IRule GetRule(RuleTypes ruleType)
        {
            switch (ruleType)
            {
                case RuleTypes.HNEFATAFL:
                    return new HnefataflRule();
                default:
                    return new CustomRule();
            }
        }
        /// <summary>
        /// Check if the last N moves are the same.
        /// </summary>
        /// <param name="moves">The list of moves</param>
        /// <param name="movesRepetition">The N</param>
        /// <returns>Whether or not the last N moves are the same</returns>
        public static bool CheckIfHasRepeatedMoves(List<Move> moves, int movesRepetition)
        {
            bool isRepeated = true;
            if (moves.Count >= movesRepetition * 2 - 1)
            {
                for (int i = 1; i < movesRepetition; i += 2)
                {
                    isRepeated &= moves[moves.Count - i].Equals(moves[moves.Count - i - 2]);
                }
                return isRepeated;
            }
            return false;
        }
    }
}
