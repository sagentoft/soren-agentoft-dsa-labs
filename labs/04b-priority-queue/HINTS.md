# Lab 4B Progressive Hints

Use these in order. Trace a small heap on paper before reading the next hint.

## Enqueue / sift up

### Hint 1

Appending a new item preserves the complete-tree shape. The only possible heap violation begins at the new item.

### Hint 2

While the item is not the root:

```text
parent = (index - 1) / 2
```

Compare the new item's priority with its parent's priority.

### Hint 3

If the parent already has a lower or equal priority, stop.

Otherwise swap parent and child, then continue from the parent's old index.

---

## DequeueNext / sift down

### Hint 1

The answer is already at index 0. Save it before changing the List.

### Hint 2

Move the final List item to index 0, then remove the final slot.

If the heap is now empty, there is nothing to sift.

### Hint 3

For index `i`:

```text
left  = 2 * i + 1
right = 2 * i + 2
```

If there is no left child, there are no children.

### Hint 4

If both children exist, identify the one with the smaller priority.

Compare the current item with that child.

If the current item is already <= that child, the invariant is restored.

Otherwise swap and continue downward.

---

## Debugging an incorrect heap

After every mutation, ask only:

> Is every parent priority <= each child priority?

Do not require the array itself to be globally sorted.

A useful trace is:

```text
enqueue: 8, 10, 12, 1
```

The `1` must eventually become the root.
