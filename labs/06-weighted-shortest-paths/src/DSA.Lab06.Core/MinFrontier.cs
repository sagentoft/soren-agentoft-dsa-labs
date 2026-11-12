using System;
using System.Collections.Generic;

namespace DSA.Lab06
{
    /// <summary>
    /// Supplied practice frontier for Lab 6.
    /// It behaves as a min-priority queue and uses a binary min-heap internally.
    ///
    /// The heap is NOT the task in Lab 6. You already practised that mechanism in Lab 4B.
    /// The Frontier Dispatch assignment must use your own assessed MinPriorityQueue<T> contract.
    /// </summary>
    public sealed class MinFrontier<T>
    {
        public readonly struct Entry
        {
            public Entry(T value, int priority)
            {
                Value = value;
                Priority = priority;
            }

            public T Value { get; }
            public int Priority { get; }
        }

        private readonly List<Entry> _heap = new List<Entry>();

        public int Count => _heap.Count;

        public void Enqueue(T value, int priority)
        {
            _heap.Add(new Entry(value, priority));
            SiftUp(_heap.Count - 1);
        }

        public Entry Dequeue()
        {
            if (_heap.Count == 0)
            {
                throw new InvalidOperationException("The frontier is empty.");
            }

            Entry result = _heap[0];
            int last = _heap.Count - 1;

            if (last == 0)
            {
                _heap.RemoveAt(0);
                return result;
            }

            _heap[0] = _heap[last];
            _heap.RemoveAt(last);
            SiftDown(0);
            return result;
        }

        private void SiftUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (_heap[parent].Priority <= _heap[index].Priority)
                {
                    return;
                }

                Swap(parent, index);
                index = parent;
            }
        }

        private void SiftDown(int index)
        {
            while (true)
            {
                int left = 2 * index + 1;
                if (left >= _heap.Count)
                {
                    return;
                }

                int right = left + 1;
                int smallerChild = left;

                if (right < _heap.Count && _heap[right].Priority < _heap[left].Priority)
                {
                    smallerChild = right;
                }

                if (_heap[index].Priority <= _heap[smallerChild].Priority)
                {
                    return;
                }

                Swap(index, smallerChild);
                index = smallerChild;
            }
        }

        private void Swap(int a, int b)
        {
            Entry temp = _heap[a];
            _heap[a] = _heap[b];
            _heap[b] = temp;
        }
    }
}
