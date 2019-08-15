using HnefataflAI.Commons.Positions;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Rules.Impl;
using HnefataflAI.Pieces;
using HnefataflAI.Pieces.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HnefataflAI.Commons.Utils.Tests
{
    [TestClass()]
    public class IsInShieldWallTest
    {
        [TestMethod()]
        public void IsInShieldWallPivotTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();
            HnefataflCaptureRuleSet.isShieldWallAllowed = true;

            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  .  .  .  A 
            /// 5	 .  .  .  .  .  A  D 
            /// 4	 .  .  .  X  .  A  D 
            /// 3	 .  .  .  .  .  .  . 
            /// 2	 .  .  .  .  .  .  . 
            /// 1	 *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddThroneTiles();
            board.AddCornerTiles();
            board.AddPiece(new Defender(new Position(5, 'g')));
            board.AddPiece(new Defender(new Position(4, 'g')));
            board.AddPiece(new Attacker(new Position(6, 'g')));
            board.AddPiece(new Attacker(new Position(5, 'f')));
            board.AddPiece(new Attacker(new Position(4, 'f')));
            IPiece piece = board.At(new Position(4, 'g'));
            Assert.IsTrue(CaptureUtils.IsInShieldWall(piece, board, Directions.RIGHT, HnefataflCaptureRuleSet).Count == 2);            
        }
        [TestMethod()]
        public void IsInShieldWallMiddleTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();
            HnefataflCaptureRuleSet.isShieldWallAllowed = true;

            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  .  .  .  A 
            /// 5	 .  .  .  .  .  A  D 
            /// 4	 .  .  .  X  .  A  D 
            /// 3	 .  .  .  .  .  .  . 
            /// 2	 .  .  .  .  .  .  . 
            /// 1	 *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddThroneTiles();
            board.AddCornerTiles();
            board.AddPiece(new Defender(new Position(5, 'g')));
            board.AddPiece(new Defender(new Position(4, 'g')));
            board.AddPiece(new Attacker(new Position(6, 'g')));
            board.AddPiece(new Attacker(new Position(5, 'f')));
            board.AddPiece(new Attacker(new Position(4, 'f')));
            IPiece piece = board.At(new Position(5, 'g'));
            Assert.IsTrue(CaptureUtils.IsInShieldWall(piece, board, Directions.RIGHT, HnefataflCaptureRuleSet).Count == 2);
        }
        [TestMethod()]
        public void IsInShieldWallMiddleFailTest()
        {
            CaptureRuleSet HnefataflCaptureRuleSet = new HnefataflRule().GetCaptureRuleSet();
            HnefataflCaptureRuleSet.isShieldWallAllowed = true;

            /// 	 A  B  C  D  E  F  G 
            /// 7	 *  .  .  .  .  .  * 
            /// 6	 .  .  .  .  .  .  A 
            /// 5	 .  .  .  .  .  .  D 
            /// 4	 .  .  .  X  .  A  D 
            /// 3	 .  .  .  .  .  .  . 
            /// 2	 .  .  .  .  .  .  . 
            /// 1	 *  .  .  .  .  .  *
            Board board = new Board(7, 7);
            board.AddThroneTiles();
            board.AddCornerTiles();
            board.AddPiece(new Defender(new Position(5, 'g')));
            board.AddPiece(new Defender(new Position(4, 'g')));
            board.AddPiece(new Attacker(new Position(6, 'g')));
            board.AddPiece(new Attacker(new Position(4, 'f')));
            IPiece piece = board.At(new Position(5, 'g'));
            Assert.IsFalse(CaptureUtils.IsInShieldWall(piece, board, Directions.RIGHT, HnefataflCaptureRuleSet).Count == 2);
        }
        [TestMethod()]
        public void IsShieldWallCompleteTest()
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
            board.AddThroneTiles();
            board.AddCornerTiles();
            board.AddPiece(new Defender(new Position(2, 'b')));
            board.AddPiece(new Defender(new Position(2, 'c')));
            board.AddPiece(new Defender(new Position(2, 'd')));
            board.AddPiece(new Defender(new Position(1, 'e')));
            board.AddPiece(new Attacker(new Position(1, 'b')));
            board.AddPiece(new Attacker(new Position(1, 'c')));
            board.AddPiece(new Attacker(new Position(1, 'd')));
            IPiece piece = board.At(new Position(1, 'c'));
            IPiece moved = board.At(new Position(1, 'd'));
            Assert.IsTrue(CaptureUtils.IsShieldWallComplete(piece, moved, board, Directions.DOWN, HnefataflCaptureRuleSet).Count == 2);
        }
    }
}
