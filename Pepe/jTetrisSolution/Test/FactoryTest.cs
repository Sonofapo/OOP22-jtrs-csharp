using Game.Api;
using Game.Impl;

namespace Test
{
    [TestClass]
    public class FactoryTest
    {
        private const int DifferentTetrominoes = 7;

        private readonly TetrominoFacotry _factory = new();

        [TestMethod]
        public void TestFactoryGeneration()
        {
            List<ITetromino> list = new();
            for (int i = 0; i < DifferentTetrominoes; i++)
            {
                list.Add(_factory.GetRandomTetromino());
            }

            var differents = list.Select(t => t.Components).Distinct().Count();
            Assert.AreEqual(DifferentTetrominoes, differents);
        }
    }
}