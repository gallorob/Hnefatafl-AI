using HnefataflAI.AI.Bots;
using HnefataflAI.AI.Bots.Impl;
using HnefataflAI.Games.Rules;

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
                case BotTypes.MAGIC:
                    return new TaflBotMagic(pieceColor, ruleType);
                case BotTypes.MINIMAX:
                    return new TaflBotMinimax(pieceColor, ruleType);
                case BotTypes.MINIMAXAB:
                    return new TaflBotMinimaxAB(pieceColor, ruleType);
                case BotTypes.RANDOM:
                    return new TaflBotRandom(pieceColor, ruleType);
                default:
                    return null;
            }
        }
    }
}
