using HnefataflAI.Commons;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System;
using System.Collections.Generic;

namespace HnefataflAI.Games.Rules.Impl
{
    class HnefataflRule : IRule
    {
        public RuleTypes RuleType { get; private set; }
        public HnefataflRule()
        {
            RuleType = RuleTypes.HNEFATAFL;
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
            if (moves.Count >= 5)
            {
                return moves[moves.Count - 1].Equals(moves[moves.Count - 3])
                    &&
                    moves[moves.Count - 3].Equals(moves[moves.Count - 5]);
            }
            return false;
        }
    }
}
