namespace AdventOfCode2022.Core.Days_10_19.Day_15;

public static class RangeExtensions
{

    public static int SumRanges(this IEnumerable<Range> ranges)
    {
        var list = ranges.Where(x => !x.IsEmpty).ToList();

        int start = list.Min(x => x.Start);
        int end = list.Max(x => x.End);

        int sum = 0;
        for (int i = start; i <= end; i++)
        {
            if (list.Any(x => x.Contains(i)))
            {
                sum++;
            }
        }

        return sum;

    }

    public record struct EmptySpot(bool Exists, int Index);
    public static EmptySpot HasEmptySpot(this IEnumerable<Range> ranges, int min, int max)
    {
        var list = ranges.Where(x => !x.IsEmpty).OrderBy(x => x.Start).ToList();

        int curr = min;

        while (curr <= max && list.Any())
        {

            var currRange = list.FirstOrDefault(x => x.Contains(curr));

            if (currRange.IsDefault())
            {
                return new(true, curr);
            }

            curr = currRange.End + 1;

            list.Remove(currRange);

        }

        return new(curr <= max, curr);
    }

}
