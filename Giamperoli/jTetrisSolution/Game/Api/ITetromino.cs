using Game.Utils;

namespace Game.Api
{
    public interface ITetromino
    {
        public ISet<Pair<int, int>> Components { get; }

        public void Rotate();

        public void Translate(int x, int y);

        public ISet<ITetromino> Delete(int position);

        public ITetromino Copy();
    }
}