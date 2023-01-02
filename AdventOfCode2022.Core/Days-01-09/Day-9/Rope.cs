namespace AdventOfCode2022.Core.Day_9;

public class Rope {

    public Head Head { get; } = new();
    public List<Tail> Tails { get; } = new();


    public Rope(int length) {
        if(length < 2) {
            throw new Exception();
        }

        Head = new();
        Tails.Add(new Tail(Head.Position));
        length -= 2; //head + tail

        while(length > 0) {
            Tails.Add(new Tail(Tails.Last().Position));
            length--;
        }
    }

    public void Execute(Instruction instruction) {

        var yMove = instruction.Direction switch {
            Direction.Up => 1,
            Direction.Down => -1,
            _ => 0,
        };

        var xMove = instruction.Direction switch {
            Direction.Right => 1,
            Direction.Left => -1,
            _ => 0,
        };

        var steps = instruction.Steps;
        while(steps > 0) {
            steps--;
            Head.Move(xMove, yMove);
            foreach(var tail in Tails) {
                tail.Follow();
            }
        }

    }

}
