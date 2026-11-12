using System;
using System.Collections.Generic;
using DSA.Lab05;

Console.WriteLine("DSA Lab 5 - Graph Representation and Traversal");
Console.WriteLine();

string[] nodes =
{
    "Entrance",
    "Hall",
    "StoreRoom",
    "Armory",
    "Courtyard",
    "BossRoom",
    "SecretRoom"
};

DirectedEdge[] edges =
{
    new DirectedEdge("Entrance", "Hall"),
    new DirectedEdge("Entrance", "StoreRoom"),
    new DirectedEdge("Hall", "Armory"),
    new DirectedEdge("Hall", "Courtyard"),
    new DirectedEdge("StoreRoom", "Courtyard"),
    new DirectedEdge("Armory", "BossRoom"),
    new DirectedEdge("Courtyard", "Hall"),
    new DirectedEdge("Courtyard", "BossRoom")
};

Console.WriteLine("Dungeon graph:");
Console.WriteLine("Entrance -> Hall, StoreRoom");
Console.WriteLine("Hall -> Armory, Courtyard");
Console.WriteLine("StoreRoom -> Courtyard");
Console.WriteLine("Armory -> BossRoom");
Console.WriteLine("Courtyard -> Hall, BossRoom");
Console.WriteLine("BossRoom ->");
Console.WriteLine("SecretRoom -> (disconnected)");
Console.WriteLine();

var edgeList = new EdgeListGraph(nodes, edges);
Console.WriteLine(
    "Edge-list neighbors of Hall: " +
    string.Join(", ", edgeList.GetNeighbors("Hall")));

var adjacency = new AdjacencyGraph(nodes, edges);
Console.WriteLine(
    "Adjacency neighbors of Hall: " +
    string.Join(", ", adjacency.GetNeighbors("Hall")));

try
{
    IReadOnlyList<string> bfs =
        GraphTraversal.BreadthFirstOrder(adjacency, "Entrance");

    IReadOnlyList<string> dfs =
        GraphTraversal.DepthFirstOrder(adjacency, "Entrance");

    Console.WriteLine("BFS: " + string.Join(" -> ", bfs));
    Console.WriteLine("DFS: " + string.Join(" -> ", dfs));
    Console.WriteLine(
        "Can Entrance reach BossRoom? " +
        GraphTraversal.IsReachableBfs(adjacency, "Entrance", "BossRoom"));
    Console.WriteLine(
        "Can Entrance reach SecretRoom? " +
        GraphTraversal.IsReachableBfs(adjacency, "Entrance", "SecretRoom"));
}
catch (NotImplementedException)
{
    Console.WriteLine();
    Console.WriteLine("Traversal methods are intentionally incomplete in the starter.");
    Console.WriteLine("Complete the adjacency grouping and traversal tasks, then run again.");
}
