namespace AdventOfCode2022.Core.Days_10_19.Day_14;

public class Structure {

    public IReadOnlyList<Position> Vertices { get; init; }

    /// <summary>
    /// Enumerates all positions occupied by vertices and edges. Note: there will be duplicate values. It won't be in order either.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Position> EnumeratePixels() {

        foreach(var vert in Vertices) {
            yield return vert;
        }

        for(int i = 1; i < Vertices.Count; i++) {
            foreach(var pos in EnumerateEdge(Vertices[i - 1], Vertices[i])) {
                yield return pos;
            }
        }

    }

    public Structure(IReadOnlyList<Position> edges) {
        Vertices = edges;
    }

    public static IEnumerable<Position> EnumerateEdge(Position start, Position end) {
        yield return start;

        while(start != end) {

            Coordinates offSet = new(0, 0);
            var xDiff = start.X - end.X;
            var yDiff = start.Y - end.Y;


            if(xDiff < 0) {
                offSet.X++;
            }
            if(xDiff > 0) {
                offSet.X--;
            }

            if(yDiff < 0) {
                offSet.Y++;
            }
            if(yDiff > 0) {
                offSet.Y--;
            }

            start += offSet;
            yield return start;

        }

        yield return end;

    }

    public static Structure Parse(string input) {

        var edgesAsString = input.Split(" -> ");
        var edges = edgesAsString
            .Select(x => x.Split(','))
            .Select(x => new Coordinates(int.Parse(x[0]), int.Parse(x[1])))
            .Select(x => new Position(x));

        return new(edges.ToList());

    }

}
