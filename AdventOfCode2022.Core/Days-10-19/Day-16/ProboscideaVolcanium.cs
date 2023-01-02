namespace AdventOfCode2022.Core.Days_10_19.Day_16;

public class ProboscideaVolcanium : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {
        return SolveFirst(InputValues.Input);
    }

    //2207
    public string SolveSecond() {
        return SolveSecond(InputValues.Input);
    }

    public string SolveFirst(string input) {
        var solver = ParseSolver(input);
        var maxPressure = solver.Find(30, "", "AA", 0);

        //Console.WriteLine("If test, should be 1651");
        var output = maxPressure;
        return output.ToString();
    }

    public string SolveSecond(string input) {
        var solver = ParseSolver(input);
        var maxPressure = solver.FindMany(26, "", new(26, "AA"), new(26, "AA"), 0);
        var output = maxPressure;
        return output.ToString();
    }

    private static Solver ParseSolver(string input) {
        var valves = input.Split("\r\n").Select(Valve.Parse).ToArray();
        var network = new ValveNetwork(valves);
        return new Solver(network);
    }

}