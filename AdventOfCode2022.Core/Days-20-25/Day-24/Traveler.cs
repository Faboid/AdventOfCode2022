namespace AdventOfCode2022.Core.Days_20_25.Day_24;

public class Traveler {

    public Traveler(ReadOnlyPosition startingPosition) {
        Position = startingPosition;
    }

    private Traveler(ReadOnlyPosition startingPosition, int steps) {
        Position = startingPosition;
        Steps = steps;
    }

    public int Steps { get; private set; } = 0;
    public Position Position { get; private set; }

    public Traveler Move(Direction direction) {
        Position.Move(direction);
        Steps++;
        return this;
    }

    public void Wait() {
        Steps++;
    }

    public Traveler Clone() => new(Position, Steps);

}
