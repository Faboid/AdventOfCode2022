namespace AdventOfCode2022.Core.Days_20_25.Day_20;

public class Range {

    public Range(int min, int max, int? start = null) {
        Min = min;
        Max = max;
        if(start != null) {
            _current = (int)start;
        } else {
            _current = Min;
        }
    }

    private int _current;

    public int Next() {
        _current++;
        if(_current > Max) {
            _current = Min;
        }

        return _current;
    }

    public int Next(int offset) {
        _current += offset;
        var normalized = Normalize(_current);
        return normalized;
    }

    public int Normalize(int value) {
        var range = (Max + 1) - Min;
        var middleValue = value % range;
        var normalized = middleValue >= 0 ? Min + middleValue : (Max + 1) + middleValue;
        return normalized;
    }

    public int Min { get; init; }
    public int Max { get; init; }

}
