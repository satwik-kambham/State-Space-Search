namespace Generation
{
    public class Maze
    {
        // Dimensions of maze
        public int n;
        public int m;
        // Grid of cells
        public Cell[,] cells;

        public Maze(int _n, int _m)
        {
            n = _n;
            m = _m;
            cells = new Cell[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++) cells[i, j] = new Cell(i, j);
        }
    }
}
