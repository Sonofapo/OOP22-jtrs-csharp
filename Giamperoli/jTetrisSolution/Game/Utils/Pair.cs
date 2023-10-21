namespace Game.Utils
{
    public class Pair<X, Y>
    {
        public Pair(X x, Y y)
        {
            GetX = x;
            GetY = y;
        }

        public X GetX { get; }

        public Y GetY { get; }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + ((GetX == null) ? 0 : GetX.GetHashCode());
            result = prime * result + ((GetY == null) ? 0 : GetY.GetHashCode());
            return result;
        }

        public override bool Equals(object? obj)
        {
            return obj is Pair<X, Y> pair &&
                EqualityComparer<X>.Default.Equals(GetX, pair.GetX) &&
                EqualityComparer<Y>.Default.Equals(GetY, pair.GetY);
        }

        public override string ToString()
        {
            return "Pair [x=" + GetX + ", y=" + GetY + "]";
        }
    }
}