namespace AdventOfCode2022.Core.Days_10_19.Day_13;

public class Pair {
    public Pair(string left, string right) {
        Left = new(left);
        Right = new(right);
    }

    public Packet Left { get; init; }
    public Packet Right { get; init; }

    public bool IsOrdered() {
        return new PacketsComparer().Compare(Left, Right) < 0;
    }

}
