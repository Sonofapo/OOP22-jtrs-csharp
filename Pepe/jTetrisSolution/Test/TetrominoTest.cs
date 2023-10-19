using System.Collections.ObjectModel;
using Game.Impl;
using Game.Utils;

namespace Test
{
    [TestClass]
    public class TetrominoTest
    {
        [TestMethod]
        public void TestRotate()
        {
            var tTetromino = new Tetromino(new HashSet<Pair<int, int>>(TetrominoData.T_COORD), 0, 0);

            tTetromino.Rotate();
            CollectionAssert.AreEquivalent(
                new Collection<Pair<int, int>> { new(1, 1), new(0, 2), new(1, 2), new(2, 2) },
                tTetromino.Components.ToList());

            tTetromino.Rotate();
            CollectionAssert.AreEquivalent(
                new Collection<Pair<int, int>> { new(2, 0), new(2, 1), new(2, 2), new(1, 1) },
                tTetromino.Components.ToList());

            tTetromino.Rotate();
            CollectionAssert.AreEquivalent(
                new Collection<Pair<int, int>> { new(0, 0), new(1, 0), new(2, 0), new(1, 1) },
                tTetromino.Components.ToList());

            tTetromino.Rotate();
            CollectionAssert.AreEquivalent(TetrominoData.T_COORD.ToList(), tTetromino.Components.ToList());
        }

        [TestMethod]
        public void TestTranslate()
        {
            var tTetromino = new Tetromino(new HashSet<Pair<int, int>>(TetrominoData.T_COORD), 0, 0);
            var x = 10;
            var y = 15;

            tTetromino.Translate(x, y);
            CollectionAssert.AreEquivalent(
                TetrominoData.T_COORD.Select(c => new Pair<int, int>(c.GetX + x, c.GetY + y)).ToList(),
                tTetromino.Components.ToList());
        }

        [TestMethod]
        public void TestDelete()
        {
            var tTetromino = new Tetromino(new HashSet<Pair<int, int>>(TetrominoData.T_COORD), 0, 0);

            var t1 = new Tetromino(new HashSet<Pair<int, int>>() { new(0, 2) }, 0, 0);
            var t2 = new Tetromino(new HashSet<Pair<int, int>>() { new(2, 2) }, 0, 0);

            tTetromino.Rotate();
            var res = tTetromino.Delete(1).Select(t => t.Components).ToHashSet();

            foreach (var r in res)
            {
                Assert.IsTrue(AreSetEquivalent(r, t1.Components) || AreSetEquivalent(r, t2.Components));
            }

            Assert.IsFalse(t1.Delete(0).Any());
            Assert.IsFalse(t2.Delete(2).Any());
        }

        private static bool AreSetEquivalent(ISet<Pair<int, int>> s1, ISet<Pair<int, int>> s2) {
            try {
                CollectionAssert.AreEquivalent(s1.ToList(), s2.ToList());
            }
            catch(AssertFailedException)
            {
                return false;
            }
            return true;
        }
    }
}