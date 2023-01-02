namespace AdventOfCode2022.Core.Days_20_25.Day_20;

public class GrovePositioningSystem : IProblem {

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
        var numbers = input.Split("\r\n").Select(long.Parse).ToArray();
        var collection = new DoubleLinkedCollection(numbers, 1);
        long count = 0;
        count += collection.GetByIndex(1000);
        count += collection.GetByIndex(2000);
        count += collection.GetByIndex(3000);

        return count.ToString();
    }

    public string SolveSecond(string input) {
        var numbers = input.Split("\r\n").Select(long.Parse).Select(x => x * InputValues.Key).ToArray();
        var collection = new DoubleLinkedCollection(numbers, 10);

        long count = 0;
        count += collection.GetByIndex(1000);
        count += collection.GetByIndex(2000);
        count += collection.GetByIndex(3000);

        return count.ToString();
    }
}