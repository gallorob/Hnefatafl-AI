using HnefataflAI.Commons;
using HnefataflAI.Games;
using HnefataflAI.Player;
using System.Collections.Generic;

namespace HnefataflAI.AI.Bots
{
    public interface IHnefataflBot : IPlayer
    {
        string[] GetMove(Board board, List<Move> moves);
    }
}
