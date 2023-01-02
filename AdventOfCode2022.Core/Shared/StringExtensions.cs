namespace AdventOfCode2022.Core.Shared;

public static class StringExtensions {

    public static string[] SplitReturn(this string input) => input.Split("\r\n");
    public static string[] SplitDoubleReturn(this string input) => input.Split("\r\n\r\n");

}

