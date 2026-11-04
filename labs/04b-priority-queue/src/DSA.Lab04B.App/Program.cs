using System;
using DSA.Lab04B;

Console.WriteLine("DSA Lab 4B - Priority Queue");
Console.WriteLine();

var baseline = new LinearDispatchQueue<string>();
baseline.Enqueue("Repair comms relay", 12);
baseline.Enqueue("Medical evacuation", 4);
baseline.Enqueue("Recover supply crate", 9);
baseline.Enqueue("Defend north gate", 6);

Console.WriteLine("Working baseline: unsorted List + scan for minimum.");
while (baseline.Count > 0)
{
    PriorityEntry<string> next = baseline.DequeueNext();
    Console.WriteLine($"  {next.Priority,2}  {next.Item}");
}

Console.WriteLine();
Console.WriteLine("Now implement BinaryHeapDispatchQueue<T> and run the tests.");
