using System.Diagnostics;

namespace AdventOfCode2022.Core.Days_10_19.Day_10;

[DebuggerDisplay("Countdown: {Countdown}, Value: {Value}")]
public class Counter {
    public Counter(int countdown, int value) {
        Countdown = countdown;
        Value = value;
    }

    public void Cycle() => Countdown--;
    public bool Expired() => Countdown <= 0;

    public int Countdown { get; private set; }
    public int Value { get; init; }

}
