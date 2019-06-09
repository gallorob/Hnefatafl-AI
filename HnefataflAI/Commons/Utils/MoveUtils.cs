using HnefataflAI.Commons.Exceptions;

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
    }
}
