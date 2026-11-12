using System;
using System.Collections.Generic;

namespace DSA.Lab06
{
    public sealed class RouteResult
    {
        private RouteResult(bool found, IReadOnlyList<string> path, int totalCost, int expandedNodes)
        {
            Found = found;
            Path = path;
            TotalCost = totalCost;
            ExpandedNodes = expandedNodes;
        }

        public bool Found { get; }
        public IReadOnlyList<string> Path { get; }
        public int TotalCost { get; }
        public int ExpandedNodes { get; }

        public static RouteResult Success(IReadOnlyList<string> path, int totalCost, int expandedNodes)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return new RouteResult(true, path, totalCost, expandedNodes);
        }

        public static RouteResult NotFound(int expandedNodes)
        {
            return new RouteResult(false, Array.Empty<string>(), 0, expandedNodes);
        }
    }
}
