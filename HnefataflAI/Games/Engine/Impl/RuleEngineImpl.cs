using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games.GameState;
using HnefataflAI.Games.Rules;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System;
using System.Collections.Generic;

namespace HnefataflAI.Games.Engine.Impl
{
    public class RuleEngineImpl : IRuleEngine
    {
        public IRule Rule { get; private set; }
        public RuleEngineImpl(IRule rule)
        {
            this.Rule = rule;
        }
        public List<Move> GetAvailableMoves(PieceColors playerColor, Board board)
        {
            List<Move> availableMoves = new List<Move>();
            List<IPiece> pieces = board.GetPiecesByColor(playerColor);

            foreach (IPiece piece in pieces)
            {
                availableMoves.AddRange(GetAvailableMoves(piece, board));
            }

            return availableMoves;
        }
        public List<Move> GetAvailableMoves(IPiece piece, Board board)
        {
            List<Move> availableMoves = new List<Move>();
            foreach (Directions direction in Enum.GetValues(typeof(Directions)))
            {
                MovesByDirection(direction, piece, availableMoves, board);
            }
            return availableMoves;
        }
        private void MovesByDirection(Directions direction, IPiece piece, List<Move> availableMoves, Board board)
        {
            Position moved = piece.Position;
            while (BoardUtils.IsPositionUpdateValid(moved, direction, board.TotalRows, board.TotalCols))
            {
                moved = moved.MoveTo(direction);
                if (board.At(moved) != null)
                {
                    break;
                }
                if (BoardUtils.CanMoveToPosition(piece, moved, board.TotalRows, board.TotalCols))
                {
                    Move move = new Move(piece, moved);
                    availableMoves.Add(move);
                }
            }
        }
        public List<IPiece> GetCapturedPieces(IPiece piece, Board board)
        {
            return Rule.CheckIfCaptures(piece, board);
        }
        public PieceColors GetNextPlayer(IPiece piece, GameStatus gameStatus)
        {
            if (Rule.RuleType is RuleTypes.BERSERK_HNEFATAFL && gameStatus.CapturedPieces.Count > 0)
            {
                return piece.PieceColors;
            }
            return PieceColorsUtils.GetOppositePieceColor(piece.PieceColors);
        }
        public GameStatus GetGameStatus(IPiece movedPiece, Board board, List<Move> whiteMoves, List<Move> blackMoves)
        {
            List<IPiece> capturedPieces = GetCapturedPieces(movedPiece, board);
            GameStatus gameStatus = new GameStatus(false);
            gameStatus.CapturedPieces.AddRange(capturedPieces);
            gameStatus.NextPlayer = GetNextPlayer(movedPiece, gameStatus);

            if (!(movedPiece is King))
            {
                foreach (IPiece piece in capturedPieces)
                {
                    if (piece is King && !gameStatus.IsGameOver)
                    {
                        // winning condition for Attacker
                        gameStatus.IsGameOver = Rule.CheckIfKingIsCaptured(piece, board);
                        gameStatus.Status = Status.WIN;
                    }
                }
            }
            if (!gameStatus.IsGameOver)
            {
                // winning condition for Defender
                gameStatus.IsGameOver = movedPiece is King && BoardUtils.IsOnBoardCorner(movedPiece.Position, board.TotalRows, board.TotalCols);
                gameStatus.Status = Status.WIN;
            }
            if (!gameStatus.IsGameOver)
            {
                // winning condition for Defender
                gameStatus.IsGameOver = board.GetPiecesByColor(PieceColors.BLACK).Count == 0;
                gameStatus.Status = Status.WIN;
            }
            if (!gameStatus.IsGameOver)
            {
                // losing condition for Attacker
                gameStatus.IsGameOver = Rule.CheckIfHasRepeatedMoves(blackMoves);
                gameStatus.Status = Status.LOSS;
            }
            if (!gameStatus.IsGameOver)
            {
                // losing condition for Defender
                gameStatus.IsGameOver = Rule.CheckIfHasRepeatedMoves(whiteMoves);
                gameStatus.Status = Status.LOSS;
            }
            return gameStatus;
        }
    }
}
