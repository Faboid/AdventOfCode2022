namespace AdventOfCode2022.Core.Days_20_25.Day_20;

public static class Debug {

    public static bool IsDebugEnabled { get; set; } = false;

    public static void WriteLine(string line) {
        if (IsDebugEnabled) {
            Console.WriteLine(line);
        }
    }

}
