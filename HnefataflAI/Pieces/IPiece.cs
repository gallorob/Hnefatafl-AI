using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;

namespace HnefataflAI.Pieces
{
    /// <summary>
    /// The interface for a generic game piece
    /// </summary>
    public interface IPiece
    {
        /// <summary>
        /// The piece's color
        /// </summary>
        PieceColors PieceColors { get; }
        /// <summary>
        /// The piece's position
        /// </summary>
        Position Position { get; }
        /// <summary>
        /// Update the piece's position
        /// </summary>
        /// <param name="newPosition">The piece's new position</param>
        void UpdatePosition(Position newPosition);
        /// <summary>
        /// Get the representing string for the piece
        /// </summary>
        /// <returns>The representing string for the piece</returns>
        string PieceRepresentation();
    }
}
