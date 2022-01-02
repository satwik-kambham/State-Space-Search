using System;

namespace Search
{
    public class MaxPQ
    {
        private IComparable[] array;
        public int count { get; private set; }

        public MaxPQ(int defaultSize = 10)
        {
            array = new IComparable[defaultSize];
            count = 0;
        }

        private void doubleSize()
        {
            IComparable[] newArray = new IComparable[array.Length * 2];
            for (int i = 0; i < array.Length; i++)
                newArray[i] = array[i];
            array = newArray;
        }

        public void insert(IComparable data)
        {
            if (++count == array.Length) doubleSize();
            array[count] = data;

            // Moving the item up to the right position
            int parent = count / 2;
            int child = count;
            while (child > 1 && array[child].CompareTo(array[parent]) > 0)
            {
                swap(child, parent);
                child = parent;
                parent /= 2;
            }
        }

        public IComparable remove()
        {
            if (count == 0) throw new IndexOutOfRangeException();

            swap(1, count);

            int parent = 1;
            int child = parent * 2;
            while (parent <= (count - 1) / 2)
            {
                if (array[child].CompareTo(array[parent]) > 0 &&
                    array[child + 1].CompareTo(array[child]) > 0
                    && child + 1 < count) child++;
                if (array[child].CompareTo(array[parent]) > 0)
                    swap(parent, child);
                parent = child;
                child = parent * 2;
            }

            IComparable x = array[count--];
            array[count + 1] = null;
            return x;
        }

        private void swap(int i, int j)
        {
            IComparable x = array[i];
            array[i] = array[j];
            array[j] = x;
        }
    }
}
