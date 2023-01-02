namespace AdventOfCode2022.Core.Days_20_25.Day_23;

public class UnstableDiffusion : IProblem {

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
        var elves = Elf.ParseElves(input);
        var map = new Map(elves);
        map.ActTurns(10);
        var emptyTiles = map.CountTiles();
        return emptyTiles.ToString();
    }

    public string SolveSecond(string input) {
        var elves = Elf.ParseElves(input);
        var map = new Map(elves);

        int count = 1;
        while(map.ActTurn()) {
            count++;
        }

        return count.ToString();
    }
}