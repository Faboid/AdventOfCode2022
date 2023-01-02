namespace AdventOfCode2022.Core.Day_6;

public class MarkSeeker {

    public MarkSeeker(int markSize) {
        MarkSize = markSize;
    }

    private readonly Queue<char> _queue = new();
    private int _counter = 0;
    public int MarkSize { get; init; }

    public bool Add(char c) {
        _queue.Enqueue(c);
        _counter++;

        if(_queue.Count > MarkSize) {
            _queue.Dequeue();
        }

        return _queue.Distinct().Count() == MarkSize;
    }

    public bool HasMark() => _queue.Distinct().Count() == MarkSize;

    public int GetCounter() => _counter;

}