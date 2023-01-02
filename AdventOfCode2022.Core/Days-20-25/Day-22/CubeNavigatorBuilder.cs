namespace AdventOfCode2022.Core.Days_20_25.Day_22;

public class CubeNavigatorBuilder {

    private readonly CubeConnections _cubeConnections;

    public CubeNavigatorBuilder(CubeConnections cubeConnections) {
        _cubeConnections = cubeConnections;
    }

    public MapNavigator BuildCube(IList<string> cubeAsString) {

        var tileList = cubeAsString.BuildTileList();
        var missingEdges = _cubeConnections.EnumerateEdges();

        foreach(var edge in missingEdges) {

            edge.Deconstruct(out ReadOnlyPosition from, out ReadOnlyPosition to, out Direction fromTo, out Direction toFrom);

            var tileA = tileList[from.Y][from.X] ?? throw new ArgumentException("The edge is null");
            var tileB = tileList[to.Y][to.X] ?? throw new ArgumentException("The edge is null.");

            switch(fromTo) {
                case Direction.Right:
                    tileA.Right = tileB;
                    break;
                case Direction.Down:
                    tileA.Down = tileB;
                    break;
                case Direction.Left:
                    tileA.Left = tileB;
                    break;
                case Direction.Up:
                    tileA.Up = tileB;
                    break;
            }

            switch(toFrom) {
                case Direction.Right:
                    tileB.Right = tileA;
                    break;
                case Direction.Down:
                    tileB.Down = tileA;
                    break;
                case Direction.Left:
                    tileB.Left = tileA;
                    break;
                case Direction.Up:
                    tileB.Up = tileA;
                    break;
            }

        }

        var root = tileList[0].First(x => x is not null);
        return new MapNavigator(root, _cubeConnections);

    }

}
