using System.Numerics;

namespace AdventOfCode2022.Core.Shared;

public readonly struct ReadOnlyPositionV3<T> where T : INumberBase<T> {

    private readonly PositionV3<T> _position;

    public ReadOnlyPositionV3(T x, T y, T z) {
        _position = new(x, y, z);
    }

    public ReadOnlyPositionV3(PositionV3<T> position) {
        _position = position;
    }

    public T X => _position.X;
    public T Y => _position.Y;
    public T Z => _position.Z;

    public override string ToString() {
        return $"X: {X}, Y: {Y}";
    }

    public static bool operator ==(ReadOnlyPositionV3<T> a, ReadOnlyPositionV3<T> b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(ReadOnlyPositionV3<T> a, ReadOnlyPositionV3<T> b) => !(a == b);

    public static implicit operator ReadOnlyPositionV3<T>(PositionV3<T> position) => new(position);
    public static implicit operator PositionV3<T>(ReadOnlyPositionV3<T> position) => new(position.X, position.Y, position.Z);

}

public class PositionV3<T> where T : INumberBase<T> {

    public PositionV3(T x, T y, T z) {
        X = x;
        Y = y;
        Z = z;
    }

    public T X { get; set; }
    public T Y { get; set; }
    public T Z { get; set; }

    public override bool Equals(object? obj) => obj is PositionV3<T> pos && pos == this;

    public static bool operator ==(PositionV3<T> a, PositionV3<T> b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;
    public static bool operator !=(PositionV3<T> a, PositionV3<T> b) => !(a == b);

    public override string ToString() {
        return $"X: {X}, Y: {Y}, Z: {Z}";
    }
}

public readonly struct ReadOnlyPosition<T> where T: INumberBase<T> {

    private readonly Position<T> _position;

    public ReadOnlyPosition(T x, T y) {
        _position = new(x, y);
    }

    public ReadOnlyPosition(Position<T> position) {
        _position = position;
    }

    public T X => _position.X;
    public T Y => _position.Y;

    public override string ToString() {
        return $"X: {X}, Y: {Y}";
    }

    public static bool operator ==(ReadOnlyPosition<T> a, ReadOnlyPosition<T> b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(ReadOnlyPosition<T> a, ReadOnlyPosition<T> b) => !(a == b);

    public static implicit operator ReadOnlyPosition<T>(Position<T> position) => new(position);
    public static implicit operator Position<T>(ReadOnlyPosition<T> position) => new(position.X, position.Y);

}

public class Position<T> where T: INumberBase<T> {

    public Position(T x, T y) {
        X = x;
        Y = y;
    }

    public T X { get; set; }
    public T Y { get; set; }

    public override bool Equals(object? obj) => obj is Position<T> pos && pos == this;

    public static bool operator ==(Position<T> a, Position<T> b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Position<T> a, Position<T> b) => !(a == b);

    public static bool operator ==(Position<T> a, ReadOnlyPosition<T> b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Position<T> a, ReadOnlyPosition<T> b) => !(a == b);

    public override string ToString() {
        return $"X: {X}, Y: {Y}";
    }
}

public readonly struct ReadOnlyPosition {

    private readonly Position _position;

    public ReadOnlyPosition(int x, int y) {
        _position = new Position(x, y);
    }

    public ReadOnlyPosition(Position position) {
        _position = position;
    }

    public int X => _position.X;
    public int Y => _position.Y;

    public override string ToString() {
        return $"X: {X}, Y: {Y}";
    }

    public static bool operator ==(ReadOnlyPosition a, ReadOnlyPosition b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(ReadOnlyPosition a, ReadOnlyPosition b) => !(a == b);

    public static implicit operator ReadOnlyPosition(Position position) => new(position);
    public static implicit operator Position(ReadOnlyPosition position) => new(position.X, position.Y);

    /// <summary>
    /// Enumerates the four directly surrounding directions. Does not enumerate diagonal movement.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ReadOnlyPosition> EnumerateAdjacent() {
        yield return new(_position.X - 1, _position.Y);
        yield return new(_position.X + 1, _position.Y);
        yield return new(_position.X, _position.Y - 1);
        yield return new(_position.X, _position.Y + 1);
    }

    /// <summary>
    /// Enumerates all the eight surrounding directions.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ReadOnlyPosition> EnumerateAdjacentDiagonal() {
        for(int y = -1; y <= 1; y++) {
            for(int x = -1; x <= 1; x++) {
                if(y is 0 && x is 0) {
                    continue;
                }

                yield return new(_position.X + x, _position.Y + y);
            }
        }
    }

}

public class Position : IEquatable<Position> {

    public Position(int x, int y) {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public override bool Equals(object? obj) => obj is Position pos && pos == this;

    public static bool operator ==(Position a, Position b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Position a, Position b) => !(a == b);
    public static bool operator ==(Position a, ReadOnlyPosition b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Position a, ReadOnlyPosition b) => !(a == b);

    /// <summary>
    /// Enumerates the four directly surrounding directions. Does not enumerate diagonal movement.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Position> EnumerateAdjacent() {
        yield return new(X - 1, Y);
        yield return new(X + 1, Y);
        yield return new(X, Y - 1);
        yield return new(X, Y + 1);
    }

    /// <summary>
    /// Enumerates all the eight surrounding directions.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Position> EnumerateAdjacentDiagonal() {
        for(int y = -1; y <= 1; y++) {
            for(int x = -1; x <= 1; x++) {
                if(y is 0 && x is 0) {
                    continue;
                }

                yield return new(X + x, Y + y);
            }
        }
    }

    public override string ToString() {
        return $"X: {X}, Y: {Y}";
    }

    public override int GetHashCode() {
        int hash = 23;
        hash = hash * 31 + X;
        hash = hash * 31 + Y;
        return hash;
    }

    public bool Equals(Position? other) {
        return other is not null && this == other;
    }
}

