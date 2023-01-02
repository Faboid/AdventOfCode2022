using System.Diagnostics;

namespace AdventOfCode2022.Core.Days_10_19.Day_15;

[DebuggerDisplay("Start: {Start}, End: {End}, IsEmpty: {IsEmpty}")]
public readonly struct Range
{
    public Range(int start, int end) : this(false)
    {
        Start = start;
        End = end;
    }

    public Range(bool isEmpty = true)
    {
        IsEmpty = isEmpty;
    }

    private readonly bool _isNotDefault = true;
    public int Start { get; init; }
    public int End { get; init; }
    public bool IsEmpty { get; init; }

    public bool IsDefault() => !_isNotDefault;
    public bool Contains(int value) => !IsEmpty && Start <= value && value <= End;

    public static Range Empty() => new(true);

}
