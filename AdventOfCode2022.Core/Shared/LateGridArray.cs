using System.Collections;

namespace AdventOfCode2022.Core.Shared;

public class LateGridArray<T> : IEnumerable<T> {

    private readonly LateArray<LateArray<T>> _grid;

    public LateGridArray(int minX, int minY, int xLength, int yLength) : this(minX, minY, xLength, yLength, default!) { }

    public LateGridArray(int minX, int minY, int xLength, int yLength, T startingValue) {
        _grid = new(minY, yLength);
        for(int y = minY; y < yLength + minY; y++) {
            _grid[y] = new LateArray<T>(minX, xLength);
            for(int x = minX; x < xLength + minX; x++) {
                _grid[y][x] = startingValue;
            }
        }
    }

    public bool HasRow(int y) => _grid.HasIndex(y);
    public bool HasIndex(int x, int y) => _grid.HasIndex(y) && _grid[y].HasIndex(x);
    public bool HasIndex(Position position) => _grid.HasIndex(position.Y) && _grid[position.Y].HasIndex(position.X);

    public LateArray<T> this[int index] {
        get => _grid[index];
        set => _grid[index] = value;
    }

    public T this[Position position] {
        get => _grid[position.Y][position.X];
        set => _grid[position.Y][position.X] = value;
    }

    public IEnumerable<IEnumerable<T>> GetRows() {
        return _grid;
    }

    public IEnumerator<T> GetEnumerator() {
        foreach(var row in _grid) {
            foreach(var cell in row) {
                yield return cell;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}