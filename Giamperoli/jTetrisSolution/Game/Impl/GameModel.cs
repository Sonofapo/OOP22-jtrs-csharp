using Game.Api;
using Game.Utils;

namespace Game.Impl
{
    public class GameModel : IGameModel
    {

        public const int GridRows = 20;

        public const int GridCols = 10;

        ///get pieces e deleted lines da modificare 
        public IList<ITetromino> Pieces { get; private set;}
        public ISet<int> DeletedLines { get; }

        public GameModel()
        {
            Pieces = new List<ITetromino>();
            DeletedLines = new HashSet<int>();
        }

        public bool Advance(IGameModel.Interaction i)
        {
            bool p(ISet<Pair<int, int>> c) => CheckAvailablePosition(c);
            Action<ITetromino> a;
            switch (i)
            {
                case IGameModel.Interaction.ROTATE:
                {
                    a = t => t.Rotate();
                    break;
                }
                case IGameModel.Interaction.DOWN:
                {
                    a = t => t.Translate(1, 0);
                    break;
                }
                case IGameModel.Interaction.LEFT:
                {
                    a = t => t.Translate(0, -1);
                    break;
                }
                case IGameModel.Interaction.RIGHT:
                {
                    a = t => t.Translate(0, 1);
                    break;
                }
                default:
                {
                    a = t => t.Copy();
                    break;
                }
            };
            return Action(a, p);
        }

        public int DeleteRows()
        {
            DeletedLines.UnionWith(GetCompletedRows());
            RemoveRows();
            return DeletedLines.Count;
        }

        public bool NextPiece(ITetromino next)
        {
            Pieces.Add(next);
            if(Collide(next.Components))
            {
                Pieces.RemoveAt(Pieces.Count - 1);
                return false;
            }
            return true;
        }

        private ITetromino GetCurrentPiece()
        {
            return Pieces.ElementAt(Pieces.Count - 1);
        }

        private List<Pair<int, int>> GetListComponents(int end)
        {
            return Pieces.Where((p, i) => i < end).ToList()
                .SelectMany(p => p.Components.ToList()).ToList();
        }

        private bool Action(Action<ITetromino> a, Predicate<ISet<Pair<int, int>>> p)
        {
            var tmp = GetCurrentPiece().Copy();
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
            return GetListComponents(Pieces.Count - 1)
                .Where(coords.Contains).Any();
        }

        private ISet<int> GetCompletedRows()
        {
            return Enumerable.Range(0, GridRows)
                .Select(i => GetListComponents(Pieces.Count)
                    .Where(c => c.GetX == i)
                    .Count() == GridCols ? i : -1)
                .Where(e => e != -1)
                .ToHashSet();
        }

        private void RemoveRows()
        {
            foreach (int l in DeletedLines.Order())
            {
                Pieces = Pieces.Select(p => p.Delete(l).ToList())
                    .SelectMany(e => e).ToList(); 
                Pack(l);
            }
        }

        private void Pack(int line)
        {
            Pieces.Where(p => p.Components.Any(c => c.GetX < line))
                .ToList().ForEach(p => p.Translate(1, 0));
        }
    }
}