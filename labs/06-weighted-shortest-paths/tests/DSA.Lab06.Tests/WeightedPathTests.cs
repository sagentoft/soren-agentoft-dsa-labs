using System.Collections.Generic;
using NUnit.Framework;

namespace DSA.Lab06.Tests
{
    public sealed class WeightedPathTests
    {
        [Test]
        public void WeightedGraph_GroupsOutgoingEdges()
        {
            WeightedGraph graph = DemoWorld.BuildMainGraph();

            Assert.That(graph.GetOutgoing("A"), Has.Count.EqualTo(3));
            Assert.That(graph.GetOutgoing("X"), Is.Empty);
        }

        [Test]
        public void MinFrontier_RemovesSmallestPriorityFirst()
        {
            var frontier = new MinFrontier<string>();
            frontier.Enqueue("slow", 9);
            frontier.Enqueue("fast", 2);
            frontier.Enqueue("middle", 5);

            Assert.That(frontier.Dequeue().Value, Is.EqualTo("fast"));
            Assert.That(frontier.Dequeue().Value, Is.EqualTo("middle"));
            Assert.That(frontier.Dequeue().Value, Is.EqualTo("slow"));
        }

        [Test]
        public void Dijkstra_StartEqualsGoal_ReturnsZeroCostSingleNodePath()
        {
            var finder = new DijkstraRouteFinder();

            RouteResult result = finder.FindRoute(DemoWorld.BuildMainGraph(), "A", "A");

            Assert.That(result.Found, Is.True);
            Assert.That(result.TotalCost, Is.EqualTo(0));
            Assert.That(result.Path, Is.EqualTo(new[] { "A" }));
        }

        [Test]
        public void Dijkstra_UnknownGoal_ReturnsNotFound()
        {
            var finder = new DijkstraRouteFinder();

            RouteResult result = finder.FindRoute(DemoWorld.BuildMainGraph(), "A", "Missing");

            Assert.That(result.Found, Is.False);
        }

        [Test]
        public void Dijkstra_FindsCheaperIndirectRouteToPreviouslyDiscoveredNode()
        {
            var finder = new DijkstraRouteFinder();

            RouteResult result = finder.FindRoute(DemoWorld.BuildMainGraph(), "A", "B");

            Assert.That(result.Found, Is.True);
            Assert.That(result.TotalCost, Is.EqualTo(4));
            Assert.That(result.Path, Is.EqualTo(new[] { "A", "C", "B" }));
        }

        [Test]
        public void Dijkstra_ChoosesLeastCostNotFewestEdges()
        {
            var finder = new DijkstraRouteFinder();

            RouteResult result = finder.FindRoute(DemoWorld.BuildMainGraph(), "A", "G");

            Assert.That(result.Found, Is.True);
            Assert.That(result.TotalCost, Is.EqualTo(13));
            Assert.That(result.Path, Is.EqualTo(new[] { "A", "C", "B", "D", "E", "F", "G" }));
        }

        [Test]
        public void Dijkstra_ReconstructsActualRoute()
        {
            var finder = new DijkstraRouteFinder();

            RouteResult result = finder.FindRoute(DemoWorld.BuildMainGraph(), "A", "E");

            Assert.That(result.Path, Is.EqualTo(new[] { "A", "C", "B", "D", "E" }));
            Assert.That(result.TotalCost, Is.EqualTo(9));
        }

        [Test]
        public void Dijkstra_ReturnsNotFoundForDisconnectedGoal()
        {
            var finder = new DijkstraRouteFinder();

            RouteResult result = finder.FindRoute(DemoWorld.BuildMainGraph(), "A", "X");

            Assert.That(result.Found, Is.False);
            Assert.That(result.Path, Is.Empty);
        }

        [Test]
        public void Dijkstra_HandlesCycleWithoutLooping()
        {
            var finder = new DijkstraRouteFinder();

            RouteResult result = finder.FindRoute(DemoWorld.BuildMainGraph(), "C", "E");

            Assert.That(result.Found, Is.True);
            Assert.That(result.TotalCost, Is.EqualTo(7));
            Assert.That(result.Path, Is.EqualTo(new[] { "C", "B", "D", "E" }));
        }

        [Test]
        public void Dijkstra_SkipsStaleQueueEntriesAndKeepsBestCost()
        {
            var finder = new DijkstraRouteFinder();

            RouteResult result = finder.FindRoute(DemoWorld.BuildMainGraph(), "A", "D");

            Assert.That(result.Found, Is.True);
            Assert.That(result.TotalCost, Is.EqualTo(6));
            Assert.That(result.Path, Is.EqualTo(new[] { "A", "C", "B", "D" }));
        }

        [Test]
        public void Dijkstra_RespectsDirectedEdges()
        {
            var finder = new DijkstraRouteFinder();

            RouteResult result = finder.FindRoute(DemoWorld.BuildMainGraph(), "G", "A");

            Assert.That(result.Found, Is.False);
        }

        [Test, Explicit("Optional Lab 6B / A* extension")]
        public void AStar_FindsSameLeastCostRouteAsDijkstra()
        {
            WeightedGraph graph = DemoWorld.BuildMainGraph();
            var dijkstra = new DijkstraRouteFinder();
            var astar = new AStarRouteFinder();

            RouteResult expected = dijkstra.FindRoute(graph, "A", "G");
            RouteResult actual = astar.FindRoute(graph, "A", "G");

            Assert.That(actual.Found, Is.True);
            Assert.That(actual.TotalCost, Is.EqualTo(expected.TotalCost));
        }

        [Test, Explicit("Optional Lab 6B / A* extension")]
        public void AStar_CanRespectBlockedNodeConstraint()
        {
            WeightedGraph graph = DemoWorld.BuildMainGraph();
            var astar = new AStarRouteFinder();
            var blocked = new HashSet<string> { "D" };

            RouteResult result = astar.FindRoute(graph, "A", "G", blocked);

            Assert.That(result.Found, Is.True);
            Assert.That(result.Path, Does.Not.Contain("D"));
        }

        [Test, Explicit("Optional Lab 6B / A* extension")]
        public void AStar_ExpandsNoMoreNodesThanDijkstraOnGuidedDemo()
        {
            WeightedGraph graph = DemoWorld.BuildAStarDemoGraph();
            var dijkstra = new DijkstraRouteFinder();
            var astar = new AStarRouteFinder();

            RouteResult d = dijkstra.FindRoute(graph, "S", "Goal");
            RouteResult a = astar.FindRoute(graph, "S", "Goal");

            Assert.That(a.Found, Is.True);
            Assert.That(a.TotalCost, Is.EqualTo(d.TotalCost));
            Assert.That(a.ExpandedNodes, Is.LessThanOrEqualTo(d.ExpandedNodes));
        }
    }
}
