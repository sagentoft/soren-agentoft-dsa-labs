# Lab 4B - Priority Queue

## What this lab is for

This lab returns to the operation from the Week 2 World Map workshop:

> Several candidates are waiting to be processed. Each has a cost/priority. We repeatedly need the cheapest one next.

A **priority queue** describes the behavior we need.

A **binary min-heap** is one common representation used to implement that behavior efficiently.

Lab 4A introduced general tree structure and traversal. Here we use a much more specialized tree shape:

```text
complete binary tree
+
min-heap ordering invariant
```

For compactness, we store that logical tree in a `List` rather than creating linked node objects.

## Core path

1. Inspect the working unsorted `LinearDispatchQueue<T>` baseline.
2. Trace a binary min-heap as both a tree and an array/List.
3. Implement insertion with **sift up**.
4. Implement removal of the minimum with **sift down**.
5. Make the tests pass.
6. Add one meaningful test.
7. Explain why a heap is not fully sorted and why that is enough.

## Run the lab

From the course repository root:

```bash
dotnet restore labs/04b-priority-queue/DSA.Lab04B.sln
dotnet build labs/04b-priority-queue/DSA.Lab04B.sln
dotnet run --project labs/04b-priority-queue/src/DSA.Lab04B.App
dotnet test labs/04b-priority-queue/tests/DSA.Lab04B.Tests
```

If you copy the lab into your own `gp26-dsa-labs` repository, use the equivalent path in your repository.

## Untouched starter state

The linear baseline and empty/peek behavior are already supplied. Heap mutation is intentionally incomplete.

After a successful build, the supplied tests are intended to begin with:

```text
3 passing
7 failing
```

The failing tests correspond to the heap operations you are meant to implement.

## Task 1 - Inspect the baseline

`LinearDispatchQueue<T>` stores entries in an unsorted `List`.

`Enqueue` can append immediately.

But `PeekNext` / `DequeueNext` must find the smallest priority by scanning the list.

Reason about:

```text
Enqueue          O(1) amortized
Peek minimum     O(n)
Dequeue minimum  O(n)
```

Ask:

- Which operation is cheap?
- Which operation repeats work?
- What happens when 100,000 candidates are waiting?
- Why did Problem D from the World Map workshop motivate a different representation?

## Task 2 - Connect the heap to Lab 4A trees

A **complete binary tree** fills each level from left to right before starting the next level.

A **binary min-heap** additionally keeps this invariant:

> Every parent has priority less than or equal to each of its children.

Example:

```text
          3
        /   \
       4     9
      / \   /
     12  7 10
```

This is a valid min-heap.

It is **not fully sorted**. The heap only guarantees enough ordering for the priority-queue operation:

> The minimum is always at the root.

Unlike Lab 4A's linked `HierarchyNode<T>`, a complete binary tree can be stored compactly in an array/List:

```text
index:     0  1  2   3  4   5
priority: [3, 4, 9, 12, 7, 10]
```

For index `i`:

```text
parent      = (i - 1) / 2
left child  = 2 * i + 1
right child = 2 * i + 2
```

Trace those relationships for indices 0 through 5.

## Task 3 - Insert with sift up

A new entry is appended at the next free List position.

That preserves the **complete-tree shape**, but it may break the min-heap invariant.

Example: insert priority 2.

```text
before:
[3, 4, 9, 12, 7, 10]

append:
[3, 4, 9, 12, 7, 10, 2]
```

Implement:

```csharp
Enqueue(T item, int priority)
SiftUp(int index)
```

Reasoning:

1. Compare the new entry with its parent.
2. If the parent already has an equal/lower priority, stop.
3. Otherwise swap.
4. Continue from the new position.

## Task 4 - Remove the minimum with sift down

The minimum is at index 0.

Removing index 0 directly would leave a hole at the root, so use the final heap entry to fill that position.

Conceptually:

1. Remember the root.
2. Move the final entry to index 0.
3. Remove the final List slot.
4. Restore the min-heap invariant downward.
5. Return the old root.

When both children exist, compare against the child with the **smaller priority**.

Implement:

```csharp
DequeueNext()
SiftDown(int index)
```

Pay special attention to:

- empty heap;
- one remaining item;
- a node with only a left child;
- choosing the smaller of two children.

## Task 5 - Run the tests

The supplied tests cover:

- empty behavior;
- insertion that must move upward;
- removal that must restore the next minimum;
- repeated removals in ascending priority order;
- one-item behavior;
- duplicate priorities.

For equal priorities, this lab does **not** require stable insertion order. It only requires that no item is lost.

## Task 6 - Add one meaningful test

Examples:

- priorities inserted in descending order;
- priorities already in ascending order;
- alternating enqueue and dequeue;
- a sift-down case with only a left child;
- another edge case you can explain.

## Complexity

For a binary heap:

```text
Enqueue          O(log n)
Peek minimum     O(1)
Dequeue minimum  O(log n)
Count            O(1)
```

Why `O(log n)`?

A complete binary tree has logarithmic height. Sift-up or sift-down follows at most one path through that height.

This is a useful connection to Lab 4A:

```text
general tree traversal      may visit every node → O(n)
heap sift operation         follows one root/leaf path → O(log n)
```

The exact cost depends on the operation and the structure, not merely on the fact that both examples are trees.

## Standard library

In production C#, you would normally consider:

```csharp
PriorityQueue<TElement, TPriority>
```

We build a small heap here because implementing the invariant once makes priority-queue behavior much easier to reason about.

## Assignment bridge

This lab prepares you for the priority-queue phase of the individual Frontier Dispatch assignment.

What transfers:

- complete-binary-tree shape;
- array/List index relationships;
- min-heap invariant;
- sift up after insertion;
- sift down after minimum removal;
- reasoning about `O(log n)` insertion/removal.

What does **not** transfer automatically:

- the exact class/API used in this lab;
- the emergency-dispatch domain;
- every contract and edge case required by the assignment.

Do not copy this class into the assignment as your solution. Use the lab to understand the mechanism, then implement and test the assignment's own `MinPriorityQueue<T>` contract deliberately.

You still do **not** need to jump straight from this lab into Dijkstra. First make the supporting representation and priority-queue behavior correct and explainable.
