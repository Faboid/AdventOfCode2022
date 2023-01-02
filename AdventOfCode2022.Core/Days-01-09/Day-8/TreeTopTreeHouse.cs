namespace AdventOfCode2022.Core.Day_8;

public class TreeTopTreeHouse : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {
        return SolveFirst(InputValues.Input);
    }

    public string SolveSecond() {
        return SolveSecond(InputValues.Input);
    }

    public string SolveFirst(string input) {

        var forest = Forest.Parse(input);
        var output = forest.Trees().Where(x => x.IsVisible).Count();
        return output.ToString();
    }

    public string SolveSecond(string input) {

        var forest = Forest.Parse(input);
        var output = forest.Trees().Max(tree => forest.TreeScenicScore(tree.X, tree.Y));
        return output.ToString();
    }

}