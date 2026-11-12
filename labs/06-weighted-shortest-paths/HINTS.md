# Lab 6 Progressive Hints

Use these in order. Prefer drawing the current `bestCost` table and frontier before reading the next hint.

## Dijkstra main loop

### Hint 1

The loop continues while:

```text
frontier.Count > 0
```

Remove one `MinFrontier<string>.Entry` each iteration.

### Hint 2

The entry contains:

```text
Value    -> node id
Priority -> total cost that was known when this entry was enqueued
```

### Hint 3 — stale entries

Look up the current authoritative cost:

```text
bestCost[current]
```

If the dequeued priority no longer matches that current best value, skip the entry.

---

## Relaxation

### Hint 1

For each outgoing edge:

```text
candidate = currentBest + edge.Cost
```

### Hint 2

An update happens if:

```text
neighbor has no known cost
OR
candidate < known cost
```

### Hint 3

On improvement, three pieces of state change:

```text
bestCost[neighbor]
cameFrom[neighbor]
frontier
```

---

## Route reconstruction

### Hint 1

Start with:

```text
current = goal
```

and add the goal to a temporary list.

### Hint 2

While current is not start:

```text
current = cameFrom[current]
add current
```

### Hint 3

You collected the path backward. Reverse the list before returning it.

---

## If the goal is unreachable

If the frontier becomes empty without finalizing the goal:

```text
return NotFound
```

---

## Optional A*

### Hint 1

Keep `bestCost` as the real cost so far:

```text
g(n)
```

Do not store `g+h` there.

### Hint 2

Only the frontier priority changes:

```text
priority = g(neighbor) + h(neighbor)
```

### Hint 3

For this lab:

```text
h = Manhattan distance to goal
```

### Hint 4

If a neighbor is blocked, do not relax/traverse into it.
