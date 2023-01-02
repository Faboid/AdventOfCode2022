namespace AdventOfCode2022.Core.Day_1;

public class Elf {

    private static int _elvesIdGenerator = 0;

    public Elf() {
        ElfId = _elvesIdGenerator;
        _elvesIdGenerator++;
    }

    public Elf(int calories) {
        ElfId = _elvesIdGenerator;
        Calories = calories;
    }

    public int ElfId { get; init; }
    public int Calories { get; set; }

}