# Lab 6 — Weighted Shortest Paths: Dijkstra → A*

## What this lab is for

Lab 5 gave us the graph-search pattern:

```text
graph representation
+
frontier
+
visited/discovery information
```

BFS used a Queue because the rule was:

> process the oldest discovered candidate next.

Weighted shortest paths need a different rule:

> process the candidate with the smallest currently known total cost next.

That is Dijkstra’s algorithm.

Lab 6 deliberately reuses earlier course concepts:

```text
Lab 3   Dictionary / prepared lookup
Lab 4B  min-priority queue
Lab 5   adjacency + graph frontier reasoning
                 ↓
Lab 6   Dijkstra
```

The required part of this lab is **Lab 6A — Dijkstra**.

**Lab 6B — A*** is optional extension work. Finish and understand Dijkstra first.

---

# Core learning goals

By the end of Lab 6A you should be able to explain and implement:

- weighted directed edges;
- best-known total costs;
- the min-priority-queue frontier;
- relaxation;
- updating a node when a cheaper route is discovered later;
- stale priority-queue entries;
- predecessor / `cameFrom` information;
- route reconstruction;
- `start == goal`;
- unreachable destinations.

You should also be able to explain why ordinary BFS is not enough when edge costs differ.

---

# Run the lab

From the course repository root:

```bash
dotnet restore labs/06-weighted-shortest-paths/DSA.Lab06.sln
dotnet build labs/06-weighted-shortest-paths/DSA.Lab06.sln
dotnet run --project labs/06-weighted-shortest-paths/src/DSA.Lab06.App
dotnet test labs/06-weighted-shortest-paths/tests/DSA.Lab06.Tests
```

If you copy the lab into your own `gp26-dsa-labs` repository, use the equivalent path there.

---

# Starter state

The graph representation and a small practice min-frontier are supplied and working.

The starter also handles two easy route-finder contracts for you:

- `start == goal`;
- unknown start/goal returns not found.

The core Dijkstra search loop is intentionally incomplete.

Expected supplied test state:

```text
4 passing
7 failing
3 explicit/skipped optional A* tests
```

Treat this as a signal, not as seven unrelated bugs.

The seven failing tests share the same missing Dijkstra mechanism.

---

# The practice graph

The main graph contains:

```text
A -> B : 5
A -> C : 2
A -> G : 20
C -> B : 2
C -> D : 6
B -> D : 2
B -> F : 8
D -> C : 2
D -> E : 3
E -> F : 2
E -> G : 5
F -> G : 2

X is disconnected.
```

Important observations:

- `A -> B` is discovered directly with cost 5;
- later, `A -> C -> B` improves B to cost 4;
- `A -> G` has only one edge but costs 20;
- the least-cost A-to-G route uses several edges and costs 13;
- `D -> C` creates a cycle;
- `X` is unreachable from A.

This one graph exercises most of the reasoning we need.

---

# Task 1 — Inspect the supplied graph representation

`WeightedGraph` is already prepared around outgoing-neighbor queries.

Conceptually:

```text
node id
-> outgoing WeightedEdge list
```

Each edge contains:

```text
From
To
Cost
```

Do not rebuild or scan a global edge list inside Dijkstra.

The point is to use the representation that was prepared for repeated neighbor access.

---

# Task 2 — Inspect the supplied frontier

`MinFrontier<T>` behaves like a min-priority queue.

It is supplied because Lab 6 is about **using** priority-queue behavior inside Dijkstra, not redoing Lab 4B.

The Frontier Dispatch assignment is different:

> its assessed Phase 3 requires **your own** `MinPriorityQueue<T>` implementation.

Do not copy `MinFrontier<T>` into the assignment.

The useful operations here are:

```text
Enqueue(value, priority)
Dequeue() -> smallest priority
Count
```

---

# Task 3 — Track best-known total cost

Create/maintain:

```csharp
Dictionary<string, int> bestCost
```

At the start:

```text
bestCost[start] = 0
```

A node that is not yet in the dictionary can be treated as having no known finite route yet.

When processing `current`, each outgoing edge gives a candidate:

```text
candidate = bestCost[current] + edge.Cost
```

Compare that candidate with the best cost currently known for `edge.To`.

---

# Task 4 — Relax an edge

This is the central operation.

For:

```text
current -> neighbor : edgeCost
```

calculate:

```text
candidate = bestCost[current] + edgeCost
```

Then:

```text
if neighbor has no known cost
OR candidate < known cost:
    bestCost[neighbor] = candidate
    cameFrom[neighbor] = current
    enqueue neighbor with priority candidate
```

Example:

```text
A -> B direct gives B = 5
A -> C gives C = 2
C -> B gives candidate = 2 + 2 = 4

4 < 5
=> improve B from 5 to 4
```

