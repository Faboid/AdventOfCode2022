namespace AdventOfCode2022.Core.Days_10_19.Day_18;

public class BoilingBoulders : IProblem {

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
        var coords = input.Split("\r\n").Select(x => x.Split(',').Select(x => int.Parse(x)).ToArray()).Select(x => new ReadOnlyPositionV3<int>(x[0], x[1], x[2]));

        var dropCalculator = new DropCalculator(coords);
        var newCount = dropCalculator
            .EnumerateLava()
            .Select(x => x.EnumerateAdjacent().Where(x => dropCalculator.IsWater(x) || dropCalculator.IsAir(x)).Count())
            .Sum();

        return newCount.ToString();
    }

    public string SolveSecond(string input) {
        var coords = input.Split("\r\n").Select(x => x.Split(',').Select(x => int.Parse(x)).ToArray()).Select(x => new ReadOnlyPositionV3<int>(x[0], x[1], x[2]));
        var dropCalculator = new DropCalculator(coords);
        var lavaToWater = dropCalculator.EnumerateLava().Select(dropCalculator.CountNearWater).Sum();
        return lavaToWater.ToString();
    }
}