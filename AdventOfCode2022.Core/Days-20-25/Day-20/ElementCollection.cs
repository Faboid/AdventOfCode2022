namespace AdventOfCode2022.Core.Days_20_25.Day_20;

public class ElementCollection {

    private readonly Element[] _elements;
    private readonly Range _range;

    public int Length => _elements.Length;
    public int Min => _range.Min;
    public int Max => _range.Max;

    public ElementCollection(Element[] elements) {
        _elements = elements;
        _range = new Range(elements.Min(x => x.Id), elements.Max(x => x.Id));
    }

    public Element this[int index] {
        get { return _elements[index]; }
        set { _elements[index] = value; }
    }

    public Element GetByInfiniteIndex(int index) {
        var zeroIndex = _elements.Single(x => x.Value == 0).Id;

        return _elements[_range.Normalize(index + zeroIndex)];
    }

    public int FindIndexById(int id) {

        var count = 0;
        foreach(var element in _elements) {
            if(element.Id == id) {
                return count;
            }

            count++;
        }

        throw new ArgumentOutOfRangeException("The given id does not exist.");
    }

    public void SwapForward(int index) {
        (_elements[_range.Normalize(index)], _elements[_range.Normalize(index + 1)]) = (_elements[_range.Normalize(index + 1)], _elements[_range.Normalize(index)]);
    }

    public void SwapBack(int index) {
        (_elements[_range.Normalize(index)], _elements[_range.Normalize(index - 1)]) = (_elements[_range.Normalize(index - 1)], _elements[_range.Normalize(index)]);
    }

    public override string ToString() => string.Join(',', _elements.Select(x => x.Value.ToString()));

}
