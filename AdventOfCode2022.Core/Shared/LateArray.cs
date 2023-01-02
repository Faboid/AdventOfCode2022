using AdventOfCode2022.Core.Days_10_19.Day_14;
using System.Collections;

namespace AdventOfCode2022.Core.Shared;

public class LateArray<T> : IEnumerable<T> {

    private readonly T[] _values;
    private readonly ReadOnlyDowngrader _downgrader;

    public LateArray(int startIndex, int length) {
        _downgrader = new(startIndex);
        _values = new T[length];
    }

    public bool HasIndex(int index) {
        var val = _downgrader.Evaluate(index);
        return val >= 0 && val < _values.Length;
    }

    public T this[int index] {
        get => _values[_downgrader.Evaluate(index)];
        set => _values[_downgrader.Evaluate(index)] = value;
    }

    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_values).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _values.GetEnumerator();
}

public class MultiLateArray<T> : IEnumerable<T> {

    private readonly T[ , , ] _values;
    private readonly ReadOnlyDowngrader _downgraderX;
    private readonly ReadOnlyDowngrader _downgraderY;
    private readonly ReadOnlyDowngrader _downgraderZ;
    public int XLength { get; init; }
    public int YLength { get; init; }
    public int ZLength { get; init; }

    public MultiLateArray(ReadOnlyPositionV3<int> startIndex, ReadOnlyPositionV3<int> length) {
        _downgraderX = new(startIndex.X);
        _downgraderY = new(startIndex.Y);
        _downgraderZ = new(startIndex.Z);
        XLength = length.X;
        YLength = length.Y;
        ZLength = length.Z;
        _values = new T[XLength, YLength, ZLength];
    }

    public MultiLateArray(ReadOnlyPositionV3<int> startIndex, ReadOnlyPositionV3<int> length, T fill) : this(startIndex, length) {
        Fill(fill);
    }

    public T this[ReadOnlyPositionV3<int> index] {
        get => this[index.X, index.Y, index.Z];
        set => this[index.X, index.Y, index.Z] = value;
    }

    public T this[int x, int y, int z] {
        get => _values[_downgraderX.Evaluate(x), _downgraderY.Evaluate(y), _downgraderZ.Evaluate(z)];
        set => _values[_downgraderX.Evaluate(x), _downgraderY.Evaluate(y), _downgraderZ.Evaluate(z)] = value;
    }

    public void Fill(T value) {
        for(int x = 0; x < XLength; x++) {
            for(int y = 0; y < YLength; y++) {
                for(int z = 0; z < ZLength; z++) {
                    _values[x, y, z] = value;
                }
            }
        }
    }

    public bool HasPosition(ReadOnlyPositionV3<int> position) {
        var x = _downgraderX.Evaluate(position.X);
        var y = _downgraderX.Evaluate(position.Y);
        var z = _downgraderX.Evaluate(position.Z);

        var xValid = x >= 0 && x < XLength;
        var yValid = y >= 0 && y < YLength;
        var zValid = z >= 0 && z < ZLength;

        return xValid && yValid && zValid;
    }

    public IEnumerator<T> GetEnumerator() {
        for(int x = 0; x < XLength; x++) {
            for(int y = 0; y < YLength; y++) {
                for(int z = 0; z < ZLength; z++) {
                    yield return _values[x, y, z];
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}