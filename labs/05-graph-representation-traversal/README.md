# Lab 5 - Graph Representation and Traversal

## What this lab is for

Many game systems are not naturally a line or a hierarchy.

A world map can contain:

- several routes into the same location;
- cycles;
- one-way connections;
- disconnected regions.

That is a **graph**.

This lab focuses on two questions:

1. **How should we represent the graph for the operations we perform repeatedly?**
2. **How do we systematically explore the reachable part of the graph?**

You will compare a simple global edge list with an adjacency representation, then implement:

- **Breadth-First Search (BFS)** using a `Queue<T>`;
- **Depth-First Search (DFS)** using a `Stack<T>`;
- a small reachability query using BFS.

This is an **unweighted** traversal lab. Weighted shortest-path selection comes later.

## Core path

1. Inspect the working `EdgeListGraph` baseline.
2. Draw the supplied dungeon graph and identify nodes, directed edges, cycles and a disconnected node.
3. Complete the adjacency representation in `AdjacencyGraph`.
4. Implement iterative BFS.
5. Implement iterative DFS.
6. Implement `IsReachableBfs`.
7. Make the supplied tests pass.
8. Add one meaningful test and explain what it protects.

## Run the lab

From the course repository root:

```bash
dotnet restore labs/05-graph-representation-traversal/DSA.Lab05.sln
dotnet build labs/05-graph-representation-traversal/DSA.Lab05.sln
dotnet run --project labs/05-graph-representation-traversal/src/DSA.Lab05.App
dotnet test labs/05-graph-representation-traversal/tests/DSA.Lab05.Tests
```

If you copy the lab into your own `gp26-dsa-labs` repository, use the equivalent path in your repository.

## Untouched starter state

The directed-edge type and global edge-list baseline are already working.

`AdjacencyGraph` already creates one adjacency bucket per supplied node, but it does **not yet group the edges into those buckets**.

The traversal methods are intentionally incomplete.

After a successful build, the supplied tests are intended to begin with:

```text
13 tests total
4 passing
9 failing
```

The first passing tests verify the baseline and the supplied node buckets. The remaining tests correspond to adjacency grouping and traversal work.

---

## Task 1 - Read the graph

The example dungeon is:

```text
Entrance
├─> Hall
│   ├─> Armory ─> BossRoom
│   └─> Courtyard ─> BossRoom
│          └────────> Hall
└─> StoreRoom ─> Courtyard

SecretRoom   (disconnected)
```

Important vocabulary:

```text
node / vertex   one location or state
edge            one connection between nodes
directed edge   connection with a specific From -> To direction
path            sequence of connected edges
cycle           route that can lead back to a previously visited node
```

This lab uses **directed** edges.

If a connection should work in both directions, represent both directions explicitly:

```text
A -> B
B -> A
```

Ask:

- Where is the cycle?
- Can `Entrance` reach `BossRoom`?
- Can `Entrance` reach `SecretRoom`?
- Can `BossRoom` reach `Entrance`?

Do not assume that a visual connection is automatically two-way.

---

## Task 2 - Inspect the edge-list baseline

`EdgeListGraph` stores all edges together:

```text
Entrance -> Hall
Entrance -> StoreRoom
Hall -> Armory
Hall -> Courtyard
...
```

To answer:

> "Which nodes can I go to directly from Hall?"

the baseline scans the global edge list and keeps edges where:

```text
edge.From == "Hall"
```

If there are `E` edges:

```text
GetNeighbors(node) → O(E)
```

That implementation is correct.

The question is whether its representation matches the workload.

If a traversal asks for neighbors again and again, scanning unrelated edges every time repeats work.

---

## Task 3 - Prepare adjacency

A representation organized around the repeated operation might look like:

```text
Entrance  -> [Hall, StoreRoom]
Hall      -> [Armory, Courtyard]
StoreRoom -> [Courtyard]
Armory    -> [BossRoom]
Courtyard -> [Hall, BossRoom]
BossRoom  -> []
SecretRoom-> []
```

In the supplied code this is represented as:

```csharp
Dictionary<string, List<string>>
```

Complete the edge-grouping step in the `AdjacencyGraph` constructor.

For every directed edge:

```text
From -> To
```

append `To` to the list belonging to `From`.

The empty lists matter.

A node can exist in the graph even if it currently has no outgoing neighbors.

### Cost

Building the adjacency representation requires processing the input:

```text
preparation: O(V + E)
additional storage: O(V + E)
```

After preparation, obtaining the adjacency list for one node uses an expected O(1) dictionary lookup, then processing that node's actual outgoing neighbors costs proportional to its degree.

