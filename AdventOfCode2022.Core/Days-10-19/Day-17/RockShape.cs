using AdventOfCode2022.Core.Shared;

namespace AdventOfCode2022.Core.Days_10_19.Day_17;

public class RockShape {
    
    public IReadOnlyList<ReadOnlyPosition> Positions { get; init; }

    public RockShape(IReadOnlyList<ReadOnlyPosition> positions) {
        Positions = positions;
    }

    public static RockShape Parse(string input) {

        var rows = input.Split("\r\n").Reverse().ToArray();
        var positions = new List<ReadOnlyPosition>();
        var pivot = new ReadOnlyPosition(0, 0); //pivot in the left-down corner

        for(int y = 0; y < rows.Length; y++) {
            for(int x = 0; x < rows[y].Length; x++) {

                //if empty, skip
                if(rows[y][x] == '.') {
                    continue;
                }

                positions.Add(new ReadOnlyPosition(x - pivot.X, y - pivot.Y));

            }
        }

        return new RockShape(positions);

    }

}