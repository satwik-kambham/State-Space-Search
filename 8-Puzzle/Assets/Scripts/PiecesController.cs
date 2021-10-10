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
            Debug.Log(new string(environment.gameState));
            int i = environment.getEmptyPieceLocation();
            if (Input.GetKeyDown(KeyCode.S) && environment.canMoveUp(i)) {
                environment.moveUp(i);
                puzzlePieces[i-3-1].transform.Translate(Vector3.back * 10);
                swap(i, i-3);
            } else if (Input.GetKeyDown(KeyCode.D) && environment.canMoveLeft(i)) {
                environment.moveLeft(i);
                puzzlePieces[i-1-1].transform.Translate(Vector3.right * 10);
                swap(i, i-1);
            } else if (Input.GetKeyDown(KeyCode.W) && environment.canMoveDown(i)) {
                environment.moveDown(i);
                puzzlePieces[i+3-1].transform.Translate(Vector3.forward * 10);
                swap(i, i+3);
            } else if (Input.GetKeyDown(KeyCode.A) && environment.canMoveRight(i)) {
                environment.moveRight(i);
                puzzlePieces[i+1-1].transform.Translate(Vector3.left * 10);
                swap(i, i+1);
            }
        }
    }

    public void swap(int i, int j) {
        GameObject x = puzzlePieces[i-1];
        puzzlePieces[i-1] = puzzlePieces[j-1];
        puzzlePieces[j-1] = x;
    }

}
