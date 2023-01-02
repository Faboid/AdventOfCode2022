namespace AdventOfCode2022.Core.Day_9;

public class InputValues {
    public const string TestInput = "R 4\r\nU 4\r\nL 3\r\nD 1\r\nR 4\r\nD 1\r\nL 5\r\nR 2";
    public static string Input => InputGetter.GetYear2022Day(9);
}

public class RopeBridge : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {
        return SolveFirst(InputValues.Input);
    }

    public string SolveSecond() {
        return SolveSecond(InputValues.Input);
    }

    public string SolveFirst(string input) {
        var instructions = input.SplitReturn().Select(Instruction.Parse);
        var map = new Rope(2);
        foreach(var instruction in instructions) {
            map.Execute(instruction);
        }
        var output = map.Tails.First().VisitedCount;
        return output.ToString();
    }

    public string SolveSecond(string input) {

        var instructions = input.SplitReturn().Select(Instruction.Parse);
        var map = new Rope(10);
        foreach(var instruction in instructions) {
            map.Execute(instruction);
        }
        var output = map.Tails.Last().VisitedCount;
        return output.ToString();

    }

}