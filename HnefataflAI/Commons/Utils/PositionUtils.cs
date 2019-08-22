using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Defaults;
using HnefataflAI.Games.Boards;
using System.Collections.Generic;
using System.Reflection;

namespace HnefataflAI.Commons.Utils
{
    public static class PositionUtils
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
        public static Directions GetOppositeDirection(Directions direction)
        {
            switch (direction)
            {
                case Directions.UP:
                    return Directions.DOWN;
                case Directions.UPRIGHT:
                    return Directions.DOWNLEFT;
                case Directions.RIGHT:
                    return Directions.LEFT;
                case Directions.DOWNRIGHT:
                    return Directions.UPLEFT;
                case Directions.DOWN:
                    return Directions.UP;
                case Directions.DOWNLEFT:
                    return Directions.UPRIGHT;
                case Directions.LEFT:
                    return Directions.RIGHT;
                case Directions.UPLEFT:
                    return Directions.DOWNRIGHT;
                default:
                    throw new System.Exception("[GetOppositeDirection] - Unexpected direction");
            }
        }
        public static Directions GetClockWiseDirection(Directions direction)
        {
            switch (direction)
            {
                case Directions.UP:
                    return Directions.RIGHT;
                case Directions.DOWN:
                    return Directions.LEFT;
                case Directions.LEFT:
                    return Directions.UP;
                case Directions.RIGHT:
                    return Directions.DOWN;
                default:
                    throw new System.Exception("[GetClockWiseDirection] - Unexpected direction");
            }
        }
        public static Directions GetCounterClockWiseDirection(Directions direction)
        {
            switch (direction)
            {
                case Directions.UP:
                    return Directions.LEFT;
                case Directions.DOWN:
                    return Directions.RIGHT;
                case Directions.LEFT:
                    return Directions.DOWN;
                case Directions.RIGHT:
                    return Directions.UP;
                default:
                    throw new System.Exception("[GetCounterClockWiseDirection] - Unexpected direction");
            }
        }

        public static Directions GetEdgeDirection(Position position, Board board)
        {
            if (position.Row == board.TotalRows)
            {
                return Directions.UP;
            }
            else if (position.Row == 1)
            {
                return Directions.DOWN;
            }
            else if (position.Col == DefaultValues.FIRST_COLUMN)
            {
                return Directions.LEFT;
            }
            else if (position.Col == DefaultValues.FIRST_COLUMN + board.TotalCols - 1)
            {
                return Directions.RIGHT;
            }
            else throw new System.Exception("[GetEdgeDirection] - Position not on edge");
        }
        public static List<Directions> GetClockWiseDirections()
        {
            return new List<Directions>()
            {
                Directions.UP,
                Directions.RIGHT,
                Directions.DOWN,
                Directions.LEFT
            };
        }
        public static List<Directions> GetAllClockWiseDirections()
        {
            return new List<Directions>()
            {
                Directions.UP,
                Directions.UPRIGHT,
                Directions.RIGHT,
                Directions.DOWNRIGHT,
                Directions.DOWN,
                Directions.DOWNLEFT,
                Directions.LEFT,
                Directions.UPLEFT                
            };
        }
        public static Directions GetPositionsDirection(Position start, Position target)
        {
            if (start.Row > target.Row)
            {
                return Directions.DOWN;
            }
            if (start.Row < target.Row)
            {
                return Directions.UP;
            }
            if (start.Col > target.Col)
            {
                return Directions.LEFT;
            }
            if (start.Col < target.Col)
            {
                return Directions.RIGHT;
            }
            throw new CustomGenericException(typeof(PositionUtils).Name, MethodBase.GetCurrentMethod().Name, string.Format("Unexpected positions: Start={0} Target={1}", start, target));
        }
        /// <summary>
        /// Get the ranges of positions from start to end (excluded)
        /// </summary>
        /// <param name="start">The starting position</param>
        /// <param name="end">The ending position</param>
        /// <returns>The range (start, end)</returns>
        public static List<Position> GetPositionsRange(Position start, Position end)
        {
            List<Position> range = new List<Position>();
            if (start.Subtract(end) > 0)
            {
                if (start.Row != end.Row)
                {
                    for (int i = 1; i < start.Subtract(end); i++)
                    {
                        range.Add(new Position(start.Row - i, start.Col));
                    }
                }
                else
                {
                    for (int i = 1; i < start.Subtract(end); i++)
                    {
                        range.Add(new Position(start.Row, (char)(start.Col - i)));
                    }
                }
            }
            else
            {
                if (start.Row != end.Row)
                {
                    for (int i = 1; i < end.Subtract(start); i++)
                    {
                        range.Add(new Position(start.Row + i, end.Col));
                    }
                }
                else
                {
                    for (int i = 1; i < end.Subtract(start); i++)
                    {
                        range.Add(new Position(end.Row, (char)(start.Col + i)));
                    }
                }
            }
            return range;
        }
    }
}
