using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public GameObject piecesController;
    public GameObject numberOfMoves;
    public GameObject nodesSearched;
    private PiecesController piecesControllerScript;

    void Start()
    {
        numberOfMoves.GetComponent<UnityEngine.UI.Text>().text = "";
        nodesSearched.GetComponent<UnityEngine.UI.Text>().text = "";
        piecesControllerScript = piecesController.GetComponent<PiecesController>();
    }

    void Update()
    {
        if (piecesControllerScript.solved) {
            numberOfMoves.GetComponent<UnityEngine.UI.Text>().text
                = $"Path found with {piecesControllerScript.moveCount} moves";
            nodesSearched.GetComponent<UnityEngine.UI.Text>().text
                = $"{piecesControllerScript.nodesSearched} nodes searched";
        }
    }
}
