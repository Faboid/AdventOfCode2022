namespace AdventOfCode2022.Core.Day_5;

public interface ICrane {
    void Move(CargoHold cargo, int amount, int from, int to);
}
