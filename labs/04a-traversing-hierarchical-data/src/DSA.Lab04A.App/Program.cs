using System;
using DSA.Lab04A;

Console.WriteLine("DSA Lab 4A - Traversing Hierarchical Data");
Console.WriteLine();

HierarchyNode<string> root = BuildSkillTree();

Console.WriteLine("Example hierarchy:");
Console.WriteLine("Survival");
Console.WriteLine("|- Movement");
Console.WriteLine("|  |- Dash");
Console.WriteLine("|  `- Climb");
Console.WriteLine("`- Combat");
Console.WriteLine("   |- Sword");
Console.WriteLine("   `- Bow");
Console.WriteLine();

try
{
    Console.WriteLine($"Node count: {HierarchyTraversal.CountNodes(root)}");
    Console.WriteLine($"Contains Bow: {HierarchyTraversal.Contains(root, "Bow")}");
    Console.WriteLine($"Max depth: {HierarchyTraversal.MaxDepth(root)}");
    Console.WriteLine("Pre-order: " + string.Join(" -> ", HierarchyTraversal.DepthFirstPreOrder(root)));
}
catch (NotImplementedException)
{
    Console.WriteLine("Traversal methods are intentionally incomplete in the starter.");
    Console.WriteLine("Implement them, then run the tests again.");
}

static HierarchyNode<string> BuildSkillTree()
{
    var survival = new HierarchyNode<string>("Survival");
    HierarchyNode<string> movement = survival.AddChild("Movement");
    movement.AddChild("Dash");
    movement.AddChild("Climb");

    HierarchyNode<string> combat = survival.AddChild("Combat");
    combat.AddChild("Sword");
    combat.AddChild("Bow");

    return survival;
}