Conceptually:

```text
GetNeighbors(v) → expected O(1) to find the list
processing all neighbors → O(degree(v))
```

This is a classic:

```text
prepare once
→ query many times
```

trade-off.

---

## Task 4 - Breadth-First Search

BFS explores outward **layer by layer**.

Use:

```text
Queue<string>   frontier
HashSet<string> visited
```

A useful recipe is:

```text
mark start visited
enqueue start

while frontier is not empty:
    dequeue current
    process current

    for each neighbor:
        if neighbor has not been visited:
            mark it visited
            enqueue it
```

For this graph, starting at `Entrance`, the expected order is:

```text
Entrance
Hall
StoreRoom
Armory
Courtyard
BossRoom
```

### Why mark visited when enqueuing?

Because a graph can contain several routes to the same node.

If a node is only marked much later, multiple parents may add the same node to the frontier.

Using:

```csharp
if (visited.Add(neighbor))
{
    frontier.Enqueue(neighbor);
}
```

combines the membership check and insertion cleanly.

---

## Task 5 - Depth-First Search

DFS uses the same broad pattern but changes the frontier:

```text
BFS → Queue → first discovered candidate processed first
DFS → Stack → most recently discovered candidate processed first
```

For the supplied deterministic neighbor order, the expected DFS order from `Entrance` is:

```text
Entrance
Hall
Armory
BossRoom
Courtyard
StoreRoom
```

Because a stack reverses removal order, an iterative DFS that wants to process the first authored neighbor first should push neighbors in **reverse** order.

That is a representation/detail issue, not a different definition of DFS.

---

## Task 6 - Why `visited` is essential

Look at:

```text
Hall -> Courtyard
Courtyard -> Hall
```

Without a visited set:

```text
Hall
→ Courtyard
→ Hall
→ Courtyard
→ ...
```

The traversal can continue forever.

`HashSet<string>` is a good match because the repeated question is:

> "Have I already discovered this node?"

That is a membership operation.

This deliberately reuses the hashing work from Lab 3.

---

## Task 7 - Reachability with early exit

Implement:

```csharp
IsReachableBfs(graph, start, target)
```

This method does **not** need to return the full traversal order.

As soon as the target is removed from the frontier, the answer is known:

```text
target found → true
frontier exhausted → false
```

This is a useful distinction:

```text
traverse everything reachable
vs
stop when the required answer is known
```

---

## Complexity

With an adjacency representation, BFS and DFS visit each reachable node at most once and inspect each reachable outgoing edge at most once.

For the whole graph:

```text
BFS → O(V + E)
DFS → O(V + E)
```

Additional traversal storage is:

```text
visited  O(V)
frontier O(V) worst case
result   O(V) when returning traversal order
```

The shape of the frontier differs:

- BFS can become wide;
- DFS can become deep.

But both still have O(V + E) time complexity for full traversal.

---

## Task 8 - Add one meaningful test

Examples:

- start node has no outgoing edges;
- graph containing a self-loop;
- several different paths reach the same node;
- disconnected components;
- a target is the start node;
- another case you can justify.

Choose a test because it protects an assumption or edge case.

---

## Optional extension - reconstruct an unweighted path

BFS can also reconstruct a shortest path by **number of edges** in an unweighted graph.

One common technique is to remember:

```text
cameFrom[child] = parent
```

when a node is discovered.

When the target is found, follow those predecessor links backward and reverse the result.

This extension is useful preparation for later path reconstruction, but it is **not required for Lab 5**.

---

## Assignment bridge

This lab prepares the graph-search reasoning used later in the Frontier Dispatch assignment.

What transfers:

- treating locations/connections as a graph;
- organizing outgoing neighbors for repeated access;
- `Queue<T>` and `Stack<T>` as different frontier policies;
- `HashSet<T>` for visited membership;
- cycles and disconnected regions;
- reasoning about `O(V + E)` traversal;
- separating graph representation from the traversal algorithm.

What does **not** transfer automatically:

- the exact dungeon classes or APIs;
- the use of string room names;
- BFS as the assignment's weighted route solution.

Frontier Dispatch connections have **travel costs**.

BFS chooses the next node by discovery order. It is appropriate for unweighted traversal and can find a shortest path by number of edges.

The weighted route phase needs a different rule:

> process the candidate with the smallest known total cost next.

That is why the priority queue from Lab 4B matters.

Do **not** replace the assignment's later Dijkstra work with BFS simply because BFS can traverse the graph.

The intended sequence is:

```text
representation
→ graph traversal
→ weighted shortest-path reasoning
```
