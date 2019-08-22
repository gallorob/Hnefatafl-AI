using HnefataflAI.Commons.Converter;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Defaults;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Rules;
using HnefataflAI.Games.Rules.Impl;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using System.Collections.Generic;
using System.Linq;

namespace HnefataflAI.Commons.Utils
{
    public class RuleUtils
    {
        /// <summary>
        /// Get the default rule given its type
        /// </summary>
        /// <param name="ruleType">The rule type</param>
        /// <returns>The default rule</returns>
        public static IRule GetRule(RuleTypes ruleType)
        {
            switch (ruleType)
            {
                case RuleTypes.HNEFATAFL:
                    return new HnefataflRule();
                case RuleTypes.TABLUT:
                    return new TablutRule();
                default:
                    return null;
            }
        }
        /// <summary>
        /// Check if the last N moves are the same.
        /// </summary>
        /// <param name="moves">The list of moves</param>
        /// <param name="movesRepetition">The N</param>
        /// <returns>Whether or not the last N moves are the same</returns>
        public static bool CheckIfHasRepeatedMoves(List<Move> moves, int movesRepetition)
        {
            bool isRepeated = true;
            if (moves.Count >= movesRepetition * 2 - 1)
            {
                for (int i = 1; i < movesRepetition; i += 2)
                {
                    isRepeated &= moves[moves.Count - i].Equals(moves[moves.Count - i - 2]);
                }
                return isRepeated;
            }
            return !isRepeated;
        }
        /// <summary>
        /// Check if the attacker has completely surrounded (cut off access to the edge) the defender
        /// </summary>
        /// <param name="board">The board</param>
        /// <returns>Wheter or not the defender is encircled</returns>
        public static bool CheckIfHasEncircled(Board board)
        {
            bool hasReachedEdge = false;
            HashSet<Position> positionsToCheck = new HashSet<Position>()
            {
                board.GetCenterPosition()
            };
            List<Position> checkedPosition = new List<Position>();
            while (!hasReachedEdge && positionsToCheck.Count > 0)
            {
                HashSet<Position> nextBatch = new HashSet<Position>();
                foreach (Position position in positionsToCheck)
                {
                    if (checkedPosition.Contains(position))
                    {
                        continue;
                    }
                    IPiece piece = board.At(position);
                    if (piece == null || !piece.PieceColors.Equals(PieceColors.BLACK))
                    {
                        foreach (Directions direction in PositionUtils.GetClockWiseDirections())
                        {
                            nextBatch.Add(position.MoveTo(direction));
                        }
                    }
                    if (BoardUtils.IsOnEdge(position, board))
                    {
                        hasReachedEdge = true;
                    }
                    checkedPosition.Add(position);
                }
                positionsToCheck = nextBatch;
            }
            if (hasReachedEdge)
            {
                return !hasReachedEdge;
            }
            // check if there are any defender pieces outside of the encirclement as well
            HashSet<IPiece> externalPieces = new HashSet<IPiece>();
            foreach (Directions direction in PositionUtils.GetClockWiseDirections())
            {
                PopulateForDirection(direction, board, externalPieces);
            }
            bool hasDefenders = externalPieces.FirstOrDefault(piece => piece.PieceColors.Equals(PieceColors.WHITE)) != null;
            return !hasReachedEdge && !hasDefenders;
        }
        /// <summary>
        /// Populate the set with the most external pieces in the board
        /// </summary>
        /// <param name="direction">The direction</param>
        /// <param name="board">The board</param>
        /// <param name="externalPieces">The set to populate</param>
        private static void PopulateForDirection(Directions direction, Board board, HashSet<IPiece> externalPieces)
        {
            int limit = 0;
            switch (direction)
            {
                case Directions.DOWN:
                case Directions.UP:
                    limit = board.TotalCols;
                    break;
                case Directions.LEFT:
                case Directions.RIGHT:
                    limit = board.TotalRows;
                    break;
            }
            for (int i = 0; i < limit; i++)
            {
                IPiece seen = BoardUtils.GetFirstPieceInDirection(board, direction, i);
                if (seen != null)
                {
                    externalPieces.Add(seen);
                }
            }
        }

