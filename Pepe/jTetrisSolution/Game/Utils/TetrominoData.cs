namespace Game.Utils
{
    public class TetrominoData
    {
        public static readonly ISet<Pair<int, int>> CoordO =
            new HashSet<Pair<int, int>>() { new(0, 0), new(0, 1), new(1, 0), new(1, 1) };

        public static readonly ISet<Pair<int, int>> CoordL =
            new HashSet<Pair<int, int>>() { new(0, 0), new(0, 1), new(0, 2), new(1, 0) };

        public static readonly ISet<Pair<int, int>> CoordJ =
            new HashSet<Pair<int, int>>() { new(0, 0), new(0, 1), new(0, 2), new(1, 2) };

        public static readonly ISet<Pair<int, int>> CoordI =
            new HashSet<Pair<int, int>>() { new(1, 0), new(1, 1), new(1, 2), new(1, 3) };

        public static readonly ISet<Pair<int, int>> CoordT =
            new HashSet<Pair<int, int>>() { new(0, 0), new(0, 1), new(0, 2), new(1, 1) };

        public static readonly ISet<Pair<int, int>> CoordZ =
            new HashSet<Pair<int, int>>() { new(0, 0), new(0, 1), new(1, 1), new(1, 2) };

        public static readonly ISet<Pair<int, int>> CoordS =
            new HashSet<Pair<int, int>>() { new(1, 0), new(1, 1), new(0, 1), new(0, 2) };
    }
}