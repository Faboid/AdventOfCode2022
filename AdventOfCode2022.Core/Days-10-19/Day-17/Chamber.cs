namespace AdventOfCode2022.Core.Days_10_19.Day_17;

public class Chamber {

    public long[] TopRocks { get; }
    public int ChamberWidth { get; init; }

    public Chamber(int chamberWidth) {
        ChamberWidth = chamberWidth;
        TopRocks = new long[ChamberWidth];
    }

    public ReadOnlyPosition<long>[] GetTopRocks() => TopRocks.Select((y, i) => new ReadOnlyPosition<long>(i, y)).ToArray();
    public long GetTowerHeight() => TopRocks.Max();
    public bool HasRock(ReadOnlyPosition<long> pos) => TopRocks[pos.X] <= pos.Y;
    public void AddRock(ReadOnlyPosition<long> pos) {
        TopRocks[pos.X] = Math.Max(TopRocks[pos.X], pos.Y);
    }
    
}