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
                Assert.IsTrue(model.NextPiece(new Tetromino(TetrominoData.O_COORD, i, 0)));
            }
            Assert.IsFalse(model.NextPiece(new Tetromino(TetrominoData.O_COORD, 0, 0)));
        }

        [TestMethod] 
        public void TestDeleteOneRow()
        {
            IGameModel model = new GameModel();
            model.NextPiece(new Tetromino(TetrominoData.I_COORD, 18, 0));
            model.NextPiece(new Tetromino(TetrominoData.I_COORD, 18, 4));
            model.NextPiece(new Tetromino(TetrominoData.O_COORD, 18, 8));

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
                model.NextPiece(new Tetromino(TetrominoData.O_COORD, 18, i));
            }
            model.NextPiece(new Tetromino(TetrominoData.O_COORD, 16, 0));

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
                model.NextPiece(new Tetromino(TetrominoData.O_COORD, 18, i));
            }
            model.NextPiece(new Tetromino(TetrominoData.I_COORD, 16, 0));
            model.NextPiece(new Tetromino(TetrominoData.I_COORD, 16, 4));
            ITetromino tmp = new Tetromino(TetrominoData.I_COORD, 16, 6);
            tmp.Rotate();
            model.NextPiece(tmp);
            tmp = new Tetromino(TetrominoData.I_COORD, 16, 7);
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
                tmp = new Tetromino(TetrominoData.I_COORD, 16, i - 2);
                tmp.Rotate();
                model.NextPiece(tmp);
            }
            model.NextPiece(new Tetromino(TetrominoData.O_COORD, 14, 0));

            Assert.AreEqual(4, model.DeleteRows());
            Assert.AreEqual(1, model.Pieces.Count);
            CollectionAssert.AreEquivalent(new Collection<Pair<int, int>> { new(19, 0), new(19, 1), new(18, 0), new(18, 1) }, 
                (System.Collections.ICollection)GetListComponents(model.Pieces));
        }

        [TestMethod]
        public void TestAdvanceDownwards()
        {
            IGameModel model = new GameModel();
            model.NextPiece(new Tetromino(TetrominoData.O_COORD, 0, 0));
            for (int i = 0; i < 18; i++) 
            {
                Assert.IsTrue(model.Advance(IGameModel.Interaction.DOWN));
            }
            Assert.IsFalse(model.Advance(IGameModel.Interaction.DOWN));
        }

        [TestMethod]
        public void TestAdvanceLeftwards()
        {
            IGameModel model = new GameModel();
            model.NextPiece(new Tetromino(TetrominoData.O_COORD, 18, 1));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.LEFT));
            Assert.IsFalse(model.Advance(IGameModel.Interaction.LEFT));

            model.NextPiece(new Tetromino(TetrominoData.O_COORD, 18, 3));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.LEFT));
            Assert.IsFalse(model.Advance(IGameModel.Interaction.LEFT));
        }

        [TestMethod]
        public void TestAdvanceRightwards()
        {
            IGameModel model = new GameModel();
            model.NextPiece(new Tetromino(TetrominoData.O_COORD, 18, 7));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.RIGHT));
            Assert.IsFalse(model.Advance(IGameModel.Interaction.RIGHT));

            model.NextPiece(new Tetromino(TetrominoData.O_COORD, 18, 5));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.RIGHT));
            Assert.IsFalse(model.Advance(IGameModel.Interaction.RIGHT));
        }

        [TestMethod]
        public void TestRotateWithPieceCollision()
        {
            IGameModel model = new GameModel();
            ITetromino tmp = new Tetromino(TetrominoData.T_COORD, 17, 5);
            model.NextPiece(tmp);
            model.Advance(IGameModel.Interaction.ROTATE);
            model.Advance(IGameModel.Interaction.ROTATE);
            model.Advance(IGameModel.Interaction.ROTATE);

            model.NextPiece(new Tetromino(TetrominoData.T_COORD, 17, 6));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.ROTATE));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.ROTATE));
            Assert.IsFalse(model.Advance(IGameModel.Interaction.ROTATE));
        }

        [TestMethod]
        public void TestRotateWithBoundCollision()
        {
            IGameModel model = new GameModel();
            model.NextPiece(new Tetromino(TetrominoData.T_COORD, 17, 1));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.ROTATE));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.ROTATE));
            Assert.IsTrue(model.Advance(IGameModel.Interaction.ROTATE));

            model.Advance(IGameModel.Interaction.ROTATE);
            model.Advance(IGameModel.Interaction.DOWN);
            Assert.IsFalse(model.Advance(IGameModel.Interaction.ROTATE));
        }

        private static IList<Pair<int, int>> GetListComponents(IList<ITetromino> pieces)
        {
            return pieces.SelectMany(p => p.Components.ToList()).ToList();
        }
    }
}