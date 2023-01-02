namespace AdventOfCode2022.Core.Days_20_25.Day_25;

public struct SnafuNumber {

    public SnafuNumber(string value) {
        AsLong = value.Convert();
        AsString = value;
    }

    public SnafuNumber(long value) {
        AsString = value.Convert();
        AsLong = value;
    }

    public string AsString { get; private set; }
    public long AsLong { get; private set; }

    public override string ToString() {
        return $"{AsString} - ({AsLong})";
    }
}
