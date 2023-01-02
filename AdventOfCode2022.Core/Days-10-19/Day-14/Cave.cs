namespace AdventOfCode2022.Core.Days_10_19.Day_14;

public class Cave {

    public Cave(Position sandSource, IEnumerable<Structure> structures) : this(sandSource, structures, false) { }

    public Cave(Position sandSource, IEnumerable<Structure> structures, bool infiniteFloor) {

        var minX = structures.Min(x => x.Vertices.Min(x => x.X));
        var minY = structures.Min(x => x.Vertices.Min(x => x.Y));
        var maxX = structures.Max(x => x.Vertices.Max(x => x.X));
        var maxY = structures.Max(x => x.Vertices.Max(x => x.Y));

        if(infiniteFloor) {
            //will need to handle a triangle of sand, so make spaces by adding the height to the widths
            maxY += 2;
            minX -= maxY;
            maxX += maxY;
        }

        //the rocks pour from [500, 0], so it must be included
        minX = Math.Min(minX, sandSource.X);
        minY = Math.Min(minY, sandSource.Y);
        maxX = Math.Max(maxX, sandSource.X);
        maxY = Math.Max(maxY, sandSource.Y);

        var width = maxX - minX;
        var height = maxY - minY;

        _caveGrid = new(minX, minY, width + 1, height + 1);
        _sandSource = sandSource;

        foreach(var structure in structures) {
            foreach(var position in structure.EnumeratePixels()) {
                _caveGrid[position] = CaveSign.Rock;
            }
        }

        //create infinite floor
        if(infiniteFloor) {
            for(int x = minX; x <= maxX; x++) {
                _caveGrid[maxY][x] = CaveSign.Rock;
            }
        }
    }

    private readonly Position _sandSource;
    private readonly CaveArray _caveGrid;

    public bool IsInBounds(Position position) => _caveGrid.HasIndex(position);

    /// <summary>
    /// Returns whether <paramref name="position"/> hits a solid body. Note: out of bounds is considered empty and will return false.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool Collides(Position position) => IsInBounds(position) && _caveGrid[position] is CaveSign.Rock or CaveSign.Sand;

    public bool GenerateSand() {

        //if it's sand, it cannot generate any more
        if(_caveGrid[_sandSource] == CaveSign.Sand) {
            return false;
        }

        var sand = new Sand(_sandSource, this);
        while(sand.TryMove()) {
            if(!IsInBounds(sand.Coordinates)) {
                return false;
            }
        }

        _caveGrid[sand.Coordinates] = CaveSign.Sand;
        return true;
    }

    public override string ToString() => _caveGrid.ToString();

}
