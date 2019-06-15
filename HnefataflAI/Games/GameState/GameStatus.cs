namespace HnefataflAI.Games.GameState
{
    public class GameStatus
    {
        public bool IsGameOver { get; set; }
        public Status Status { get; set; }
        public GameStatus(bool isGameOver)
        {
            this.IsGameOver = isGameOver;
        }
        public GameStatus(bool isGameOver, Status status)
        {
            this.IsGameOver = isGameOver;
            this.Status = status;
        }
    }
}
