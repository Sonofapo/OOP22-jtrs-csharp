using Game.Utils;

namespace Game.Api
{
    public interface ITetromino
    {
        ISet<Pair<int, int>> Components { get; }

        void Rotate();

        void Translate();

        ISet<ITetromino> Delete();

        void Copy();
    }
}