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
        public override string ToString()
        {
            return this.Col.ToString().ToUpper() + " : " + this.Row;
        }
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
    }
}
