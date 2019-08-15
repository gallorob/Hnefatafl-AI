using HnefataflAI.Commons.Positions;
using HnefataflAI.Defaults;
using System.Collections.Generic;

namespace TaflWPF.Utils
{
    public static class GridUtils
    {
        public static int GetIndexFromPosition(Position position, int boardCol)
        {
            int column = position.Col - DefaultValues.FIRST_COLUMN + 1;
            int actualRow = boardCol - position.Row;
            return (boardCol * actualRow) + column - 1;
        }
        public static Position GetPositionFromIndex(int index, int boardCols)
        {
            int actualCol = index % boardCols;
            int actualRow = boardCols - (index / boardCols);
            return new Position(actualRow, (char)(DefaultValues.FIRST_COLUMN + actualCol));
        }
        public static List<char> GetLetters(int boardCols)
        {
            List<char> letters = new List<char>();
            for (int i = 0; i < boardCols; i++)
            {
                letters.Add((char)('A' + i));
            }
            return letters;
        }
        public static List<int> GetNumbers(int boardRows)
        {
            List<int> numbers = new List<int>();
            for (int i = boardRows; i > 0; i--)
            {
                numbers.Add(i);
            }
            return numbers;
        }
    }
}
