namespace AdventOfCode2022.Core.Day_4;

public class CampCleanup : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {
        return SolveFirst(InputValues.Input);
    }

    public string SolveSecond() {
        return SolveSecond(InputValues.Input);
    }

    public string SolveFirst(string input) {
        var output = SplitInput(input).Where(x => x.OneFullyContains).Count();
        return output.ToString();
    }

    public string SolveSecond(string input) {
        var output = SplitInput(input).Where(x => x.HasOverlap).Count();
        return output.ToString();
    }

    private Pair[] SplitInput(string input) => input.SplitReturn().Select(Pair.Parse).ToArray();

}

