using System.Collections;

namespace AdventOfCode2022.Core.Days_10_19.Day_12;

public class Map : IEnumerable<Tile> {

    public Map(Tile[][] map) {
        _grid = map;
        var squashed = map.SelectMany(x => x);
        DeepestHeight = squashed.Min(x => x.Height);
        TallestHeight = squashed.Max(x => x.Height);
        MaxY = map.Length;
        MaxX = map[0].Length;
    }

    public static Map Parse(string input) {
        var grid = input.Split("\r\n").Select(x => x.ToCharArray());
        var heightGrid = grid.Select((row, y) => row.Select((tile, x) => new Tile(x, y, tile)));
        var map = heightGrid.Select(x => x.ToArray()).ToArray();
        return new(map);
    }

    private readonly Tile[][] _grid;

    public int DeepestHeight { get; }
    public int TallestHeight { get; }

    public Position Start { get; }
    public Position End { get; }

    public int MaxX { get; }
    public int MaxY { get; }

    public IEnumerable<Tile> GetRow(int y) => _grid[y];

    public IEnumerator<Tile> GetEnumerator() {
        return _grid.SelectMany(x => x).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public Tile this[int x, int y] => _grid[y][x];
    public Tile this[Position pos] => _grid[pos.Y][pos.X];

}
