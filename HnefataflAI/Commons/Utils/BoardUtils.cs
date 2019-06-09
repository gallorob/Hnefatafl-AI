using HnefataflAI.Commons.Positions;

namespace HnefataflAI.Commons.Utils
{
    public static class BoardUtils
    {
        private static readonly char FIRST_COLUMN = 'a';
        private static readonly char UPPER_FIRST_COLUMN = 'A';
        public static string GetBoardColumnsChars(int totalCols)
        {
            string ret = "";
            for (int i = 0; i < totalCols; i++)
            {
                ret += string.Format(" {0} ", (char)(System.Convert.ToUInt16(UPPER_FIRST_COLUMN) + i));
            }
            return ret;
        }
        public static bool IsPositionUpdateValid(Position position, Directions direction, int totalRows, int totalCols)
        {
            switch (direction)
            {
                case Directions.UP:
                    return position.Row <= totalRows;
                case Directions.DOWN:
                    return position.Row > 0;
                case Directions.LEFT:
                    return position.Col > FIRST_COLUMN;
                case Directions.RIGHT:
                    return position.Col < FIRST_COLUMN + totalCols;
                default:
                    return false;
            }
        }
        public static bool IsPositionValid(Position position, int totalRows, int totalCols)
        {
            return (position.Row <= totalRows
                &&
                position.Row > 0
                &&
                position.Col > FIRST_COLUMN
                &&
                position.Col < FIRST_COLUMN + totalCols);
        }
        public static bool IsOnBoardCorner(Position position, int totalRows, int totalCols)
        {
            return (
                (position.Col == FIRST_COLUMN && position.Row == 1)
                ||
                (position.Col == FIRST_COLUMN && position.Row == totalRows)
                ||
                (position.Col == FIRST_COLUMN + totalCols - 1 && position.Row == 1)
                ||
                (position.Col == FIRST_COLUMN + totalCols - 1 && position.Row == totalRows)
                );
        }
        public static bool IsOnThrone(Position position, int totalRows, int totalCols)
        {
            return (position.Col - FIRST_COLUMN == totalCols / 2
                &&
                position.Row == totalRows / 2 + 1);
        }
    }
}
