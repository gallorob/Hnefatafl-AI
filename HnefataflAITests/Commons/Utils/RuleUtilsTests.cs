using HnefataflAI.Commons.Positions;
using HnefataflAI.Games.Boards;
using HnefataflAI.Pieces.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HnefataflAI.Games.Rules;

namespace HnefataflAI.Commons.Utils.Tests
{
    [TestClass()]
    public class RuleUtilsTests
    {
        [TestMethod()]
        public void CheckIfHasEncircledTest()
        {
            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  A  A  A  A  .  . 
            /// 5	 .  A  D  .  .  A  . 
            /// 4	 .  A  .  K  .  A  . 
            /// 3	 .  .  A  D  .  A  . 
            /// 2	 .  .  .  A  A  .  . 
            /// 1	 *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddThroneTiles();
            board.AddCornerTiles();
            board.AddPiece(new Defender(new Position(5, 'c')));
            board.AddPiece(new Defender(new Position(3, 'd')));
            board.AddPiece(new King(new Position(4, 'd')));
            board.AddPiece(new Attacker(new Position(6, 'b')));
            board.AddPiece(new Attacker(new Position(6, 'c')));
            board.AddPiece(new Attacker(new Position(6, 'd')));
            board.AddPiece(new Attacker(new Position(6, 'e')));
            board.AddPiece(new Attacker(new Position(5, 'b')));
            board.AddPiece(new Attacker(new Position(5, 'f')));
            board.AddPiece(new Attacker(new Position(4, 'b')));
            board.AddPiece(new Attacker(new Position(4, 'f')));
            board.AddPiece(new Attacker(new Position(3, 'c')));
            board.AddPiece(new Attacker(new Position(3, 'f')));
            board.AddPiece(new Attacker(new Position(2, 'd')));
            board.AddPiece(new Attacker(new Position(2, 'e')));
            Assert.IsTrue(RuleUtils.CheckIfHasEncircled(board));
        }
        [TestMethod()]
        public void CheckIfHasEncircledTest2()
        {
            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  A  A  A  A  .  . 
            /// 5	 .  A  D  .  .  A  . 
            /// 4	 .  A  .  K  .  A  . 
            /// 3	 .  .  A  D  .  A  . 
            /// 2	 A  .  .  A  A  .  . 
            /// 1	 *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddThroneTiles();
            board.AddCornerTiles();
            board.AddPiece(new Defender(new Position(5, 'c')));
            board.AddPiece(new Defender(new Position(3, 'd')));
            board.AddPiece(new King(new Position(4, 'd')));
            board.AddPiece(new Attacker(new Position(6, 'b')));
            board.AddPiece(new Attacker(new Position(6, 'c')));
            board.AddPiece(new Attacker(new Position(6, 'd')));
            board.AddPiece(new Attacker(new Position(6, 'e')));
            board.AddPiece(new Attacker(new Position(5, 'b')));
            board.AddPiece(new Attacker(new Position(5, 'f')));
            board.AddPiece(new Attacker(new Position(4, 'b')));
            board.AddPiece(new Attacker(new Position(4, 'f')));
            board.AddPiece(new Attacker(new Position(3, 'c')));
            board.AddPiece(new Attacker(new Position(3, 'f')));
            board.AddPiece(new Attacker(new Position(2, 'a')));
            board.AddPiece(new Attacker(new Position(2, 'd')));
            board.AddPiece(new Attacker(new Position(2, 'e')));
            Assert.IsTrue(RuleUtils.CheckIfHasEncircled(board));
        }
        [TestMethod()]
        public void CheckIfHasEncircledFailTest()
        {
            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  A  A  .  A  .  . 
            /// 5	 .  A  D  .  .  A  . 
            /// 4	 .  A  .  K  .  A  . 
            /// 3	 .  .  A  D  .  A  . 
            /// 2	 .  .  .  A  A  .  . 
            /// 1	 *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddThroneTiles();
            board.AddCornerTiles();
            board.AddPiece(new Defender(new Position(5, 'c')));
            board.AddPiece(new Defender(new Position(3, 'd')));
            board.AddPiece(new King(new Position(4, 'd')));
            board.AddPiece(new Attacker(new Position(6, 'b')));
            board.AddPiece(new Attacker(new Position(6, 'c')));
            board.AddPiece(new Attacker(new Position(6, 'e')));
            board.AddPiece(new Attacker(new Position(5, 'b')));
            board.AddPiece(new Attacker(new Position(5, 'f')));
            board.AddPiece(new Attacker(new Position(4, 'b')));
            board.AddPiece(new Attacker(new Position(4, 'f')));
            board.AddPiece(new Attacker(new Position(3, 'c')));
            board.AddPiece(new Attacker(new Position(3, 'f')));
            board.AddPiece(new Attacker(new Position(2, 'd')));
            board.AddPiece(new Attacker(new Position(2, 'e')));
            Assert.IsFalse(RuleUtils.CheckIfHasEncircled(board));
        }
        [TestMethod()]
        public void CheckIfHasEncircledFail2Test()
        {
            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  A  A  A  .  .  . 
            /// 5	 .  A  D  .  .  A  . 
            /// 4	 .  A  .  K  .  A  . 
            /// 3	 .  .  A  D  A  A  . 
            /// 2	 .  .  .  A  A  .  . 
            /// 1	 *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddThroneTiles();
            board.AddCornerTiles();
            board.AddPiece(new Defender(new Position(5, 'c')));
            board.AddPiece(new Defender(new Position(3, 'd')));
            board.AddPiece(new King(new Position(4, 'd')));
            board.AddPiece(new Attacker(new Position(6, 'b')));
            board.AddPiece(new Attacker(new Position(6, 'c')));
            board.AddPiece(new Attacker(new Position(6, 'd')));
            board.AddPiece(new Attacker(new Position(3, 'e')));
            board.AddPiece(new Attacker(new Position(5, 'b')));
            board.AddPiece(new Attacker(new Position(5, 'f')));
            board.AddPiece(new Attacker(new Position(4, 'b')));
            board.AddPiece(new Attacker(new Position(4, 'f')));
            board.AddPiece(new Attacker(new Position(3, 'c')));
            board.AddPiece(new Attacker(new Position(3, 'f')));
            board.AddPiece(new Attacker(new Position(2, 'd')));
            board.AddPiece(new Attacker(new Position(2, 'e')));
            Assert.IsFalse(RuleUtils.CheckIfHasEncircled(board));
        }
        [TestMethod()]
        public void CheckIfHasEncircledFail3Test()
        {
            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  A  A  A  A  .  . 
            /// 5	 .  A  D  .  .  A  . 
            /// 4	 .  A  .  K  .  A  . 
            /// 3	 .  .  A  D  .  A  . 
            /// 2	 D  .  .  A  A  .  . 
            /// 1	 *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddThroneTiles();
            board.AddCornerTiles();
            board.AddPiece(new Defender(new Position(5, 'c')));
            board.AddPiece(new Defender(new Position(3, 'd')));
            board.AddPiece(new Defender(new Position(2, 'a')));
            board.AddPiece(new King(new Position(4, 'd')));
            board.AddPiece(new Attacker(new Position(6, 'b')));
            board.AddPiece(new Attacker(new Position(6, 'c')));
            board.AddPiece(new Attacker(new Position(6, 'd')));
            board.AddPiece(new Attacker(new Position(6, 'e')));
            board.AddPiece(new Attacker(new Position(5, 'b')));
            board.AddPiece(new Attacker(new Position(5, 'f')));
            board.AddPiece(new Attacker(new Position(4, 'b')));
            board.AddPiece(new Attacker(new Position(4, 'f')));
            board.AddPiece(new Attacker(new Position(3, 'c')));
            board.AddPiece(new Attacker(new Position(3, 'f')));
            board.AddPiece(new Attacker(new Position(2, 'd')));
            board.AddPiece(new Attacker(new Position(2, 'e')));
            Assert.IsFalse(RuleUtils.CheckIfHasEncircled(board));
        }
        [TestMethod()]
        public void CheckIfHasEncircledFail4Test()
        {
            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  A  .  * 
            /// 6	 .  A  A  .  .  A  . 
            /// 5	 .  A  D  A  .  A  . 
            /// 4	 .  A  .  K  .  A  . 
            /// 3	 .  .  A  D  .  A  . 
            /// 2	 D  .  .  A  A  .  . 
            /// 1	 *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddThroneTiles();
            board.AddCornerTiles();
            board.AddPiece(new Defender(new Position(5, 'c')));
            board.AddPiece(new Defender(new Position(3, 'd')));
            board.AddPiece(new Defender(new Position(2, 'a')));
            board.AddPiece(new King(new Position(4, 'd')));
            board.AddPiece(new Attacker(new Position(7, 'e')));
            board.AddPiece(new Attacker(new Position(6, 'b')));
            board.AddPiece(new Attacker(new Position(6, 'c')));
            board.AddPiece(new Attacker(new Position(6, 'f')));
            board.AddPiece(new Attacker(new Position(5, 'b')));
            board.AddPiece(new Attacker(new Position(5, 'd')));
            board.AddPiece(new Attacker(new Position(5, 'f')));
            board.AddPiece(new Attacker(new Position(4, 'b')));
            board.AddPiece(new Attacker(new Position(4, 'f')));
            board.AddPiece(new Attacker(new Position(3, 'c')));
            board.AddPiece(new Attacker(new Position(3, 'f')));
            board.AddPiece(new Attacker(new Position(2, 'd')));
            board.AddPiece(new Attacker(new Position(2, 'e')));
            Assert.IsFalse(RuleUtils.CheckIfHasEncircled(board));
        }

