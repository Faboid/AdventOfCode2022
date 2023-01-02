namespace AdventOfCode2022.Core.Day_5;

public class Crane9000 : ICrane {

    public void Move(CargoHold cargo, int amount, int from, int to) {

        for(int i = 0; i < amount; i++) {
            var crate = cargo.Stacks[from - 1].Pop();
            cargo.Stacks[to - 1].Push(crate);
        }
    }

}
