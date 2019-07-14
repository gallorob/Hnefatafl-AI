using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Pieces;
using System.Collections.Generic;

namespace HnefataflAI.Games.Rules
{
    /// <summary>
    /// The interface for a Rule
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// The Rule type
        /// </summary>
        RuleTypes RuleType { get; }
        /// <summary>
        /// How many tiles can a pawn move
        /// </summary>
        int PawnMoveLimiter { get; }
        /// <summary>
        /// How many tiles can the king move
        /// </summary>
        int KingMoveLimiter { get; }
        /// <summary>
        /// How many pieces are needed to capture the king
        /// </summary>
        int KingPiecesForCapture { get; }
        /// <summary>
        /// Does the throne count as a capture piece for king as well?
        /// </summary>
        bool IsThroneHostileToAll { get; }
        /// <summary>
        /// Does the corner count as a capture piece for king as well?
        /// </summary>
        bool IsCornerHostileToAll { get; }
        /// <summary>
        /// How many moves it takes to repeat before losing the game
        /// </summary>
        int MovesRepetition { get; }
        /// <summary>
        /// Check if the king is captured
        /// </summary>
        /// <param name="king">The King piece</param>
        /// <param name="board">The board</param>
        /// <returns>Whether or not the king is captured</returns>
        bool CheckIfKingIsCaptured(IPiece king, Board board);
        /// <summary>
        /// CHeck if the piece captures anything on the board
        /// </summary>
        /// <param name="piece">The move's piece</param>
        /// <param name="board">The board</param>
        /// <returns>A list of all captures by the piece (possible King captures as well)</returns>
        List<IPiece> CheckIfCaptures(IPiece piece, Board board);
        /// <summary>
        /// Check if a move is valid
        /// </summary>
        /// <param name="piece">The moving piece</param>
        /// <param name="to">The destination position</param>
        /// <returns>Whether or not the move is valid</returns>
        bool CheckIfMoveIsValid(IPiece piece, Position to);
        /// <summary>
        /// Check if the moves are repeated
        /// </summary>
        /// <param name="moves">The list of moves</param>
        /// <returns>Whether or not the moves are repeated</returns>
        bool CheckIfHasRepeatedMoves(List<Move> moves);
    }
}
