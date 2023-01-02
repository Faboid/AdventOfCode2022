using System.Text;

namespace AdventOfCode2022.Core.Days_20_25.Day_22;

public class CubeConnections {

    //hardcode this class
    private readonly int _size = 50;
    private readonly int[,] _grid = new int[200, 150];

    public IRotationDictionary RotationsDictionary { get; init; }

    public record Edge(ReadOnlyPosition From, ReadOnlyPosition To, Direction FromTo, Direction ToFrom);

    public CubeConnections(IRotationDictionary rotationDictionary) {
        RotationsDictionary = rotationDictionary;
        SetUpGrid();
    }

    public bool TryRotate(Tile last, Tile curr, out Direction direction) {

        var from = _grid[last.Row - 1, last.Column - 1];
        var to = _grid[curr.Row - 1, curr.Column - 1];

        if(from == to) {
            direction = default;
            return false;
        }

        direction = RotationsDictionary[from, to];
        return true;

    }

    public List<Edge> EnumerateEdges() {

        var list = new List<Edge>();

        //1 <-> 5
        var oneToFive = EnumerateEdge(Direction.Left, 1, 0).Reverse().ToArray();
        var FiveToOne = EnumerateEdge(Direction.Left, 0, 2).ToArray();
        var oneFiveMerge = MergeEdges(oneToFive, FiveToOne, Direction.Left, Direction.Left);
        list.AddRange(oneFiveMerge);

        //1 <-> 6
        var oneToSix = EnumerateEdge(Direction.Up, 1, 0).ToArray();
        var sixToOne = EnumerateEdge(Direction.Left, 0, 3).ToArray();
        var oneToSixMerge = MergeEdges(oneToSix, sixToOne, Direction.Up, Direction.Left);
        list.AddRange(oneToSixMerge);

        //2 <-> 6
        var twoToSix = EnumerateEdge(Direction.Up, 2, 0).ToArray();
        var sixToTwo = EnumerateEdge(Direction.Down, 0, 3).ToArray();
        var twoToSixMerge = MergeEdges(twoToSix, sixToTwo, Direction.Up, Direction.Down);
        list.AddRange(twoToSixMerge);

        //2 <-> 4
        var twoToFour = EnumerateEdge(Direction.Right, 2, 0).ToArray();
        var fourToTwo = EnumerateEdge(Direction.Right, 1, 2).Reverse().ToArray();
        var twoToFourMerge = MergeEdges(twoToFour, fourToTwo, Direction.Right, Direction.Right);
        list.AddRange(twoToFourMerge);

        //2 <-> 3
        var twoToThree = EnumerateEdge(Direction.Down, 2, 0).ToArray();
        var threeToTwo = EnumerateEdge(Direction.Right, 1, 1).ToArray();
        var twoToThreeMerge = MergeEdges(twoToThree, threeToTwo, Direction.Down, Direction.Right);
        list.AddRange(twoToThreeMerge);

        //3 <-> 5
        var threeToFive = EnumerateEdge(Direction.Left, 1, 1).ToArray();
        var fiveToThree = EnumerateEdge(Direction.Up, 0, 2).ToArray();
        var threeToFiveMerge = MergeEdges(threeToFive, fiveToThree, Direction.Left, Direction.Up);
        list.AddRange(threeToFiveMerge);

        //4 <-> 6
        var fourToSix = EnumerateEdge(Direction.Down, 1, 2).ToArray();
        var sixToFour = EnumerateEdge(Direction.Right, 0, 3).ToArray();
        var fourToSixMerge = MergeEdges(fourToSix, sixToFour, Direction.Down, Direction.Right);
        list.AddRange(fourToSixMerge);

        return list;

    }

    private void SetUpGrid() {
        SetGrid(1, 0, 1); // 1
        SetGrid(2, 0, 2); // 2
        SetGrid(1, 1, 3); // 3
        SetGrid(1, 2, 4); // 4
        SetGrid(0, 2, 5); // 5
        SetGrid(0, 3, 6); // 6
    }

    private IEnumerable<Edge> MergeEdges(ReadOnlyPosition[] from, ReadOnlyPosition[] to, Direction fromTo, Direction toFrom) {
        if(from.Length != to.Length) {
            throw new ArgumentException("From and To have a different length.");
        }

        foreach(var edge in Enumerable.Range(0, from.Length).Select(x => new Edge(from[x], to[x], fromTo, toFrom))) {
            yield return edge;
        }
    }

    private IEnumerable<ReadOnlyPosition> EnumerateEdge(Direction direction, int x, int y) {
        var (fromX, toX, fromY, toY) = GetSquare(x, y);

        var startX = direction switch {
            Direction.Left => fromX,
            Direction.Right => toX,
            _ => fromX
        };

        var endX = direction switch {
            Direction.Left => fromX,
            Direction.Right => toX,
            _ => toX
        };

        var startY = direction switch {
            Direction.Up => fromY,
            Direction.Down => toY,
            _ => fromY
        };

        var endY = direction switch {
            Direction.Up => fromY,
            Direction.Down => toY,
            _ => toY
        };

        for(y = startY; y <= endY; y++) {
            for(x = startX; x <= endX; x++) {
                yield return new ReadOnlyPosition(x - 1, y - 1);
            }
        }
    }

    private (int fromX, int toX, int fromY, int toY) GetSquare(int x, int y) {
        return (x * _size + 1, (x + 1) * _size, y * _size + 1, (y + 1) * _size);
    }

    private void SetGrid(int x, int y, int value) {
        var (fromX, toX, fromY, toY) = GetSquare(x, y);
        SetGrid(fromX, toX, fromY, toY, value);
    }

    private void SetGrid(int fromX, int toX, int fromY, int toY, int value) {
        for(int x = fromX; x <= toX; x++) {
            for(int y = fromY; y <= toY; y++) {
                _grid[y - 1, x - 1] = value; //-1 because it's using index 0
            }
        }
    }

    public override string ToString() {
        StringBuilder sb = new();

        for(int y = 0; y < _grid.GetLength(0); y++) {
            for(int x = 0; x < _grid.GetLength(1); x++) {
                sb.Append(_grid[y, x]);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
