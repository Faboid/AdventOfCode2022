using AdventOfCode2022.Core.Shared;

namespace AdventOfCode2022.Core.Days_10_19.Day_18;

public class LavaDroplet {

    //x, y, z
    private readonly bool[ , , ] _grid;

    public int XLength { get; init; }
    public int YLength { get; init; }
    public int ZLength { get; init; }

    public bool this[ReadOnlyPositionV3<int> position] => _grid[position.X, position.Y, position.Z];
    public bool this[int x, int y, int z] => _grid[x, y, z];

    public LavaDroplet(IEnumerable<ReadOnlyPositionV3<int>> positions) {

        var maxX = positions.Max(x => x.X) + 1;
        var maxY = positions.Max(x => x.Y) + 1;
        var maxZ = positions.Max(x => x.Z) + 1;

        XLength = maxX + 1;
        YLength = maxY + 1;
        ZLength = maxZ + 1;

        _grid = new bool[XLength, YLength, ZLength];

        //fill standard grid starting from index 1
        foreach(var position in positions) {
            _grid[position.X + 1, position.Y + 1, position.Z + 1] = true;
        }
    }

    public IEnumerable<ReadOnlyPositionV3<int>> EnumerateCubes() {
        for(int x = 0; x < XLength; x++) {
            for(int y = 0; y < YLength; y++) {
                for(int z = 0; z < ZLength; z++) {
                    if(_grid[x, y, z]) {
                        yield return new(x, y, z);
                    }
                }
            }
        }
    }

    public bool HasCubeAt(ReadOnlyPositionV3<int> position) => HasCubeAt(position.X, position.Y, position.Z);
    public bool HasCubeAt(int x, int y, int z) {
        if(x < 0 || x >= XLength) return false;
        if(y < 0 || y >= YLength) return false;
        if(z < 0 || z >= ZLength) return false;
        return _grid[x, y, z];
    }

    public int CountExposedFaces(ReadOnlyPositionV3<int> position) => CountExposedFaces(position.X, position.Y, position.Z);
    public int CountExposedFaces(int x, int y, int z) {

        if(!_grid[x, y, z]) {
            return 0; //this cube does not exist
        }

        //start with the maximum amount of faces
        int max = 6;

        //subtract any face that's covered by a nearby cube
        if(HasCubeAt(x - 1, y, z)) {
            max--;
        }

        if(HasCubeAt(x + 1, y, z)) {
            max--;
        }

        if(HasCubeAt(x, y - 1, z)) {
            max--;
        }

        if(HasCubeAt(x, y + 1, z)) {
            max--;
        }

        if(HasCubeAt(x, y, z - 1)) {
            max--;
        }

        if(HasCubeAt(x, y, z + 1)) {
            max--;
        }

        return max;

    }

}