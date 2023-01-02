namespace AdventOfCode2022.Core.Day_2;

public class InputValues {

    public const string TestInput = "A Y\r\nB X\r\nC Z";
    public static string Input => InputGetter.GetYear2022Day(2);
    public static readonly IReadOnlyCollection<string> Inputs = Input.Split("\r\n");

}