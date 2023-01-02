using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2022.Core.Days_10_19.Day_12;

public readonly struct Position {
    public Position(int x, int y) {
        X = x;
        Y = y;
    }

    public int X { get; init; }
    public int Y { get; init; }

    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Position pos && pos.X == X && pos.Y == Y;

    public static bool operator ==(Position left, Position right) {
        return left.Equals(right);
    }

    public static bool operator !=(Position left, Position right) {
        return !(left == right);
    }

    public override int GetHashCode() {
        return ((X + Y) * Y) + ((Y - X) * X);
    }
}
