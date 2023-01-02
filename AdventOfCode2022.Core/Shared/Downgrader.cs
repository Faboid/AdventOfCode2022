using System.Numerics;

namespace AdventOfCode2022.Core.Shared;

/// <summary>
/// Used to construct an indexer with starting values that aren't 0.
/// </summary>
public class ReadOnlyDowngrader {

    /// <summary>
    /// Instances a new <see cref="ReadOnlyDowngrader"/>.
    /// </summary>
    /// <param name="min">The downgrader will subtract this value to act as the starting (0) index.</param>
    public ReadOnlyDowngrader(int min) {
        StartIndex = min;
    }

    public int StartIndex { get; init; }

    public int Evaluate(int value) => value - StartIndex;

}

/// <summary>
/// Used to construct an indexer with starting values that aren't 0.
/// </summary>
public class Downgrader<T> where T: INumberBase<T> {

    /// <summary>
    /// Instances a new <see cref="Downgrader"/>.
    /// </summary>
    /// <param name="min">The downgrader will subtract this value to act as the starting (0) index.</param>
    public Downgrader(T min) {
        StartIndex = min;
    }

    public T StartIndex { get; set; }

    public T Evaluate(T value) => value - StartIndex;

}