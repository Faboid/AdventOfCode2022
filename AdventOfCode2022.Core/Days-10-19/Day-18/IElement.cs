namespace AdventOfCode2022.Core.Days_10_19.Day_18;

public interface IElement {
    bool IsAir { get; }
    bool IsLava { get; }
    bool IsWater { get; }
}
