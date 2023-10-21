using Game.Api;
using Game.Utils;

namespace Game.Impl
{
    public class TetrominoFactory : ITetrominoFactory
    {
        private List<ITetromino> _bag;

        public TetrominoFactory()
        {
            _bag = Initialize();
        }

        private static List<ITetromino> Initialize()
        {
            return new()
            {
                new Tetromino(TetrominoData.CoordO, 0, 0),
                new Tetromino(TetrominoData.CoordL, 0, 0),
                new Tetromino(TetrominoData.CoordJ, 0, 0),
                new Tetromino(TetrominoData.CoordI, 0, 0),
                new Tetromino(TetrominoData.CoordT, 0, 0),
                new Tetromino(TetrominoData.CoordZ, 0, 0),
                new Tetromino(TetrominoData.CoordS, 0, 0)
            };
        }

        public ITetromino GetRandomTetromino()
        {
            if (!_bag.Any())
            {
                _bag = Initialize();
            }
            var t = _bag.ElementAt(0);
            _bag.RemoveAt(0);
            return t;
        }
    }
}