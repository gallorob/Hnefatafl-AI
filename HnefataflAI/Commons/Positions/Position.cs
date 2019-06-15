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
            }
            return newPosition;
        }
        public override string ToString()
        {
            return this.Col.ToString().ToUpper() + ":" + this.Row;
        }
        public override bool Equals(object obj)
        {
            var otherPosition = (Position) obj;
            return this.Col == otherPosition.Col && this.Row == otherPosition.Row;
        }
        public override int GetHashCode()
        {
            var hashCode = 1084646500;
            hashCode = hashCode * -1521134295 + Row.GetHashCode();
            hashCode = hashCode * -1521134295 + Col.GetHashCode();
            return hashCode;
        }
    }
}
