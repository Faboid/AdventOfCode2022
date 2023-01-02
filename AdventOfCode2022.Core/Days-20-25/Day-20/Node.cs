using System.Diagnostics;

namespace AdventOfCode2022.Core.Days_20_25.Day_20;

[DebuggerDisplay("Value: {Value}")]
public class Node {

    public Node(long value) {
        Value = value;
    }

    public long Value { get; set; }

    public Node? Parent { get; set; }
    public Node? Child { get; set; }

    public void InsertBetween(Node parent, Node child) {
        parent.Child = this;
        Parent = parent;
        child.Parent = this;
        Child = child;
    }

    public void RemoveBetween(Node parent, Node child) {
        parent.Child = child;
        Parent = null;
        child.Parent = parent;
        Child = null;
    }

    public void MoveUp(long times) {
        
        var curr = this;
        while(times > 0) {
            curr = curr?.Parent;
            times--;
        }

        if(curr is null || curr.Parent is null) {
            throw new ArgumentOutOfRangeException(nameof(times));
        }

        if(curr == this) {
            //return;
            curr = curr.Child;
        }

        RemoveBetween(Parent!, Child!);
        InsertBetween(curr.Parent, curr);
        
    }

    public void MoveDown(long times) {

        var curr = this;
        while(times > 0) {
            curr = curr?.Child;
            times--;
        }

        if(curr == this) {
            //return;
            curr = curr.Parent;
        }

        if(curr is null || curr.Parent is null) {
            throw new ArgumentOutOfRangeException(nameof(times));
        }

        RemoveBetween(Parent!, Child!);
        InsertBetween(curr, curr.Child);
    }

}