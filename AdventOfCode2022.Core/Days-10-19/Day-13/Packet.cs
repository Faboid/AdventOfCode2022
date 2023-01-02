using System.Diagnostics;

namespace AdventOfCode2022.Core.Days_10_19.Day_13;

[DebuggerDisplay("{Value}")]
public class Packet {

    public Packet(string value) {
        Value = value;
    }

    public string Value { get; init; }

    public char this[int i] => Value[i];

}
