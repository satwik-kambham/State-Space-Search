using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System;

namespace Generation
{
    public class RDFS
    {
        Controller c;
        Stack<Cell> backtrackingPath; // Stack used for backtracking
        Random random = new Random();
        public RDFS(Controller _c)
        {
            c = _c;
            backtrackingPath = new Stack<Cell>();
        }

        // Generates maze from the given starting cell
        public void generateMaze()
        {
            Cell[] completeNeighbors;
            Cell currentCell;

            // Selecting random starting cell
            int i = random.Next(c.n), j = random.Next(c.m);
            Cell startingCell = c.maze.cells[i, j];

            // Adding randomly chosen starting cell to the backtracking stack
            backtrackingPath.Push(startingCell);

            // Run loop until all the cells have been visited
            while (backtrackingPath.Count != 0)
            {
                // Get the cell to explore from the stack
                currentCell = backtrackingPath.Peek();

                currentCell.visited = true;

                // Get the possible neighbors of the current cell (null reference if a neighbor does not exist)
                completeNeighbors = c.getNeighbors(currentCell);

                // If all the neighbors have been viewed then backtrack
                if (completeNeighbors.Length - getVisitedNeighborsCount(completeNeighbors) == 0)
                    backtrackingPath.Pop();
                else
                {
                    // Select a random neighbor from all non-null and un-visited cells
                    IEnumerable<Cell> neighbors = completeNeighbors.Where(cell => cell != null && cell.visited != true);
                    int direction = random.Next(neighbors.Count());
                    Cell neighbor = neighbors.ToArray()[direction];

                    // Add selected neighbor to stack for backtracking
                    backtrackingPath.Push(neighbor);

                    // Remove walls between neighbor and set as actual neighbor
                    c.setNeighbor(getDirection(completeNeighbors, neighbor), currentCell);
                }
            }
        }

        // Find direction of the neighbor wrt to the current cell
        public int getDirection(Cell[] neighbors, Cell neighbor)
        {
            for (int i = 0; i < neighbors.Length; i++)
            {
                if (neighbors[i] == neighbor) return i;
            }
            return -1;
        }

        // Get the number of neighors are null or which have been visited
        public int getVisitedNeighborsCount(Cell[] neighbors)
        {
            int count = 0;
            foreach (Cell cell in neighbors)
                if (cell == null || cell.visited) count++;
            return count;
        }
    }
}