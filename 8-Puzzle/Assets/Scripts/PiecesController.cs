using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightPuzzle;
using Algorithms;

public class PiecesController : MonoBehaviour
{
    public GameObject puzzlePiecePrefab;
    private GameObject[] puzzlePieces;
    public Environment environment;
    private Stack<char> moves;
    public float moveDelay = 0.001f;
    public float delayBetweenMoves = 1f;
    public bool solved = false;
    public bool stop = false;
    public int moveCount;
    public int nodesSearched;
    public int duplicatesEncountered;
    private int heuristic;
    private int heuristic1;
    private int heuristic2;

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
            int i = environment.getEmptyPieceLocation(environment.gameState);
            if (Input.GetKeyDown(KeyCode.S)) moveUp(i);
            else if (Input.GetKeyDown(KeyCode.W)) moveDown(i);
            else if (Input.GetKeyDown(KeyCode.A)) moveRight(i);
            else if (Input.GetKeyDown(KeyCode.D)) moveLeft(i);
        }
    }

    public void setMoveDelay(float x) => moveDelay = x;
    public void setDelayBetweenMoves(float x) => delayBetweenMoves = x;

    public void stopSolving() => stop = true;

    public void getMoves(Node node) {
        moves = new Stack<char>();
        while (node != null) {
            moves.Push(node.move);
            node = node.parent;
        }
        StartCoroutine(performMoves());
    }

    public void solveUsingBFS() {
        BFS bfs = new BFS(environment);
        Node node = bfs.search(out nodesSearched, out duplicatesEncountered);
        getMoves(node);
    }

    public void solveUsingDFS() {
        DFS dfs = new DFS(environment);
        Node node = dfs.search(out nodesSearched, out duplicatesEncountered);
        getMoves(node);
    }

    public void changeHeuristic(int i) => heuristic = i;
    public void changeHeuristic1(int i) => heuristic1 = i;
    public void changeHeuristic2(int i) => heuristic2 = i;

    public void solveUsingGreedy() {
        Greedy greedy = new Greedy(environment, heuristic);
        Node node = greedy.search(out nodesSearched, out duplicatesEncountered);
        getMoves(node);
    }

    public void solveUsingAStar() {
        AStar astar = new AStar(environment, heuristic1, heuristic2);
        Node node = astar.search(out nodesSearched, out duplicatesEncountered);
        getMoves(node);
    }

    IEnumerator performMoves() {
        moveCount = moves.Count;
        solved = true;
        while (moves.Count != 0 && !stop) {
            int i = environment.getEmptyPieceLocation(environment.gameState);
            char move = moves.Pop();
            if (move == 'U') moveUp(i);
            else if (move == 'D') moveDown(i);
            else if (move == 'L') moveLeft(i);
            else if (move == 'R') moveRight(i);
            yield return new WaitForSeconds(delayBetweenMoves);
        }
        solved = false;
        stop = false;
        yield return null;
    }

    IEnumerator move(int i, Vector3 v) {
        GameObject gameObject = puzzlePieces[i];
        for (int j = 0; j < 50; j++) {
            gameObject.transform.Translate(v/50);
            yield return new WaitForSeconds(moveDelay);
        }
    }

    public void moveUp(int i) {
        if (environment.canMoveUp(i)) {
            environment.moveUp(i);
            swap(i, i-3);
            StartCoroutine(move(i-1, Vector3.back * 10));
        }
    }

    public void moveDown(int i) {
        if (environment.canMoveDown(i)) {
            environment.moveDown(i);
            swap(i, i+3);
            StartCoroutine(move(i-1, Vector3.forward * 10));
        }
    }

    public void moveLeft(int i) {
        if (environment.canMoveLeft(i)) {
            environment.moveLeft(i);
            swap(i, i-1);
            StartCoroutine(move(i-1, Vector3.right * 10));
        }
    }

    public void moveRight(int i) {
        if (environment.canMoveRight(i)) {
            environment.moveRight(i);
            swap(i, i+1);
            StartCoroutine(move(i-1, Vector3.left * 10));
        }
    }

    public void scrambleState(int limit = 50) {
        System.Random random = new System.Random();
        int i;
        while(limit-- > 0) {
            i = environment.getEmptyPieceLocation(environment.gameState);
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
