namespace AdventOfCode2022.Core.Days_10_19.Day_14;

public class RegolithReservoir : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {
        return SolveFirst(InputValues.Input);
    }

    public string SolveSecond() {
        return SolveSecond(InputValues.Input);
    }

    public string SolveFirst(string input) {

        var structures = input.SplitReturn().Select(Structure.Parse).ToList();
        var cave = new Cave(new Coordinates(500, 0), structures);

        int count = 0;
        while(cave.GenerateSand()) {
            count++;
            //Console.WriteLine(cave.ToString());
        }

        //Console.WriteLine(cave.ToString());
        var output = count;
        return output.ToString();

    }

    public string SolveSecond(string input) {

        var structures = input.SplitReturn().Select(Structure.Parse).ToList();
        var cave = new Cave(new Coordinates(500, 0), structures, true);

        int count = 0;
        while(cave.GenerateSand()) {
            count++;
            //Console.WriteLine(cave.ToString());
        }

        //Console.WriteLine(cave.ToString());
        var output = count;
        return output.ToString();

    }

}