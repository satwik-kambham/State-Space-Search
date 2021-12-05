using System.Collections.Generic;
using Generation;

namespace Search
{
    public class BFS
    {
        Cell start;
        Cell goal;

        public BFS(Cell _start, Cell _goal)
        {
            start = _start;
            goal = _goal;
        }

        public Cell solve()
        {
            Queue<Cell> queue = new Queue<Cell>();
            HashSet<Cell> visited = new HashSet<Cell>();

            queue.Enqueue(start);

            while (queue.Count != 0)
            {
                Cell c = queue.Dequeue();
                visited.Add(c);
                if (c == goal) return c;

                foreach (Cell cell in c.neighbors)
                    if (cell != null && !visited.Contains(cell))
                    {
                        cell.parent = c;
                        queue.Enqueue(cell);
                    }
            }

            return null;
        }
    }
}