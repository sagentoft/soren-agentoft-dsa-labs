using System;
using System.Collections.Generic;

namespace DSA.Lab06
{
    /// <summary>
    /// Prepared weighted directed graph.
    /// Neighbor lookup is organized around outgoing edges.
    /// </summary>
    public sealed class WeightedGraph
    {
        private readonly Dictionary<string, WeightedNode> _nodesById =
            new Dictionary<string, WeightedNode>();

        private readonly Dictionary<string, List<WeightedEdge>> _outgoingByNode =
            new Dictionary<string, List<WeightedEdge>>();

        public WeightedGraph(IEnumerable<WeightedNode> nodes, IEnumerable<WeightedEdge> edges)
        {
            if (nodes == null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            if (edges == null)
            {
                throw new ArgumentNullException(nameof(edges));
            }

            foreach (WeightedNode node in nodes)
            {
                if (_nodesById.ContainsKey(node.Id))
                {
                    throw new ArgumentException($"Duplicate node id '{node.Id}'.", nameof(nodes));
                }

                _nodesById.Add(node.Id, node);
                _outgoingByNode.Add(node.Id, new List<WeightedEdge>());
            }

            foreach (WeightedEdge edge in edges)
            {
                if (!_nodesById.ContainsKey(edge.From) || !_nodesById.ContainsKey(edge.To))
                {
                    throw new ArgumentException($"Edge {edge} refers to an unknown node.", nameof(edges));
                }

                _outgoingByNode[edge.From].Add(edge);
            }
        }

        public int NodeCount => _nodesById.Count;

        public bool ContainsNode(string id)
        {
            return _nodesById.ContainsKey(id);
        }

        public WeightedNode GetNode(string id)
        {
            if (!_nodesById.TryGetValue(id, out WeightedNode node))
            {
                throw new ArgumentException($"Unknown node '{id}'.", nameof(id));
            }

            return node;
        }

        public IReadOnlyList<WeightedEdge> GetOutgoing(string id)
        {
            if (!_outgoingByNode.TryGetValue(id, out List<WeightedEdge>? edges))
            {
                return Array.Empty<WeightedEdge>();
            }

            return edges;
        }
    }
}
