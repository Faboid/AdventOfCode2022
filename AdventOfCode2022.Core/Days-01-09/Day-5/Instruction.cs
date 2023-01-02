namespace AdventOfCode2022.Core.Day_5;

public record Instruction(int From, int To, int Amount) {
    public void Execute(ICrane crane, CargoHold cargo) {
        crane.Move(cargo, Amount, From, To);
    }

    public static Instruction Parse(string input) {

        var words = input.Split(' ');
        var amount = int.Parse(words[1]);
        var from = int.Parse(words[3]);
        var to = int.Parse(words[5]);

        return new Instruction(from, to, amount);
    }

}