        [TestMethod()]
        public void IsInExitFortTest()
        {
            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  .  .  .  . 
            /// 5	 .  .  .  .  .  .  . 
            /// 4	 .  .  .  x  .  .  . 
            /// 3	 .  A  D  D  A  .  . 
            /// 2	 .  .  D  .  D  A  . 
            /// 1	 *  .  D  K  D  A  *
            CaptureRuleSet captureRuleSet = RuleUtils.GetRule(RuleTypes.HNEFATAFL).GetCaptureRuleSet();
            Board board = new Board(7, 7);
            board.AddThroneTiles();
            board.AddCornerTiles();
            board.AddPiece(new King(new Position(1, 'd')));
            board.AddPiece(new Defender(new Position(3, 'c')));
            board.AddPiece(new Defender(new Position(3, 'd')));
            board.AddPiece(new Defender(new Position(2, 'c')));
            board.AddPiece(new Defender(new Position(2, 'e')));
            board.AddPiece(new Defender(new Position(1, 'c')));
            board.AddPiece(new Defender(new Position(1, 'e')));
            board.AddPiece(new Attacker(new Position(3, 'b')));
            board.AddPiece(new Attacker(new Position(3, 'e')));
            board.AddPiece(new Attacker(new Position(2, 'f')));
            board.AddPiece(new Attacker(new Position(1, 'f')));
            Assert.IsTrue(RuleUtils.IsInExitFort(board, captureRuleSet));
        }
        [TestMethod()]
        public void IsInExitFortFailTest()
        {
            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  .  .  .  . 
            /// 5	 .  .  .  .  .  .  . 
            /// 4	 .  .  .  x  .  .  . 
            /// 3	 .  A  D  D  A  .  . 
            /// 2	 .  D  .  .  D  A  . 
            /// 1	 *  .  D  K  D  A  *
            CaptureRuleSet captureRuleSet = RuleUtils.GetRule(RuleTypes.HNEFATAFL).GetCaptureRuleSet();
            Board board = new Board(7, 7);
            board.AddThroneTiles();
            board.AddCornerTiles();
            board.AddPiece(new King(new Position(1, 'd')));
            board.AddPiece(new Defender(new Position(3, 'c')));
            board.AddPiece(new Defender(new Position(3, 'd')));
            board.AddPiece(new Defender(new Position(2, 'b')));
            board.AddPiece(new Defender(new Position(2, 'e')));
            board.AddPiece(new Defender(new Position(1, 'c')));
            board.AddPiece(new Defender(new Position(1, 'e')));
            board.AddPiece(new Attacker(new Position(3, 'b')));
            board.AddPiece(new Attacker(new Position(3, 'e')));
            board.AddPiece(new Attacker(new Position(2, 'f')));
            board.AddPiece(new Attacker(new Position(1, 'f')));
            Assert.IsFalse(RuleUtils.IsInExitFort(board, captureRuleSet));
        }

