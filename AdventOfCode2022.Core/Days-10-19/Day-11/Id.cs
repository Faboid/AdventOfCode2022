namespace AdventOfCode2022.Core.Days_10_19.Day_11;

public class Id {

    private static int _counter = 0;

    public static void ResetCount() => _counter = 0;

    public Id() {
        Value = _counter;
        _counter++;
    }

    public int Value { get; init; }

    public static implicit operator int(Id id) => id.Value;

}