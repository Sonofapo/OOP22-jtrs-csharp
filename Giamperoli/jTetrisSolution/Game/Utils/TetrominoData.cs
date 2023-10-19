namespace Game.Utils
{
    public class TetrominoData
    {
        public static readonly ISet<Pair<int, int>> O_COORD =
            new HashSet<Pair<int, int>>() { new(0, 0), new(0, 1), new(1, 0), new(1, 1) };

        public static readonly ISet<Pair<int, int>> L_COORD =
            new HashSet<Pair<int, int>>() { new(0, 0), new(0, 1), new(0, 2), new(1, 0) };

        public static readonly ISet<Pair<int, int>> J_COORD =
            new HashSet<Pair<int, int>>() { new(0, 0), new(0, 1), new(0, 2), new(1, 2) };

        public static readonly ISet<Pair<int, int>> I_COORD =
            new HashSet<Pair<int, int>>() { new(1, 0), new(1, 1), new(1, 2), new(1, 3) };

        public static readonly ISet<Pair<int, int>> T_COORD =
            new HashSet<Pair<int, int>>() { new(0, 0), new(0, 1), new(0, 2), new(1, 1) };

        public static readonly ISet<Pair<int, int>> Z_COORD =
            new HashSet<Pair<int, int>>() { new(0, 0), new(0, 1), new(1, 1), new(1, 2) };

        public static readonly ISet<Pair<int, int>> S_COORD =
            new HashSet<Pair<int, int>>() { new(1, 0), new(1, 1), new(0, 1), new(0, 2) };
    }
}