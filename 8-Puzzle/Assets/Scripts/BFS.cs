using System.Collections.Generic;
using EightPuzzle;

namespace Algorithms
{
    public class BFS
    {
        private HashSet<string> exploredNodes;
        private Queue<Node> toExplore;
        private Environment environment;

        public BFS(Environment environment) {
            this.environment = environment;
            exploredNodes = new HashSet<string>();
            toExplore = new Queue<Node>();
            Node startNode = new Node() {
                data = new string(environment.gameState),
                move = ' ',
                parent = null
            };
            toExplore.Enqueue(startNode);
        }

        public Node search(out int nodesSearched) {
            nodesSearched = 0;
            while(true) {
                if (toExplore.Count == 0) return null;
                Node currentNode = toExplore.Dequeue();
                nodesSearched++;
                if (environment.isGoalState(currentNode.data.ToCharArray()))
                    return currentNode;
                exploredNodes.Add(currentNode.data);
                var possibleMoves = environment.getPossibleMoves(currentNode.data, out var action);
                for (int i = 0; i < possibleMoves.Count; i++) {
                    if (!exploredNodes.Contains(possibleMoves[i])) {
                        toExplore.Enqueue(new Node() {
                                data = possibleMoves[i],
                                move = action[i],
                                parent = currentNode
                            });
                    }
                }
            }
        }
    }
}
