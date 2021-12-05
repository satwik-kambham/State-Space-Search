using System.Collections.Generic;
using Generation;

namespace Search
{
    public class DFS
    {
        Cell start;
        Cell goal;

        public DFS(Cell _start, Cell _goal)
        {
            start = _start;
            goal = _goal;
        }

        public Cell solve()
        {
            Stack<Cell> stack = new Stack<Cell>();
            HashSet<Cell> visited = new HashSet<Cell>();

            stack.Push(start);

            while (stack.Count != 0)
            {
                Cell c = stack.Pop();
                visited.Add(c);
                if (c == goal) return c;

                foreach (Cell cell in c.neighbors)
                    if (cell != null && !visited.Contains(cell))
                    {
                        cell.parent = c;
                        stack.Push(cell);
                    }
            }

            return null;
        }
    }
}