namespace AdventOfCode2022.Core.Shared;

public class RangeLimiter {

    public RangeLimiter(int min, int max, int? start = null) {
        Min = min;
        Max = max;
        if(start != null) {
            _current = (int)start;
        } else {
            _current = Min;
        }
    }

    private int _current;

    public IEnumerable<int> EnumerateRange() {

        var starting = _current;

        for(int i = starting; i <= Max; i++) {
            yield return i;
        }

        for(int i = Min; i < starting; i++) {
            yield return i;
        }

    }

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
        var range = Max + 1 - Min;
        var middleValue = value % range;
        var normalized = middleValue >= 0 ? Min + middleValue : Max + 1 + middleValue;
        return normalized;
    }

    public int Min { get; init; }
    public int Max { get; init; }

}
