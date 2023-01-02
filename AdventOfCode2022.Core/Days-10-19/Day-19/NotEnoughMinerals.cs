namespace AdventOfCode2022.Core.Days_10_19.Day_19;

public class NotEnoughMinerals : IProblem {

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
        var blueprints = input.Split("\r\n").Select(Blueprint.Parse).ToArray();
        var calculators = blueprints.Select(x => new BlueprintCalculator(x, 24)).ToArray();
        var count = calculators.AsParallel().Select(x => x.GetQualityLevel()).Sum();
        return count.ToString();
    }

    public string SolveSecond(string input) {
        var blueprints = input.Split("\r\n").Take(3).Select(Blueprint.Parse).ToArray();
        var calculators = blueprints.Select(x => new BlueprintCalculator(x, 32)).ToArray();

        var n = calculators.Last().GetMaxGeodes();
        Console.WriteLine(n);

        var count = 1;
        foreach(var calculator in calculators) {
            var geodes = calculator.GetMaxGeodes();
            Console.WriteLine($"Geodes: {geodes}");
            count *= geodes;
            Console.WriteLine($"Count: {count}");
        }

        return count.ToString();
    }
}