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
            _components = new HashSet<Pair<int, int>>(components);
            Components = _components;
            XPosition = x;
            YPosition = y;
            Center = EvaluateCenter();
        }

        private Pair<double, double> EvaluateCenter()
        {
            double c = Components
                .Select(e => e.GetX)
                .Concat(_components.Select(e => e.GetY)).Max() / 2.0;
            return new Pair<double, double>(c, c);
        }   

        public ITetromino Copy()
        {
            return new Tetromino(_components, XPosition, YPosition);
        }

        public ISet<ITetromino> Delete(int position)
        {
            /*
            if (this.compontents.stream().anyMatch(c -> c.getX() + this.xPosition == position)) {
            return this.compontents.stream()
                .filter(c -> c.getX() + this.xPosition != position)
                .map(c -> new TetrominoImpl(Set.of(c), this.xPosition, this.yPosition, this.color))
                .collect(Collectors.toCollection(HashSet::new));
            }
            return new HashSet<>(Set.of(this));
            */
            if (_components.Any(c => c.GetX + XPosition == position))
            {
                return new HashSet<ITetromino>(_components
                    .Where(c => c.GetX + XPosition != position)
                    .Select(c => new Tetromino(new HashSet<Pair<int, int>> { c }, XPosition, YPosition)));

            }
            return new HashSet<ITetromino> { this };
        }

        public void Rotate()
        {
            Components = new HashSet<Pair<int, int>>(
                _components.Select(c => new Pair<int, int>(
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
