using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;

namespace HnefataflAI.Commons.Utils
{
    static class PositionUtils
    {
        public static string PositionAsInput(Position position)
        {
            return position.ToString().ToLower().Replace(" ", "");
        }
        public static Position ValidateAndReturnInputPosition(string input)
        {
            int row;
            char col;
            if (input.Length > 3)
            {
                throw new InvalidInputException(ErrorMessages.INVALID_INPUT);
            }
            try
            {
                row = System.Int32.Parse(input.Substring(1));
            } catch (System.Exception)
            {
                throw new InvalidInputException(ErrorMessages.INVALID_IPUT_POSITION);
            }
            try
            {
                col = input[0];
            }
            catch (System.Exception)
            {
                throw new InvalidInputException(ErrorMessages.INVALID_IPUT_POSITION);
            }
            return new Position(row, col);
        }
    }
}
