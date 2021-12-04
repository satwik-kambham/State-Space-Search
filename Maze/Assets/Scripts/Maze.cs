namespace Generation
{
    public class Maze
    {
        // Grid of cells
        public Cell[,] cells;

        public Maze(int n, int m)
        {
            cells = new Cell[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++) cells[i, j] = new Cell(i, j);
        }
    }
}
