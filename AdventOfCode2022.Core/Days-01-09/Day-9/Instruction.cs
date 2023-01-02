namespace AdventOfCode2022.Core.Day_9;

public record Instruction(Direction Direction, int Steps) {

    public static Instruction Parse(string input) {
        var split = input.Split(" ");

        var direction = split[0] switch {
            "L" => Direction.Left,
            "R" => Direction.Right,
            "D" => Direction.Down,
            "U" => Direction.Up,
            _ => throw new ArgumentException()
        };

        var steps = int.Parse(split[1]);

        return new Instruction(direction, steps);
    }

}
