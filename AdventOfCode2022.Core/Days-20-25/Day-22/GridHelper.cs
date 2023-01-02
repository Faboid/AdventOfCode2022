namespace AdventOfCode2022.Core.Days_20_25.Day_22;

public static class GridHelper {

    public static IEnumerable<ReadOnlyPosition> Enumerate(Range xRange, Range yRange) {

        for(int y = yRange.Start.Value; y <= (yRange.End.Value + 1); y++) {
            for(int x = xRange.Start.Value; x <= xRange.End.Value + 1; x++) {
                yield return new(x, y);
            }
        }

    }

}