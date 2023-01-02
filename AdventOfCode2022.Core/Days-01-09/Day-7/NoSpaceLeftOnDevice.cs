namespace AdventOfCode2022.Core.Day_7;

public class NoSpaceLeftOnDevice : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {
        return SolveFirst(InputValues.Input);
    }

    public string SolveSecond() {
        return SolveSecond(InputValues.Input);
    }

    public string SolveFirst(string input) {
        var parser = new Parser();
        parser.Parse(input.SplitReturn());

        var output = parser.FileSystem.GetRoot().EnumerateDirectories().Select(x => x.GetSize()).Where(x => x <= 100000).Sum();
        return output.ToString();
    }

    public string SolveSecond(string input) {

        var parser = new Parser();
        parser.Parse(input.SplitReturn());
        var directories = parser.FileSystem.GetRoot().EnumerateDirectories();
        var totalOccupiedSize = parser.FileSystem.GetRoot().GetSize();

        var requiredMinDirSize = totalOccupiedSize - (InputValues.MaxFileSystemSize - InputValues.RequiredSize);

        var dirSizes = directories.Select(x => x.GetSize());
        var output = dirSizes.Where(x => x >= requiredMinDirSize).Min();
        return output.ToString();

    }
}
