namespace AdventOfCode2022.Core.Days_10_19.Day_11;

public class Item {

    public static bool CalmAfterInspection { get; set; } = true;

    public Item(int worry) {
        StartingWorry = worry;
        Worry = worry;
    }

    public int StartingWorry { get; init; }
    public long Worry { get; private set; }

    public void AddWorry(long value) {
        Worry = (Worry + value) % 9699690; //96577;
    }

    public void MultiplyWorry(long value) {
        Worry = (Worry * value) % 9699690; //96577;
    }

    public void CalmWorry() {
        if(CalmAfterInspection) {
            Worry /= 3;
        }
    }

}
