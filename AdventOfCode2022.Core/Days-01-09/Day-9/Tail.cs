namespace AdventOfCode2022.Core.Day_9;

public class Tail {

    public Position Position { get; private set; }
    public int VisitedCount => _visited.Count;

    private readonly HashSet<Position> _visited = new();
    private readonly Position _head;

    public Tail(Position head) {
        Position = new(0, 0);
        _visited.Add(Position.Copy());
        _head = head;
    }

    public void Follow() {

        var xDiff = _head.X - Position.X;
        var yDiff = _head.Y - Position.Y;

        //at least two of distance
        var shouldMove = Math.Abs(xDiff) > 1 || Math.Abs(yDiff) > 1;

        if(!shouldMove) {
            return;
        }

        var xNegative = xDiff < 0;
        var yNegative = yDiff < 0;

        if(xNegative) {
            xDiff = Math.Max(xDiff, -1);
        } else {
            xDiff = Math.Min(xDiff, 1);
        }

        if(yNegative) {
            yDiff = Math.Max(yDiff, -1);
        } else {
            yDiff = Math.Min(yDiff, 1);
        }

        Move(xDiff, yDiff);

    }

    public void Move(int x, int y) {
        Position.X += x;
        Position.Y += y;
        _visited.Add(Position.Copy());
    }

}