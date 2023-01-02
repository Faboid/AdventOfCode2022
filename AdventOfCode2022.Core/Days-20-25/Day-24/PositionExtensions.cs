using AdventOfCode2022.Core.Shared;

namespace AdventOfCode2022.Core.Days_20_25.Day_24;

public static class PositionExtensions {

    public static void Move(this Position position, Direction direction) {

        switch(direction) {
            case Direction.Up:
                position.Y--;
                break;
            case Direction.Down:
                position.Y++;
                break;
            case Direction.Left:
                position.X--;
                break;
            case Direction.Right:
                position.X++;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction));
        };

    }

}
