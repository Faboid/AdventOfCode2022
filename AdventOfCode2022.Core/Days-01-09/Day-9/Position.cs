using System.Diagnostics;

namespace AdventOfCode2022.Core.Day_9;

[DebuggerDisplay("x: {X}, y: {Y}")]
public class Position {
    public Position(int x, int y) {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public Position Copy() => new(X, Y);

    public override bool Equals(object? obj) {
        return obj is Position pos && pos.X == X && pos.Y == Y;
    }

    public override int GetHashCode() {
        var hash = 23;
        hash *= 31 + X.GetHashCode();
        hash *= 31 + Y.GetHashCode();
        return hash;
    }
}
