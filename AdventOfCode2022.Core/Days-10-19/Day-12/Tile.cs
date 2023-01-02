namespace AdventOfCode2022.Core.Days_10_19.Day_12;

public readonly struct Tile {

    public Tile(int x, int y, int height) {
        Position = new(x, y);
        Height = height;
    }

    public Tile(int x, int y, char height) {
        Position = new(x, y);
        Height = height switch {
            'S' => 1,
            'E' => 'z' - 'a' + 1,
            _ => height - 'a' + 1
        };
    }

    public int X => Position.X;
    public int Y => Position.Y;

    public Position Position { get; init; }
    public int Height { get; init; }

}
