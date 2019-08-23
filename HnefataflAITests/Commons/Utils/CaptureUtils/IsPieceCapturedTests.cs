using HnefataflAI.Commons;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Rules.Impl;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HnefataflAI.Commons.Utils.Tests
{
    [TestClass()]
    public class IsPieceCapturedTests
    {
        [TestMethod()]
        public void IsPieceCapturedDefenderTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();

            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  .  .  .  . 
            /// 5	 .  .  .  .  .  .  . 
            /// 4	 .  .  .  X  .  A  . 
            /// 3	 .  .  .  .  .  D  . 
            /// 2	 .  .  .  .  .  A  . 
            /// 1	 *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddPiece(new Defender(new Position(3, 'f')));
            board.AddPiece(new Attacker(new Position(4, 'f')));
            board.AddPiece(new Attacker(new Position(2, 'f')));
            IPiece piece = board.At(new Position(3, 'f'));
            IPiece moved = board.At(new Position(2, 'f'));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            Assert.IsTrue(CaptureUtils.IsPieceCaptured(piece, moved, board, HnefataflCaptureRuleSet));
        }

        [TestMethod()]
        public void IsPieceCapturedBetweenThroneAndAttackerTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();

            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  .  .  .  . 
            /// 5	 .  .  .  .  .  .  . 
            /// 4	 .  .  .  X  D  A  . 
            /// 3	 .  .  .  .  .  .  . 
            /// 2	 .  .  .  .  .  .  . 
            /// 1	 *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddPiece(new Defender(new Position(4, 'e')));
            board.AddPiece(new Attacker(new Position(4, 'f')));
            IPiece piece = board.At(new Position(4, 'e'));
            IPiece moved = board.At(new Position(4, 'f'));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            Assert.IsTrue(CaptureUtils.IsPieceCaptured(piece, moved, board, HnefataflCaptureRuleSet));
        }

        [TestMethod()]
        public void IsPieceCapturedBetweenCornerAndAttackerTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();

            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  .  .  .  . 
            /// 5	 .  .  .  .  .  .  . 
            /// 4	 .  .  .  X  .  .  . 
            /// 3	 .  .  .  .  .  .  A 
            /// 2	 .  .  .  .  .  .  D 
            /// 1	 *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddPiece(new Defender(new Position(2, 'g')));
            board.AddPiece(new Attacker(new Position(3, 'g')));
            IPiece piece = board.At(new Position(2, 'g'));
            IPiece moved = board.At(new Position(3, 'g'));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            Assert.IsTrue(CaptureUtils.IsPieceCaptured(piece, moved, board, HnefataflCaptureRuleSet));
        }

        [TestMethod()]
        public void IsPieceCapturedShieldWallTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();
            HnefataflCaptureRuleSet.isShieldWallAllowed = true;

            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  .  .  .  . 
            /// 5	 .  .  .  .  .  .  . 
            /// 4	 .  .  .  X  .  .  . 
            /// 3	 .  .  .  .  .  .  . 
            /// 2	 .  D  D  D  .  .  . 
            /// 1	 *  A  A  A  D  .  *
            Board board = new Board(7, 7);
            board.AddPiece(new Defender(new Position(2, 'b')));
            board.AddPiece(new Defender(new Position(2, 'c')));
            board.AddPiece(new Defender(new Position(2, 'd')));
            board.AddPiece(new Defender(new Position(1, 'e')));
            board.AddPiece(new Attacker(new Position(1, 'b')));
            board.AddPiece(new Attacker(new Position(1, 'c')));
            board.AddPiece(new Attacker(new Position(1, 'd')));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            IPiece piece = board.At(new Position(1, 'c'));
            IPiece moved = board.At(new Position(1, 'e'));
            Assert.IsTrue(CaptureUtils.IsPieceCaptured(piece, moved, board, HnefataflCaptureRuleSet));
        }

        [TestMethod()]
        public void IsPieceCapturedShieldWallFailTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();
            HnefataflCaptureRuleSet.isShieldWallAllowed = true;

            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  .  .  .  . 
            /// 5	 .  .  .  .  .  .  . 
            /// 4	 .  .  .  X  .  .  . 
            /// 3	 .  .  .  .  .  .  . 
            /// 2	 .  D  D  D  .  .  . 
            /// 1	 *  A  A  A  D  .  *
            Board board = new Board(7, 7);
            board.AddPiece(new Defender(new Position(2, 'b')));
            board.AddPiece(new Defender(new Position(2, 'c')));
            board.AddPiece(new Defender(new Position(2, 'd')));
            board.AddPiece(new Defender(new Position(1, 'e')));
            board.AddPiece(new Attacker(new Position(1, 'b')));
            board.AddPiece(new Attacker(new Position(1, 'c')));
            board.AddPiece(new Attacker(new Position(1, 'd')));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            IPiece piece = board.At(new Position(1, 'c'));
            IPiece moved = board.At(new Position(2, 'c'));
            Assert.IsFalse(CaptureUtils.IsPieceCaptured(piece, moved, board, HnefataflCaptureRuleSet));
        }
        [TestMethod()]
        public void IsPieceCapturedShieldWallFail2Test()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();
            HnefataflCaptureRuleSet.isShieldWallAllowed = true;

            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  .  .  .  . 
            /// 5	 .  .  .  .  .  .  . 
            /// 4	 .  .  .  X  .  .  . 
            /// 3	 .  .  .  .  .  .  . 
            /// 2	 .  .  D  D  .  .  . 
            /// 1	 *  A  A  A  D  .  *
            Board board = new Board(7, 7);
            board.AddPiece(new Defender(new Position(2, 'c')));
            board.AddPiece(new Defender(new Position(2, 'd')));
            board.AddPiece(new Defender(new Position(1, 'e')));
            board.AddPiece(new Attacker(new Position(1, 'b')));
            board.AddPiece(new Attacker(new Position(1, 'c')));
            board.AddPiece(new Attacker(new Position(1, 'd')));
            board.FinalizeCreation();
            board.AddThroneTiles();
            board.AddCornerTiles();
            IPiece piece = board.At(new Position(1, 'd'));
            IPiece moved = board.At(new Position(1, 'e'));
            Assert.IsFalse(CaptureUtils.IsPieceCaptured(piece, moved, board, HnefataflCaptureRuleSet));
        }
    }
}
