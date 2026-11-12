using System;

namespace DSA.Lab05
{
    /// <summary>
    /// One directed connection in a graph.
    ///
    /// If a game connection should work in both directions, represent that
    /// explicitly with two directed edges.
    /// </summary>
    public readonly struct DirectedEdge
    {
        public DirectedEdge(string from, string to)
        {
            From = from ?? throw new ArgumentNullException(nameof(from));
            To = to ?? throw new ArgumentNullException(nameof(to));
        }

        public string From { get; }

        public string To { get; }

        public override string ToString()
        {
            return $"{From} -> {To}";
        }
    }
}
