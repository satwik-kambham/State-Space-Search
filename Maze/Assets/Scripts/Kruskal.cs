using System.Collections.Generic;
using System;

namespace Generation
{
    public class Kruskal
    {
        DisjointSet connectedCells;
        List<Wall> walls;
        Controller controller;

        public Kruskal(Controller controller)
        {
            this.controller = controller;
            connectedCells = new DisjointSet(controller.n * controller.m);
            walls = getWallsList(controller.maze.cells, controller);
        }

        public void generateMaze()
        {
            shuffleWalls(walls);
            foreach (var wall in walls)
            {
                if (connectedCells.find(getIndex(wall.c1)) != connectedCells.find(getIndex(wall.c2)))
                {
                    UnityEngine.Debug.Log("Hello");
                    controller.setNeighbor(getDirection(controller.getNeighbors(wall.c1), wall.c2), wall.c1);
                    connectedCells.union(getIndex(wall.c1), getIndex(wall.c2));
                }
            }
        }

        public int getIndex(Cell c)
        {
            return c.i * controller.n + c.j;
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

        public void shuffleWalls(List<Wall> walls)
        {
            Random random = new Random();
            for (int i = 0; i < walls.Count; i++)
            {
                int j = random.Next(walls.Count);
                var x = walls[i];
                walls[i] = walls[j];
                walls[j] = x;
            }
        }

        public List<Wall> getWallsList(Cell[,] cells, Controller controller)
        {
            List<Wall> walls = new List<Wall>();
            for (int i = 0; i < controller.n; i++)
            {
                for (int j = 0; j < controller.m - 1; j++)
                {
                    walls.Add(new Wall()
                    {
                        c1 = cells[i, j],
                        c2 = cells[i, j + 1]
                    });
                }
            }

            for (int i = 0; i < controller.n - 1; i++)
            {
                for (int j = 0; j < controller.m; j++)
                {
                    walls.Add(new Wall()
                    {
                        c1 = cells[i, j],
                        c2 = cells[i + 1, j]
                    });
                }
            }
            return walls;
        }
    }
}