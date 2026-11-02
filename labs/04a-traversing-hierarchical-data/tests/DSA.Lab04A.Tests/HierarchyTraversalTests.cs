using NUnit.Framework;

namespace DSA.Lab04A.Tests
{
    public sealed class HierarchyTraversalTests
    {
        [Test]
        public void NewNode_StartsAsLeaf()
        {
            var root = new HierarchyNode<string>("Root");

            Assert.That(root.IsLeaf, Is.True);
            Assert.That(root.Children.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddChild_PreservesChildOrder()
        {
            var root = new HierarchyNode<string>("Root");
            root.AddChild("A");
            root.AddChild("B");

            Assert.That(root.Children[0].Value, Is.EqualTo("A"));
            Assert.That(root.Children[1].Value, Is.EqualTo("B"));
            Assert.That(root.IsLeaf, Is.False);
        }

        [Test]
        public void CountNodes_SingleNodeReturnsOne()
        {
            var root = new HierarchyNode<string>("Root");

            Assert.That(HierarchyTraversal.CountNodes(root), Is.EqualTo(1));
        }

        [Test]
        public void CountNodes_CountsWholeHierarchy()
        {
            HierarchyNode<string> root = BuildExample();

            Assert.That(HierarchyTraversal.CountNodes(root), Is.EqualTo(7));
        }

        [Test]
        public void Contains_FindsNestedValue()
        {
            HierarchyNode<string> root = BuildExample();

            Assert.That(HierarchyTraversal.Contains(root, "Bow"), Is.True);
        }

        [Test]
        public void Contains_ReturnsFalseForMissingValue()
        {
            HierarchyNode<string> root = BuildExample();

            Assert.That(HierarchyTraversal.Contains(root, "Magic"), Is.False);
        }

        [Test]
        public void DepthFirstPreOrder_VisitsParentBeforeChildren()
        {
            HierarchyNode<string> root = BuildExample();

            Assert.That(
                HierarchyTraversal.DepthFirstPreOrder(root),
                Is.EqualTo(new[] { "Survival", "Movement", "Dash", "Climb", "Combat", "Sword", "Bow" }));
        }

        [Test]
        public void DepthFirstPreOrder_PreservesSiblingOrder()
        {
            var root = new HierarchyNode<int>(0);
            HierarchyNode<int> left = root.AddChild(1);
            left.AddChild(3);
            root.AddChild(2);

            Assert.That(
                HierarchyTraversal.DepthFirstPreOrder(root),
                Is.EqualTo(new[] { 0, 1, 3, 2 }));
        }

        [Test]
        public void MaxDepth_SingleNodeIsZero()
        {
            var root = new HierarchyNode<string>("Root");

            Assert.That(HierarchyTraversal.MaxDepth(root), Is.EqualTo(0));
        }

        [Test]
        public void MaxDepth_UsesDeepestBranch()
        {
            HierarchyNode<string> root = BuildExample();
            HierarchyNode<string> deep = root.Children[0].Children[0];
            deep.AddChild("Air Dash");

            Assert.That(HierarchyTraversal.MaxDepth(root), Is.EqualTo(3));
        }

        private static HierarchyNode<string> BuildExample()
        {
            var survival = new HierarchyNode<string>("Survival");
            HierarchyNode<string> movement = survival.AddChild("Movement");
            movement.AddChild("Dash");
            movement.AddChild("Climb");

            HierarchyNode<string> combat = survival.AddChild("Combat");
            combat.AddChild("Sword");
            combat.AddChild("Bow");

            return survival;
        }
    }
}