        public static bool IsInDrawFort(Board board, CaptureRuleSet captureRuleSet)
        {
            IPiece king = board.GetPieces().Where(piece => piece is King).First();
            List<Move> kingMoves = new List<Move>();
            foreach (Directions direction in PositionUtils.GetClockWiseDirections())
            {
                kingMoves.AddRange(MoveUtils.GetMovesForPiece(king, board, direction, captureRuleSet.moveRuleSet));
            }
            if (kingMoves.Count == 0)
            {
                return false;
            }
            return ValidateFort(board, captureRuleSet, king);
        }

        public static bool IsInExitFort(Board board, CaptureRuleSet captureRuleSet)
        {
            IPiece king = board.GetPieces().Where(piece => piece is King).First();
            List<Move> kingMoves = new List<Move>();
            foreach (Directions direction in PositionUtils.GetClockWiseDirections())
            {
                kingMoves.AddRange(MoveUtils.GetMovesForPiece(king, board, direction, captureRuleSet.moveRuleSet));
            }
            // make sure either the king is on edge, has at least a move and one of his moves reaches the edge
            if (!BoardUtils.IsOnEdge(king.Position, board) && (kingMoves.Count == 0 || kingMoves.Where(move => BoardUtils.IsOnEdge(move.To, board)).FirstOrDefault() == null))
            {
                return false;
            }
            return ValidateFort(board, captureRuleSet, king);
        }

        private static bool ValidateFort(Board board, CaptureRuleSet captureRuleSet, IPiece king)
        {
            bool isFortValid = true;
            List<IPiece> closestDefenders = new List<IPiece>();
            HashSet<Position> checkedPositions = new HashSet<Position>();
            HashSet<Position> positionsToCheck = new HashSet<Position>
            {
                king.Position
            };
            while (isFortValid && positionsToCheck.Count > 0)
            {
                HashSet<Position> nextBatch = new HashSet<Position>();
                foreach (Position position in positionsToCheck)
                {
                    if (checkedPositions.Contains(position))
                    {
                        continue;
                    }
                    IPiece piece = board.At(position);
                    if (piece == null || piece.Equals(king))
                    {
                        foreach (Directions direction in PositionUtils.GetClockWiseDirections())
                        {
                            if (BoardUtils.IsPositionMoveValid(position, direction, board))
                            {
                                nextBatch.Add(position.MoveTo(direction));
                            }
                        }
                    }
                    else
                    {
                        if (piece.PieceColors.Equals(PieceColors.WHITE))
                        {
                            closestDefenders.Add(piece);
                        }
                        else
                        {
                            // if there's an attacker inside the fort, it's invalid
                            isFortValid = false;
                        }
                    }
                    checkedPositions.Add(position);
                }
                positionsToCheck = nextBatch;
            }
            List<Directions> clockwiseDirections = PositionUtils.GetAllClockWiseDirections();
            foreach (IPiece defender in closestDefenders)
            {
                bool[] surroundings = new bool[8];
                for (int i = 0; i < surroundings.Length; i++)
                {
                    Directions direction = clockwiseDirections[i];
                    if (BoardUtils.IsPositionMoveValid(defender.Position, direction, board))
                    {
                        Position newPosition = defender.Position.MoveTo(direction);
                        if (!checkedPositions.Contains(newPosition))
                        {
                            IPiece piece = board.At(newPosition);
                            surroundings[i] = piece == null || piece.PieceColors.Equals(PieceColors.BLACK)
                                || (captureRuleSet.isCornerHostileToPawns && BoardUtils.IsOnCorner(newPosition, board))
                                || (captureRuleSet.isThroneHostileToPawns && BoardUtils.IsOnThrone(newPosition, board))
                                || (captureRuleSet.isEdgeHostileToPawns && BoardUtils.IsOnEdge(newPosition, board));
                        }
                    }
                }
                int value = ArrayToSingleValueConverter.Convert(surroundings);
                int idx = captureRuleSet.piecesForPawn;
                if (DefaultValues.UNSAFE_DICT.ContainsKey(idx))
                {
                    foreach (int num in DefaultValues.UNSAFE_DICT[idx])
                    {
                        if ((value & num) == num)
                        {
                            isFortValid = false;
                        }
                    }
                }
                if (!isFortValid)
                {
                    return isFortValid;
                }
            }
            return isFortValid;
        }
    }
}
