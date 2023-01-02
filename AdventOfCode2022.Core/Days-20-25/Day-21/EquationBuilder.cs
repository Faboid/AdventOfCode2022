namespace AdventOfCode2022.Core.Days_20_25.Day_21;

public class EquationBuilder {

    public Dictionary<string, Monkey> Monkeys = new();
    public EquationBuilder(IEnumerable<Monkey> monkeys) {
        Monkeys = monkeys.ToDictionary(x => x.Id, x => x);
    }

    //public string BuildInverted(string fromId, string toId) {



    //}

    public string BuildEquation(string monkeyId) {

        var monkey = Monkeys[monkeyId];

        if(int.TryParse(monkey.Yell, out var _)) {
            return monkey.Yell;
        }

        var split = monkey.Yell.Split(' ');
        var first = split[0];
        var op = split[1];
        var second = split[2];

        return $"({BuildEquation(first)} {op} {BuildEquation(second)})";
    }


}
