namespace AdventOfCode2022.Core.Day_5;

public class SupplyStacks : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {
        return SolveFirst(InputValues.Input);
    }

    public string SolveSecond() {
        return SolveSecond(InputValues.Input);
    }

    public string SolveFirst(string input) {

        var splitter = new Splitter(input);
        var instructions = splitter.Instructions;
        var cargoHold = CargoHold.Parse(splitter.Cargo);
        var crane = new Crane9000();

        foreach(var instruction in instructions) {
            instruction.Execute(crane, cargoHold);
        }

        var output = cargoHold.PeekTop();
        return output;

    }

    public string SolveSecond(string input) {

        var splitter = new Splitter(input);
        var instructions = splitter.Instructions;
        var cargoHold = CargoHold.Parse(splitter.Cargo);
        var crane = new Crane9001();

        foreach(var instruction in instructions) {
            instruction.Execute(crane, cargoHold);
        }

        var output = cargoHold.PeekTop();
        return output;

    }

}

public class Splitter {

    public Splitter(string input) {

        var split = input.SplitDoubleReturn();
        Cargo = split[0];
        Instructions = split[1].SplitReturn().Select(Instruction.Parse).ToList();

    }

    public IReadOnlyList<Instruction> Instructions { get; init; }
    public string Cargo { get; init; }

}