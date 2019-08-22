using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HnefataflAI.Commons.Positions;

namespace HnefataflAI.Commons.Utils.Tests
{
    [TestClass()]
    public class PositionUtilsTests
    {
        [TestMethod()]
        public void GetPositionsRangeTest()
        {
            Position start = new Position(4, 'a');
            Position end = new Position(4, 'e');
            List<Position> expected = new List<Position>
            {
                new Position(4, 'b'),
                new Position(4, 'c'),
                new Position(4, 'd')
            };
            List<Position> result = PositionUtils.GetPositionsRange(start, end);
            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }
        [TestMethod()]
        public void GetPositionsRange2Test()
        {
            Position start = new Position(1, 'b');
            Position end = new Position(1, 'e');
            List<Position> expected = new List<Position>
            {
                new Position(1, 'c'),
                new Position(1, 'd')
            };
            List<Position> result = PositionUtils.GetPositionsRange(start, end);
            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }
    }
}