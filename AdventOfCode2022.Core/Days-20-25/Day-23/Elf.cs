using System.Diagnostics;

namespace AdventOfCode2022.Core.Days_20_25.Day_23;

[DebuggerDisplay("Position: {Position}, Proposed: {Proposed}")]
public class Elf {
    
    public Position Position { get; set; }

    public Elf(Position position) {
        Position = position;
    }

    public Position? Proposed { get; set; }
    public void SetProposed(Direction direction) {
        Proposed = direction switch {
            Direction.None => null!,
            Direction.North => new Position(Position.X, Position.Y - 1),
            Direction.South => new Position(Position.X, Position.Y + 1),
            Direction.West => new Position(Position.X - 1, Position.Y),
            Direction.East => new Position(Position.X + 1, Position.Y),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    public static IEnumerable<Elf> ParseElves(string input) {

        var split = input.Split("\r\n");

        for(int y = 0; y < split.Length; y++) {
            for(int x = 0; x < split[0].Length; x++) {
                if(split[y][x] == '#') {
                    yield return new Elf(new Position(x, y));
                }
            }
        }

    }

}
