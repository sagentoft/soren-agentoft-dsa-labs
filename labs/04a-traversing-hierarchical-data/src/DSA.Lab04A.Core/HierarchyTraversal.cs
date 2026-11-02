using System;
using System.Collections.Generic;

namespace DSA.Lab04A
{
    /// <summary>
    /// Recursive algorithms over a rooted hierarchy.
    ///
    /// The key idea is structural recursion:
    /// solve the current node, then apply the same operation to each child subtree.
    /// </summary>
    public static class HierarchyTraversal
    {
        public static int CountNodes<T>(HierarchyNode<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            // TODO: count this node, then count every child subtree.
            int count = 1;
            
            foreach (HierarchyNode<T> child in root.Children)
            {
                count += CountNodes(child);
            }
            
            return count;
        }

        public static bool Contains<T>(HierarchyNode<T> root, T target)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            // TODO: compare the current node, then search child subtrees.
            if (EqualityComparer<T>.Default.Equals(root.Value, target))
            {
                return true;
            }

            foreach (HierarchyNode<T> child in root.Children)
            {
                if (Contains(child, target))
                {
                    return true;
                }
            }

            return false;
        }

        public static IReadOnlyList<T> DepthFirstPreOrder<T>(HierarchyNode<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            // TODO: return values in pre-order:
            // current node first, then each child subtree from left to right.
            var result = new List<T>();
            
            AddPreOrder(root, result);
            return result;
        }

        public static int MaxDepth<T>(HierarchyNode<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            // Depth is measured in edges from the root.
            // A single-node tree therefore has max depth 0.
            int deepestChild = -1;

            foreach (HierarchyNode<T> child in root.Children)
            {
                deepestChild = Math.Max(deepestChild, MaxDepth<T>(child));
            }

            return deepestChild + 1;
        }

        private static void AddPreOrder<T>(HierarchyNode<T> node, List<T> result)
        {
            result.Add(node.Value);

            foreach (HierarchyNode<T> child in node.Children)
            {
                AddPreOrder(child, result);
            }
        }
    }
}
