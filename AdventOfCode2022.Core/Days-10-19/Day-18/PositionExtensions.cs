namespace AdventOfCode2022.Core.Days_10_19.Day_18;

public static class PositionExtensions {

    public static IEnumerable<ReadOnlyPositionV3<int>> EnumerateAdjacent(this ReadOnlyPositionV3<int> pos) {
        yield return new(pos.X + 1, pos.Y, pos.Z);
        yield return new(pos.X - 1, pos.Y, pos.Z);
        yield return new(pos.X, pos.Y + 1, pos.Z);
        yield return new(pos.X, pos.Y - 1, pos.Z);
        yield return new(pos.X, pos.Y, pos.Z + 1);
        yield return new(pos.X, pos.Y, pos.Z - 1);
    }

    public static IEnumerable<ReadOnlyPositionV3<int>> EnumerateDiagonalAdjacent(this ReadOnlyPositionV3<int> pos) {

        for(int z = -1; z <= 1; z++) {
            for(int y = -1; y <= 1; y++) {
                for(int x = -1; x <= 1; x++) {

                    if(z is 0 && y is 0 && x is 0) {
                        continue;
                    }

                    yield return new(pos.X + x, pos.Y + y, pos.Z + z);

                }
            }
        }

    }

}
