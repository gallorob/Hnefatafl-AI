using HnefataflAI.Commons;
using HnefataflAI.Commons.DataTypes;
using HnefataflAI.Games;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;

namespace HnefataflAI.AI
{
    public class MovesEvaluator
    {
        public int EvaluateBoard(Board board, PieceColors playerColor)
        {
            int boardValue = 0;
            Matrix<IPiece> matrix = board.GetCurrentBoard();
            foreach(IPiece piece in matrix.GetRange(0, 0, board.TotalRows))
            {
                if (!(piece is null))
                {
                    if (piece is King)
                    {
                        boardValue += new PieceValues().pieceValues["King"]
                            *
                            (piece.PieceColors == playerColor ? 1 : -1);
                    }
                    else
                    {
                        boardValue += new PieceValues().pieceValues["Pawn"]
                            *
                            (piece.PieceColors == playerColor ? 1 : -1);
                    }
                }
            }
            return boardValue;
        }
    }
}
