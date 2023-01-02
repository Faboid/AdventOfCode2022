namespace AdventOfCode2022.Core.Days_20_25.Day_24;

public class Blizzard {

    public Blizzard(Direction direction, ReadOnlyPosition startingPosition) {
        Direction = direction;
        Position = startingPosition;
    }

    public Direction Direction { get; init; }
    public Position Position { get; private set; }

    public void Move() {
        Position.Move(Direction);
    }

    public Blizzard Clone() => new(Direction, Position);

}
