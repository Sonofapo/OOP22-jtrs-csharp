 namespace Game.Api
{
    public interface IGameModel
    {
        public enum GameState
        {
            Start,
            Running,
            Pause,
            Over
        }

        public enum Interaction
        {
            Down,
            Left,
            Right,
            Rotate
        }

        public IList<ITetromino> Pieces { get; }

        public bool NextPiece(ITetromino next);

        public int DeleteRows();

        public bool Advance(Interaction i);

        public ISet<int> DeletedLines { get; }
    }
}