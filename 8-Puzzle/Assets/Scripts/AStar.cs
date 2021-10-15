using System.Collections.Generic;
using EightPuzzle;
using UnityEngine;

namespace Algorithms
{
    public class AStar
    {
        private HashSet<string> exploredNodes;
        private MaxPQ toExplore;
        private Environment environment;
        private int heuristic1;
        private int heuristic2;
        private string initialState;

        public AStar(Environment environment, int heuristic1, int heuristic2) {
            this.environment = environment;
            this.heuristic1 = heuristic1;
            this.heuristic2 = heuristic2;
            initialState = new string(environment.gameState);
            exploredNodes = new HashSet<string>();
            toExplore = new MaxPQ();
            Node startNode = new Node() {
                data = new string(environment.gameState),
                move = ' ',
                parent = null,
                score = heuristicFunction(initialState, initialState, heuristic1)
                    + heuristicFunction(initialState, environment.goalState, heuristic2),
                depth = 0
            };
            toExplore.insert(startNode);
        }

        public Node search(out int nodesSearched, out int duplicatesEncountered) {
            nodesSearched = 0;
            duplicatesEncountered = 0;
            while(true) {
                if (toExplore.count == 0) return null;
                Node currentNode = toExplore.remove() as Node;
                nodesSearched++;
                if (environment.isGoalState(currentNode.data.ToCharArray()))
                    return currentNode;
                exploredNodes.Add(currentNode.data);
                var possibleMoves = environment.getPossibleMoves(currentNode.data, out var action);
                for (int i = 0; i < possibleMoves.Count; i++) {
                    if (!exploredNodes.Contains(possibleMoves[i])) {
                        toExplore.insert(new Node() {
                                data = possibleMoves[i],
                                move = action[i],
                                parent = currentNode,
                                score = (heuristic1 == 3 ? -currentNode.depth : heuristicFunction(initialState, possibleMoves[i], heuristic1))
                                    + heuristicFunction(possibleMoves[i], environment.goalState, heuristic2),
                                depth = currentNode.depth + 1
                            });
                    } else duplicatesEncountered++;
                }
            }
        }

        public float heuristicFunction(string state, string goalState, int heuristic) {
            if (heuristic == 0) return (float) environment.misplacedTiles(state, goalState);
            if (heuristic == 1) return environment.eucledianDistance(state, goalState);
            if (heuristic == 2) return (float) environment.manhattanDistance(state, goalState);
            return (float) environment.misplacedTiles(state, goalState);
        }
    }
}
