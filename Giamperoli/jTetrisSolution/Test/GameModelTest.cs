using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Api;
using Game.Impl;
using Game.Utils;
using System.Collections.ObjectModel;

namespace Test
{
    [TestClass]
    public class GameModelTest
    {
        [TestMethod]
        public void TestNextPiece()
        {
            IGameModel model = new GameModel();
            for (int i = 18; i >= 0; i -= 2)
            {
                Assert.IsTrue(model.NextPiece(new Tetromino(TetrominoData.CoordO, i, 0)));
            }
            Assert.IsFalse(model.NextPiece(new Tetromino(TetrominoData.CoordO, 0, 0)));
        }

        [TestMethod]
        public void TestDeleteOneRow()
        {
            IGameModel model = new GameModel();
            model.NextPiece(new Tetromino(TetrominoData.CoordI, 18, 0));
            model.NextPiece(new Tetromino(TetrominoData.CoordI, 18, 4));
            model.NextPiece(new Tetromino(TetrominoData.CoordO, 18, 8));

            Assert.AreEqual(1, model.DeleteRows());
            Assert.AreEqual(2, model.Pieces.Count);
            CollectionAssert.AreEquivalent(new Collection<Pair<int, int>> { new(19, 8), new(19, 9) },
                (System.Collections.ICollection)GetListComponents(model.Pieces));
        }

        [TestMethod]
        public void TestDeleteTwoRows()
        {
            IGameModel model = new GameModel();
            for(int i = 0; i < 10; i += 2)
            {
                model.NextPiece(new Tetromino(TetrominoData.CoordO, 18, i));
            }
            model.NextPiece(new Tetromino(TetrominoData.CoordO, 16, 0));

            Assert.AreEqual(2, model.DeleteRows());
            Assert.AreEqual(1, model.Pieces.Count);
            CollectionAssert.AreEquivalent(new Collection<Pair<int, int>> { new(19, 0), new(19, 1), new(18, 0), new(18, 1) },
                (System.Collections.ICollection)GetListComponents(model.Pieces));
        }

        [TestMethod]
        public void TestDeleteThreeRows()
        {
            IGameModel model = new GameModel();
            for(int i = 0; i < 8; i += 2)
            {
                model.NextPiece(new Tetromino(TetrominoData.CoordO, 18, i));
            }
            model.NextPiece(new Tetromino(TetrominoData.CoordI, 16, 0));
            model.NextPiece(new Tetromino(TetrominoData.CoordI, 16, 4));
            ITetromino tmp = new Tetromino(TetrominoData.CoordI, 16, 6);
            tmp.Rotate();
            model.NextPiece(tmp);
            tmp = new Tetromino(TetrominoData.CoordI, 16, 7);
            tmp.Rotate();
            model.NextPiece(tmp);

            Assert.AreEqual(3, model.DeleteRows());
            Assert.AreEqual(2, model.Pieces.Count);
            CollectionAssert.AreEquivalent(new Collection<Pair<int, int>> { new(19, 8), new(19, 9) },
                (System.Collections.ICollection)GetListComponents(model.Pieces));
        }

        [TestMethod]
        public void TestDeleteFourRows()
        {
            IGameModel model = new GameModel();
            ITetromino tmp;
            for(int i = 0; i < 10; i ++)
            {
                tmp = new Tetromino(TetrominoData.CoordI, 16, i - 2);
                tmp.Rotate();
                model.NextPiece(tmp);
            }
            model.NextPiece(new Tetromino(TetrominoData.CoordO, 14, 0));

            Assert.AreEqual(4, model.DeleteRows());
            Assert.AreEqual(1, model.Pieces.Count);
            CollectionAssert.AreEquivalent(new Collection<Pair<int, int>> { new(19, 0), new(19, 1), new(18, 0), new(18, 1) },
                (System.Collections.ICollection)GetListComponents(model.Pieces));
        }

        [TestMethod]
        public void TestAdvanceDownwards()
        {
            IGameModel model = new GameModel();
            model.NextPiece(new Tetromino(TetrominoData.CoordO, 0, 0));
            for (int i = 0; i < 18; i++)
            {
                Assert.IsTrue(model.Advance(IGameModel.Interaction.Down));
            }
            Assert.IsFalse(model.Advance(IGameModel.Interaction.Down));
        }

        [TestMethod]
        public void TestAdvanceLeftwards()
        {
            IGameModel model = new GameModel();
            model.NextPiece(new Tetromino(TetrominoData.CoordO, 18, 1));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.Left));
            Assert.IsFalse(model.Advance(IGameModel.Interaction.Left));

            model.NextPiece(new Tetromino(TetrominoData.CoordO, 18, 3));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.Left));
            Assert.IsFalse(model.Advance(IGameModel.Interaction.Left));
        }

        [TestMethod]
        public void TestAdvanceRightwards()
        {
            IGameModel model = new GameModel();
            model.NextPiece(new Tetromino(TetrominoData.CoordO, 18, 7));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.Right));
            Assert.IsFalse(model.Advance(IGameModel.Interaction.Right));

            model.NextPiece(new Tetromino(TetrominoData.CoordO, 18, 5));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.Right));
            Assert.IsFalse(model.Advance(IGameModel.Interaction.Right));
        }

        [TestMethod]
        public void TestRotateWithPieceCollision()
        {
            IGameModel model = new GameModel();
            ITetromino tmp = new Tetromino(TetrominoData.CoordT, 17, 5);
            model.NextPiece(tmp);
            model.Advance(IGameModel.Interaction.Rotate);
            model.Advance(IGameModel.Interaction.Rotate);
            model.Advance(IGameModel.Interaction.Rotate);

            model.NextPiece(new Tetromino(TetrominoData.CoordT, 17, 6));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.Rotate));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.Rotate));
            Assert.IsFalse(model.Advance(IGameModel.Interaction.Rotate));
        }

        [TestMethod]
        public void TestRotateWithBoundCollision()
        {
            IGameModel model = new GameModel();
            model.NextPiece(new Tetromino(TetrominoData.CoordT, 17, 1));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.Rotate));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.Rotate));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.Rotate));

            model.Advance(IGameModel.Interaction.Rotate);
            model.Advance(IGameModel.Interaction.Down);
            Assert.IsFalse(model.Advance(IGameModel.Interaction.Rotate));
        }

        private static IList<Pair<int, int>> GetListComponents(IList<ITetromino> pieces)
        {
            return pieces.SelectMany(p => p.Components.ToList()).ToList();
        }
    }
}