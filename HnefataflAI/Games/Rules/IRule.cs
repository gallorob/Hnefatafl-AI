using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Games.Boards;
using HnefataflAI.Pieces;
using System.Collections.Generic;

namespace HnefataflAI.Games.Rules
{
    /// <summary>
    /// The interface for a Rule
    /// </summary>
    public interface IRule
    {
        #region Properties
        /// <summary>
        /// The Rule type
        /// </summary>
        RuleTypes RuleType { get; }
        bool IsCornerEscape { get; }
        bool IsEdgeEscape { get; }
        bool AllowEncirclement { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Check if the king is captured
        /// </summary>
        /// <param name="king">The King piece</param>
        /// <param name="board">The board</param>
        /// <returns>Whether or not the king is captured</returns>
        //bool CheckIfKingIsCaptured(IPiece king, Board board);
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
        bool CheckIfUnderThreat(IPiece piece, Board board);
        /// <summary>
        /// Get the moves for a piece in all directions
        /// </summary>
        /// <param name="piece">The piece</param>
        /// <param name="board">The board</param>
        /// <returns>The moves for a piece in all directions</returns>
        List<Move> GetMovesForPiece(IPiece piece, Board board);
        CaptureRuleSet GetCaptureRuleSet();
        MoveRuleSet GetMoveRuleSet();
        #endregion
    }
}
