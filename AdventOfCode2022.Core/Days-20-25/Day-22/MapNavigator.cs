namespace AdventOfCode2022.Core.Days_20_25.Day_22;

public class MapNavigator {

    public Tile Root { get; init; }

    private readonly CubeConnections? _cubeConnections;

    public MapNavigator(Tile root, CubeConnections? cubeConnections = null) {
        Root = root;
        _cubeConnections = cubeConnections;
    }

    public Navigator FollowDirections(string input) {

        var parser = new CommandParser(input);
        var navigator = new Navigator(Root, Direction.Right, _cubeConnections);

        foreach(var command in parser.Parse()) {

            if(int.TryParse(command.ToString(), out var value)) {
                navigator.Go(value);
                continue;
            }

            if(command is "L") {
                navigator.RotateLeft();
                continue;
            }

            if(command is "R") {
                navigator.RotateRight();
                continue;
            }

            throw new ArgumentException();

        }

        return navigator;

    }

}

public class CommandParser {

    private readonly string _command;

    public CommandParser(string command) {
        _command = command;
    }

    public IEnumerable<string> Parse() {
        for(int i = 0; i < _command.Length; i++) {

            if(_command[i] is 'L' or 'R') {
                yield return _command[i].ToString();
                continue;
            }

            var number = "";
            while(i < _command.Length && _command[i] is not 'L' and not 'R') {
                number += _command[i].ToString();
                i++;
            }

            if(i == _command.Length) {
                yield return number;
                yield break;
            }

            i--;

            yield return number;

        }
    }

}