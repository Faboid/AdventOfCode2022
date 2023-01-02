using System.Text;

namespace AdventOfCode2022.Core.Days_10_19.Day_14;

public class CaveArray {

    private readonly LateArray<LateArray<CaveSign>> _grid;

    public CaveArray(int minX, int minY, int xLength, int yLength) {
        _grid = new(minY, yLength);
        for(int y = 0; y < yLength; y++) {
            _grid[y] = new LateArray<CaveSign>(minX, xLength);
            for(int x = minX; x < xLength + minX; x++) {
                _grid[y][x] = CaveSign.Empty;
            }
        }
    }

    public bool HasIndex(Position position) => _grid.HasIndex(position.Y) && _grid[position.Y].HasIndex(position.X);

    public LateArray<CaveSign> this[int index] {
        get => _grid[index];
        set => _grid[index] = value;
    }

    public CaveSign this[Position position] {
        get => _grid[position.Y][position.X];
        set => _grid[position.Y][position.X] = value;
    }

    public override string ToString() {
        StringBuilder sb = new();

        foreach(var row in _grid) {
            foreach(var cell in row) {
                sb.Append((char)cell);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
