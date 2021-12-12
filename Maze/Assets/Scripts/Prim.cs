using System;
using System.Collections.Generic;

namespace Generation
{
    public class Prim
    {
        Controller controller;
        List<Wall> walls;

        public Prim(Controller controller)
        {
            this.controller = controller;
            walls = new List<Wall>();
        }

        public void generateMaze()
        {
            Random random = new Random();
            Cell randomStartingCell = controller.maze.cells[random.Next(controller.n), random.Next(controller.m)];

            randomStartingCell.inMaze = true;
            addWalls(randomStartingCell);

            while (walls.Count != 0)
            {
                Wall randomWall = walls[random.Next(walls.Count)];

                if (randomWall.c1.inMaze ^ randomWall.c2.inMaze)
                {
                    controller.setNeighbor(getDirection(controller.getNeighbors(randomWall.c1), randomWall.c2), randomWall.c1);

                    Cell toAdd;
                    if (randomWall.c1.inMaze) toAdd = randomWall.c2;
                    else toAdd = randomWall.c1;

                    toAdd.inMaze = true;
                    addWalls(toAdd);
                }

                walls.Remove(randomWall);
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

        public void addWalls(Cell c)
        {
            foreach (var cell in controller.getNeighbors(c))
            {
                if (cell != null)
                {
                    walls.Add(new Wall()
                    {
                        c1 = c,
                        c2 = cell
                    });
                }
            }
        }
    }
}
