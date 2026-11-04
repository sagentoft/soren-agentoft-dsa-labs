using System;
using System.Collections.Generic;

namespace DSA.Lab04B
{
    /// <summary>
    /// A correct but deliberately simple baseline.
    /// Enqueue appends in O(1), while finding/removing the cheapest item is O(n).
    /// </summary>
    public sealed class LinearDispatchQueue<T>
    {
        private readonly List<PriorityEntry<T>> _entries = new List<PriorityEntry<T>>();

        public int Count => _entries.Count;

        public void Enqueue(T item, int priority)
        {
            _entries.Add(new PriorityEntry<T>(item, priority));
        }

        public PriorityEntry<T> PeekNext()
        {
            return _entries[FindMinimumIndex()];
        }

        public PriorityEntry<T> DequeueNext()
        {
            int index = FindMinimumIndex();
            PriorityEntry<T> result = _entries[index];
            _entries.RemoveAt(index);
            return result;
        }

        private int FindMinimumIndex()
        {
            if (_entries.Count == 0)
            {
                throw new InvalidOperationException("The dispatch queue is empty.");
            }

            int bestIndex = 0;

            for (int i = 1; i < _entries.Count; ++i)
            {
                if (_entries[i].Priority < _entries[bestIndex].Priority)
                {
                    bestIndex = i;
                }
            }

            return bestIndex;
        }
    }
}
