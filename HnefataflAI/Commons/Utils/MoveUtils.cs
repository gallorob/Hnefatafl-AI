using System;
using System.Collections.Generic;
using System.Reflection;
using HnefataflAI.Commons.Exceptions;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Rules;
using HnefataflAI.Pieces;

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
        /// <param name="moveLimiter">The maximum number of moves the piece can make in the direction</param>
        /// <param name="canMoveOnThrone">If the piece can move on the throne</param>
        /// <param name="canMoveOnCorner">If the piece can move on the board corner</param>
        /// <param name="canMoveOnEnemyBaseCamp">If the piece can move on the enemy base camp</param>
        /// <param name="canMoveOnBaseCamp">If the piece can move on an ally base camp</param>
        /// <returns>The moves for a piece in a given direction</returns>
        public static List<Move> GetMovesForPiece(IPiece piece, Board board, Directions direction, int moveLimiter, bool canMoveOnThrone, bool canMoveOnCorner, bool canMoveOnEnemyBaseCamp, bool canMoveOnBaseCamp)
        {
            int counter = 0;
            List<Move> availableMoves = new List<Move>();
            Position moved = piece.Position;
            while (counter < moveLimiter && BoardUtils.IsPositionMoveValid(moved, direction, board))
            {
                moved = moved.MoveTo(direction);
                counter++;
                // todo: refactor to use a moveruleset and add if anchor tiles are physical obstacles (throne, base camps...)
                if (board.At(moved) != null)
                {
                    break;
                }
                if (CanMoveToPosition(piece, moved, board, canMoveOnThrone, canMoveOnCorner, canMoveOnEnemyBaseCamp, canMoveOnBaseCamp))
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
        /// <param name="canMoveOnThrone">If the piece can move on the throne</param>
        /// <param name="canMoveOnCorner">If the piece can move on the corner</param>
        /// <param name="canMoveOnEnemyBaseCamp">If the piece can move on the enemy base camp</param>
        /// <param name="canMoveOnBaseCamp">If the piece can move on an ally base camp</param>
        /// <returns>If a piece can move to a position</returns>
        public static bool CanMoveToPosition(IPiece piece, Position position, Board board, bool canMoveOnThrone = true, bool canMoveOnCorner = true, bool canMoveOnEnemyBaseCamp = true, bool canMoveOnBaseCamp = true)
        {
            if (BoardUtils.IsOnThrone(position, board))
            {
                return canMoveOnThrone;
            }
            else if (BoardUtils.IsOnCorner(position, board))
            {
                return canMoveOnCorner;
            }
            else if (BoardUtils.IsOnEnemyCamp(position, board, piece.PieceColors))
            {
                return canMoveOnEnemyBaseCamp;
            }
            else if (BoardUtils.IsOnEnemyCamp(position, board, PieceColorsUtils.GetOppositePieceColor(piece.PieceColors)))
            {
                return canMoveOnBaseCamp;
            }
            else return true;
        }
        //public static List<Move> GetCapturingMoves(List<Move> moves, Board board, RuleTypes ruleType)
        //{
        //    List<Move> capturingMoves = new List<Move>();
        //    foreach (Move move in moves)
        //    {
        //        if (RuleUtils.GetRule(ruleType).CheckIfCaptures(move.Piece, board).Count != 0)
        //        {
        //            capturingMoves.Add(move);
        //        }
        //    }
        //    return capturingMoves;
        //}
        //public static List<Move> GetEscapingMoves(List<Move> moves, Board board, RuleTypes ruleType)
        //{
        //    List<Move> escapingMoves = new List<Move>();
        //    foreach (Move move in moves)
        //    {
        //        if (RuleUtils.GetRule(ruleType).CheckIfUnderCapture(move.Piece, board))
        //        {
        //            escapingMoves.Add(move);
        //        }
        //    }
        //    return escapingMoves;
        //}
    }
}
