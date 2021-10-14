using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public GameObject piecesController;
    public GameObject numberOfMoves;
    public GameObject nodesSearched;
    public GameObject duplicatesEncountered;
    private PiecesController piecesControllerScript;

    void Start()
    {
        piecesControllerScript = piecesController.GetComponent<PiecesController>();
    }

    // TODO Change speed on the go
    void Update()
    {
        if (piecesControllerScript.solved) {
            numberOfMoves.GetComponent<UnityEngine.UI.Text>().text
                = $"Path found with {piecesControllerScript.moveCount} moves";
            nodesSearched.GetComponent<UnityEngine.UI.Text>().text
                = $"{piecesControllerScript.nodesSearched} nodes searched";
            duplicatesEncountered.GetComponent<UnityEngine.UI.Text>().text
                = $"{piecesControllerScript.duplicatesEncountered} duplicates Encountered";
        }
    }
}
