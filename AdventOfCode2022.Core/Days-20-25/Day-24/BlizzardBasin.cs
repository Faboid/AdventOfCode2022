namespace AdventOfCode2022.Core.Days_20_25.Day_24;

public class BlizzardBasin : IProblem {

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
        var simulation = Simulation.Parse(input);
        var minSteps = simulation.CalculateStepsToExit();
        return minSteps.ToString();
    }

    public string SolveSecond(string input) {
        var simulation = Simulation.Parse(input);
        var totalSteps = simulation.CalculateBackAndForth();
        return totalSteps.ToString();
    }
}