The fact that B was already discovered does **not** mean its first cost was final.

This is one major difference from the simple visited logic used in BFS.

---

# Task 5 — Handle stale queue entries

Our simple frontier does not expose a special decrease-key operation.

So after improving:

```text
B : 5
```

to:

```text
B : 4
```

the queue may temporarily contain both.

That is okay.

When you dequeue an entry, compare its priority with the current authoritative value in `bestCost`.

If they do not match, the queue entry is stale:

```text
bestCost[B] = 4
but dequeued entry is B : 5
=> skip it
```

Do not reset the correct cost back to the stale value.

---

# Task 6 — Know when the goal is final

Do not stop merely because the goal is first **discovered**.

A cheaper route might still be waiting.

With non-negative edge costs, Dijkstra can finalize the goal when the goal is removed from the min-priority queue with its current best-known cost.

At that point you can reconstruct and return the route.

---

# Task 7 — Remember predecessors

Maintain:

```csharp
Dictionary<string, string> cameFrom
```

Only update it when you improve the corresponding best cost.

For example:

```text
cameFrom[B] = C
cameFrom[D] = B
cameFrom[E] = D
cameFrom[F] = E
cameFrom[G] = F
```

When the goal is finalized:

1. begin at the goal;
2. repeatedly follow `cameFrom` backward;
3. stop at start;
4. reverse the collected sequence.

This converts:

```text
G <- F <- E <- D <- B <- C <- A
```

into:

```text
A -> C -> B -> D -> E -> F -> G
```

---

# Task 8 — Run tests progressively

Do not write the whole algorithm and only test at the end.

Useful progression:

1. direct or simple route;
2. cheaper route discovered later;
3. route reconstruction;
4. unreachable goal;
5. cycle;
6. stale queue entry;
7. least cost versus fewest edges.

When a test fails, draw the frontier and `bestCost` state for that small graph.

---

# Complexity

With prepared adjacency and a binary-heap-style priority queue, Dijkstra avoids repeatedly scanning the whole graph to choose the next node.

A standard binary-heap Dijkstra analysis is often written approximately as:

```text
O((V + E) log V)
```

This lab uses a practical duplicate-entry technique instead of decrease-key. Successful relaxations may add new queue entries and old entries can become stale.

The exact bound for that implementation can be described in terms of the number of queue entries (up to O(E) successful relaxations), giving roughly:

```text
O((V + E) log E)
```

For this course, the important reasoning is:

- adjacency prevents repeated full-edge scans for neighbor access;
- the priority queue gives logarithmic insertion/removal of the next cheapest candidate;
- each successful relaxation represents a real improvement to a best-known cost.

Be precise about the implementation you are actually analyzing.

---

# Add one meaningful test

After the supplied Dijkstra tests pass, add at least one test you can justify.

Good examples:

- two equal-cost routes;
- start has no outgoing edges;
- a self-loop;
- zero-cost edges;
- a graph where the goal is discovered early but finalized later;
- another stale-entry situation.

---

# Assignment bridge

This lab is intentionally close in **reasoning** but different in **API/domain** from Frontier Dispatch.

What transfers:

```text
weighted adjacency
best-known cost
priority-queue frontier
relaxation
stale-entry handling
cameFrom / predecessor data
route reconstruction
unreachable and start==goal cases
```

What you must adapt yourself:

- Frontier Dispatch types and IDs;
- the exact `DijkstraRouteFinder` contract;
- your own Phase 2 representation;
- your own assessed `MinPriorityQueue<T>`;
- the assignment result type and diagnostics;
- your own meaningful tests.

After Lab 6A, you have been taught the main algorithmic concepts needed for the assignment’s G-level route-finding phase.

---

# Lab 6B — Optional: from Dijkstra to A*

Finish Dijkstra first.

A* keeps Dijkstra’s best-known cost:

```text
g(n) = known cost from start to n
```

but changes frontier priority to:

```text
f(n) = g(n) + h(n)
```

where `h(n)` estimates remaining cost to the goal.

The supplied practice graph includes coordinates and a Manhattan heuristic:

```csharp
Heuristics.Manhattan(node, goal)
```

The optional `AStarRouteFinder` asks you to adapt the Dijkstra mechanism rather than rewrite everything from scratch.

It also accepts an optional set of blocked nodes to practise traversal constraints.

Questions to answer before coding:

1. What stays exactly the same as Dijkstra?
2. What changes in the priority placed in the frontier?
3. Which value should `bestCost` store: `g` or `g + h`?
4. Why should a heuristic not overestimate if you want the usual least-cost guarantee?
5. How should blocked nodes affect neighbor expansion?

The assignment’s VG extension has its own contracts and scenarios. Treat this as conceptual practice, not copyable assignment code.
