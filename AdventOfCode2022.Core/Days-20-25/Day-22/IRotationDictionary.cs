namespace AdventOfCode2022.Core.Days_20_25.Day_22;

public interface IRotationDictionary {
    Direction this[int from, int to] { get; }
}
