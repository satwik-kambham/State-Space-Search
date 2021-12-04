using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System;

namespace Generation
{
    public class RDFS
    {
        Controller c;
        Stack<Cell> backtrackingPath;
        Random random = new Random();
        public RDFS(Controller _c)
        {
            c = _c;
            backtrackingPath = new Stack<Cell>();
        }

        // Generates maze from the given starting cell
        public void generateMaze()
        {
            int i = random.Next(c.n), j = random.Next(c.m);
            Cell startingCell = c.maze.cells[i, j];
            backtrackingPath.Push(startingCell);

            Cell[] completeNeighbors;
            Cell currentCell;
            while (backtrackingPath.Count != 0)
            {
                currentCell = backtrackingPath.Peek();
                currentCell.visited = true;
                completeNeighbors = c.getNeighbors(currentCell);
                if (completeNeighbors.Length - getVisitedNeighborsCount(completeNeighbors) == 0)
                    backtrackingPath.Pop();
                else
                {
                    IEnumerable<Cell> neighbors = completeNeighbors.Where(cell => cell != null && cell.visited != true);
                    int direction = random.Next(neighbors.Count());
                    Cell neighbor = neighbors.ToArray()[direction];
                    backtrackingPath.Push(neighbor);
                    c.setNeighbor(getDirection(completeNeighbors, neighbor), currentCell);
                }
            }
        }

        public int getDirection(Cell[] neighbors, Cell neighbor)
        {
            for (int i = 0; i < neighbors.Length; i++)
            {
                if (neighbors[i] == neighbor) return i;
            }
            return -1;
        }

        public int getDirection(Cell c1, Cell c2)
        {
            if (c1.j - c2.j == 1) return 0;
            if (c1.i - c2.i == 1) return 1;
            if (c1.j - c2.j == -1) return 2;
            if (c1.i - c2.i == -1) return 3;
            return -1;
        }

        public int getVisitedNeighborsCount(Cell[] neighbors)
        {
            int count = 0;
            foreach (Cell cell in neighbors)
                if (cell == null || cell.visited) count++;
            return count;
        }
    }
}