        [TestMethod()]
        public void IsKingInDrawFortTest()
        {
            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  A  .  D  D  .  . 
            /// 5	 .  .  D  .  .  D  . 
            /// 4	 .  A  D  K  .  D  . 
            /// 3	 .  .  .  D  D  .  . 
            /// 2	 .  .  .  .  A  .  . 
            /// 1	 *  .  .  .  .  .  *
            CaptureRuleSet captureRuleSet = RuleUtils.GetRule(RuleTypes.HNEFATAFL).GetCaptureRuleSet();
            Board board = new Board(7, 7);
            board.AddThroneTiles();
            board.AddCornerTiles();
            board.AddPiece(new King(new Position(4, 'd')));
            board.AddPiece(new Defender(new Position(6, 'd')));
            board.AddPiece(new Defender(new Position(6, 'e')));
            board.AddPiece(new Defender(new Position(5, 'c')));
            board.AddPiece(new Defender(new Position(5, 'f')));
            board.AddPiece(new Defender(new Position(4, 'c')));
            board.AddPiece(new Defender(new Position(4, 'f')));
            board.AddPiece(new Defender(new Position(3, 'd')));
            board.AddPiece(new Defender(new Position(3, 'e')));
            board.AddPiece(new Attacker(new Position(6, 'b')));
            board.AddPiece(new Attacker(new Position(4, 'b')));
            board.AddPiece(new Attacker(new Position(2, 'e')));
            Assert.IsTrue(RuleUtils.IsInDrawFort(board, captureRuleSet));
        }
        [TestMethod()]
        public void IsKingInDrawFortFailTest()
        {
            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  A  .  D  D  .  . 
            /// 5	 .  .  .  D  .  D  . 
            /// 4	 .  A  D  K  .  D  . 
            /// 3	 .  .  .  D  D  .  . 
            /// 2	 .  .  .  .  A  .  . 
            /// 1	 *  .  .  .  .  .  *
            CaptureRuleSet captureRuleSet = RuleUtils.GetRule(RuleTypes.HNEFATAFL).GetCaptureRuleSet();
            Board board = new Board(7, 7);
            board.AddThroneTiles();
            board.AddCornerTiles();
            board.AddPiece(new King(new Position(4, 'd')));
            board.AddPiece(new Defender(new Position(6, 'd')));
            board.AddPiece(new Defender(new Position(6, 'e')));
            board.AddPiece(new Defender(new Position(5, 'd')));
            board.AddPiece(new Defender(new Position(5, 'f')));
            board.AddPiece(new Defender(new Position(4, 'c')));
            board.AddPiece(new Defender(new Position(4, 'f')));
            board.AddPiece(new Defender(new Position(3, 'd')));
            board.AddPiece(new Defender(new Position(3, 'e')));
            board.AddPiece(new Attacker(new Position(6, 'b')));
            board.AddPiece(new Attacker(new Position(4, 'b')));
            board.AddPiece(new Attacker(new Position(2, 'e')));
            Assert.IsFalse(RuleUtils.IsInDrawFort(board, captureRuleSet));
        }
    }
}