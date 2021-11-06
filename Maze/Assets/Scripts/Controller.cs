using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generation;

public class Controller : MonoBehaviour {
	public Maze maze;
	public GameObject wall;
	public int n;
	public int m;
	private float cellWidth;
	private float cellHeight;

	void Start() {
		maze       = new Maze(n, m);
		cellWidth  = 100f / n;
		cellHeight = 100f / m;
		Vector3    location;
		Quaternion rotation;

		for (int k = 0; k < 4; k++) {
			if (k % 2 == 0) {
				wall.transform.localScale = new Vector3(cellWidth, wall.transform.localScale.y, 1);
				if (k == 2) location = new Vector3(0, 0, cellHeight / 2);
				else location = new Vector3(0, 0, -cellHeight / 2);
				rotation = new Quaternion(90, 0, 0, 0);
			} else {
				wall.transform.localScale = new Vector3(1, wall.transform.localScale.y, cellHeight);
				if (k == 1) location = new Vector3(cellWidth / 2, 0, 0);
				else location = new Vector3(-cellWidth / 2, 0, 0); ;
				rotation = new Quaternion(0, 0, 90, 0);
			}
			for (int i = 0; i < n; i++)
				for (int j = 0; j < m; j++)
					maze.cells[i, j].walls[k] = Instantiate(wall, location + new Vector3((i - n / 2) * cellWidth, 0, (j - m / 2) * cellHeight), rotation);
		}
	}

	void Update() {
		if (Input.anyKeyDown)
			if (Input.GetKeyDown(KeyCode.S)) StartGeneration();
	}

	void StartGeneration() {
		//
	}
}
