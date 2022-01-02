using System.Collections.Generic;
using Generation;
using System;

namespace Search
{
    public class Greedy
    {
        Cell start;
        Cell goal;

        public Greedy(Cell _start, Cell _goal)
        {
            start = _start;
            goal = _goal;
        }

        public Cell solve()
        {
            MaxPQ maxPQ = new MaxPQ();
            HashSet<Cell> visited = new HashSet<Cell>();

            start.score = - (Math.Abs(goal.i - start.i) + Math.Abs(goal.j - start.j));
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
                        cell.score = - (Math.Abs(goal.i - cell.i) + Math.Abs(goal.j - cell.j));
                        maxPQ.insert(cell);
                    }
            }

            return null;
        }
    }
}