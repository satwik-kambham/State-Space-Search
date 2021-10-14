using System.Collections.Generic;
using EightPuzzle;
using UnityEngine;

namespace Algorithms
{
    public class Greedy
    {
        private HashSet<string> exploredNodes;
        private MaxPQ toExplore;
        private Environment environment;
        private int heuristic;

        public Greedy(Environment environment, int heuristic) {
            this.environment = environment;
            exploredNodes = new HashSet<string>();
            toExplore = new MaxPQ();
            Node startNode = new Node() {
                data = new string(environment.gameState),
                move = ' ',
                parent = null,
                score = environment.misplacedTiles(new string(environment.gameState))
            };
            toExplore.insert(startNode);
            this.heuristic = heuristic;
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
                                score = heuristicFunction(possibleMoves[i])
                            });
                    } else duplicatesEncountered++;
                }
            }
        }

        public float heuristicFunction(string state) {
            if (heuristic == 0) return (float) environment.misplacedTiles(state);
            if (heuristic == 1) return environment.eucledianDistance(state);
            if (heuristic == 3) return (float) environment.manhattanDistance(state);
            return (float) environment.misplacedTiles(state);
        }
    }
}
