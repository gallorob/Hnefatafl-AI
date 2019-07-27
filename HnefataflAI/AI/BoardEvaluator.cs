using HnefataflAI.Commons;
using HnefataflAI.Commons.DataTypes;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games.Boards;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System;

namespace HnefataflAI.AI
{
    public class BoardEvaluator
    {
        public int EvaluateBoard(Board board, PieceColors playerColor)
        {
            int boardValue = 0;
            Matrix<IPiece> matrix = board.GetCurrentBoard();
            foreach (IPiece piece in matrix.GetRange(0, 0, board.TotalRows))
            {
                if (!(piece is null))
                {
                    if (piece is King)
                    {
                        boardValue += PieceValues.KingValue
                            *
                            (IsPieceUnderAttack(piece, board) ? PieceValues.KingUnderAttackMultiplier : 1)
                            *
                            (IsKingOnCorner(piece, board) ? PieceValues.KingPositionMultiplier : 1)
                            *
                            (piece.PieceColors == playerColor ? 1 : -1);
                    }
                    else
                    {
                        boardValue += PieceValues.PawnValue
                            *
                            (IsPieceUnderAttack(piece, board) ? PieceValues.PawnUnderAttackMultiplier : 1)
                            *
                            (piece.PieceColors == playerColor ? 1 : -1);
                    }
                }
            }
            boardValue += CheckLines(matrix, board.TotalRows, board.TotalCols, playerColor);
            return boardValue;
        }
        private bool IsPieceUnderAttack(IPiece piece, Board board)
        {
            bool underAttack = false;
            foreach (Directions direction in Enum.GetValues(typeof(Directions)))
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
            if (BoardUtils.IsPositionUpdateValid(piece.Position, direction, board))
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
                        (BoardUtils.IsOnBoardCorner(otherPosition, board)
                        ||
                        BoardUtils.IsOnThrone(otherPosition, board));
                }
            }
            return false;
        }
        private bool IsKingOnCorner(IPiece king, Board board)
        {
            return BoardUtils.IsOnBoardCorner(king.Position, board);
        }
        private int CheckLines(Matrix<IPiece> matrix, int rows, int cols, PieceColors pieceColor)
        {
            int boardValue = 0;
            // check rows
            for (int i = 0; i < rows; i++)
            {
                boardValue += EvaluteSingleLines(matrix.GetRow(i), pieceColor);
            }
            // check columns
            for (int i = 0; i < cols; i++)
            {
                boardValue += EvaluteSingleLines(matrix.GetCol(i), pieceColor);
            }
            return boardValue;
        }
        private int EvaluteSingleLines(IPiece[] line, PieceColors pieceColor)
        {
            int boardValue = 0;
            if (IsLineEmpty(line))
            {
                // open line are good for white
                boardValue += PieceValues.OpenLineValue * (pieceColor.Equals(PieceColors.WHITE) ? 1 : -1);
            }
            else
            {
                if (IsKingOnLine(line))
                {
                    boardValue += PieceValues.KingOnOpenLine * (pieceColor.Equals(PieceColors.WHITE) ? 1 : -1);
                }
                boardValue += PieceValues.PawnOnOpenLine * CountPiecesOnLine(line, pieceColor);
                boardValue += PieceValues.PawnOnOpenLine * CountPiecesOnLine(line, PieceColorsUtils.GetOppositePieceColor(pieceColor)) * -1;
            }
            return boardValue;
        }
        private bool IsLineEmpty(IPiece[] line)
        {
            foreach (IPiece piece in line)
            {
                if (piece != null)
                {
                    return false;
                }
            }
            return true;
        }
        private bool IsKingOnLine(IPiece[] line)
        {
            foreach (IPiece piece in line)
            {
                if (piece != null && piece is King)
                {
                    return true;
                }
            }
            return false;
        }
        private int CountPiecesOnLine(IPiece[] line, PieceColors pieceColor)
        {
            int count = 0;
            foreach (IPiece piece in line)
            {
                if (piece != null && piece.PieceColors.Equals(pieceColor) && !(piece is King))
                {
                    count++;
                }
            }
            return count;
        }
    }
}
