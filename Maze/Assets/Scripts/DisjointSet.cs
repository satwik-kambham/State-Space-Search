namespace Generation
{
    public class DisjointSet
    {
        private int[] parent;
        private int[] size;
        private int count;

        public DisjointSet(int n)
        {
            count = n;
            parent = new int[n];
            size = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
                size[i] = 1;
            }
        }

        public int find(int p)
        {
            int root = p;
            while (root != parent[root])
                root = parent[root];
            while (p != root)
            {
                int newParent = parent[p];
                parent[p] = root;
                p = newParent;
            }
            return root;
        }

        public void union(int p, int q)
        {
            int rootP = find(p);
            int rootQ = find(q);
            if (rootP == rootQ) return;

            if (size[rootP] < size[rootQ])
            {
                parent[rootP] = rootQ;
                size[rootQ] += size[rootP];
            }
            else
            {
                parent[rootQ] = rootP;
                size[rootP] += size[rootQ];
            }
            count--;
        }
    }
}