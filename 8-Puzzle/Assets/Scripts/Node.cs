using System;

namespace Algorithms
{
    public class Node : IComparable
    {
        public string data { get; set; }
        public char move { get; set; }
        public Node parent { get; set; }
        public float score { get; set; }
        public int depth { get; set; }

        public int CompareTo(object obj) {
            if (obj == null) return 1;

            Node otherNode = obj as Node;
            return (int) (this.score - otherNode.score);
       }
    }
}
