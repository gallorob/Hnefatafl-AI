using HnefataflAI.Games.Rules;
using HnefataflAI.Games.Rules.Impl;

namespace HnefataflAI.Commons.Utils
{
    public class RuleUtils
    {
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
    }
}
