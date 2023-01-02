namespace AdventOfCode2022.Core.Days_20_25.Day_24;

public class Map {
    public Map(Walls heights, Walls widths, ReadOnlyPosition entrancePosition, ReadOnlyPosition exitPosition) {
        Heights = heights;
        Widths = widths;
        EntrancePosition = entrancePosition;
        ExitPosition = exitPosition;
    }

    public record struct Walls(int Min, int Max);

    /// <summary>
    /// Shows min and max height.
    /// </summary>
    public Walls Heights { get; init; }

    /// <summary>
    /// Shows min and max width.
    /// </summary>
    public Walls Widths { get; init; }

    public ReadOnlyPosition EntrancePosition { get; init; }
    public ReadOnlyPosition ExitPosition { get; init; }

    public void WallWarping(Blizzard blizzard) {

        if(blizzard.Position.Y == Heights.Min) {
            blizzard.Position.Y = Heights.Max - 1;
            return;
        }

        if(blizzard.Position.Y == Heights.Max) {
            blizzard.Position.Y = Heights.Min + 1;
            return;
        }

        if(blizzard.Position.X == Widths.Min) {
            blizzard.Position.X = Widths.Max - 1;
            return;
        }

        if(blizzard.Position.X == Widths.Max) {
            blizzard.Position.X = Widths.Min + 1;
            return;
        }

    }

    public bool IsOutOfBoundsOrInWalls(ReadOnlyPosition position) {

        if(position == EntrancePosition || position == ExitPosition) {
            return false;
        }

        return position.Y <= Heights.Min || position.Y >= Heights.Max || position.X <= Widths.Min || position.X >= Widths.Max;
    }

    public Traveler SpawnNew() => new(EntrancePosition);
    public bool HasReachedGoal(Traveler traveler) => traveler.Position == ExitPosition;

}
