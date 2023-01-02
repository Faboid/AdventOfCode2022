namespace AdventOfCode2022.Core.Days_10_19.Day_17;

public class PyroclasticFlow : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {
        var input = InputValues.Input;
        return SolveFirst(input);
    }

    public string SolveSecond() {
        var input = InputValues.Input;
        return SolveSecond(input);
    }

    public string SolveFirst(string input) {

        var rocks = InputValues.Rocks.SplitDoubleReturn();
        var rockGenerator = RockFactory.Parse(rocks);
        var chamber = new Chamber(7);
        var chamberGrid = new ChamberGrid(7);
        var jetGenerator = new JetGenerator(input);

        var simulator = new Simulator(chamber, rockGenerator, jetGenerator);

        var rocksTotal = 2022;
        for(int i = 0; i < rocksTotal; i++) {
            simulator.SimulateNextRock(chamberGrid);
        }

        var towerHeight = chamberGrid.GetTowerHeight();
        return towerHeight.ToString();

    }

    public string SolveSecond(string input) {

        var rocks = InputValues.Rocks.SplitDoubleReturn();
        var rockGenerator = RockFactory.Parse(rocks);
        var chamber = new Chamber(7);
        var chamberGrid = new ChamberGrid(7);
        var jetGenerator = new JetGenerator(input);
        var simulator = new Simulator(chamber, rockGenerator, jetGenerator);
        var cycleFinder = new CycleFinder(rockGenerator, chamber, simulator, jetGenerator);

        var cycle = cycleFinder.SearchCycle(chamberGrid);

        var rocksTotal = 1000000000000;
        var rocksRemaining = rocksTotal - (cycle.RocksPerCycle + cycle.StartingCycleSetup); //subtract setting up + first cycle

        var cyclesInRemaining = rocksRemaining / cycle.RocksPerCycle;
        rocksRemaining = rocksRemaining % cycle.RocksPerCycle;
        long calculatedHeight = cyclesInRemaining * cycle.HeightPerCycle;

        for(int i = 0; i < rocksRemaining; i++) {
            simulator.SimulateNextRock(chamberGrid);
        }

        var towerHeight = chamberGrid.GetTowerHeight() + calculatedHeight;
        return towerHeight.ToString();

    }
}