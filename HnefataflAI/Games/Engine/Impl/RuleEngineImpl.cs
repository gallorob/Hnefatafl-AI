using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System;
using System.Collections.Generic;

namespace HnefataflAI.Games.Engine.Impl
{
    public class RuleEngineImpl : IRuleEngine
    {
        public bool CanMoveToPosition(IPiece piece, Position position, int totalRows, int totalCols)
        {
            return (piece is King) || !(IsMoveOnBoardCorner(position, totalRows, totalCols) || IsMoveOnThrone(position, totalRows, totalCols));
        }
        public bool IsPositionUpdateValid(Position moved, Directions direction, int totalRows, int totalCols)
        {
            return BoardUtils.IsPositionUpdateValid(moved, direction, totalRows, totalCols);
        }
        public bool IsPositionValid(Position position, int totalRows, int totalCols)
        {
            return BoardUtils.IsPositionValid(position, totalRows, totalCols);
        }
        public bool IsMoveOnBoardCorner(Position move, int totalRows, int totalCols)
        {
            return BoardUtils.IsOnBoardCorner(move, totalRows, totalCols);
        }
        public bool IsMoveOnThrone(Position move, int totalRows, int totalCols)
        {
            return BoardUtils.IsOnThrone(move, totalRows, totalCols);
        }
        public List<IPiece> CheckIfHasCaptured(IPiece piece, Board board)
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
            if (IsPositionUpdateValid(piece.Position, direction, board.TotalRows, board.TotalCols))
            {
                IPiece middlePiece = board.At(piece.Position.MoveTo(direction));
                if (middlePiece != null && middlePiece.PieceColors != piece.PieceColors)
                {
                    if (IsPositionUpdateValid(middlePiece.Position, direction, board.TotalRows, board.TotalCols))
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
                            IsMoveOnThrone(middlePiece.Position.MoveTo(direction), board.TotalRows, board.TotalCols)
                            ||
                            IsMoveOnBoardCorner(middlePiece.Position.MoveTo(direction), board.TotalRows, board.TotalCols)
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
        public bool CheckKingSurroundings(Position kingPosition, Directions direction, Board board)
        {
            if (IsPositionUpdateValid(kingPosition, direction, board.TotalRows, board.TotalCols))
            {
                IPiece piece = board.At(kingPosition.MoveTo(direction));
                return piece != null
                    && piece.PieceColors.Equals(PieceColors.BLACK)
                    ||
                    (IsMoveOnThrone(kingPosition.MoveTo(direction), board.TotalRows, board.TotalCols)
                    ||
                    IsMoveOnBoardCorner(kingPosition.MoveTo(direction), board.TotalRows, board.TotalCols));
            }
            return true;
        }

        public bool HasRepeatedMoves(List<Move> moves)
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
