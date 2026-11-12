using System;

namespace DSA.Lab06
{
    public static class Heuristics
    {
        public static int Manhattan(WeightedNode from, WeightedNode goal)
        {
            return Math.Abs(from.X - goal.X) + Math.Abs(from.Y - goal.Y);
        }
    }
}
