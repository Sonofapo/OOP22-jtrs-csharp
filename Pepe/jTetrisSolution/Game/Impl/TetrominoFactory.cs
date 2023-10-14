using Game.Api;
using Game.Utils;

namespace Game.Impl
{
    public class TetrominoFacotry : ITetrominoFactory
    {
        private List<ITetromino> _bag { get; set; }

        public TetrominoFacotry()
        {
            _bag = Initialize();
        }

        private static List<ITetromino> Initialize()
        {
            return new()
            {
                new Tetromino(TetrominoData.O_COORD, 0, 0),
                new Tetromino(TetrominoData.L_COORD, 0, 0),
                new Tetromino(TetrominoData.J_COORD, 0, 0),
                new Tetromino(TetrominoData.I_COORD, 0, 0),
                new Tetromino(TetrominoData.T_COORD, 0, 0),
                new Tetromino(TetrominoData.Z_COORD, 0, 0),
                new Tetromino(TetrominoData.S_COORD, 0, 0)
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