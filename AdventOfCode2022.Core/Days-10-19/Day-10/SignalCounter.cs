namespace AdventOfCode2022.Core.Days_10_19.Day_10;

public class SignalCounter {

    private readonly Receiver _receiver;
    private int _count = 0;
    public int Count => _count;

    private List<int> ImportantCycles = new();

    public SignalCounter SubscribeToCycle(int cycle) {
        ImportantCycles.Add(cycle);
        return this;
    }

    public SignalCounter(Receiver receiver) {
        _receiver = receiver;
        _receiver.CycleChanged += CountCycle;
    }

    private void CountCycle(int cycle, int value) {
        if(ImportantCycles.Contains(cycle)) {
            _count += (value * cycle);
        }
    }

}
