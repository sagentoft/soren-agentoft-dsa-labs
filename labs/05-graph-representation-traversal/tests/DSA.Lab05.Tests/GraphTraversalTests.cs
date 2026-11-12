using NUnit.Framework;

namespace DSA.Lab05.Tests
{
    public sealed class GraphTraversalTests
    {
        [Test]
        public void EdgeList_GetNeighbors_ReturnsOutgoingOnly()
        {
            GraphFixture fixture = BuildExample();

            Assert.That(
                fixture.EdgeList.GetNeighbors("Hall"),
                Is.EqualTo(new[] { "Armory", "Courtyard" }));
        }

        [Test]
        public void EdgeList_ContainsIsolatedNode()
        {
            GraphFixture fixture = BuildExample();

            Assert.That(fixture.EdgeList.ContainsNode("SecretRoom"), Is.True);
            Assert.That(fixture.EdgeList.GetNeighbors("SecretRoom"), Is.Empty);
        }

        [Test]
        public void AdjacencyGraph_ContainsEverySuppliedNode()
        {
            GraphFixture fixture = BuildExample();

            Assert.That(fixture.Adjacency.NodeCount, Is.EqualTo(7));
            Assert.That(fixture.Adjacency.ContainsNode("SecretRoom"), Is.True);
        }

        [Test]
        public void AdjacencyGraph_UnknownNodeReturnsEmptyNeighbors()
        {
            GraphFixture fixture = BuildExample();

            Assert.That(fixture.Adjacency.GetNeighbors("Missing"), Is.Empty);
        }

        [Test]
        public void AdjacencyGraph_GroupsOutgoingNeighborsInInputOrder()
        {
            GraphFixture fixture = BuildExample();

            Assert.That(
                fixture.Adjacency.GetNeighbors("Entrance"),
                Is.EqualTo(new[] { "Hall", "StoreRoom" }));

            Assert.That(
                fixture.Adjacency.GetNeighbors("Courtyard"),
                Is.EqualTo(new[] { "Hall", "BossRoom" }));
        }

        [Test]
        public void BreadthFirstOrder_VisitsByLayer()
        {
            GraphFixture fixture = BuildExample();

            Assert.That(
                GraphTraversal.BreadthFirstOrder(fixture.Adjacency, "Entrance"),
                Is.EqualTo(new[]
                {
                    "Entrance",
                    "Hall",
                    "StoreRoom",
                    "Armory",
                    "Courtyard",
                    "BossRoom"
                }));
        }

        [Test]
        public void BreadthFirstOrder_VisitsEachNodeOnceInCycle()
        {
            GraphFixture fixture = BuildExample();

            Assert.That(
                GraphTraversal.BreadthFirstOrder(fixture.Adjacency, "Hall"),
                Is.EqualTo(new[]
                {
                    "Hall",
                    "Armory",
                    "Courtyard",
                    "BossRoom"
                }));
        }

        [Test]
        public void BreadthFirstOrder_DoesNotVisitDisconnectedNodes()
        {
            GraphFixture fixture = BuildExample();

            Assert.That(
                GraphTraversal.BreadthFirstOrder(fixture.Adjacency, "Entrance"),
                Does.Not.Contain("SecretRoom"));
        }

        [Test]
        public void DepthFirstOrder_GoesDeepBeforeLaterSibling()
        {
            GraphFixture fixture = BuildExample();

            Assert.That(
                GraphTraversal.DepthFirstOrder(fixture.Adjacency, "Entrance"),
                Is.EqualTo(new[]
                {
                    "Entrance",
                    "Hall",
                    "Armory",
                    "BossRoom",
                    "Courtyard",
                    "StoreRoom"
                }));
        }

        [Test]
        public void DepthFirstOrder_VisitsEachNodeOnceInCycle()
        {
            GraphFixture fixture = BuildExample();

            var result = GraphTraversal.DepthFirstOrder(
                fixture.Adjacency,
                "Hall");

            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result, Is.EquivalentTo(new[]
            {
                "Hall",
                "Armory",
                "Courtyard",
                "BossRoom"
            }));
        }

        [Test]
        public void IsReachableBfs_ReturnsTrueForReachableTarget()
        {
            GraphFixture fixture = BuildExample();

            Assert.That(
                GraphTraversal.IsReachableBfs(
                    fixture.Adjacency,
                    "Entrance",
                    "BossRoom"),
                Is.True);
        }

        [Test]
        public void IsReachableBfs_ReturnsFalseForDisconnectedTarget()
        {
            GraphFixture fixture = BuildExample();

            Assert.That(
                GraphTraversal.IsReachableBfs(
                    fixture.Adjacency,
                    "Entrance",
                    "SecretRoom"),
                Is.False);
        }

        [Test]
        public void IsReachableBfs_StartEqualsTargetIsReachable()
        {
            GraphFixture fixture = BuildExample();

            Assert.That(
                GraphTraversal.IsReachableBfs(
                    fixture.Adjacency,
                    "Entrance",
                    "Entrance"),
                Is.True);
        }

        private static GraphFixture BuildExample()
        {
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

            return new GraphFixture(
                new EdgeListGraph(nodes, edges),
                new AdjacencyGraph(nodes, edges));
        }

        private sealed class GraphFixture
        {
            public GraphFixture(
                EdgeListGraph edgeList,
                AdjacencyGraph adjacency)
            {
                EdgeList = edgeList;
                Adjacency = adjacency;
            }

            public EdgeListGraph EdgeList { get; }

            public AdjacencyGraph Adjacency { get; }
        }
    }
}
