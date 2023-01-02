using System.Text;

namespace AdventOfCode2022.Core.Days_10_19.Day_17;

public class CycleFinder {

    private readonly RockFactory _rockFactory;
    private readonly Chamber _chamber;
    private readonly Simulator _simulator;
    private readonly JetGenerator _jetGenerator;

    public CycleFinder(RockFactory rockFactory, Chamber chamber, Simulator simulator, JetGenerator jetGenerator) {
        _rockFactory = rockFactory;
        _chamber = chamber;
        _simulator = simulator;
        _jetGenerator = jetGenerator;
    }

    public record Cycle(long HeightPerCycle, long RocksPerCycle, long StartingCycleSetup);

    public Cycle SearchCycle() {

        Dictionary<string, (long, long)> steps = new();
        (long Height, long Cycles) output;

        long count = 0;
        while(!GetOrAdd(steps, BuildStep(GetDiff(_chamber.TopRocks)), _chamber.GetTowerHeight(), count, out output)) {
            _simulator.SimulateNextRock();
            count++;
        }

        var startingCycles = output.Cycles;
        var rocksPerCycle = count - output.Cycles;
        var heightPerCycle = _chamber.GetTowerHeight() - output.Height;

        return new Cycle(heightPerCycle, rocksPerCycle, startingCycles);

    }

    public Cycle SearchCycle(ChamberGrid chamberGrid) {

        Dictionary<string, (long, long)> steps = new();
        (long Height, long Cycles) output;

        long count = 0;
        while(!GetOrAdd(steps, BuildStep(chamberGrid.GetUpperCycleString()), chamberGrid.GetTowerHeight(), count, out output)) {
            _simulator.SimulateNextRock(chamberGrid);
            count++;
        }

        var startingCycles = output.Cycles;
        var rocksPerCycle = count - output.Cycles;
        var heightPerCycle = chamberGrid.GetTowerHeight() - output.Height;

        return new Cycle(heightPerCycle, rocksPerCycle, startingCycles);

    }

    private static long[] GetDiff(long[] heights) {
        var min = heights.Min();
        return heights.Select(x => x - min).ToArray();
    }

    private static bool GetOrAdd(Dictionary<string, (long, long)> dict, string key, long height, long cycles, out (long, long) output) {

        if(dict.TryGetValue(key, out output)) {
            return true;
        }

        dict.Add(key, (height, cycles));
        return false;

    }

    private string BuildStep(string cycleAppearance) {
        return $"{cycleAppearance}, {_jetGenerator.GetCurrentIndex()}, {_rockFactory.GetCurrentRotationNumber()}";
    }

    private string BuildStep(long[] diffFromFloor) {
        return $"{diffFromFloor.Aggregate(new StringBuilder(), (a, b) => a.Append(b))}, {_jetGenerator.GetCurrentIndex()}, {_rockFactory.GetCurrentRotationNumber()}";
    }


}
