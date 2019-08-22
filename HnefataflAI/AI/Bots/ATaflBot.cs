using System.Collections.Generic;
using HnefataflAI.Commons;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Rules;

namespace HnefataflAI.AI.Bots
{
    public abstract class ATaflBot : ITaflBot
    {
        public BotTypes BotType { get; protected set; }
        public IRule Rule { get; protected set; }
        public PieceColors PieceColors { get; protected set; }
        public string PlayerName { get; protected set; }
        public List<string> AdditionalInfo { get; protected set; }

        /// <summary>
        /// Only for implementation
        /// </summary>
        /// <returns>Nothing; throws NotImplementedException</returns>
        public string[] GetMove()
        {
            throw new System.NotImplementedException();
        }

        abstract public string[] GetMove(Board board);
    }
}
