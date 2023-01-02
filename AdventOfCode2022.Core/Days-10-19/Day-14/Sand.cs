namespace AdventOfCode2022.Core.Days_10_19.Day_14;

public class Sand {

    private readonly Cave _cave;
    public Coordinates Coordinates { get; set; }
    public Sand(Coordinates coordinates, Cave cave) {
        Coordinates = coordinates;
        _cave = cave;
    }

    public bool TryMove() {

        if(!_cave.Collides(Coordinates.Down())) {
            Coordinates = Coordinates.Down();
            return true;
        }

        if(!_cave.Collides(Coordinates.DownLeft())) {
            Coordinates = Coordinates.DownLeft();
            return true;
        }

        if(!_cave.Collides(Coordinates.DownRight())) {
            Coordinates = Coordinates.DownRight();
            return true;
        }

        return false;
    }

}
