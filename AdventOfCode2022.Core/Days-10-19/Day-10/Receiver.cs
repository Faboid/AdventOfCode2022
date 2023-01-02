namespace AdventOfCode2022.Core.Days_10_19.Day_10;

public class Receiver {

    public event Action<int, int>? CycleChanged;

    private int _cycle = 0;
    public int Cycle {
        get { return _cycle; }
        set {
            _cycle = value;
            CycleChanged?.Invoke(_cycle, X);
        }
    }

    public int X { get; private set; } = 1;

    private readonly List<Counter> _counters = new();

    public Receiver(IEnumerable<string> instructions) {

        int expectedCycle = 0;
        foreach(var instruction in instructions) {
            expectedCycle++;

            if(instruction is "noop") {
                continue;
            }

            var split = instruction.Split(" ");
            if(split.Length == 2 && split[0] is "addx") {
                expectedCycle++;
                var value = int.Parse(split[1]);
                _counters.Add(new Counter(expectedCycle, value));
            }
        }

    }

    public void ExecuteCycles(int amount) {
        for(int i = 0; i < amount; i++) {
            ExecuteCycle();
        }
    }

    public void ExecuteCycle() {

        Cycle++;
        _counters.ForEach(x => x.Cycle());

        var expired = _counters.Where(x => x.Expired()).ToList();
        foreach(var exp in expired) {
            X += exp.Value;
            _counters.Remove(exp);
        }
    }

}