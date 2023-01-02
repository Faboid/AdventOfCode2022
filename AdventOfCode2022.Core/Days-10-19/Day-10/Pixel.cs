namespace AdventOfCode2022.Core.Days_10_19.Day_10;

public class Pixel {
    public bool Active { get; set; }
    public override string ToString() => Active ? "#" : ".";
}
