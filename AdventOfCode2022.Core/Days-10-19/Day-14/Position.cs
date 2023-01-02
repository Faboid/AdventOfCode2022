namespace AdventOfCode2022.Core.Days_10_19.Day_14;

public readonly struct Position {
    public Position(Coordinates coords) {
        _coords = coords;
    }

    private readonly Coordinates _coords;
    public int X => _coords.X;
    public int Y => _coords.Y;

    public static bool operator ==(Position a, Position b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Position a, Position b) => !(a == b);

    public static implicit operator Position(Coordinates coords) => new(coords);
    public static implicit operator Coordinates(Position position) => new(position.X, position.Y);
}
