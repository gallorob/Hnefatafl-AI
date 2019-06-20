using HnefataflAI.Commons;
using HnefataflAI.Commons.DataTypes;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System;

namespace HnefataflAI.AI
{
    public class BoardEvaluator
    {
        public int EvaluateBoard(Board board, PieceColors playerColor)
        {
            PieceValues pieceValues = new PieceValues();
            int boardValue = 0;
            Matrix<IPiece> matrix = board.GetCurrentBoard();
            foreach(IPiece piece in matrix.GetRange(0, 0, board.TotalRows))
            {
                if (!(piece is null))
                {
                    if (piece is King)
                    {
                        boardValue += pieceValues.kingValue
                            *
                            (IsPieceUnderAttack(piece, board) ? pieceValues.kingUnderAttackMultiplier : 1)
                            *
                            (IsKingOnCorner(piece, board) ? pieceValues.kingPositionMultiplier : 1)
                            *
                            (piece.PieceColors == playerColor ? 1 : -1);
                    }
                    else
                    {
                        boardValue += pieceValues.pawnValue
                            *
                            (IsPieceUnderAttack(piece, board) ? pieceValues.pawnUnderAttackMultiplier : 1)
                            *
                            (piece.PieceColors == playerColor ? 1 : -1);
                    }
                }
            }
            return boardValue;
        }
        private bool IsPieceUnderAttack(IPiece piece, Board board)
        {
            bool underAttack = false;
            foreach(Directions direction in Enum.GetValues(typeof(Directions)))
            {
                underAttack = IsPieceUnderAttakByDirection(piece, board, direction, piece is King);
                if (underAttack)
                {
                    break;
                }
            }
            return underAttack;
        }
        private bool IsPieceUnderAttakByDirection(IPiece piece, Board board, Directions direction, bool isKing)
        {
            if (BoardUtils.IsPositionUpdateValid(piece.Position, direction, board.TotalRows, board.TotalCols))
            {
                Position otherPosition = piece.Position.MoveTo(direction);
                IPiece otherPiece = board.At(otherPosition);
                if (otherPiece != null)
                {
                    return !piece.PieceColors.Equals(otherPiece.PieceColors);
                }
                else
                {
                    return !isKing &&
                        (BoardUtils.IsOnBoardCorner(otherPosition, board.TotalRows, board.TotalCols)
                        ||
                        BoardUtils.IsOnThrone(otherPosition, board.TotalRows, board.TotalCols));
                }
            }
            return false;
        }
        private bool IsKingOnCorner(IPiece king, Board board)
        {
            return BoardUtils.IsOnBoardCorner(king.Position, board.TotalRows, board.TotalCols);
        }
    }
}
