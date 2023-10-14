using Game.Impl;

namespace Game.Api
{
    public interface ITetrominoFactory
    {
        ITetromino GetRandomTetromino();
    }
}