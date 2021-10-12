using System.Collections.Generic;
using EightPuzzle;

namespace Algorithms
{
    public class DFS
    {
        private HashSet<string> exploredNodes;
        private Stack<Node> toExplore;
        private Environment environment;

        public DFS(Environment environment) {
            this.environment = environment;
            exploredNodes = new HashSet<string>();
            toExplore = new Stack<Node>();
            Node startNode = new Node() {
                data = new string(environment.gameState),
                move = ' ',
                parent = null
            };
            toExplore.Push(startNode);
        }

        public Node search() {
            while(true) {
                if (toExplore.Count == 0) return null;
                Node currentNode = toExplore.Pop();
                if (environment.isGoalState(currentNode.data.ToCharArray()))
                    return currentNode;
                exploredNodes.Add(currentNode.data);
                var possibleMoves = environment.getPossibleMoves(currentNode.data, out var action);
                for (int i = 0; i < possibleMoves.Count; i++) {
                    if (!exploredNodes.Contains(possibleMoves[i])) {
                        toExplore.Push(new Node() {
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
