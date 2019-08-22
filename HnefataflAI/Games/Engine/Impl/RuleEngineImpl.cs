using HnefataflAI.Commons;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.GameState;
using HnefataflAI.Games.Rules;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
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
            return this.Rule.GetMovesForPiece(piece, board);
        }
        public List<IPiece> GetCapturedPieces(IPiece piece, Board board)
        {
            return Rule.CheckIfCaptures(piece, board);
        }
        public PieceColors GetNextPlayer(IPiece piece, GameStatus gameStatus)
        {
            // todo: to implement
            //if (Rule.RuleType is RuleTypes.BERSERK_HNEFATAFL && gameStatus.CapturedPieces.Count > 0)
            //{
            //    return piece.PieceColors;
            //}
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
                        gameStatus.IsGameOver = true;
                        gameStatus.Status = Status.WIN;
                        gameStatus.Reason = "King has been captured";
                    }
                }
            }
            if (!gameStatus.IsGameOver && Rule.IsCornerEscape)
            {
                // winning condition for Defender
                gameStatus.IsGameOver = movedPiece is King && BoardUtils.IsOnCorner(movedPiece.Position, board);
                gameStatus.Status = Status.WIN;
                gameStatus.Reason = "King has escaped (corner)";
            }
            if (!gameStatus.IsGameOver && Rule.IsEdgeEscape)
            {
                // winning condition for Defender
                gameStatus.IsGameOver = movedPiece is King && BoardUtils.IsOnEdge(movedPiece.Position, board);
                gameStatus.Status = Status.WIN;
                gameStatus.Reason = "King has escaped (edge)";
            }
            if (!gameStatus.IsGameOver)
            {
                // winning condition for Defender
                gameStatus.IsGameOver = board.GetPiecesByColor(PieceColors.BLACK).Count == 0;
                gameStatus.Status = Status.WIN;
                gameStatus.Reason = "No more attacker pieces";
            }
            if (!gameStatus.IsGameOver)
            {
                // losing condition for Attacker
                gameStatus.IsGameOver = Rule.CheckIfHasRepeatedMoves(blackMoves);
                gameStatus.Status = Status.LOSS;
                gameStatus.Reason = "Repeated moves for attacker";
            }
            if (!gameStatus.IsGameOver)
            {
                // losing condition for Defender
                gameStatus.IsGameOver = Rule.CheckIfHasRepeatedMoves(whiteMoves);
                gameStatus.Status = Status.LOSS;
                gameStatus.Reason = "Repeated moves for defender";
            }
            if (!gameStatus.IsGameOver)
            {
                if (!CanMove(gameStatus.NextPlayer, board))
                {
                    gameStatus.Status = Status.WIN;
                    gameStatus.IsGameOver = true;
                    gameStatus.Reason = string.Format("No available moves for {0}", PieceColorsUtils.GetRoleFromPieceColor(gameStatus.NextPlayer));
                }
            }
            if (!gameStatus.IsGameOver && Rule.AllowEncirclement)
            {
                if (RuleUtils.CheckIfHasEncircled(board))
                {
                    gameStatus.Status = Status.WIN;
                    gameStatus.IsGameOver = true;
                    gameStatus.Reason = "Attacker has successfully encircled the defender!";
                }
            }
            return gameStatus;
        }

        public bool CanMove(PieceColors playerColor, Board board)
        {
            return GetAvailableMoves(playerColor, board).Count != 0;
        }

        public void UpdatePiecesThreatLevel(Board board)
        {
            foreach (IPiece piece in board.GetPieces())
            {
                piece.IsThreatened = Rule.CheckIfUnderThreat(piece, board);
            }
        }
    }
}
