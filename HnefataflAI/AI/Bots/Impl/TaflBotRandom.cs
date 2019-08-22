using HnefataflAI.Commons;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Engine;
using HnefataflAI.Games.Engine.Impl;
using HnefataflAI.Games.Rules;
using System;
using System.Collections.Generic;

namespace HnefataflAI.AI.Bots.Impl
{
    /// <summary>
    /// A Hnefatafl bot that plays random valid moves
    /// </summary>
    class TaflBotRandom : ATaflBot
    {
        private readonly IGameEngine GameEngine;
        /// <summary>
        /// Constructor for the TaflBotRandom
        /// </summary>
        /// <param name="pieceColors">The piece color</param>
        /// <param name="ruleType">The rule type</param>
        public TaflBotRandom(PieceColors pieceColors, RuleTypes ruleType)
        {
            this.PieceColors = pieceColors;
            this.Rule = RuleUtils.GetRule(ruleType);
            this.GameEngine = new GameEngineImpl(ruleType);
            this.BotType = BotTypes.RANDOM;
            //temporary
            this.PlayerName = "Rando";
            this.AdditionalInfo = new List<string> { "Just a random player bot" };
        }
        /// <summary>
        /// Get a random move as a user input
        /// </summary>
        /// <param name="board">The board</param>
        /// <param name="moves">The list of all possible moves</param>
        /// <returns>A move as a user input</returns>
        public override string[] GetMove(Board board)
        {
            List<Move> moves = GameEngine.GetMovesByColor(PieceColors, board);
            Random rnd = new Random();
            int index = rnd.Next(moves.Count);
            return MoveUtils.MoveAsInput(moves[index]);
        }
    }
}
