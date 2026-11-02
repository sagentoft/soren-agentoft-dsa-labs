# Lab 4A - Traversing Hierarchical Data

## What this lab is for

Games contain a lot of **hierarchical data**:

- skill trees;
- dialogue or quest hierarchies;
- UI/widget trees;
- scene/object hierarchies;
- decision or behaviour trees;
- file/folder-like content structures.

In this lab you will work with a small general tree and practise the idea that makes recursive tree algorithms possible:

> A tree is a node plus zero or more child trees.

The goal is not to learn binary search trees yet. The goal is to understand **tree vocabulary, recursive structure, traversal order, depth, and cost**.

## Core path

1. Inspect the supplied `HierarchyNode<T>` representation.
2. Draw the example hierarchy and label root, parent, child, leaf and subtree.
3. Implement recursive `CountNodes`.
4. Implement recursive `Contains`.
5. Implement depth-first pre-order traversal.
6. Implement `MaxDepth`.
7. Make the tests pass.
8. Add one meaningful test and explain its purpose.

## Run the lab

From the course repository root:

```bash
dotnet restore labs/04a-traversing-hierarchical-data/DSA.Lab04A.sln
dotnet build labs/04a-traversing-hierarchical-data/DSA.Lab04A.sln
dotnet run --project labs/04a-traversing-hierarchical-data/src/DSA.Lab04A.App
dotnet test labs/04a-traversing-hierarchical-data/tests/DSA.Lab04A.Tests
```

If you copy the lab into your own `gp26-dsa-labs` repository, use the equivalent path in your repository.

## Untouched starter state

The node representation is already working. The traversal algorithms are intentionally incomplete.

After a successful build, the supplied tests are intended to begin with:

```text
2 passing
8 failing
```

The two passing tests verify basic tree construction. The failing tests correspond to the recursive algorithms you are meant to implement.

## Task 1 - Read the hierarchy

The example skill hierarchy is:

```text
Survival
|- Movement
|  |- Dash
|  `- Climb
`- Combat
   |- Sword
   `- Bow
```

Identify:

- the **root**;
- one **parent**;
- one **child**;
- all **leaves**;
- the subtree rooted at `Movement`;
- the depth of `Dash` if root depth is 0.

Expected vocabulary:

```text
root     = the top node
parent   = a node directly above another node
child    = a node directly below another node
leaf     = a node with no children
subtree  = one node together with everything below it
depth    = number of edges from the root
```

## Task 2 - Recognize the recursive shape

Look at `HierarchyNode<T>`.

Each node contains:

```text
Value
Children
```

But every child is itself another:

```csharp
HierarchyNode<T>
```

That repeated shape is why recursion fits naturally.

A useful recursive recipe is:

```text
1. Do the work for the current node.
2. For each child:
      solve the same problem for that child subtree.
3. Combine/return the result.
```

A leaf is often where the recursion naturally stops because there are no child calls to make.

## Task 3 - Implement `CountNodes`

For one node:

```text
count = 1
```

For a node with children:

```text
count this node
+
count each child subtree
```

Implement:

```csharp
CountNodes(root)
```

Before coding, trace it by hand on:

```text
A
|- B
`- C
```

The answer should be 3.

## Task 4 - Implement `Contains`

Question:

> Does this hierarchy contain a node whose value equals the target?

Reasoning:

1. Compare the current node.
2. If it matches, return `true`.
3. Otherwise search each child subtree.
4. If no subtree contains the target, return `false`.

Use normal value equality through `EqualityComparer<T>.Default` rather than assuming `T` is a string or integer.

## Task 5 - Depth-first pre-order traversal

For this lab, **pre-order** means:

```text
visit current node
then visit each child subtree from left to right
```

For:

```text
A
|- B
|  `- D
`- C
```

pre-order is:

```text
A, B, D, C
```

Implement:

```csharp
DepthFirstPreOrder(root)
```

Return the visited values in order.

The important idea is not memorizing the word "pre-order". It is being able to state exactly **when** the current node is processed relative to its children.

## Task 6 - Implement `MaxDepth`

Depth in this lab is measured in **edges from the root**.

Therefore:

```text
single root node            depth 0
root -> child               max depth 1
root -> child -> grandchild max depth 2
```

A node's maximum depth depends on the deepest of its child subtrees.

Implement:

```csharp
MaxDepth(root)
```

## Task 7 - Reason about recursion

For each recursive method, be able to answer:

- What work happens at the current node?
- What smaller version of the same problem is delegated to each child?
- What stops the recursion?
- What result is returned or accumulated?

Recursion uses the program's **call stack**. That is related to the Stack idea from Lab 2, although you are not manually pushing and popping `Stack<T>` entries here.

A very deep hierarchy can therefore use significant call-stack space.

## Complexity

If a traversal must inspect every node once and the tree contains `n` nodes:

```text
CountNodes             O(n)
Contains worst case    O(n)
Depth-first traversal  O(n)
MaxDepth               O(n)
```

The recursive call-stack usage depends on tree height `h`:

```text
additional call-stack space: O(h)
```

A broad shallow tree and a long chain can contain the same number of nodes but have very different heights.

## Task 8 - Add one meaningful test

Examples:

- an unbalanced/deep hierarchy;
- duplicate values;
- a target stored at the root;
- a target stored in the final subtree;
- another case you can justify.

Choose a test because it protects an assumption or edge case, not simply to increase the number of tests.

## Assignment bridge

This lab is mainly **foundation** for the next course step rather than a direct assignment implementation.

What transfers:

- thinking in parent/child relationships;
- understanding tree depth/height;
- tracing recursive work through a hierarchy;
- recognizing that a tree can be represented in more than one way.

On Thursday, the priority-queue lab introduces a **binary min-heap**. A heap is a specialized tree-shaped structure with strict shape and ordering rules, commonly stored compactly in an array/List rather than as linked node objects.

What does **not** transfer automatically:

- `HierarchyNode<T>` is not the assignment priority queue;
- the assignment does not require you to turn world locations into this kind of tree;
- recursion is not automatically the correct approach for every hierarchy or graph problem.

Use this lab to understand hierarchy and traversal. Then adapt that understanding to the specific structure and operations required by later problems.
