using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightPuzzle;

public class PiecesController : MonoBehaviour
{
    public GameObject puzzlePiecePrefab;
    private GameObject[] puzzlePieces;
    public Environment environment;

    void Start()
    {
        environment = new Environment();
        puzzlePieces = new GameObject[9];

        for (int i = 0; i < puzzlePieces.Length - 1; i++) {
            puzzlePiecePrefab.transform.Find("Value").gameObject.GetComponent<TextMesh>().text
                = environment.gameState[i].ToString();
            puzzlePieces[i] = Instantiate(puzzlePiecePrefab, new Vector3(i%3-1, 0, 1-i/3) * 10, Quaternion.identity, transform);
        }
    }

    void Update()
    {
        if (Input.anyKeyDown) {
            int i = environment.getEmptyPieceLocation();
            if (Input.GetKeyDown(KeyCode.S)) moveUp(i);
            else if (Input.GetKeyDown(KeyCode.W)) moveDown(i);
            else if (Input.GetKeyDown(KeyCode.A)) moveRight(i);
            else if (Input.GetKeyDown(KeyCode.D)) moveLeft(i);
            else if (Input.GetKeyDown(KeyCode.R)) scrambleState(10);
        }
    }

    public void moveUp(int i) {
        if (environment.canMoveUp(i)) {
            environment.moveUp(i);
            puzzlePieces[i-3-1].transform.Translate(Vector3.back * 10);
            swap(i, i-3);
        }
    }

    public void moveDown(int i) {
        if (environment.canMoveDown(i)) {
            environment.moveDown(i);
            puzzlePieces[i+3-1].transform.Translate(Vector3.forward * 10);
            swap(i, i+3);
        }
    }

    public void moveLeft(int i) {
        if (environment.canMoveLeft(i)) {
            environment.moveLeft(i);
            puzzlePieces[i-1-1].transform.Translate(Vector3.right * 10);
            swap(i, i-1);
        }
    }

    public void moveRight(int i) {
        if (environment.canMoveRight(i)) {
            environment.moveRight(i);
            puzzlePieces[i+1-1].transform.Translate(Vector3.left * 10);
            swap(i, i+1);
        }
    }

    public void scrambleState(int limit = 50) {
        System.Random random = new System.Random();
        int i;
        while(limit-- > 0) {
            i = environment.getEmptyPieceLocation();
            int x = random.Next(4);
            if (x == 0) moveUp(i);
            else if (x == 1) moveDown(i);
            else if (x == 2) moveLeft(i);
            else moveRight(i);
        }
    }

    public void swap(int i, int j) {
        GameObject x = puzzlePieces[i-1];
        puzzlePieces[i-1] = puzzlePieces[j-1];
        puzzlePieces[j-1] = x;
    }

}
