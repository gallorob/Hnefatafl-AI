using System;
using System.Collections.Generic;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Rules;

namespace HnefataflAI.Commons.Utils
{
    static class MoveUtils
    {
        public static string[] MoveAsInput(Move move)
        {
            string[] input = new string[2];
            input[0] = PositionUtils.PositionAsInput(move.From);
            input[1] = PositionUtils.PositionAsInput(move.To);
            return input;
        }
        public static string[] MoveFromInput(string input)
        {
            string[] move = input.ToLower().Split('-');
            if (move.Length != 2)
            {
                throw new InvalidInputException(ErrorMessages.INVALID_INPUT);
            }
            return move;
        }
        internal static void ValidateMove(Position from, Position to)
        {
            if (from.Col != to.Col && from.Row != to.Row)
            {
                throw new InvalidInputException(ErrorMessages.INVALID_DESTINATION_POSITION);
            }
        }
        public static bool IsDuplicatedMove(List<Move> listMoves, Move move, IRule rule)
        {
            List<Move> tempMoves = new List<Move>(listMoves)
            {
                move
            };
            return rule.CheckIfHasRepeatedMoves(tempMoves);
        }
        public static void OrderMovesByCapture(List<Move> moves, Board board, RuleTypes ruleType)
        {
            foreach (Move move in moves)
            {
                move.DoesNotCapture = RuleUtils.GetRule(ruleType).CheckIfCaptures(move.Piece, board).Count == 0;
            }
            moves.Sort((x, y) => x.DoesNotCapture.CompareTo(y.DoesNotCapture));
        }
    }
}
