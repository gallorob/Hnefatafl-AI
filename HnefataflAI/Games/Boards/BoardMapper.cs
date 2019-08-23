using HnefataflAI.Commons.Converter;
using HnefataflAI.Commons.Positions;
using System.Collections.Generic;

namespace HnefataflAI.Games.Boards
{
    public static class BoardMapper
    {
        public static Dictionary<Position, TileTypes> LookUpTable;

        public static void InitializeTable(int numRows, int numCols)
        {
            if (LookUpTable == null)
            {
                LookUpTable = new Dictionary<Position, TileTypes>(numRows * numCols);
                for (int i = 0; i < numRows * numCols; i++)
                {
                    LookUpTable.Add(PositionIndexConverter.GetPositionFromIndex(i, numCols), TileTypes.SIMPLE);
                }
            }
        }
        public static void AddEntry(Position position, TileTypes tileType)
        {
            LookUpTable[position] = tileType;
        }
    }
}
