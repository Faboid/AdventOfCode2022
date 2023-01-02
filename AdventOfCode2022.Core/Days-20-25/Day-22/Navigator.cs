namespace AdventOfCode2022.Core.Days_20_25.Day_22;

public class Navigator {

    public Navigator(Tile startingPosition, Direction startingDirection, CubeConnections? cubeConnections = null) {
        Position = startingPosition;
        CurrentDirection = startingDirection;
        _cubeConnections = cubeConnections;
    }

    public Tile Position { get; private set; }
    public Direction CurrentDirection { get; set; }

    private readonly CubeConnections? _cubeConnections;

    public void Go(int steps) {

        while(steps > 0) {
            var newPosition = Position.Go(CurrentDirection);
            if(_cubeConnections is not null && _cubeConnections.TryRotate(Position, newPosition, out var direction)) {
                CurrentDirection = direction;
            }

            Position = newPosition;
            steps--;
        }

    }

    public void RotateRight() {

        CurrentDirection = CurrentDirection switch {
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            Direction.Up => Direction.Right,
        };

    }

    public void RotateLeft() {

        CurrentDirection = CurrentDirection switch {
            Direction.Right => Direction.Up,
            Direction.Down => Direction.Right,
            Direction.Left => Direction.Down,
            Direction.Up => Direction.Left,
        };

    }

}
