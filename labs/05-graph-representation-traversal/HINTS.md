# Lab 5 Progressive Hints

Use these in order. Draw the graph before reading the next hint.

## AdjacencyGraph

### Hint 1

The constructor already creates one empty list for every supplied node.

### Hint 2

For each edge:

```text
From -> To
```

you need to find the list stored under `From`.

### Hint 3

The missing operation is conceptually:

```text
neighborsByNode[edge.From].Add(edge.To)
```

Do not add the reverse direction unless a reverse edge actually exists.

---

## BreadthFirstOrder

### Hint 1

BFS needs two separate ideas:

```text
frontier = what is waiting to be processed
visited  = what has already been discovered
```

### Hint 2

Use a:

```text
Queue<string>
HashSet<string>
```

Add the start node to both the visited set and the queue.

### Hint 3

Each iteration:

1. dequeue one node;
2. add it to the result;
3. inspect its neighbors;
4. enqueue only newly visited neighbors.

### Hint 4

This pattern is useful:

```csharp
if (visited.Add(neighbor))
{
    frontier.Enqueue(neighbor);
}
```

---

## DepthFirstOrder

### Hint 1

The overall structure is very similar to BFS.

Change the frontier from:

```text
Queue
```

to:

```text
Stack
```

### Hint 2

If neighbor order is:

```text
A, B
```

and you want `A` to be popped first, push:

```text
B first
A second
```

because the stack is LIFO.

### Hint 3

Iterate through the neighbor list from the final index down to 0.

---

## IsReachableBfs

### Hint 1

You do not need to construct the entire traversal order.

### Hint 2

If `current == target`, return `true` immediately.

### Hint 3

If the queue becomes empty without finding the target, return `false`.

### Hint 4

If either start or target does not exist in the graph, this lab's contract returns `false`.
