namespace AdventOfCode2022.Core.Day_2;

public class RockPaperScissors : IProblem {
    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {
        return SolveFirst(InputValues.Input);
    }

    public string SolveSecond() {
        return SolveSecond(InputValues.Input);
    }

    public string SolveFirst(string input) {
        var matches = input.SplitReturn().Select(x => new Match(x));
        var myTotal = matches.Sum(x => x.Points().B);
        return myTotal.ToString();
    }

    public string SolveSecond(string input) {
        var split = input.SplitReturn().Select(x => x.Split(' ')).Select(x => (opponentChoice: x[0], matchResult: x[1]));
        var result = split.Select(x => new MatchEvaluator(x.opponentChoice, x.matchResult)).Sum(x => x.Evaluate());
        return result.ToString();
    }
}