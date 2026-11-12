using System;
using DSA.Lab06;

Console.WriteLine("DSA Lab 6 - Weighted Shortest Paths");
Console.WriteLine();

WeightedGraph graph = DemoWorld.BuildMainGraph();
var finder = new DijkstraRouteFinder();
RouteResult result = finder.FindRoute(graph, "A", "G");

if (result.Found)
{
    Console.WriteLine("Dijkstra route A -> G:");
    Console.WriteLine(string.Join(" -> ", result.Path));
    Console.WriteLine($"Total cost: {result.TotalCost}");
    Console.WriteLine($"Expanded nodes: {result.ExpandedNodes}");
}
else
{
    Console.WriteLine("No route found yet. In the starter, Dijkstra is intentionally incomplete.");
}
