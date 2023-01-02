namespace AdventOfCode2022.Core.Day_6;

public class TuningTrouble : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {
        return SolveFirst(InputValues.Input);
    }


    public string SolveSecond() {
        return SolveSecond(InputValues.Input);
    }

    public string SolveFirst(string input) {
        var output = Tuner.FindMarkerIndex(input, 4);
        return output.ToString();
    }

    public string SolveSecond(string input) {
        var output = Tuner.FindMarkerIndex(input, 14);
        return output.ToString();
    }
}