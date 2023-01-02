namespace AdventOfCode2022.Core.Days_20_25.Day_22;

public class RotationDictionary : Dictionary<int, Direction>, IRotationDictionary {

    private static readonly Dictionary<int, Dictionary<int, Direction>> _map = SetUpRotations();

    public Direction this[int from, int to] => _map[from][to];

    private static Dictionary<int, Dictionary<int, Direction>> SetUpRotations() {

        var dictionary = new Dictionary<int, Dictionary<int, Direction>>();

        var one = new Dictionary<int, Direction> {
            { 2, Direction.Right },
            { 3, Direction.Down },
            { 5, Direction.Right },
            { 6, Direction.Right },
        };

        var two = new Dictionary<int, Direction> {
            { 1, Direction.Left },
            { 3, Direction.Left },
            { 4, Direction.Left },
            { 6, Direction.Up },
        };

        var three = new Dictionary<int, Direction> {
            { 1, Direction.Up },
            { 2, Direction.Up },
            { 4, Direction.Down },
            { 5, Direction.Down },
        };

        var four = new Dictionary<int, Direction> {
            { 3, Direction.Up },
            { 2, Direction.Left },
            { 5, Direction.Left },
            { 6, Direction.Left },
        };

        var five = new Dictionary<int, Direction> {
            { 1, Direction.Right },
            { 3, Direction.Right },
            { 4, Direction.Right },
            { 6, Direction.Down },
        };

        var six = new Dictionary<int, Direction> {
            { 4, Direction.Up },
            { 5, Direction.Up },
            { 1, Direction.Down },
            { 2, Direction.Down },
        };

        dictionary.Add(1, one);
        dictionary.Add(2, two);
        dictionary.Add(3, three);
        dictionary.Add(4, four);
        dictionary.Add(5, five);
        dictionary.Add(6, six);
        return dictionary;

    }

}
