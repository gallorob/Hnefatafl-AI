using System.Collections.Generic;
using System.Reflection;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Rules;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;

namespace HnefataflAI.Commons.Utils
{
    /// <summary>
    /// Utils class for moves
    /// </summary>
    static class MoveUtils
    {
        /// <summary>
        /// Get the move as an user's input
        /// </summary>
        /// <param name="move">The move</param>
        /// <returns>The move as an user's input</returns>
        public static string[] MoveAsInput(Move move)
        {
            string[] input = new string[2];
            input[0] = PositionUtils.PositionAsInput(move.From);
            input[1] = PositionUtils.PositionAsInput(move.To);
            return input;
        }
        /// <summary>
        /// Get the input as an user's move input
        /// </summary>
        /// <param name="input">The input</param>
        /// <returns>The input as an user's move input</returns>
        public static string[] MoveFromInput(string input)
        {
            string[] move = input.ToLower().Split('-');
            if (move.Length != 2)
            {
                throw new CustomGenericException(typeof(MoveUtils).Name, MethodBase.GetCurrentMethod().Name, ErrorMessages.INVALID_INPUT);
            }
            return move;
        }
        /// <summary>
        /// Validate the move
        /// </summary>
        /// <param name="from">The starting position</param>
        /// <param name="to">The ending position</param>
        internal static void ValidateMove(Position from, Position to)
        {
            if (from.Col != to.Col && from.Row != to.Row)
            {
                throw new CustomGenericException(typeof(MoveUtils).Name, MethodBase.GetCurrentMethod().Name, ErrorMessages.INVALID_DESTINATION_POSITION);
            }
        }
        /// <summary>
        /// Check if the new move is repeated for the given rule
        /// </summary>
        /// <param name="listMoves">The user's moves</param>
        /// <param name="move">The new move to check if repeated</param>
        /// <param name="rule">The current rule</param>
        /// <returns>If the new move is repeated</returns>
        public static bool IsRepeatedMove(List<Move> listMoves, Move move, IRule rule)
        {
            List<Move> tempMoves = new List<Move>(listMoves)
            {
                move
            };
            return rule.CheckIfHasRepeatedMoves(tempMoves);
        }
        /// <summary>
        /// Get the moves for a piece in a given direction
        /// </summary>
        /// <param name="piece">The moving piece</param>
        /// <param name="board">The board</param>
        /// <param name="direction">The direction the piece is moving</param>
        /// <param name="moveRuleSet">The move rule set</param>
        /// <returns>The moves for a piece in a given direction</returns>
        public static List<Move> GetMovesForPiece(IPiece piece, Board board, Directions direction, MoveRuleSet moveRuleSet)
        {
            int counter = 0;
            List<Move> availableMoves = new List<Move>();
            Position moved = piece.Position;
            int moveLimiter;
            if (piece is King)
            {
                moveLimiter = moveRuleSet.kingMovesLimiter;
            }
            // commander
            // elite guard
            else
            {
                moveLimiter = moveRuleSet.pawnMovesLimiter;
            }
            while (counter < moveLimiter && BoardUtils.IsPositionMoveValid(moved, direction, board))
            {
                moved = moved.MoveTo(direction);
                counter++;
                // todo: refactor to use a moveruleset and add if anchor tiles are physical obstacles (throne, base camps...)
                if (board.At(moved) != null)
                {
                    break;
                }
                if (CanMoveToPosition(piece, moved, board, moveRuleSet))
                {
                    Move move = new Move(piece, moved);
                    availableMoves.Add(move);
                }
            }
            return availableMoves;
        }
        /// <summary>
        /// Check if a piece can move to a position
        /// </summary>
        /// <param name="piece">The moving piece</param>
        /// <param name="position">The destination position</param>
        /// <param name="board">The board</param>
        /// <param name="moveRuleSet">The move rule set</param>
        /// <returns>If a piece can move to a position</returns>
        public static bool CanMoveToPosition(IPiece piece, Position position, Board board, MoveRuleSet moveRuleSet)
        {
            if (BoardUtils.IsOnThrone(position, board))
            {
                if (piece is King)
                {
                    return moveRuleSet.canKingLandOnThrone;
                }
                // commander
                // elite guard
                return moveRuleSet.canPawnLandOnThrone;
            }
            else if (BoardUtils.IsOnCorner(position, board))
            {
                if (piece is King)
                {
                    return moveRuleSet.canKingLandOnCorner;
                }
                // commander
                // elite guard
                return moveRuleSet.canPawnLandOnCorner;
            }
            else if (BoardUtils.IsOnEnemyCamp(position, board))
            {
                if (piece is King)
                {
                    return moveRuleSet.canKingLandOnEnemyBaseCamps;
                }
                // commander
                // elite guard
                if (piece.PieceColors.Equals(PieceColors.BLACK))
                {
                    return moveRuleSet.canPawnLandBackOnOwnBaseCamp;
                }
                return moveRuleSet.canPawnLandOnEnemyBaseCamps;
            }
            else return true;
        }
    }
}
