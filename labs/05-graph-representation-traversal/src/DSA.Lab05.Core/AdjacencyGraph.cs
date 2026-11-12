using System;
using System.Collections.Generic;

namespace DSA.Lab05
{
    /// <summary>
    /// Graph representation organized around the repeated operation:
    /// "give me the outgoing neighbors of this node".
    ///
    /// The node buckets are prepared for you. Complete the edge-grouping step.
    /// </summary>
    public sealed class AdjacencyGraph
    {
        private readonly Dictionary<string, List<string>> _neighborsByNode =
            new Dictionary<string, List<string>>();

        public AdjacencyGraph(IEnumerable<string> nodes, IEnumerable<DirectedEdge> edges)
        {
            if (nodes == null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            if (edges == null)
            {
                throw new ArgumentNullException(nameof(edges));
            }

            foreach (string node in nodes)
            {
                if (!_neighborsByNode.ContainsKey(node))
                {
                    _neighborsByNode.Add(node, new List<string>());
                }
            }

            foreach (DirectedEdge edge in edges)
            {
                if (!_neighborsByNode.ContainsKey(edge.From) ||
                    !_neighborsByNode.ContainsKey(edge.To))
                {
                    throw new ArgumentException(
                        $"Edge {edge} refers to a node that was not supplied.");
                }

                // TODO:
                // Add edge.To to the adjacency list belonging to edge.From.
                _neighborsByNode[edge.From].Add(edge.To);
            }
        }

        public int NodeCount => _neighborsByNode.Count;

        public bool ContainsNode(string node)
        {
            return _neighborsByNode.ContainsKey(node);
        }

        public IReadOnlyList<string> GetNeighbors(string node)
        {
            if (!_neighborsByNode.TryGetValue(node, out List<string>? neighbors))
            {
                return Array.Empty<string>();
            }

            return neighbors;
        }
    }
}
