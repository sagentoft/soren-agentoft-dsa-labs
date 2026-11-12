using System;
using System.Collections.Generic;

namespace DSA.Lab05
{
    /// <summary>
    /// Correct but deliberately simple graph representation.
    ///
    /// Nodes are stored separately and every edge is stored in one global List.
    /// Asking for outgoing neighbors scans the entire edge list.
    /// </summary>
    public sealed class EdgeListGraph
    {
        private readonly List<string> _nodes;
        private readonly List<DirectedEdge> _edges;

        public EdgeListGraph(IEnumerable<string> nodes, IEnumerable<DirectedEdge> edges)
        {
            if (nodes == null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            if (edges == null)
            {
                throw new ArgumentNullException(nameof(edges));
            }

            _nodes = new List<string>(nodes);
            _edges = new List<DirectedEdge>(edges);
        }

        public int NodeCount => _nodes.Count;

        public int EdgeCount => _edges.Count;

        public bool ContainsNode(string node)
        {
            return _nodes.Contains(node);
        }

        public IReadOnlyList<string> GetNeighbors(string node)
        {
            var result = new List<string>();

            foreach (DirectedEdge edge in _edges)
            {
                if (edge.From == node)
                {
                    result.Add(edge.To);
                }
            }

            return result;
        }
    }
}
