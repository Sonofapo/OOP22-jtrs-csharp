using Game.Api;
using Game.Utils;

namespace Game.Impl
{
    public class Tetromino : ITetromino
    {
        private int _xPosition;
        private int _yPosition;
        private readonly Pair<double, double> _center;
        private ISet<Pair<int, int>> _components;

        public ISet<Pair<int, int>> Components
        {
            get => _components
                .Select(c => new Pair<int, int>(c.GetX + _xPosition, c.GetY + _yPosition))
                .ToHashSet();
        }

        public Tetromino(ISet<Pair<int, int>> components, int x, int y)
        {
            _components = components.ToHashSet();
            _xPosition = x;
            _yPosition = y;
            _center = EvaluateCenter();
        }

        private Pair<double, double> EvaluateCenter()
        {
            double c = _components
                .Select(e => e.GetX)
                .Concat(_components.Select(e => e.GetY))
                .Max() / 2.0;
            return new(c, c);
        }

        public ITetromino Copy()
        {
            return new Tetromino(_components, _xPosition, _yPosition);
        }

        public ISet<ITetromino> Delete(int position)
        {
            if (_components.Any(c => c.GetX + _xPosition == position))
            {
                return _components
                    .Where(c => c.GetX + _xPosition != position)
                    .Select(c => new Tetromino(new HashSet<Pair<int, int>> { c },
                        _xPosition, _yPosition) as ITetromino)
                    .ToHashSet();

            }
            return new HashSet<ITetromino> { this };
        }

        public void Rotate()
        {
            _components = _components.Select(c => new Pair<int, int>(
                (int) (c.GetY - _center.GetY + _center.GetX),
                (int) (_center.GetX - c.GetX + _center.GetY))
            ).ToHashSet();
        }

        public void Translate(int x, int y)
        {
            _xPosition += x;
            _yPosition += y;
        }
    }
}
