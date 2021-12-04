using UnityEngine;

namespace Generation
{
    public class Cell
    {
        // Neighboring cells
        public Cell[] neighbors;
        public GameObject[] walls;
        public Cell()
        {
            neighbors = new Cell[4];
            walls = new GameObject[4];
        }
    }
}
