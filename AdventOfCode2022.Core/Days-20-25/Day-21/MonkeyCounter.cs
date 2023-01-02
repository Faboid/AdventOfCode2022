namespace AdventOfCode2022.Core.Days_20_25.Day_21;

public class MonkeyCounter {

    public Dictionary<string, Monkey> Monkeys = new();

    public MonkeyCounter(IEnumerable<Monkey> monkeys) {
        Monkeys = monkeys.ToDictionary(x => x.Id, x => x);
    }

    public void ChangeMonkeyYell(string monkeyId, string newYell) {
        Monkeys[monkeyId].Yell = newYell;
    }

    public int? Compare(string monkeyId) {
        var monkey = Monkeys[monkeyId];
        var split = monkey.Yell.Split(' ');
        var first = GetValue(split[0]);
        var second = GetValue(split[2]);

        //if(first < 0) {
        //    return null; //assume an issue with / has happened
        //}

        return first.CompareTo(second);
    }

    public bool CheckEquality(string monkeyId) {

        var monkey = Monkeys[monkeyId];
        var split = monkey.Yell.Split(' ');
        var first = GetValue(split[0]);
        var second = GetValue(split[2]);
        return first == second;
    }

    public decimal GetValue(string monkeyId) {

        var monkey = Monkeys[monkeyId];

        if(decimal.TryParse(monkey.Yell, out var result)) {
            return result;
        }

        var split = monkey.Yell.Split(' ');
        var first = GetValue(split[0]);
        var op = split[1];
        var second = GetValue(split[2]);

        return op switch {
            "+" => first + second,
            "-" => first - second,
            "*" => first * second,
            "/" => (first / second),
            _ => throw new ArgumentException(nameof(op))
        };

    }

    public bool CheckEqualityBigInteger(string monkeyId) {

        var monkey = Monkeys[monkeyId];
        var split = monkey.Yell.Split(' ');
        var first = GetValueBigInteger(split[0]);
        var second = GetValueBigInteger(split[2]);
        return first == second;
    }

    public decimal GetValueBigInteger(string monkeyId) {

        var monkey = Monkeys[monkeyId];

        if(decimal.TryParse(monkey.Yell, out var result)) {
            return result;
        }

        var split = monkey.Yell.Split(' ');
        var first = GetValueBigInteger(split[0]);
        var op = split[1];
        var second = GetValueBigInteger(split[2]);

        return op switch {
            "+" => first + second,
            "-" => first - second,
            "*" => first * second,
            "/" => (first / second),
            _ => throw new ArgumentException(nameof(op))
        };

    }

}
