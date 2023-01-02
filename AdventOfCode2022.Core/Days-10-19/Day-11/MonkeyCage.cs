namespace AdventOfCode2022.Core.Days_10_19.Day_11;

public class MonkeyCage {

    private readonly Dictionary<int, Monkey> _monkeys = new();
    public long MonkeyBusiness => _monkeys.Values.Select(x => x.InspectedItems).OrderByDescending(x => x).Take(2).Aggregate((long)1, (curr, x) => curr *= x);

    public Monkey GetMonkey(int id) {
        return _monkeys[id];
    }

    public void AddMonkey(Monkey monkey) {
        _monkeys.Add(monkey.Id, monkey);
    }

}
