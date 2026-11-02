using System;
using System.Collections.Generic;

namespace DSA.Lab04A
{
    /// <summary>
    /// One node in a general rooted hierarchy.
    ///
    /// A node can have zero or more children. A node with no children is a leaf.
    /// The class stores the shape; traversal algorithms live separately.
    /// </summary>
    public sealed class HierarchyNode<T>
    {
        private readonly List<HierarchyNode<T>> _children = new List<HierarchyNode<T>>();

        public HierarchyNode(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public IReadOnlyList<HierarchyNode<T>> Children => _children;

        public bool IsLeaf => _children.Count == 0;

        public HierarchyNode<T> AddChild(T value)
        {
            var child = new HierarchyNode<T>(value);
            _children.Add(child);
            return child;
        }

        public void AddChild(HierarchyNode<T> child)
        {
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            _children.Add(child);
        }
    }
}
