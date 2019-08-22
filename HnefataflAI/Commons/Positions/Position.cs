using System;
using System.Collections.Generic;

namespace HnefataflAI.Commons.Positions
{
    public struct Position
    {
        public Position(int row, char col)
            : this()
        {
            this.Row = row;
            this.Col = col;
        }
        public int Row { get; private set; }
        public char Col { get; private set; }
        public Position MoveTo(Directions direction)
        {
            Position newPosition = (Position) this.MemberwiseClone();
            switch (direction)
            {
                case Directions.UP:
                    newPosition.Row++;
                    break;
                case Directions.DOWN:
                    newPosition.Row--;
                    break;
                case Directions.LEFT:
                    newPosition.Col--;
                    break;
                case Directions.RIGHT:
                    newPosition.Col++;
                    break;
                case Directions.UPRIGHT:
                    newPosition.Row++;
                    newPosition.Col++;
                    break;
                case Directions.DOWNRIGHT:
                    newPosition.Row--;
                    newPosition.Col++;
                    break;
                case Directions.DOWNLEFT:
                    newPosition.Row--;
                    newPosition.Col--;
                    break;
                case Directions.UPLEFT:
                    newPosition.Row++;
                    newPosition.Col--;
                    break;
            }
            return newPosition;
        }
        public override string ToString()
        {
            return this.Col.ToString().ToUpper() + this.Row;
        }
        public override bool Equals(object obj)
        {
            var otherPosition = (Position) obj;
            return this.Col == otherPosition.Col && this.Row == otherPosition.Row;
        }
        public override int GetHashCode()
        {
			return base.GetHashCode();
        }

        internal int Subtract(Position other)
        {
            if (this.Row == other.Row)
            {
                return this.Col - other.Col;
            }
            else if (this.Col == other.Col)
            {
                return this.Row - other.Row;
            }
            else return -1;
        }
    }
}
