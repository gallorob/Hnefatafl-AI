using System;
using System.Collections.Generic;
using HnefataflAI.Commons;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Rules;

namespace HnefataflAI.AI.Bots.Impl
{
    /// <summary>
    /// A Hnefatafl bot that plays with magic numbers
    /// </summary>
    class TaflBotMagic : ITaflBot
    {
        /// <summary>
        /// The rule type for the bot
        /// </summary>
        public RuleTypes RuleType { get; private set; }
        /// <summary>
        /// The piece color
        /// </summary>
        public PieceColors PieceColors { get; private set; }
        public String PlayerName { get; private set; }
        public List<String> AdditionalInfo { get; private set; }
        /// <summary>
        /// How many times it shuffles the list of moves
        /// </summary>
        private readonly int Shuffles;
        /// <summary>
        /// The number's favourite number
        /// </summary>
        private readonly int Index;
        /// <summary>
        /// Constructor for the TaflBotMagic
        /// </summary>
        /// <param name="pieceColors">The piece color</param>
        /// <param name="ruleType">The rule type</param>
        public TaflBotMagic(PieceColors pieceColors, RuleTypes ruleType)
        {
            this.PieceColors = pieceColors;
            this.RuleType = ruleType;
            Random rnd = new Random();
            this.Shuffles = rnd.Next(10);
            this.Index = rnd.Next(42);
            //temporary
            this.PlayerName = "Wizard";
            this.AdditionalInfo = new List<String> { "Just a semi-random player bot" };
        }
        /// <summary>
        /// Shuffle the moves N times and return either the last move or the bot's favourite.
        /// </summary>
        /// <param name="board">The board</param>
        /// <param name="moves">The list of all possible moves</param>
        /// <returns>Pretty much a random move, really.</returns>
        public string[] GetMove(Board board, List<Move> moves)
        {
            for (int i = 0; i < this.Shuffles; i++)
            {
                ListUtils.ShuffleList(moves);
            }
            return MoveUtils.MoveAsInput(moves[(moves.Count >= this.Index ? this.Index - 1 : moves.Count - 1)]);
        }
        /// <summary>
        /// Only for implementation
        /// </summary>
        /// <returns>Nothing; throws NotImplementedException</returns>
        public string[] GetMove()
        {
            throw new NotImplementedException();
        }
    }
}
