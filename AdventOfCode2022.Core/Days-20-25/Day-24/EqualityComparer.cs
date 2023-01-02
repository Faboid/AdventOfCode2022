using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2022.Core.Days_20_25.Day_24;

public class EqualityComparer<T> : IEqualityComparer<T> {

    private readonly Func<T?, T?, bool> _areEqual;
    private EqualityComparer(Func<T?, T?, bool> areEqual) {
        _areEqual = areEqual;
    }

    public static EqualityComparer<T> Create(Func<T?, T?, bool> areEqual) => new(areEqual);

    public bool Equals(T? x, T? y) => _areEqual.Invoke(x, y);
    public int GetHashCode([DisallowNull] T obj) => obj.GetHashCode();
}
