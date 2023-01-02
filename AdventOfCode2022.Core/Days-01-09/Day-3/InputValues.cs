namespace AdventOfCode2022.Core.Day_3;

public class InputValues {

    public const string TestInput = "vJrwpWtwJgWrhcsFMMfFFhFp\r\njqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\r\nPmmdzqPrVvPwwTWBwg\r\nwMqvLMZHhHMvwLHjbvcjnnSBnvTQFn\r\nttgJtRGJQctTZtZT\r\nCrZsJsPPZsGzwwsLwLmpwMDw";
    public static string Input => InputGetter.GetYear2022Day(3);
    public static readonly IReadOnlyCollection<string> Rucksacks = Input.Split("\r\n");

}
