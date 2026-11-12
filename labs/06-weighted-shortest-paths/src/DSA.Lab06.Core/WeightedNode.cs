using System;

namespace DSA.Lab06
{
    public readonly struct WeightedNode
    {
        public WeightedNode(string id, int x, int y)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            X = x;
            Y = y;
        }

        public string Id { get; }
        public int X { get; }
        public int Y { get; }

        public override string ToString()
        {
            return $"{Id} ({X}, {Y})";
        }
    }
}
