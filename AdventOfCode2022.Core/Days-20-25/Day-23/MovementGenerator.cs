namespace AdventOfCode2022.Core.Days_20_25.Day_23;

public class MovementGenerator {

    private readonly RangeLimiter _cycle = new((int)Direction.North, (int)Direction.East, (int)Direction.None);

    public Direction MoveNext() => (Direction)(_cycle.Next());

    public Direction ProposeNext(ReadOnlyPosition elfPosition, Map map) {

        foreach(var direction in _cycle.EnumerateRange().Select(x => (Direction)x)) {

            var enumerateTiles = direction switch {
                Direction.North => EnumerateNorth(elfPosition),
                Direction.South => EnumerateSouth(elfPosition),
                Direction.West => EnumerateWest(elfPosition),
                Direction.East => EnumerateEast(elfPosition),
                _ => throw new ArgumentOutOfRangeException(),
            };

            //only return the direction if no elf is in the three tiles
            if(enumerateTiles.Any(map.HasElfIn)) {
                continue;
            }

            return direction;

        }

        return Direction.None;
    }

    private IEnumerable<ReadOnlyPosition> EnumerateNorth(ReadOnlyPosition position) {
        yield return new(position.X - 1, position.Y - 1);
        yield return new(position.X, position.Y - 1);
        yield return new(position.X + 1, position.Y - 1);
    }

    private IEnumerable<ReadOnlyPosition> EnumerateSouth(ReadOnlyPosition position) {
        yield return new(position.X - 1, position.Y + 1);
        yield return new(position.X, position.Y + 1);
        yield return new(position.X + 1, position.Y + 1);
    }


    private IEnumerable<ReadOnlyPosition> EnumerateWest(ReadOnlyPosition position) {
        yield return new(position.X - 1, position.Y + 1);
        yield return new(position.X - 1, position.Y);
        yield return new(position.X - 1, position.Y - 1);
    }

    private IEnumerable<ReadOnlyPosition> EnumerateEast(ReadOnlyPosition position) {
        yield return new(position.X + 1, position.Y + 1);
        yield return new(position.X + 1, position.Y);
        yield return new(position.X + 1, position.Y - 1);
    }

}
