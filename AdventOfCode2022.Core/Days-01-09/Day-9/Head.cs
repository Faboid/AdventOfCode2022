namespace AdventOfCode2022.Core.Day_9;

public class Head {

    public Position Position { get; private set; } = new(0, 0);

    public void Move(int x, int y) {
        Position.X += x;
        Position.Y += y;
    }

}
