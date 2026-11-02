# Notes

This folder is optional.

Use it for your own:
- checkpoint-question answers;
- predictions;
- complexity notes;
- debugging notes;
- reflections.

You do not need to keep formal notes unless a lab specifically asks you to.

# Lab 0:

1. Which of the three operations may need to inspect every player?
    FindHighestsScore has to loop through every element in the list to get the highest score. The others may loop through through 
    every element in the list, and then return false or null, as a worst-case.

2. If the number of players becomes 100 times larger, which operation(s) do you expect to become noticeably more expensive?
    Since FindHighestScore always has to loop over the entire collection, I would expect that one to become noticeably more expensive.
    That being said, all methods are at worst O(n), however, FindHighestScore's only case of being O(1) is if the list is empty.
    The other methods can exit early.

3. What information could we organize differently if finding players by ID became extremely common?
    We could turn the collection into a dictionary. Since ID is unique, that could work as the key. That way, the FindById could simply do return _players[id]

4. What edge case did you choose to test, and why?
    I tested the case sensitivity given that was in the lab instructions.

5. At what point did you make a Git commit? What made that a sensible checkpoint?
    After every step. So after implementing each function, and then after writing and running the new test.
    And I will commit after this, too.

# Lab 1:

Task 1

1. What does i represent?
    i represents the current index that is being compared to target.

2. When does the method stop early?
    When it finds a value at an index that matches the target.

3. What does -1 mean?
    That it didn't find a value matching the target.

4. What is the best case?
    That it finds a value matching target on index 0.

5. What is the worst case?
    That it iterates over the whole collection without finding the target or finding the target
    at the very last index.

6. If the array becomes twice as large, what happens to the worst-case amount of work?
    It also becomes twice as large. The method is O(n) time complexity.

Task 2

Binary search only works with ordered data because it relies on the order of the values
 to determine which half of the data to discard. Without ordered data, there would be no
 way to guarantee that the target wasn't in the discarded half.


Task 5

Input size	Linear search	Binary search	What changed?
10,000		6.45 µs	        0.10 µs         Baseline
100,000		33.71 µs        0.12 µs	        Linear search took 5.2 times longer while binary took 20% longer
1,000,000	342.70 µs		0.20 µs         Linear search took more than 10 times longer while binary search took 66.66...%

Linear search - O(n): Worst-case outcome is examining every element.
Binary search - O(log n): Worst-case outcome comes out to about 20 comparisons

Task 6

1. why array indexing is treated as O(1)?
    Elements are stored in contiguous memory, so the address of an element can be calculated directly
    using its index. It essentially means that accessing an element takes constant time, regardless
    of array size.

2. why linear search is O(n) in the worst case?
    As a worst-case, linear search must examine every element in the array before finding the target,
    or determining it doesn't exist. Therefore, the amount of work grows linearly with the number of elements.

3. why binary search is O(log n)?
    Binary search eliminates half the remaining search space with each iteration. Because the number of elements
    is repeatedly halves, the number of iterations grows logarithmically with array size.

4. why this insertion-sort implementation is O(n²) in the worst case?
    In the worst case, when the array is sorted in reverse order, each element must be compared with and shifted past all other elements. Since the number of comparisons and and moves increases every iteration of tthe outer loop, the total
    amount of work grows quadratically with the array's size.

# Lab 3

Task 1

1. What is the worst case when the key is missing?
    We iterate through the entire list without finding the key, then make a new entry.

2. What changes when the collection grows from 100 entries to 100,000?
    The number of operations increases by up to 100,000. O(n)

3. What operation are we trying to improve?
    We are trying to improve the efficiency of looking up a key, ideally without
    having to iterate over the entire list. Perhaps a dictionary might be a solution.

Task 2

The supplied hash table uses positive integer keys and:

bucketIndex = key % bucketCount;
With five buckets, calculate the bucket for:

101, 205, 734, 206, 999
Which keys collide?

Given bucketIndex = key % bucketCount where bucketCount = 5.
Bucket 0: 205
Bucket 1: 101, 206
Bucket 4: 734, 999

Task 4

1. Why can keys 1 and 6 both exist when the table has five buckets?
    Because 1 % 5 and 6 % 5 both equal 1. And because hash tables support
    collisions, they an both exist in the same bucket.
2. What happens to lookup cost if almost every key ends up in the same bucket?
    As the number if keys in a bucket increases, the lookup cost increases linearly.
    Approaching O(n)
3. Why does expected O(1) lookup not mean "exactly one CPU operation"?
   O(1) describes how the amount of work scales with the number of entries,
   not how many CPU operations are performed.

# Lab 4A

Task 1

Survival ---- root, parent, depth 0
|- Movement - parent, child, subtree root, depth 1
|  |- Dash -- child, leaf, depth 2
|  `- Climb - child, leaf, depth 2
`- Combat --- child, parent, subtree root, depth 1
   |- Sword - child, leaf, depth 2
   `- Bow --- child, leaf, depth 2


