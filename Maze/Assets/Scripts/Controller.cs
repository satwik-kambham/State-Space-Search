using System.Collections;
using System.Collections.Generic;
using Generation;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Maze maze; // Maze containing cells
    public GameObject wall; // Prefab of wall
    public int n;
    public int m;
    private float cellWidth;
    private float cellHeight;

    void Start()
    {
        maze = new Maze(n, m);
        cellWidth = 100f / n;
        cellHeight = 100f / m;
        Vector3 location;
        Quaternion rotation;

        // Instantiating walls between cells
        for (int k = 0; k < 4; k++)
        {
            if (k % 2 == 0)
            {
                wall.transform.localScale =
                    new Vector3(cellWidth, wall.transform.localScale.y, 1);
                if (k == 2)
                    location = new Vector3(0, 0, -cellHeight / 2);
                else
                    location = new Vector3(0, 0, +cellHeight / 2);
                rotation = new Quaternion(90, 0, 0, 0);
            }
            else
            {
                wall.transform.localScale =
                    new Vector3(1, wall.transform.localScale.y, cellHeight);
                if (k == 1)
                    location = new Vector3(-cellWidth / 2, 0, 0);
                else
                    location = new Vector3(+cellWidth / 2, 0, 0);

                rotation = new Quaternion(0, 0, 90, 0);
            }

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    maze.cells[i, j].walls[k] =
                        Instantiate(wall,
                        location +
                        new Vector3((i - n / 2) * cellWidth,
                            0,
                            (j - m / 2) * cellHeight),
                        rotation);
        }
    }

    void Update()
    {
        if (Input.anyKeyDown)
            if (Input.GetKeyDown(KeyCode.S)) StartGeneration();
    }

    void StartGeneration()
    {
        setNeighbor(3, maze.cells[2, 2]);
    }

    // Sets reference to neighbor and destroy walls in between
    // top - 0, left - 1, down - 2, right - 3
    public void setNeighbor(int direction, Cell c1)
    {
        Cell c2;
        if (direction == 0)
            c2 = maze.cells[c1.i, c1.j + 1];
        else if (direction == 1)
            c2 = maze.cells[c1.i - 1, c1.j];
        else if (direction == 2)
            c2 = maze.cells[c1.i, c1.j - 1];
        else
            c2 = maze.cells[c1.i + 1, c1.j];


        c1.neighbors[direction] = c2;
        Destroy(c1.walls[direction]);
        c1.walls[direction] = null;

        if (direction > 1)
            direction -= 2;
        else
            direction += 2;

        c2.neighbors[direction] = c1;
        Destroy(c2.walls[direction]);
        c2.walls[direction] = null;
    }
}
