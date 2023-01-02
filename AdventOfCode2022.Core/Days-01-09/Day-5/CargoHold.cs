using System.Text;

namespace AdventOfCode2022.Core.Day_5;

public class CargoHold {

    public List<Stack<Crate>> Stacks { get; init; } = new();

    /// <summary>
    /// Initializes empty cargo hold.
    /// </summary>
    public CargoHold() { }

    public CargoHold(List<Stack<Crate>> stacks) {
        Stacks = stacks;
    }

    public string PeekTop() {
        return Stacks.Aggregate(new StringBuilder(), (sb, x) => sb.Append(x.Peek().ID)).ToString();
    }

    public static CargoHold Parse(string input) {

        //get rows from bottom to top
        var rows = input.Split("\r\n").Reverse().ToArray();
        var stacksNumber = int.Parse(rows[0].Where(x => x is not ' ').Last().ToString());
        var stacks = Enumerable.Repeat(0, stacksNumber).Select(x => new Stack<Crate>()).ToList();

        foreach(var row in rows.Skip(1)) {

            int currStack = 0;

            //skip first and last. Add three + current to handle the distance between each cargo id
            for(int i = 1; i < row.Length - 1; i += 4) {

                if(row[i] is not ' ') {
                    stacks[currStack].Push(new Crate(row[i]));
                }

                currStack++;
            }

        }

        return new(stacks);
    }

}
