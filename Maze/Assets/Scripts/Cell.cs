using UnityEngine;

namespace Generation {
	public class Cell {
		// Neighbouring cells
		public Cell[] neighbours;
		public GameObject[] walls;

		public Cell() {
			neighbours = new Cell[4];
			walls      = new GameObject[4];
		}

		public void setNeighbour(int i, Cell c) {
			neighbours[i] = c;
		}
	}
}
