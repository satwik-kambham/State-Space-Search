using System.Collections.Generic;
using Generation;
using System;

namespace Search
{
    public class AStar
    {
        Cell start;
        Cell goal;

        public AStar(Cell _start, Cell _goal)
        {
            start = _start;
            goal = _goal;
        }

        public Cell solve()
        {
            MaxPQ maxPQ = new MaxPQ();
            HashSet<Cell> visited = new HashSet<Cell>();

            start.score = -(Math.Abs(goal.i - start.i) + Math.Abs(goal.j - start.j));
            start.depth = 0;
            maxPQ.insert(start);

            while (maxPQ.count != 0)
            {
                Cell c = maxPQ.remove() as Cell;
                visited.Add(c);
                if (c == goal) return c;

                foreach (Cell cell in c.neighbors)
                    if (cell != null && !visited.Contains(cell))
                    {
                        cell.parent = c;
                        cell.depth = c.depth + 1;
                        cell.score = -(Math.Sqrt(Math.Pow(goal.i - cell.i, 2) + Math.Pow(goal.j - cell.j, 2))
                            + cell.depth);
                        maxPQ.insert(cell);
                    }
            }

            return null;
        }
    }
}