namespace AdventOfCode2022.Core.Days_20_25.Day_21;

public class Monkey {

    public Monkey(string id, string yell) {
        Id = id;
        Yell = yell;
    }

    public string Id { get; init; }
    public string Yell { get; set; }

    public static Monkey Parse(string input) {

        var split = input.Split(": ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return new Monkey(split[0], split[1]);

    }

}