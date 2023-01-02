namespace AdventOfCode2022.Core.Days_10_19.Day_11;

public class MonkeyInTheMiddle : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {
        return SolveFirst(InputValues.Input);
    }

    public string SolveSecond() {
        return SolveSecond(InputValues.Input);
    }

    public string SolveFirst(string input) {

        Id.ResetCount();
        var monkeyCage = new MonkeyCage();
        var monkeys = GetMonkeys(monkeyCage, input);
        monkeys.ForEach(monkeyCage.AddMonkey);

        Item.CalmAfterInspection = true;

        for(int i = 0; i < 20; i++) {
            foreach(var monkey in monkeys) {
                monkey.ThrowAll();
            }
        }

        var output = monkeyCage.MonkeyBusiness;
        return output.ToString();

    }

    public string SolveSecond(string input) {

        Id.ResetCount();
        var monkeyCage = new MonkeyCage();
        var monkeys = GetMonkeys(monkeyCage, input);
        monkeys.ForEach(monkeyCage.AddMonkey);
        Item.CalmAfterInspection = false;

        for(int i = 0; i < 10000; i++) {
            foreach(var monkey in monkeys) {
                monkey.ThrowAll();
            }
        }

        var output = monkeyCage.MonkeyBusiness;
        return output.ToString();

    }

    private static List<Monkey> GetMonkeys(MonkeyCage monkeyCage, string input) => input.SplitDoubleReturn().Select(x => Monkey.Parse(x, monkeyCage)).ToList();

}