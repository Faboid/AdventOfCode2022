namespace AdventOfCode2022.Core.Days_10_19.Day_10;

public class CathodeRayTube : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {
        return SolveFirst(InputValues.Input);
    }

    public string SolveSecond() {
        return SolveSecond(InputValues.Input);
    }

    public string SolveFirst(string input) {
        var receiver = new Receiver(input.SplitReturn());

        var signalCounter = new SignalCounter(receiver);
        signalCounter
            .SubscribeToCycle(20)
            .SubscribeToCycle(60)
            .SubscribeToCycle(100)
            .SubscribeToCycle(140)
            .SubscribeToCycle(180)
            .SubscribeToCycle(220);

        receiver.ExecuteCycles(220);

        var output = signalCounter.Count;
        return output.ToString();
    }

    public string SolveSecond(string input) {
        var receiver = new Receiver(input.SplitReturn());
        var display = new Display(receiver, 240);
        receiver.ExecuteCycles(240);

        return display.ToString();
    }
}
