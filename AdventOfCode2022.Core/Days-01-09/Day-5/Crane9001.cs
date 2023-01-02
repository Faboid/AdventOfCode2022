namespace AdventOfCode2022.Core.Day_5;

public class Crane9001 : ICrane {
    public void Move(CargoHold cargo, int amount, int from, int to) {

        var crates = cargo.Stacks[from - 1].PopRange(amount);
        crates.Reverse();//reverse to compensate for the stack's popping natural retrieval.
        cargo.Stacks[to - 1].PushAll(crates);

    }
}
