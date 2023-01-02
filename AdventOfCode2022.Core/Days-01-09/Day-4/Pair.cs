namespace AdventOfCode2022.Core.Day_4;

public class Pair {

    public Pair(Shift first, Shift second) {
        First = first;
        Second = second;
    }

    public Shift First { get; init; }
    public Shift Second { get; init; }

    public bool OneFullyContains => First.Contains(Second) || Second.Contains(First);
    public bool HasOverlap => First.Overlaps(Second) || Second.Overlaps(First);

    public static Pair Parse(string input) {
        var split = input.Split(',');
        return new(Shift.Parse(split[0]), Shift.Parse(split[1]));
    }

}

