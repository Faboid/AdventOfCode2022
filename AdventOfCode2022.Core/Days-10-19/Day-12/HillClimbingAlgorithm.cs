namespace AdventOfCode2022.Core.Days_10_19.Day_12;

public class HillClimbingAlgorithm : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public string SolveFirst() {
        return SolveFirst(InputValues.Input);
    }

    public string SolveSecond() {
        return SolveSecond(InputValues.Input);
    }

    public string SolveFirst(string input) {

        var map = Map.Parse(input);
        var pathFinder = new PathFinder(map);
        var grid = InputValues.MapAsGrid(input);

        Position starting = new();
        Position target = new();

        for(int y = 0; y < grid.Length; y++) {
            for(int x = 0; x < grid[0].Length; x++) {
                if(grid[y][x] == 'S') {
                    starting = new Position(x, y);
                }

                if(grid[y][x] == 'E') {
                    target = new Position(x, y);
                }
            }
        }

        bool result = pathFinder.TryFindPath(starting, target, out var path);

        if(!result) {
            throw new Exception();
        }

        var count = path!.Length - 1;
        var output = count;
        return output.ToString();

        //Console.WriteLine();
        //ConsoleHelper.DrawMap(map, path, starting, target);
        //Console.WriteLine();

    }

    public string SolveSecond(string input) {

        var map = Map.Parse(input);
        var pathFinder = new PathFinder(map);
        var grid = InputValues.MapAsGrid(input);

        var startingPositions = new List<Position>();
        Position target = new();

        for(int y = 0; y < grid.Length; y++) {
            for(int x = 0; x < grid[0].Length; x++) {
                if(grid[y][x] == 'S' || grid[y][x] == 'a') {
                    startingPositions.Add(new Position(x, y));
                }

                if(grid[y][x] == 'E') {
                    target = new Position(x, y);
                }
            }
        }

        var paths = startingPositions
            .Select(x => (success: pathFinder.TryFindPath(x, target, out var path), path));

        int completed = 0;
        var listPaths = new List<Tile[]>();
        foreach(var path in paths) {
            completed++;
            Console.WriteLine($"Completed {completed} / {startingPositions.Count}");

            if(path.success && path.path is not null) {
                listPaths.Add(path.path);
            }

        }

        var bestPath = listPaths.MinBy(x => x.Length);

        var count = bestPath!.Length - 1;
        var output = count;
        return output.ToString();

        //Console.WriteLine();
        //ConsoleHelper.DrawMap(map, bestPath, bestPath.Last().Position, bestPath.First().Position);
        //Console.WriteLine();
    }
}