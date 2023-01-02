namespace AdventOfCode2022.Core.Days_10_19.Day_12;

public static class EnumerableExtensions {

    public static bool TryFirst<T>(this IEnumerable<T> enumerable, Func<T, bool> match, out T? value) {
        if(!enumerable.Any(x => match.Invoke(x))) {
            value = default;
            return false;
        }

        value = enumerable.First(x => match.Invoke(x));
        return true;
    }

    public static IEnumerable<T> UnWrap<T>(this T obj, Func<T, T?> unwrapper) {
        yield return obj;

        while(true) {

            var next = unwrapper.Invoke(obj);

            if(next is null) {
                yield break;
            }

            yield return next;
            obj = next;

        }
    }

}