using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueues
{
    class MinPQ<Key> : IEnumerable<Key>
    {
        private Key[] pq; // // store items at indices 1 to n, 0 not use..        
        private int n;   // number of items on priority queue
        private Comparer<Key> comparer; // optional comparator

        //
        /**
          * Initializes an empty priority queue with the given initial capacity.
          *
          * @param  initCapacity the initial capacity of this priority queue
          */
        public MinPQ(int initCapacity)
        {
            pq = new Key[initCapacity + 1];
            n = 0;
        }
        /**
    * Initializes an empty priority queue with the given initial capacity,
    * using the given comparator.
    *
    * @param  initCapacity the initial capacity of this priority queue
    * @param  comparator the order in which to compare the keys
    */
        public MinPQ(int initCapacity, Comparer<Key> comparer)
        {
            this.comparer = comparer;
            pq = new Key[initCapacity + 1];
            n = 0;
        }
        // --- one more init
        public MinPQ(Comparer<Key> Comparer, int N, Key[] PQ)
        {
            this.comparer = Comparer;
            this.pq = PQ;
            this.n = N;
        }
        /**
         * Initializes an empty priority queue.
         */
        public MinPQ()
        {
            pq = new Key[1];
            n = 0;
        }
        /**
         * Initializes an empty priority queue using the given comparator.
         *
         * @param  comparator the order in which to compare the keys
         */
        public MinPQ(Comparer<Key> comparer)
        {
            this.comparer = comparer;
            pq = new Key[1];
            n = 0;
        }

        /**
         * Adds a new key to this priority queue.
         *
         * @param  x the new key to add to this priority queue
         */
        public void Insert(Key x)
        {
            // double size of array if necessary
            if (n == pq.Length - 1) Resize(2 * pq.Length);
            // add x, and percolate it up to maintain heap invariant
            pq[++n] = x;
            Swim(n);
            Debug.Assert(IsMinHeap());
        }
        /**
           * Removes and returns a largest key on this priority queue.
           *
           * @return a largest key on this priority queue
           * @throws NoSuchElementException if this priority queue is empty
           */
        public Key DelMin()
        {
            if (IsEmpty()) throw new ArgumentException("Priority queue underflow");
            Key min = pq[1];
            Exch(1, n--);
            Sink(1);
            pq[n + 1] = default(Key);// null;     // to avoid loiterig and help with garbage collection
            if ((n > 0) && (n == (pq.Length - 1) / 4)) Resize(pq.Length / 2);
            Debug.Assert(IsMinHeap());
            return min;
        }
        /**
         * Returns true if this priority queue is empty.
         *
         * @return {@code true} if this priority queue is empty;
         *         {@code false} otherwise
         */
        public bool IsEmpty()
        {
            return n == 0;
        }
        // Returns the number of keys on this priority queue.                          
        public int Size()
        {
            return n;
        }
        // Returns a largest key on this priority queue.   
        public Key Min()
        {
            if (IsEmpty()) throw new ArgumentException("Priority queue is empty");
            return pq[1];
        }
        // Helper function to double the size of the heap array
        private void Resize(int capacity)
        {
            Debug.Assert(capacity > n);
            Key[] temp = new Key[capacity];
            for (int i = 1; i <= n; i++)
            {
                temp[i] = pq[i];
            }
            pq = temp;
        }
        /***************************************************************************
         * Helper functions to restore the heap invariant.
         ***************************************************************************/
        private void Swim(int k)
        {
            while (k > 1 && Greater(k / 2, k))
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
                if (j < n && Greater(j, j + 1)) j++;
                if (!Greater(k, j)) break;
                Exch(k, j);
                k = j;
            }
        }
        /***************************************************************************
         * Helper functions for compares and swaps.
         ***************************************************************************/
        private bool Greater(int i, int j)
        {
            if (comparer == null)
            {
                //return Comparer<Key>(pq[i]).compareTo(pq[j]) < 0;
                throw new ArgumentException("comparer is null");
            }
            else
            {
                return comparer.Compare(pq[i], pq[j]) > 0;
            }
        }

        private void Exch(int i, int j)
        {
            Key swap = pq[i];
            pq[i] = pq[j];
            pq[j] = swap;
        }

        // is pq[1..N] a max heap?
        private bool IsMinHeap()
        {
            return IsMinHeap(1);
        }
        // is subtree of pq[1..n] rooted at k a max heap?
        private bool IsMinHeap(int k)
        {
            if (k > n) return true;
            int left = 2 * k;
            int right = 2 * k + 1;
            if (left <= n && Greater(k, left)) return false;
            if (right <= n && Greater(k, right)) return false;
            return IsMinHeap(left) && IsMinHeap(right);
        }
        //
        /*
        public IEnumerator<Key> Iterator()
        {
            return new HeapIterator<Key>();
        }
        */

        public IEnumerator<Key> GetEnumerator()
        {
            return new HeapIterator<Key>(comparer, Size(), n, pq);
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
