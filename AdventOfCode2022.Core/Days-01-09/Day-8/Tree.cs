namespace AdventOfCode2022.Core.Day_8;

public record Tree(int Height, int X, int Y) {
    public bool IsVisible { get; set; } = false;
}

