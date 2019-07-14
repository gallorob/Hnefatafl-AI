using HnefataflAI.Commons;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Defaults;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System;
using System.Collections.Generic;

namespace HnefataflAI.Games.Rules.Impl
{
    class HnefataflRule : IRule
    {
        /// <summary>
        /// The Rule type
        /// </summary>
        public RuleTypes RuleType { get; private set; }
        /// <summary>
        /// How many tiles can a pawn move
        /// </summary>
        public int PawnMoveLimiter { get; private set; }
        /// <summary>
        /// How many tiles can the king move
        /// </summary>
        public int KingMoveLimiter { get; private set; }
        /// <summary>
        /// How many pieces are needed to capture the king
        /// </summary>
        public int KingPiecesForCapture { get; private set; }
        /// <summary>
        /// Does the throne count as a capture piece for king as well?
        /// </summary>
        public bool IsThroneHostileToAll { get; private set; }
        /// <summary>
        /// Does the corner count as a capture piece for king as well?
        /// </summary>
        public bool IsCornerHostileToAll { get; private set; }
        /// <summary>
        /// How many moves it takes to repeat before losing the game
        /// </summary>
        public int MovesRepetition { get; private set; }
        public HnefataflRule()
        {
            this.RuleType = RuleTypes.HNEFATAFL;
            this.PawnMoveLimiter = Math.Max(DefaultValues.MAX_COLS, DefaultValues.MAX_ROWS);
            this.KingMoveLimiter = this.PawnMoveLimiter;
            this.KingPiecesForCapture = 4;
            this.IsThroneHostileToAll = true;
            this.IsCornerHostileToAll = true;
            this.MovesRepetition = 3;
        }
        public List<IPiece> CheckIfCaptures(IPiece piece, Board board)
        {
            List<IPiece> capturedPieces = new List<IPiece>();
            foreach (Directions direction in Enum.GetValues(typeof(Directions)))
            {
                AddPieceIfNotNull(HasCapturedPiece(piece, board, direction), capturedPieces);
            }
            return capturedPieces;
        }
        private IPiece HasCapturedPiece(IPiece piece, Board board, Directions direction)
        {
            if (BoardUtils.IsPositionUpdateValid(piece.Position, direction, board.TotalRows, board.TotalCols))
            {
                IPiece middlePiece = board.At(piece.Position.MoveTo(direction));
                if (middlePiece != null && middlePiece.PieceColors != piece.PieceColors)
                {
                    if (BoardUtils.IsPositionUpdateValid(middlePiece.Position, direction, board.TotalRows, board.TotalCols))
                    {
                        IPiece otherPiece = board.At(middlePiece.Position.MoveTo(direction));
                        if (otherPiece != null)
                        {
                            if (otherPiece.PieceColors == piece.PieceColors && !(otherPiece is King))
                            {
                                return middlePiece;
                            }
                        }
                        else if (
                            BoardUtils.IsOnThrone(middlePiece.Position.MoveTo(direction), board.TotalRows, board.TotalCols)
                            ||
                            BoardUtils.IsOnBoardCorner(middlePiece.Position.MoveTo(direction), board.TotalRows, board.TotalCols)
                            )
                        {
                            return middlePiece;
                        }
                    }
                    else if (middlePiece is King)
                    {
                        return middlePiece;
                    }
                }
            }
            return null;
        }
        private void AddPieceIfNotNull(IPiece piece, List<IPiece> pieces)
        {
            if (piece != null)
            {
                pieces.Add(piece);
            }
        }
        public bool CheckIfKingIsCaptured(IPiece king, Board board)
        {
            bool captured = true;
            foreach (Directions direction in Enum.GetValues(typeof(Directions)))
            {
                captured &= CheckKingSurroundings(king.Position, direction, board);
            }
            return captured;
        }
        private bool CheckKingSurroundings(Position kingPosition, Directions direction, Board board)
        {
            if (BoardUtils.IsPositionUpdateValid(kingPosition, direction, board.TotalRows, board.TotalCols))
            {
                IPiece piece = board.At(kingPosition.MoveTo(direction));
                return piece != null
                    && piece.PieceColors.Equals(PieceColors.BLACK)
                    ||
                    (BoardUtils.IsOnThrone(kingPosition.MoveTo(direction), board.TotalRows, board.TotalCols)
                    ||
                    BoardUtils.IsOnBoardCorner(kingPosition.MoveTo(direction), board.TotalRows, board.TotalCols));
            }
            return true;
        }
        public bool CheckIfMoveIsValid(IPiece piece, Position to)
        {
            try
            {
                MoveUtils.ValidateMove(piece.Position, to);
            }
            catch (InvalidInputException)
            {
                return false;
            }
            return true;
        }
        public bool CheckIfHasRepeatedMoves(List<Move> moves)
        {
            bool isRepeated = true;
            if (moves.Count >= this.MovesRepetition * 2 - 1)
            {
                for (int i = 1; i < this.MovesRepetition; i += 2)
                {
                    isRepeated &= moves[moves.Count - i].Equals(moves[moves.Count - i - 2]);
                }
                return isRepeated;
            }
            return false;
        }
    }
}
