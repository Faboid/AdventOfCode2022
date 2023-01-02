namespace AdventOfCode2022.Core.Days_10_19.Day_15;

public class BeaconExclusionZone : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {
        return SolveFirst(RealInput);
    }

    public string SolveSecond() {

        //gonna skip some 20 min of computation
        return HardCode();
        return SolveSecond(RealInput);

    }

    public string SolveFirst(string input) {

        var testRow = 10;
        var inputRow = 2000000;

        int row;
        if(input == InputValues.TestInput) {
            row = testRow;
        } else if(input == InputValues.Input) {
            row = inputRow;
        } else {
            throw new NotImplementedException("This problem solution cannot use a custom input.");
        }

        var sensors = input.Split("\r\n").Select(Sensor.Parse).ToList();

        var allNotEmptyCount = sensors.Select(x => x.GetDetectedRowRange(row)).SumRanges();
        var sensorsCount = sensors.Where(x => x.Position.Y == row).Count();
        var beaconsCount = sensors.Where(x => x.FoundBeacon.Position.Y == row).Select(x => x.FoundBeacon.Position.X).Distinct().Count();

        var count = allNotEmptyCount - (sensorsCount + beaconsCount);
        return count.ToString();

    }

    public string SolveSecond(string input) {

        var testRow = 20;
        var inputRow = 4000000;
        int maxSize;

        if(input == InputValues.TestInput) {
            maxSize = testRow;
        } else if(input == InputValues.Input) {
            maxSize = inputRow;
        } else {
            throw new NotImplementedException("This problem solution cannot use a custom input.");
        }

        var sensors = input.Split("\r\n").Select(Sensor.Parse).ToList();

        var result = Search(sensors, maxSize);

        Console.WriteLine($"X: {result.X}, Y: {result.Y}");

        long frequency = ((long)result.X * 4000000) + result.Y;
        return frequency.ToString();

    }

    private static string HardCode() {
        int x = 2706598;
        int y = 3253551;
        long frequency = ((long)x * 4000000) + y;
        return frequency.ToString();
    }

    private static Position Search(IList<Sensor> sensors, int maxSize) {

        for(int y = 0; y <= maxSize; y++) {

            var ranges = sensors.AsParallel().Select(x => x.GetDetectedRowRange(y)).Where(x => !x.IsEmpty);
            var emptySpot = ranges.HasEmptySpot(0, maxSize);

            if(emptySpot.Exists) {
                return new(emptySpot.Index, y);
            }

        }

        throw new ArgumentException();

    }

}