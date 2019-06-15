using HnefataflAI.Commons;

namespace HnefataflAI.AI
{
    public class MoveValue
    {
        public Move Move { get; set; }
        public int Value { get; set; }
        public MoveValue(Move move, int value)
        {
            this.Move = move;
            this.Value = value;
        }
    }
}
