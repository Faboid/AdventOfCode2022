using System.Linq;

namespace AdventOfCode2022.Core.Day_3;

public class RucksackReorganization : IProblem {
    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {
        return SolveFirst(InputValues.Input);
    }


    public string SolveSecond() {
        return SolveSecond(InputValues.Input);
    }

    public string SolveFirst(string input) {
        var output = input.SplitReturn().Select(x => new Rucksack(x)).Select(x => x.Matches().Sum(x => x.ToItemPriority())).Sum();
        return output.ToString();
    }

    public string SolveSecond(string input) {
        var groups = input
            .SplitReturn()
            .Select((x, i) => (rucksack: x, index: i))
            .GroupBy(x => x.index / 3, x => x.rucksack)
            .Select(x => x.Select(x => new Rucksack(x)).ToArray())
            .Select(x => new Group(x[0], x[1], x[2]))
            .ToArray();

        var output = groups.Select(x => x.GetBadge().ToItemPriority()).Sum();
        return output.ToString();
    }

}

public class Group {

    public Group(Rucksack first, Rucksack second, Rucksack third) {
        First = first;
        Second = second;
        Third = third;
    }

    public Rucksack First { get; init; }
    public Rucksack Second { get; init; }
    public Rucksack Third { get; init; }

    public char GetBadge() {
        return First.AllCompartments.First(x => Second.Contains(x) && Third.Contains(x));
    }

}

public class Rucksack {

    public string AllCompartments { get; init; }
    public string FirstCompartment { get; init; }
    public string SecondCompartment { get; init; }

    public Rucksack(string items) {
        AllCompartments = items;
        var middle = items.Length / 2;
        FirstCompartment = items[..middle];
        SecondCompartment = items[middle..];
    }

    public IEnumerable<char> Matches() => FirstCompartment.Where(x => SecondCompartment.Contains(x)).Distinct();
    public bool Contains(char item) => AllCompartments.Contains(item);

}

public static class ItemValueConverter {

    public static int ToItemPriority(this char value) {

        if(value is >= 'a' and <= 'z') {
            return value - 'a' + 1;
        }

        if(value is >= 'A' && value <= 'Z') {
            return value - 'A' + 27;
        }

        throw new ArgumentOutOfRangeException(nameof(value));

    }

}