namespace HnefataflAI.Commons.Exceptions
{
    static class ErrorMessages
    {
        public static readonly string INVALID_BOARD = "Can't create a board with those dimensions";
        public static readonly string INVALID_INPUT = "Input is not a valid move input";
        public static readonly string INVALID_IPUT_POSITION = "Input is not a valid position";
        public static readonly string POSITION_OUT_OF_BOARD = "The position is outside the board space";
        public static readonly string NULL_PIECE = "The selected position refers to a nonexistent piece";
        public static readonly string OPPONENT_PIECE = "The selected position refers to a opponent's piece";
        public static readonly string INVALID_DESTINATION_PIECE = "The selected position is already occupied";
        public static readonly string INVALID_DESTINATION_POSITION = "The selected piece can't move to that position";
    }
}
