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
            AddPieceIfNotNull(HasCapturedPiece(piece, board, Directions.UP), capturedPieces);
            AddPieceIfNotNull(HasCapturedPiece(piece, board, Directions.DOWN), capturedPieces);
            AddPieceIfNotNull(HasCapturedPiece(piece, board, Directions.RIGHT), capturedPieces);
            AddPieceIfNotNull(HasCapturedPiece(piece, board, Directions.LEFT), capturedPieces);
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
                            if (otherPiece.PieceColors == piece.PieceColors)
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
            bool up = CheckKingSurroundings(king.Position, Directions.UP, board);
            bool down = CheckKingSurroundings(king.Position, Directions.DOWN, board);
            bool right = CheckKingSurroundings(king.Position, Directions.RIGHT, board);
            bool left = CheckKingSurroundings(king.Position, Directions.LEFT, board);

            return up && down && right && left;
        }
        private bool CheckKingSurroundings(Position kingPosition, Directions direction, Board board)
        {
            if (IsPositionUpdateValid(kingPosition, direction, board.TotalRows, board.TotalCols))
            {
                IPiece piece = board.At(kingPosition.MoveTo(direction));
                return piece != null
                    ||
                    (IsMoveOnThrone(kingPosition.MoveTo(direction), board.TotalRows, board.TotalCols)
                    ||
                    IsMoveOnBoardCorner(kingPosition.MoveTo(direction), board.TotalRows, board.TotalCols));
            }
            return true;
        }
    }
}
