using HnefataflAI.AI.Bots;
using HnefataflAI.AI.Bots.Impl;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Games.Rules;
using System.Reflection;

namespace HnefataflAI.Commons.Utils
{
    public static class BotUtils
    {
        public static ITaflBot GetTaflBot(BotTypes botType, PieceColors pieceColor, RuleTypes ruleType)
        {
            switch (botType)
            {
                case BotTypes.BASIC:
                    return new TaflBotBasic(pieceColor, ruleType);
                case BotTypes.MINIMAX:
                    return new TaflBotMinimax(pieceColor, ruleType);
                case BotTypes.MINIMAXAB:
                    return new TaflBotMinimaxAB(pieceColor, ruleType);
                case BotTypes.RANDOM:
                    return new TaflBotRandom(pieceColor, ruleType);
                default:
                    throw new CustomGenericException(typeof(BotUtils).Name, MethodBase.GetCurrentMethod().Name, string.Format("Unsupported BOT Type, got: {0}", botType));
            }
        }
    }
}
