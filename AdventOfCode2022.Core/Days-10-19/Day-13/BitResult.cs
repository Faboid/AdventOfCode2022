namespace AdventOfCode2022.Core.Days_10_19.Day_13;

public class BitResult {

    public BitResult(int? value, int depth) {
        Value = value;
        Depth = depth;
    }

    public int? Value { get; } = null;
    public int Depth { get; }

}
