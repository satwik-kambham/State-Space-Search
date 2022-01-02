using UnityEngine;
using System;

namespace Generation
{
    public class Cell : IComparable
    {
        public int i, j; // Index in cells array of maze object
        public Cell[] neighbors; // Neighboring cells
        public GameObject[] walls;
        public bool visited;
        public Cell parent;
        public bool inMaze; // Whether the cell is a part of the maze
        public double score;
        public int depth;

        public Cell(int _i, int _j)
        {
            neighbors = new Cell[4];
            walls = new GameObject[4];
            i = _i;
            j = _j;
            visited = false;
            parent = null;
            inMaze = false;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Cell otherNode = obj as Cell;
            return (int)(this.score - otherNode.score);
        }
    }
}
