namespace AdventOfCode2022.Core.Days_20_25.Day_22;

public class MonkeyMap : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {

        var input = InputValues.Input;
        return SolveFirst(input);
        
    }


    public string SolveSecond() {

        var input = InputValues.Input;
        return SolveSecond(input);

    }

    public string SolveFirst(string input) {

        var mapAsString = InputValues.ExtractMap(input).Split("\r\n");
        var commandsAsString = InputValues.ExtractCommands(input);

        var mapNavigator = MapNavigatorBuilder.BuildAsGrid(mapAsString);
        var navigator = mapNavigator.FollowDirections(commandsAsString);

        var row = navigator.Position.Row;
        var column = navigator.Position.Column;
        var direction = navigator.CurrentDirection;

        var total = (row * 1000) + (column * 4) + direction;
        //Console.WriteLine(mapNavigator.Root);
        return total.ToString();

    }

    public string SolveSecond(string input) {

        if(input == TestInput) {
            return "The values for the second phase of this problem are hard-coded to work on the real input's layout. As the test input's layout is different, it will fail.";
        }

        var mapAsString = InputValues.ExtractMap(input).Split("\r\n");
        var commandsAsString = InputValues.ExtractCommands(input);

        var cubeConnections = new CubeConnections(new RotationDictionary());
        var cubeNavigatorBuilder = new CubeNavigatorBuilder(cubeConnections);
        var mapNavigator = cubeNavigatorBuilder.BuildCube(mapAsString);

        var navigator = mapNavigator.FollowDirections(commandsAsString);

        var row = navigator.Position.Row;
        var column = navigator.Position.Column;
        var direction = navigator.CurrentDirection;

        var total = (row * 1000) + (column * 4) + direction;
        //Console.WriteLine(mapNavigator.Root);
        return total.ToString();

    }

}