using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueues
{
    class IndexMaxPQ<Key>:IEnumerable<Key>  //Maximum-oriented indexed PQ implementation using a binary heap.
    {
        private int n;           // number of elements on PQ
        private int[] pq;        // binary heap using 1-based indexing
        private int[] qp;        // inverse of pq - qp[pq[i]] = pq[qp[i]] = i
        private Key[] keys;      // keys[i] = priority of i
        private Comparer<Key> comparer;

        // Initializes an empty indexed priority queue with indices between 0 and maxN-1
        public IndexMaxPQ(int maxN,Comparer<Key> comparer)
        {
            if (maxN < 0) throw new ArgumentException();
            n = 0;
            keys = new Key[maxN + 1];    // make this of length maxN??
            pq = new int[maxN + 1];
            qp = new int[maxN + 1];                   // make this of length maxN??
            for (int i = 0; i <= maxN; i++)
                qp[i] = -1;
            this.comparer = comparer;
        }
        // IsEmpty()
        public bool IsEmpty()
        {
            return n == 0;
        }
        // Contains
        public bool Contains(int i)
        {
            return qp[i] != -1;
        }
        // Size
        public int Size()
        {
            return n;
        }
        //
        public int MaxIndex()
        {
            if (n == 0) throw new ArgumentException("Priority queue underflow");
            return pq[1];
        }
        public Key MaxKey()
        {
            if (n == 0) throw new ArgumentException("Priority queue underflow");
            return keys[pq[1]];
        }
        // Removes a maximum key and returns its associated index.
        public int DelMax()
        {
            if (n == 0) throw new ArgumentException("Priority queue underflow");
            int min = pq[1];
            Exch(1, n--);
            Sink(1);

            Debug.Assert(pq[n + 1] == min);
            qp[min] = -1;        // delete
            keys[min] = default(Key);    // to help with garbage collection
            pq[n + 1] = -1;        // not needed
            return min;
        }
        // Associate key with index i
        public void Insert(int i, Key key)
        {
            if (Contains(i)) throw new ArgumentException("index is already in the priority queue");
            n++;
            qp[i] = n;
            pq[n] = i;
            keys[i] = key;
            Swim(n);
        }
        //
        public Key KeyOf(int i)
        {
            if (!Contains(i)) throw new ArgumentException("index is not in the priority queue");
            else return keys[i];
        }
        //
        public void ChangeKey(int i, Key key)
        {
            if (!Contains(i)) throw new ArgumentException("index is not in the priority queue");
            keys[i] = key;
            Swim(qp[i]);
            Sink(qp[i]);
        }
        // Increase the key associated with index {@code i} to the specified value.
        public void IncreaseKey(int i, Key key)
        {
            if (!Contains(i)) throw new ArgumentException("index is not in the priority queue");
            //if (keys[i].compareTo(key) >= 0)
            if (comparer.Compare(keys[i], key) < 0)
                throw new ArgumentException("Calling increaseKey() with given argument would not strictly increase the key");
            keys[i] = key;
            Swim(qp[i]);
        }
        // Decrease the key associated with index {@code i} to the specified value.
        public void DecreaseKey(int i, Key key)
        {
            if (!Contains(i)) throw new ArgumentException("index is not in the priority queue");            
            if (comparer.Compare(keys[i], key) > 0)
                throw new ArgumentException("Calling decreaseKey() with given argument would not strictly decrease the key");
            keys[i] = key;
            Sink(qp[i]);
        }
        // Remove the key on the priority queue associated with index
        public void Delete(int i)
        {
            if (!Contains(i)) throw new ArgumentException("index is not in the priority queue");
            int index = qp[i];
            Exch(index, n--);
            Swim(index);
            Sink(index);
            keys[i] = default(Key);
            qp[i] = -1;
        }
        /***************************************************************************
         * General helper functions.
         ***************************************************************************/
        private bool Less(int i, int j)
        {
            // return keys[pq[i]].CompareTo(keys[pq[j]]) < 0;
            return comparer.Compare(keys[pq[i]], keys[pq[j]]) < 0;
        }

        private void Exch(int i, int j)
        {
            int swap = pq[i];
            pq[i] = pq[j];
            pq[j] = swap;
            qp[pq[i]] = i;
            qp[pq[j]] = j;
        }


        /***************************************************************************
         * Heap helper functions.
         ***************************************************************************/
        private void Swim(int k)
        {
            while (k > 1 && Less(k / 2, k))
            {
                Exch(k, k / 2);
                k = k / 2;
            }
        }

        private void Sink(int k)
        {
            while (2 * k <= n)
            {
                int j = 2 * k;
                if (j < n && Less(j, j + 1)) j++;
                if (!Less(k, j)) break;
                Exch(k, j);
                k = j;
            }
        }

        public int CompareTo(Key other)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Key> GetEnumerator()
        {
            return new HeapIterator<Key>(comparer, Size(), n, keys);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        private class HeapIterator<Key> : IEnumerator<Key>
        {
            public Key[] PQ { get; set; } // // store items at indices 1 to n, 0 not use.. 
            public int Size { get; set; }
            public int N { get; set; }
            public Comparer<Key> Comparer { get; set; }
            //
            private int position = -1;
            private MaxPQ<Key> copy;
            public HeapIterator()
            {
                if (Comparer == null) copy = new MaxPQ<Key>(Size);
                else copy = new MaxPQ<Key>(Size, Comparer);
                for (int i = 0; i < N; N++)
                    copy.Insert(PQ[i]);
            }
            public HeapIterator(Comparer<Key> Comparer, int Size, int N, Key[] PQ)
            {
                this.Comparer = Comparer;
                this.Size = Size;
                this.N = N;
                this.PQ = PQ;

                if (Comparer == null) copy = new MaxPQ<Key>(Size);
                else copy = new MaxPQ<Key>(Size, Comparer);
                for (int i = 0; i <= N; i++)
                    copy.Insert(this.PQ[i]);
            }

            public bool HasNext() { return !copy.IsEmpty(); }
            public void Remove() { throw new ArgumentException("error"); }
            public Key Next()
            {
                if (!HasNext()) throw new ArgumentException("error");
                return copy.DelMax();
            }


            public void Dispose()
            {
                //throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                position++;
                //return !copy.IsEmpty();
                return position < copy.Size();
            }

            public void Reset()
            {
                position = -1;
            }
            //
            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Key Current
            {
                get
                {
                    try
                    {
                        return PQ[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }


        }
    }
}
