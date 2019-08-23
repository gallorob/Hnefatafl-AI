using HnefataflAI.Commons.Positions;
using HnefataflAI.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HnefataflAI.Commons.Converter
{
    public static class PositionIndexConverter
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
    }
}
