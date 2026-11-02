# Lab 4A Progressive Hints

Use these in order. Draw a tiny tree before reading the next hint.

## CountNodes

### Hint 1

Every call should count the current node once.

### Hint 2

Then ask each child subtree for its own count and add those counts.

### Hint 3

A leaf needs no special `if` statement if your `foreach` loop simply has zero children.

---

## Contains

### Hint 1

Check the current node before searching children.

### Hint 2

As soon as one child subtree returns `true`, the whole search can return `true`.

### Hint 3

If the current node does not match and no child subtree matches, return `false`.

---

## DepthFirstPreOrder

### Hint 1

"Pre-order" here means current node first.

### Hint 2

A small helper can receive the current node and one shared result `List<T>`.

### Hint 3

Add the current value, then call the same helper for every child in order.

---

## MaxDepth

### Hint 1

A leaf has maximum depth 0 relative to itself.

### Hint 2

For a non-leaf, find the largest depth reported by any child subtree.

### Hint 3

The current node adds one edge above that deepest child path.
