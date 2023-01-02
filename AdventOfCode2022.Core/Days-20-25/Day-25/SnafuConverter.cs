namespace AdventOfCode2022.Core.Days_20_25.Day_25;

public static class SnafuConverter {

    public const int Base = 5;

    public static long Convert(this string value) {

        long sum = 0;
        long digitCount = 1;
        foreach(var digit in value.Reverse()) {
            var digitValue = digit.Convert();
            var curr = digitValue * digitCount;
            sum += curr;
            digitCount *= Base;
        }

        return sum;

    }

    public static string Convert(this long value) {

        long digitCount = 1;
        //get to the highest
        while(value > digitCount) {
            digitCount *= Base;
        }
        digitCount /= Base;

        var chars = new List<char>();
        while(digitCount >= 1) {

            var remainder = value / digitCount;
            value -= (remainder * digitCount);
            var digitChar = remainder.ConvertSingle(out var extra);
            chars.Add(digitChar);

            int count = 1;
            while(extra) {
                count++;
                var newVal = chars[^count].AddOne(out extra);
                chars[^count] = newVal;
            }

            digitCount /= Base;
        }

        return new string(chars.ToArray());

    }

    public static char AddOne(this char value, out bool remainder) {

        remainder = value is '2';
        return value switch {
            '0' => '1',
            '1' => '2',
            '2' => '=',
            '=' => '-',
            '-' => '0',
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };

    }

    public static char ConvertSingle(this int value, out bool remainder) => ConvertSingle((long)value, out remainder);
    public static char ConvertSingle(this long value, out bool remainder) {

        if(0 <= value && value <= 2) {
            remainder = false;
        } else {
            remainder = true;
        }

        return value switch {
            0 => '0',
            1 => '1',
            2 => '2',
            3 => '=',
            4 => '-',
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };

    }

    private static int Convert(this char value) {

        return value switch {
            '=' => -2,
            '-' => -1,
            '0' => 0,
            '1' => 1,
            '2' => 2,
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };

    }

}