using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DSA.Lab04B.Tests
{
    public sealed class PriorityQueueTests
    {
        [Test]
        public void LinearBaseline_ReturnsSmallestPriorityFirst()
        {
            var queue = new LinearDispatchQueue<string>();
            queue.Enqueue("A", 12);
            queue.Enqueue("B", 4);
            queue.Enqueue("C", 9);

            Assert.That(queue.DequeueNext().Item, Is.EqualTo("B"));
        }

        [Test]
        public void HeapQueue_EmptyQueueHasCountZero()
        {
            var queue = new BinaryHeapDispatchQueue<string>();
            Assert.That(queue.Count, Is.EqualTo(0));
        }

        [Test]
        public void HeapQueue_PeekOnEmptyThrows()
        {
            var queue = new BinaryHeapDispatchQueue<string>();
            Assert.Throws<InvalidOperationException>(() => queue.PeekNext());
        }

        [Test]
        public void HeapQueue_EnqueueAndPeekReturnsMinimum()
        {
            var queue = new BinaryHeapDispatchQueue<string>();
            queue.Enqueue("A", 12);
            queue.Enqueue("B", 4);
            queue.Enqueue("C", 9);

            PriorityEntry<string> next = queue.PeekNext();

            Assert.That(next.Item, Is.EqualTo("B"));
            Assert.That(next.Priority, Is.EqualTo(4));
            Assert.That(queue.Count, Is.EqualTo(3));
        }

        [Test]
        public void HeapQueue_DequeueReturnsPrioritiesInAscendingOrder()
        {
            var queue = new BinaryHeapDispatchQueue<string>();
            queue.Enqueue("A", 12);
            queue.Enqueue("B", 4);
            queue.Enqueue("C", 9);
            queue.Enqueue("D", 6);
            queue.Enqueue("E", 3);
            queue.Enqueue("F", 7);

            var priorities = new List<int>();
            while (queue.Count > 0)
            {
                priorities.Add(queue.DequeueNext().Priority);
            }

            Assert.That(priorities, Is.EqualTo(new[] { 3, 4, 6, 7, 9, 12 }));
        }

        [Test]
        public void HeapQueue_SiftDownHandlesNodeWithOnlyLeftChild()
        {
            var queue = new BinaryHeapDispatchQueue<string>();
            queue.Enqueue("A", 1);
            queue.Enqueue("B", 2);
            queue.Enqueue("C", 3);
            queue.Enqueue("D", 4);
            queue.Enqueue("E", 5);
            
            var priorities = new List<int>();
            while (queue.Count > 0)
            {
                priorities.Add(queue.DequeueNext().Priority);
            }

            Assert.That(priorities, Is.EqualTo(new[] { 1, 2, 3, 4, 5 }));
        }

        [Test]
        public void HeapQueue_LateSmallItemCanMoveToFront()
        {
            var queue = new BinaryHeapDispatchQueue<string>();
            queue.Enqueue("A", 8);
            queue.Enqueue("B", 10);
            queue.Enqueue("C", 12);
            queue.Enqueue("Urgent", 1);

            Assert.That(queue.PeekNext().Item, Is.EqualTo("Urgent"));
        }

        [Test]
        public void HeapQueue_RemovingRootRestoresNextMinimum()
        {
            var queue = new BinaryHeapDispatchQueue<string>();
            queue.Enqueue("A", 1);
            queue.Enqueue("B", 4);
            queue.Enqueue("C", 2);
            queue.Enqueue("D", 9);
            queue.Enqueue("E", 7);
            queue.Enqueue("F", 3);

            Assert.That(queue.DequeueNext().Priority, Is.EqualTo(1));
            Assert.That(queue.PeekNext().Priority, Is.EqualTo(2));
            Assert.That(queue.Count, Is.EqualTo(5));
        }

        [Test]
        public void HeapQueue_SingleItemCanBeDequeued()
        {
            var queue = new BinaryHeapDispatchQueue<string>();
            queue.Enqueue("Only", 5);

            Assert.That(queue.DequeueNext().Item, Is.EqualTo("Only"));
            Assert.That(queue.Count, Is.EqualTo(0));
        }

        [Test]
        public void HeapQueue_DequeueOnEmptyThrows()
        {
            var queue = new BinaryHeapDispatchQueue<string>();
            Assert.Throws<InvalidOperationException>(() => queue.DequeueNext());
        }

        [Test]
        public void HeapQueue_DuplicatePrioritiesKeepAllItems()
        {
            var queue = new BinaryHeapDispatchQueue<string>();
            queue.Enqueue("A", 5);
            queue.Enqueue("B", 5);
            queue.Enqueue("C", 5);

            var items = new HashSet<string>
            {
                queue.DequeueNext().Item,
                queue.DequeueNext().Item,
                queue.DequeueNext().Item
            };

            Assert.That(items, Is.EquivalentTo(new[] { "A", "B", "C" }));
        }
    }
}
