namespace AdventOfCode2022.Core.Days_10_19.Day_14;

public class Coordinates {
    public Coordinates(int x, int y) {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public static Coordinates operator +(Coordinates a, Coordinates b) => new(a.X + b.X, a.Y + b.Y);

    public Coordinates Down() => new(X, Y + 1);
    public Coordinates DownRight() => new(X + 1, Y + 1);
    public Coordinates DownLeft() => new(X - 1, Y + 1);

}