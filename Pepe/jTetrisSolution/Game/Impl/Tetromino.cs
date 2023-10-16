#pragma warning disable CS8618
using Game.Api;
using Game.Utils;

namespace Game.Impl
{
    public class Tetromino : ITetromino
    {
        private int XPosition { get; set; }
        private int YPosition { get; set; }
        private Pair<double, double> Center { get; set; }
        
        private ISet<Pair<int, int>> _components;
        public ISet<Pair<int, int>> Components
        {
            get => new HashSet<Pair<int, int>>(
                    _components.Select(c => new Pair<int, int>(c.GetX + XPosition, c.GetY + YPosition)));
            private set => _components = value;
        }

        public Tetromino(ISet<Pair<int, int>> components, int x, int y)
        {
            Components = new HashSet<Pair<int, int>>(components);
            XPosition = x;
            YPosition = y;
            Center = EvaluateCenter();
        }

        private Pair<double, double> EvaluateCenter()
        {
            double c = Components
                .Select(e => e.GetX)
                .Concat(Components.Select(e => e.GetY)).Max() / 2.0;
            return new Pair<double, double>(c, c);
        }   

        public ITetromino Copy()
        {
            return new Tetromino(Components, XPosition, YPosition);
        }

        public ISet<ITetromino> Delete(int position)
        {
            if (Components.Any(c => c.GetX + XPosition == position))
            {
                return new HashSet<ITetromino>(Components
                    .Where(c => c.GetX + XPosition != position)
                    .Select(c => new Tetromino(new HashSet<Pair<int, int>> { c }, XPosition, YPosition)));

            }
            return new HashSet<ITetromino> { this };
        }

        public void Rotate()
        {
            Components = new HashSet<Pair<int, int>>(
                Components.Select(c => new Pair<int, int>(
                    (int) (c.GetY - Center.GetY + Center.GetX),
                    (int) (Center.GetX - c.GetX + Center.GetY))
                )
            );
        }

        public void Translate(int x, int y)
        {
            XPosition += x;
            YPosition += y;
        }
    }
}
