 namespace Game.Api
{
    public interface IGameModel
    {
        enum GameState
        {
            START,
            RUNNING,
            PAUSE,
            OVER
        }

        enum Interaction
        {
            DOWN,
            LEFT, 
            RIGHT,
            ROTATE
        }

        IList<ITetromino> Pieces { get; }

        bool NextPiece(ITetromino next);

        int DeleteRows();

        bool Advance(Interaction i);

        ISet<int> DeletedLines { get; }
    }
}