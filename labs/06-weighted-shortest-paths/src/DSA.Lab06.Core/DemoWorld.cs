namespace DSA.Lab06
{
    public static class DemoWorld
    {
        public static WeightedGraph BuildMainGraph()
        {
            WeightedNode[] nodes =
            {
                new WeightedNode("A", 0, 0),
                new WeightedNode("B", 1, 0),
                new WeightedNode("C", 0, 1),
                new WeightedNode("D", 1, 1),
                new WeightedNode("E", 2, 1),
                new WeightedNode("F", 2, 0),
                new WeightedNode("G", 3, 0),
                new WeightedNode("X", 5, 5)
            };

            WeightedEdge[] edges =
            {
                new WeightedEdge("A", "B", 5),
                new WeightedEdge("A", "C", 2),
                new WeightedEdge("A", "G", 20),
                new WeightedEdge("C", "B", 2),
                new WeightedEdge("C", "D", 6),
                new WeightedEdge("B", "D", 2),
                new WeightedEdge("B", "F", 8),
                new WeightedEdge("D", "C", 2),
                new WeightedEdge("D", "E", 3),
                new WeightedEdge("E", "F", 2),
                new WeightedEdge("E", "G", 5),
                new WeightedEdge("F", "G", 2)
            };

            return new WeightedGraph(nodes, edges);
        }

        public static WeightedGraph BuildAStarDemoGraph()
        {
            WeightedNode[] nodes =
            {
                new WeightedNode("S", 0, 0),
                new WeightedNode("P1", 1, 0),
                new WeightedNode("P2", 2, 0),
                new WeightedNode("P3", 3, 0),
                new WeightedNode("P4", 4, 0),
                new WeightedNode("Goal", 5, 0),
                new WeightedNode("U1", 0, 1),
                new WeightedNode("U2", 0, 2),
                new WeightedNode("U3", 0, 3),
                new WeightedNode("U4", 0, 4)
            };

            WeightedEdge[] edges =
            {
                new WeightedEdge("S", "P1", 1),
                new WeightedEdge("P1", "P2", 1),
                new WeightedEdge("P2", "P3", 1),
                new WeightedEdge("P3", "P4", 1),
                new WeightedEdge("P4", "Goal", 1),
                new WeightedEdge("S", "U1", 1),
                new WeightedEdge("U1", "U2", 1),
                new WeightedEdge("U2", "U3", 1),
                new WeightedEdge("U3", "U4", 1)
            };

            return new WeightedGraph(nodes, edges);
        }
    }
}
