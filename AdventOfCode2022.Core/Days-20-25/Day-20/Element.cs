using System.Diagnostics;

namespace AdventOfCode2022.Core.Days_20_25.Day_20;

[DebuggerDisplay("Id: {Id}, Value: {Value}")]
public class Element {

    public Element(int id, int value) {
        Value = value;
        Id = id;
    }

    public int Id { get; init; }
    public int Value { get; init; }

    public Element Clone() => new Element(Id, Value);

}