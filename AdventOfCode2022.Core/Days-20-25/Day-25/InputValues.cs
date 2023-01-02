namespace AdventOfCode2022.Core.Days_20_25.Day_25;

public class InputValues {

    public const string TestInput = "1=-0-2\r\n12111\r\n2=0=\r\n21\r\n2=01\r\n111\r\n20012\r\n112\r\n1=-1=\r\n1-12\r\n12\r\n1=\r\n122";
    public static string Input => InputGetter.GetYear2022Day(25);
    
}

public class FullOfHotAir : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {

        var input = InputValues.Input;
        return SolveFirst(input);

    }

    public string SolveSecond() {
        return "Success!";
    }

    public string SolveFirst(string input) {
        var snafus = input.Split("\r\n").Select(x => new SnafuNumber(x)).ToArray();
        var sum = snafus.Sum(x => x.AsLong);
        Console.WriteLine($"Numeric : {sum}");

        var snafu = new SnafuNumber(sum);
        return snafu.AsString;
    }

    public string SolveSecond(string input) {
        return "Success!";
    }
}