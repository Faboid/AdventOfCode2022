namespace AdventOfCode2022.Core.Day_7;

public class Parser {

    public FileSystem FileSystem { get; init; } = new();

    public void Parse(IEnumerable<string> commandLines) {
        foreach(var line in commandLines) {
            ParseLine(line);
        }
    }

    private void ParseLine(string command) {

        var split = command.Split(' ');

        if(split[0] == "$" && split[1] == "cd") {
            FileSystem.ChangeDirectory(MergeStrings(split.Skip(2)));
            return;
        }

        if(split[0] == "$" && split[1] == "ls") {
            return; //skip line
        }

        //nested directory
        if(split[0] == "dir") {
            FileSystem.AddDirectory(MergeStrings(split.Skip(1)));
            return;
        }

        //file
        if(int.TryParse(split[0], out int fileSize)) {
            FileSystem.AddFile(MergeStrings(split.Skip(1)), fileSize);
            return;
        }

        throw new ArgumentException($"Couldn't parse the given command {command}");

    }

    private static string MergeStrings(IEnumerable<string> strings) => string.Join(" ", strings);

}
