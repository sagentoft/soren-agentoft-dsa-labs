using System;

namespace DSA.Lab06
{
    public readonly struct WeightedEdge
    {
        public WeightedEdge(string from, string to, int cost)
        {
            if (cost < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(cost), "Dijkstra requires non-negative edge costs.");
            }

            From = from ?? throw new ArgumentNullException(nameof(from));
            To = to ?? throw new ArgumentNullException(nameof(to));
            Cost = cost;
        }

        public string From { get; }
        public string To { get; }
        public int Cost { get; }

        public override string ToString()
        {
            return $"{From} -> {To} ({Cost})";
        }
    }
}
