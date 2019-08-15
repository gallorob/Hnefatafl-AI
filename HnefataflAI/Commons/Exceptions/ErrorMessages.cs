namespace HnefataflAI.Commons.Exceptions
{
    static class ErrorMessages
    {
        // board errors
        public static readonly string INVALID_BOARD_DIMENSION = "Invalid board dimensions";
        public static readonly string MISSING_KING = "King piece is missing";
        public static readonly string MISSING_ATTACKERS = "There are no attackers";
        public static readonly string MISSING_DEFENDERS = "There are no defenders";
        public static readonly string INVALID_A_TO_D_RATIO = "The attackers should be at least twice the number of defenders (minus the King)";
        // move errors
        public static readonly string INVALID_INPUT = "Input is not a valid move input";
        public static readonly string INVALID_IPUT_POSITION = "Input is not a valid position";
        public static readonly string POSITION_OUT_OF_BOARD = "The position is outside the board space";
        public static readonly string NULL_PIECE = "The selected position refers to a nonexistent piece";
        public static readonly string OPPONENT_PIECE = "The selected position refers to an opponent's piece";
        public static readonly string INVALID_DESTINATION_PIECE = "The selected position is already occupied";
        public static readonly string INVALID_DESTINATION_POSITION = "The selected piece can't move to that position";
    }
}
