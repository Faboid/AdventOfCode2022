namespace AdventOfCode2022.Core.Day_1;

public class CalorieCounting : IProblem {
    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {
        return SolveFirst(RealInput);
    }

    public string SolveSecond() {
        return SolveSecond(RealInput);
    }

    public string SolveFirst(string input) {
        var splitInput = input.Split("\r\n\r\n");
        var elves = splitInput.Select(x => x.Split("\r\n").Select(x => int.Parse(x)).Sum()).Select(x => new Elf(x));
        var top = elves.Max(x => x.Calories);
        return top.ToString();
    }

    public string SolveSecond(string input) {
        var splitInput = input.Split("\r\n\r\n");
        var elves = splitInput.Select(x => x.Split("\r\n").Select(x => int.Parse(x)).Sum()).Select(x => new Elf(x));
        var sumOfBestThree = elves.OrderByDescending(x => x.Calories).Select(x => x.Calories).Take(3).Sum();
        return sumOfBestThree.ToString();
    }

}