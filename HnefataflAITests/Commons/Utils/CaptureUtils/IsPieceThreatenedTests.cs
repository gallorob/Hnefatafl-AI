using HnefataflAI.Commons.Positions;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Rules.Impl;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HnefataflAI.Commons.Utils.Tests
{
    [TestClass()]
    public class IsPieceThreatenedTests
    {
        [TestMethod()]
        public void IsPieceThreatenedDefenderTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();

            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  .  .  .  . 
            /// 5	 .  .  .  .  .  .  . 
            /// 4	 .  .  .  X  .  A  . 
            /// 3	 .  .  .  .  .  D  . 
            /// 2	 .  .  .  A  .  .  . 
            /// 1	 *  .  .  .  .  .  * 
            Board board = new Board(7, 7);
            board.AddPiece(new Defender(new Position(3, 'f')));
            board.AddPiece(new Attacker(new Position(4, 'f')));
            board.AddPiece(new Attacker(new Position(2, 'd')));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            IPiece piece = board.At(new Position(3, 'f'));
            Assert.IsTrue(CaptureUtils.IsPieceThreatened(piece, board, HnefataflCaptureRuleSet));
        }
        [TestMethod()]
        public void IsPieceThreatenedAttackerTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();

            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  .  .  .  . 
            /// 5	 .  .  .  .  .  .  . 
            /// 4	 .  .  .  X  .  D  . 
            /// 3	 .  .  .  .  .  A  . 
            /// 2	 .  .  .  D  .  .  . 
            /// 1	 *  .  .  .  .  .  * 
            Board board = new Board(7, 7);
            board.AddPiece(new Attacker(new Position(3, 'f')));
            board.AddPiece(new Defender(new Position(4, 'f')));
            board.AddPiece(new Defender(new Position(2, 'd')));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            IPiece piece = board.At(new Position(3, 'f'));
            Assert.IsTrue(CaptureUtils.IsPieceThreatened(piece, board, HnefataflCaptureRuleSet));
        }
        [TestMethod()]
        public void IsPieceThreatenedDefenderDoesntReachTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();
            HnefataflCaptureRuleSet.moveRuleSet.pawnMovesLimiter = 1;

            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  .  .  .  . 
            /// 5	 .  .  .  .  .  .  . 
            /// 4	 .  .  .  X  .  D  . 
            /// 3	 .  .  .  .  .  A  . 
            /// 2	 .  .  .  D  .  .  . 
            /// 1	 *  .  .  .  .  .  * 
            Board board = new Board(7, 7);
            board.AddPiece(new Attacker(new Position(3, 'f')));
            board.AddPiece(new Defender(new Position(4, 'f')));
            board.AddPiece(new Defender(new Position(2, 'd')));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            IPiece piece = board.At(new Position(3, 'f'));
            Assert.IsFalse(CaptureUtils.IsPieceThreatened(piece, board, HnefataflCaptureRuleSet));
        }
        [TestMethod()]
        public void IsPieceThreatenedKingTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();

            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  .  .  .  . 
            /// 5	 .  .  .  .  .  .  . 
            /// 4	 .  .  .  X  .  A  . 
            /// 3	 .  .  .  .  A  K  A 
            /// 2	 .  .  .  A  .  .  . 
            /// 1	 *  .  .  .  .  .  * 
            Board board = new Board(7, 7);
            board.AddPiece(new King(new Position(3, 'f')));
            board.AddPiece(new Attacker(new Position(4, 'f')));
            board.AddPiece(new Attacker(new Position(3, 'g')));
            board.AddPiece(new Attacker(new Position(2, 'd')));
            board.AddPiece(new Attacker(new Position(3, 'e')));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            IPiece piece = board.At(new Position(3, 'f'));
            Assert.IsTrue(CaptureUtils.IsPieceThreatened(piece, board, HnefataflCaptureRuleSet));
        }
        [TestMethod()]
        public void IsPieceThreatenedAttackerWithArmedKingTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();

            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  .  .  .  . 
            /// 5	 .  .  .  .  .  .  . 
            /// 4	 .  .  .  X  .  D  . 
            /// 3	 .  .  .  .  .  A  . 
            /// 2	 .  .  .  K  .  .  . 
            /// 1	 *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddPiece(new Attacker(new Position(3, 'f')));
            board.AddPiece(new Defender(new Position(4, 'f')));
            board.AddPiece(new King(new Position(2, 'd')));
            IPiece piece = board.At(new Position(3, 'f'));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            HnefataflCaptureRuleSet.isKingArmed = true;
            Assert.IsTrue(CaptureUtils.IsPieceThreatened(piece, board, HnefataflCaptureRuleSet));
        }
        [TestMethod()]
        public void IsPieceThreatenedKingSafeTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();

            //       A  B  C  D  E  F  G 
            // 7	 *  .  .  .  A  .  * 
            // 6	 .  .  .  .  .  .  . 
            // 5	 .  .  .  .  .  .  . 
            // 4	 .  .  .  .  .  A  . 
            // 3	 .  .  .  .  .  K  A 
            // 2	 .  .  .  A  .  .  . 
            // 1	 *  .  .  .  .  .  * 
            Board board = new Board(7, 7);
            board.AddPiece(new King(new Position(3, 'f')));
            board.AddPiece(new Attacker(new Position(4, 'f')));
            board.AddPiece(new Attacker(new Position(3, 'g')));
            board.AddPiece(new Attacker(new Position(2, 'd')));
            board.AddPiece(new Attacker(new Position(7, 'e')));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            IPiece piece = board.At(new Position(3, 'f'));
            Assert.IsFalse(CaptureUtils.IsPieceThreatened(piece, board, HnefataflCaptureRuleSet));
        }
        [TestMethod()]
        public void IsPieceThreatenedPawnBetweenThroneAndAttackerTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();

            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  A  .  .  . 
            /// 5	 .  .  .  D  .  .  . 
            /// 4	 .  .  .  X  .  .  . 
            /// 3	 .  .  .  .  .  .  . 
            /// 2	 .  .  .  .  .  .  . 
            /// 1	 *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddPiece(new Defender(new Position(5, 'd')));
            board.AddPiece(new Attacker(new Position(6, 'd')));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            IPiece piece = board.At(new Position(5, 'd'));
            Assert.IsFalse(CaptureUtils.IsPieceThreatened(piece, board, HnefataflCaptureRuleSet));
        }
        [TestMethod()]
        public void IsPieceThreatenedPawnNextToThroneAndDefenderAboveTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();

            ///          A  B  C  D  E  F  G
            /// 7        *  .  .  .  .  .  *
            /// 6        .  .  .  .  .  .  .
            /// 5        .  .  .  D  .  .  .
            /// 4        .  A  D  X  .  .  .
            /// 3        .  .  .  .  .  .  .
            /// 2        .  .  .  .  .  .  .
            /// 1        *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddPiece(new Defender(new Position(4, 'c')));
            board.AddPiece(new Defender(new Position(5, 'd')));
            board.AddPiece(new Attacker(new Position(4, 'b')));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            IPiece piece = board.At(new Position(4, 'c'));
            Assert.IsFalse(CaptureUtils.IsPieceThreatened(piece, board, HnefataflCaptureRuleSet));
        }
        [TestMethod()]
        public void IsPieceThreatenedAttackerAndArmedKingAboveTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();
            HnefataflCaptureRuleSet.isKingArmed = true;

            ///          A  B  C  D  E  F  G
            /// 7        *  .  .  K  .  .  *
            /// 6        .  .  .  .  .  .  .
            /// 5        .  .  .  .  .  .  .
            /// 4        .  D  A  X  .  .  .
            /// 3        .  .  .  .  .  .  .
            /// 2        .  .  .  .  .  .  .
            /// 1        *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddPiece(new Defender(new Position(4, 'b')));
            board.AddPiece(new King(new Position(7, 'd')));
            board.AddPiece(new Attacker(new Position(4, 'c')));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            IPiece piece = board.At(new Position(4, 'c'));
            Assert.IsTrue(CaptureUtils.IsPieceThreatened(piece, board, HnefataflCaptureRuleSet));
        }
        [TestMethod()]
        public void IsPieceThreatenedAttackerAndArmedKingAboveCantLandTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();
            HnefataflCaptureRuleSet.isKingArmed = true;
            HnefataflCaptureRuleSet.moveRuleSet.canKingLandOnThrone = false;

            ///          A  B  C  D  E  F  G
            /// 7        *  .  .  K  .  .  *
            /// 6        .  .  .  .  .  .  .
            /// 5        .  .  .  .  .  .  .
            /// 4        .  D  A  X  .  .  .
            /// 3        .  .  .  .  .  .  .
            /// 2        .  .  .  .  .  .  .
            /// 1        *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddPiece(new Defender(new Position(4, 'b')));
            board.AddPiece(new King(new Position(7, 'd')));
            board.AddPiece(new Attacker(new Position(4, 'c')));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            IPiece piece = board.At(new Position(4, 'c'));
            Assert.IsFalse(CaptureUtils.IsPieceThreatened(piece, board, HnefataflCaptureRuleSet));
        }
        [TestMethod()]
        public void IsPieceThreatenedInvalidShieldwallTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();
            HnefataflCaptureRuleSet.isKingArmed = true;
            HnefataflCaptureRuleSet.moveRuleSet.canKingLandOnThrone = false;

            ///          A  B  C  D  E  F  G
            /// 7        *  .  .  .  .  .  *
            /// 6        .  .  .  .  .  .  D
            /// 5        .  .  .  .  .  D  A
            /// 4        .  .  .  x  .  D  A
            /// 3        .  .  .  .  D  .  A
            /// 2        .  .  .  .  .  .  .
            /// 1        *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddPiece(new Defender(new Position(6, 'g')));
            board.AddPiece(new Defender(new Position(5, 'f')));
            board.AddPiece(new Defender(new Position(5, 'g')));
            board.AddPiece(new Defender(new Position(4, 'f')));
            board.AddPiece(new Defender(new Position(3, 'e')));
            board.AddPiece(new Attacker(new Position(4, 'g')));
            board.AddPiece(new Attacker(new Position(3, 'g')));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            IPiece piece = board.At(new Position(4, 'g'));
            Assert.IsFalse(CaptureUtils.IsPieceThreatened(piece, board, HnefataflCaptureRuleSet));
        }
        [TestMethod()]
        public void IsPieceThreatenedFutureShieldwallTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();
            HnefataflCaptureRuleSet.isKingArmed = true;
            HnefataflCaptureRuleSet.moveRuleSet.canKingLandOnThrone = false;

            ///          A  B  C  D  E  F  G
            /// 7        *  .  .  .  .  .  *
            /// 6        .  .  .  .  .  .  D
            /// 5        .  .  .  .  .  D  A
            /// 4        .  .  .  x  .  D  A
            /// 3        .  .  .  .  D  .  .
            /// 2        .  .  .  .  .  .  .
            /// 1        *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddPiece(new Defender(new Position(6, 'g')));
            board.AddPiece(new Defender(new Position(5, 'f')));
            board.AddPiece(new Attacker(new Position(5, 'g')));
            board.AddPiece(new Defender(new Position(4, 'f')));
            board.AddPiece(new Defender(new Position(3, 'e')));
            board.AddPiece(new Attacker(new Position(4, 'g')));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            IPiece piece = board.At(new Position(4, 'g'));
            Assert.IsTrue(CaptureUtils.IsPieceThreatened(piece, board, HnefataflCaptureRuleSet));
        }
    }
}