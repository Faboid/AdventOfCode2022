namespace AdventOfCode2022.Core.Shared;

public static class EnumerableExtensions {

    public static IEnumerable<T> Concat<T>(this IEnumerable<T> enumerable, T other) {
        foreach(var item in enumerable) {
            yield return item;
        }

        yield return other;
    }


}

