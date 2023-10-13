using Game.Utils;

namespace Game.Api
{
    public interface ITetromino
    {
        ISet<Pair<int, int>> Components { get; }

        void Rotate();

        void Translate(int x, int y);

        ISet<ITetromino> Delete();

        void Copy();
    }
}