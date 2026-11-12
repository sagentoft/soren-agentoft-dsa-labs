using System;
using System.Collections.Generic;

namespace DSA.Lab05
{
    /// <summary>
    /// Iterative graph traversals.
    ///
    /// Complete these methods after the adjacency representation is working.
    /// Think in terms of:
    /// frontier + visited + neighbors.
    /// </summary>
    public static class GraphTraversal
    {
        public static IReadOnlyList<string> BreadthFirstOrder(
            AdjacencyGraph graph,
            string start)
        {
            var result = new List<string>();
            var frontier = new Queue<string>();
            var visited = new HashSet<string>();

            visited.Add(start);
            frontier.Enqueue(start);

            while (frontier.Count > 0)
            {
                string current = frontier.Dequeue();
                result.Add(current);

                foreach (string neighbor in graph.GetNeighbors(current))
                {
                    if (visited.Add(neighbor))
                    {
                        frontier.Enqueue(neighbor);
                    }
                }
            }

            return result;
        }

        public static IReadOnlyList<string> DepthFirstOrder(
            AdjacencyGraph graph,
            string start)
        {
            var result = new List<string>();
            var frontier = new Stack<string>();
            var visited = new HashSet<string>();

            visited.Add(start);
            frontier.Push(start);

            while (frontier.Count > 0)
            {
                string current = frontier.Pop();
                result.Add(current);

                IReadOnlyList<string> neighbors = graph.GetNeighbors(current);
                
                for (int i = neighbors.Count - 1; i >= 0; i--)
                {
                    string neighbor = neighbors[i];

                    if (visited.Add(neighbor))
                    {
                        frontier.Push(neighbor);
                    }
                }
            }

            return result;
        }

        public static bool IsReachableBfs(
            AdjacencyGraph graph,
            string start,
            string target)
        {
            var frontier = new Queue<string>();
            var visited = new HashSet<string>();

            visited.Add(start);
            frontier.Enqueue(start);

            while (frontier.Count > 0)
            {
                string current = frontier.Dequeue();

                if (current == target)
                {
                    return true;
                }

                foreach (string neighbor in graph.GetNeighbors(current))
                {
                    if (visited.Add(neighbor))
                    {
                        frontier.Enqueue(neighbor);
                    }
                }
            }

            return false;
        }
    }
}
