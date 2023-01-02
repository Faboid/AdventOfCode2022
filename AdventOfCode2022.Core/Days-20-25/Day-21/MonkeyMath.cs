namespace AdventOfCode2022.Core.Days_20_25.Day_21;

public class MonkeyMath : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {

        var input = InputValues.Input;
        return SolveFirst(input);
        
    }

    public string SolveSecond() {

        var input = InputValues.Input;
        return SolveSecond(input);

    }

    public string SolveFirst(string input) {
        var monkeys = input.Split("\r\n").Select(Monkey.Parse);
        var counter = new MonkeyCounter(monkeys);
        var rootValue = counter.GetValue("root");
        return rootValue.ToString();
    }

    public string SolveSecond(string input) {
        var monkeys = input.Split("\r\n").Select(Monkey.Parse);
        var counter = new MonkeyCounter(monkeys);

        long min = 0;
        long max = long.MaxValue;
        long curr = ((max - min) / 2) + min;
        while(true) {

            counter.ChangeMonkeyYell("humn", curr.ToString());

            int? result = counter.Compare("root");
            if(result == null) {
                curr = GetMid(curr, max);
                continue;
            }

            if(result == 0) {
                break;
            }

            if(result < 0) {
                max = curr - 1;
                curr = GetMid(min, max);
            } else {
                min = curr + 1;
                curr = GetMid(min, max);
            }

            if(curr == 34) {
                curr++;
            }

        }

        return curr.ToString();
    }

    private long GetMid(long min, long max) => ((max - min) / 2) + min;
    private int GetMid(int min, int max) => ((max - min) / 2) + min;

}
