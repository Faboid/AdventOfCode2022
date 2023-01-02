namespace AdventOfCode2022.Core.Days_20_25.Day_20;

public class DoubleLinkedCollection {

    private readonly Node[] _values;
    private readonly Node _root;

    public DoubleLinkedCollection(long[] values, int mixTimes) {
        _values = values.Select(x => new Node(x)).ToArray();
        _root = _values.Single(x => x.Value == 0);

        //set up cycle
        var first = _values[0];
        var last = _values[^1];
        first.Parent = last;
        last.Child = first;

        //link chain
        var curr = first;
        foreach(var node in _values.Skip(1)) {
            curr.Child = node;
            node.Parent = curr;
            curr = node;
        }

        while(mixTimes > 0) {
            DecryptAll();
            mixTimes--;
        }
    }

    public IEnumerable<long> GetOneLoop() {

        var curr = _root;
        while(curr!.Child!.Value != _root.Value) {
            yield return curr.Value;
            curr = curr.Child;
        }

        yield return curr.Value;

    }

    public long GetByIndex(int index) {
        var curr = _root;
        while(index > 0) {
            curr = curr!.Child;
            index--;
        }

        return curr!.Value;
    }

    private void DecryptAll() {
        foreach(var node in _values) {
            Debug.WriteLine(this.ToString());
            Debug.WriteLine($"({node.Value})");
            Decrypt(node);
        }
        Debug.WriteLine(this.ToString());
    }

    private void Decrypt(Node node) {

        var normalized = node.Value % (_values.Length - 1);

        //if the value is divisible by the length, it would just return to itself
        if(node.Value == 0 || normalized == 0) {
            return;
        }

        if(node.Value < 0) {
            node.MoveUp(-normalized);
        }

        if(node.Value > 0) {
            node.MoveDown(normalized);
        }

    }

    public override string ToString() {
        return string.Join(',', GetOneLoop());
    }
}
