namespace DSA.Lab04B
{
    /// <summary>
    /// One item waiting to be processed.
    /// Lower Priority values are processed first in this lab.
    /// </summary>
    public readonly struct PriorityEntry<T>
    {
        public PriorityEntry(T item, int priority)
        {
            Item = item;
            Priority = priority;
        }

        public T Item { get; }
        public int Priority { get; }
    }
}
