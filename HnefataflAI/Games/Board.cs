﻿using HnefataflAI.Commons;
using HnefataflAI.Commons.DataTypes;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Pieces;
using System.Collections.Generic;

namespace HnefataflAI.Games
{
    public class Board
    {
        private readonly Matrix<IPiece> board;
        public int TotalRows { get; private set; }
        public int TotalCols { get; private set; }
        public Board(int numRows, int numCols)
        {
            this.TotalRows = numRows;
            this.TotalCols = numCols;
            this.board = new Matrix<IPiece>(numRows, numCols);
        }
        public Board(Matrix<IPiece> newBoard)
        {
            this.board = newBoard;
            this.TotalRows = newBoard.RowsNumber();
            this.TotalCols = newBoard.ColumnsNumber();
        }
        public Matrix<IPiece> GetCurrentBoard()
        {
            return this.board;
        }
        public void AddPiece(IPiece piece, Position position)
        {
            this.Set(piece, position);
        }
        public void AddPiece(IPiece piece)
        {
            this.AddPiece(piece, piece.Position);
        }
        public void RemovePiece(IPiece piece, Position position)
        {
            IPiece currentPiece = this.At(position);

            if (currentPiece.Equals(piece))
            {
                this.Set(null, position);
            }
        }
        public void RemovePiece(IPiece piece)
        {
            this.RemovePiece(piece, piece.Position);
        }
        private void Set(IPiece piece, Position position)
        {
            int arrRow = BoardUtils.GetArrayRow(this.TotalRows, position.Row);
            int arrCol = BoardUtils.GetArrayCol(position.Col);

            this.board.Set(arrRow, arrCol, piece);
        }
        public IPiece At(Position position)
        {
            int arrRow = BoardUtils.GetArrayRow(this.TotalRows, position.Row);
            int arrCol = BoardUtils.GetArrayCol(position.Col);

            return this.board.At(arrRow, arrCol);
        }
        public override string ToString()
        {
            string result = System.String.Format("\t{0}\n\r", BoardUtils.GetBoardColumnsChars(this.TotalCols));
            for (int i = 0; i < this.TotalRows; i++)
            {
                result += this.TotalRows - i + "\t";
                for (int j = 0; j < this.TotalCols; j++)
                {
                    IPiece piece = this.board.At(i, j);
                    result += piece == null ? " . " : piece.PieceRepresentation();
                }
                result += "\n\r";
            }
            return result;
        }
        public List<IPiece> GetPiecesByColor(PieceColors pieceColor)
        {
            List<IPiece> pieces = new List<IPiece>();

            for (int i = 0; i < this.TotalRows; i++)
            {
                for (int j = 0; j < this.TotalCols; j++)
                {
                    if (this.board.At(i, j) != null)
                    {
                        IPiece piece = this.board.At(i, j);
                        if (piece.PieceColors.Equals(pieceColor))
                            pieces.Add(piece);
                    }
                }
            }
            return pieces;
        }
    }
}
