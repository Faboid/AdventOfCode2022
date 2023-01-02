namespace AdventOfCode2022.Core.Day_4;

public class Shift {
    public Shift(int starts, int ends) {
        Starts = starts;
        Ends = ends;
    }

    public int Starts { get; init; }
    public int Ends { get; init; }

    public bool Overlaps(Shift shift) {
        return Contains(shift.Starts) || Contains(shift.Ends);
    }

    public bool Contains(int place) {
        return place >= Starts && place <= Ends;
    }

    public bool Contains(Shift shift) {
        return Starts <= shift.Starts && Ends >= shift.Ends;
    }

    public static Shift Parse(string input) {
        var split = input.Split('-');
        return new(int.Parse(split[0]), int.Parse(split[1]));
    }

}

