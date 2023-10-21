using Game.Api;
using Game.Utils;

namespace Game.Impl
{
    public class GameModel : IGameModel
    {
        public const int GridRows = 20;
        public const int GridCols = 10;

        private IList<ITetromino> _pieces;
        private readonly ISet<int> _deletedLines;

        public IList<ITetromino> Pieces
        {
            get => _pieces.Select(p => p.Copy()).ToList();
        }
        public ISet<int> DeletedLines {
            get
            {
                ISet<int> res = _deletedLines.ToHashSet();
                _deletedLines.Clear();
                return res;
            }
        }

        public GameModel()
        {
            _pieces = new List<ITetromino>();
            _deletedLines = new HashSet<int>();
        }

        public bool Advance(IGameModel.Interaction i)
        {
            bool p(ISet<Pair<int, int>> c) => CheckAvailablePosition(c);
            Action<ITetromino> a = i switch
            {
                IGameModel.Interaction.Rotate => t => t.Rotate(),
                IGameModel.Interaction.Down => t => t.Translate(1, 0),
                IGameModel.Interaction.Left => t => t.Translate(0, -1),
                IGameModel.Interaction.Right => t => t.Translate(0, 1),
                _ => t => t.Copy(),
            };
            return Action(a, p);
        }

        public int DeleteRows()
        {
            _deletedLines.UnionWith(GetCompletedRows());
            RemoveRows();
            return _deletedLines.Count;
        }

        public bool NextPiece(ITetromino next)
        {
            _pieces.Add(next);
            if (Collide(next.Components))
            {
                _pieces.RemoveAt(_pieces.Count - 1);
                return false;
            }
            return true;
        }

        private ITetromino GetCurrentPiece()
        {
            return _pieces.ElementAt(_pieces.Count - 1);
        }

        private List<Pair<int, int>> GetListComponents(int end)
        {
            return _pieces.Where((p, i) => i < end).ToList()
                .SelectMany(p => p.Components.ToList()).ToList();
        }

        private bool Action(Action<ITetromino> a, Predicate<ISet<Pair<int, int>>> p)
        {
            ITetromino tmp = GetCurrentPiece().Copy();
            a.Invoke(tmp);
            if (p.Invoke(tmp.Components))
            {
                a.Invoke(GetCurrentPiece());
                return true;
            }
            return false;
        }

        private bool CheckAvailablePosition(ISet<Pair<int, int>> coords)
        {
            return InBound(coords) && !Collide(coords);
        }

        private static bool InBound(ISet<Pair<int, int>> coords)
        {
            return coords.Select(c => c.GetY).Min() >= 0
                && coords.Select(c => c.GetY).Max() < GridCols
                && coords.Select(c => c.GetX).Max() < GridRows;
        }

        private bool Collide(ISet<Pair<int, int>> coords)
        {
            return GetListComponents(_pieces.Count - 1)
                .Where(coords.Contains).Any();
        }

        private ISet<int> GetCompletedRows()
        {
            return Enumerable.Range(0, GridRows)
                .Select(i => GetListComponents(_pieces.Count)
                    .Where(c => c.GetX == i)
                    .Count() == GridCols ? i : -1)
                .Where(e => e != -1)
                .ToHashSet();
        }

        private void RemoveRows()
        {
            foreach (int l in _deletedLines.Order())
            {
                _pieces = _pieces.Select(p => p.Delete(l).ToList())
                    .SelectMany(e => e).ToList();
                Pack(l);
            }
        }

        private void Pack(int line)
        {
            _pieces.Where(p => p.Components.Any(c => c.GetX < line))
                .ToList().ForEach(p => p.Translate(1, 0));
        }
    }
}