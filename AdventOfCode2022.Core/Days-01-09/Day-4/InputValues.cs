namespace AdventOfCode2022.Core.Day_4;

public class InputValues {

    public const string TestInput = "2-4,6-8\r\n2-3,4-5\r\n5-7,7-9\r\n2-8,3-7\r\n6-6,4-6\r\n2-6,4-8";
    public static string Input => InputGetter.GetYear2022Day(4);

    public static readonly IReadOnlyCollection<Pair> Pairs = Input.Split("\r\n").Select(x => Pair.Parse(x)).ToArray();

}