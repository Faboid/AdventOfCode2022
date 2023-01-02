using System.Diagnostics;
using System.Text;

namespace AdventOfCode2022.Core.Days_20_25.Day_22;

[DebuggerDisplay("Row {Row}, Column {Column}")]
public class Tile {
    public Tile(int row, int column, TileType tileType) {
        Row = row;
        Column = column;
        TileType = tileType;
    }

    public int Row { get; init; }
    public int Column { get; init; }

    public TileType TileType { get; init; }
    public Direction TraveledTo { get; private set; } = (Direction)99;

    public Tile? Up { get; set; }
    public Tile? Down { get; set; }
    public Tile? Right { get; set; }
    public Tile? Left { get; set; }

    public Tile Go(Direction direction) {

        var next = Next(direction);

        if(next.TileType is TileType.Wall) {
            return this; //can't move
        }

        TraveledTo = direction;
        return next;

    }

    private Tile Next(Direction direction) {
        var output = direction switch {
            Direction.Up => Up,
            Direction.Down => Down,
            Direction.Left => Left,
            Direction.Right => Right,
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };

        if(output is null) {
            throw new InvalidOperationException("There has been an error when building the tile map. The direction that's being requested is null.");
        }

        return output;
    }

    public IEnumerable<Tile> EnumerateSurrounding() {
        yield return Up ?? throw new InvalidOperationException("There has been an error when building the tile map. The direction that's being requested is null.");
        yield return Right ?? throw new InvalidOperationException("There has been an error when building the tile map. The direction that's being requested is null.");
        yield return Down ?? throw new InvalidOperationException("There has been an error when building the tile map. The direction that's being requested is null.");
        yield return Left ?? throw new InvalidOperationException("There has been an error when building the tile map. The direction that's being requested is null.");
    }

    public List<Tile> EnumerateTiles() {
        var output = new List<Tile>();
        EnumerateTiles(output);
        return output;
    }

    private void EnumerateTiles(List<Tile> visited) {

        var stack = new Stack<Tile>();
        stack.Push(this);
        while(stack.Count > 0) {

            var curr = stack.Pop();
            visited.Add(curr);

            var adjacent = curr.EnumerateSurrounding();
            foreach(var node in adjacent) {

                if(visited.Contains(node)) {
                    continue;
                }

                stack.Push(node);

            }

        }

    }

    private char DirectionToChar(Direction direction) {
        return direction switch {
            Direction.Right => '>',
            Direction.Down => 'v',
            Direction.Left => '<',
            Direction.Up => '^',
            _ => '.'
        };
    }

    public override string ToString() {

        var map = EnumerateTiles();
        var lengthX = map.Max(x => x.Column) + 1;
        var lengthY = map.Max(y => y.Row) + 1;

        char[][] grid = Enumerable.Repeat(0, lengthY).Select(x => Enumerable.Repeat(' ', lengthX).ToArray()).ToArray();

        foreach(var tile in map) {

            if(tile.TileType is TileType.Empty) {
                var value = DirectionToChar(tile.TraveledTo);
                grid[tile.Row - 1][tile.Column - 1] = value;
                continue;
            }

            grid[tile.Row - 1][tile.Column - 1] = (char)tile.TileType;
        }

        StringBuilder sb = new();
        for(int y = 0; y < grid.Length; y++) {
            for(int x = 0; x < grid[0].Length; x++) {
                sb.Append(grid[y][x]);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
