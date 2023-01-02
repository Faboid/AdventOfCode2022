namespace AdventOfCode2022.Core.Days_10_19.Day_15;

public class Beacon {

    public ReadOnlyPosition Position { get; init; }

    public Beacon(ReadOnlyPosition position) {
        Position = position;
    }
}