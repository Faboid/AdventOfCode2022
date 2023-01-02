namespace AdventOfCode2022.Core.Days_10_19.Day_18;

public class DropCalculator {

    private readonly IElement[ , , ] _grid;

    public int XLength { get; init; }
    public int YLength { get; init; }
    public int ZLength { get; init; }

    public DropCalculator(IEnumerable<ReadOnlyPositionV3<int>> lavaPositions) {

        var maxX = lavaPositions.Max(x => x.X);
        var maxY = lavaPositions.Max(x => x.Y);
        var maxZ = lavaPositions.Max(x => x.Z);

        XLength = maxX + 2;
        YLength = maxY + 2;
        ZLength = maxZ + 2;

        _grid = new IElement[XLength, YLength, ZLength];

        //fill with the reverse of the droplet
        for(int x = 0; x < XLength; x++) {
            for(int y = 0; y < YLength; y++) {
                for(int z = 0; z < ZLength; z++) {
                    _grid[x, y, z] = new Air();
                }
            }
        }

        foreach(var position in lavaPositions) {
            _grid[position.X, position.Y, position.Z] = new Lava();
        }

        Flood();
    }

    public IElement this[ReadOnlyPositionV3<int> position] {
        get {
            return _grid[position.X, position.Y, position.Z];
        }
        set {
            _grid[position.X, position.Y, position.Z] = value;
        }
    }

    public bool TryGet(ReadOnlyPositionV3<int> position, out IElement? element) => TryGet(position.X, position.Y, position.Z, out element);
    public bool TryGet(int x, int y, int z, out IElement? element) {
        element = null;
        if(x < 0 || x >= XLength) {
            return false;
        }

        if(y < 0 || y >= YLength) {
            return false;
        }

        if(z < 0 || z >= ZLength) {
            return false;
        }

        element = _grid[x, y, z];
        return true;
    }

    public IEnumerable<ReadOnlyPositionV3<int>> EnumerateAir() {
        for(int x = 0; x < XLength; x++) {
            for(int y = 0; y < YLength; y++) {
                for(int z = 0; z < ZLength; z++) {
                    if(_grid[x, y, z].IsAir) {
                        yield return new(x, y, z);
                    }
                }
            }
        }
    }

    public IEnumerable<ReadOnlyPositionV3<int>> EnumerateWater() {
        for(int x = 0; x < XLength; x++) {
            for(int y = 0; y < YLength; y++) {
                for(int z = 0; z < ZLength; z++) {
                    if(_grid[x, y, z].IsWater) {
                        yield return new(x, y, z);
                    }
                }
            }
        }
    }

    public IEnumerable<ReadOnlyPositionV3<int>> EnumerateLava() {
        for(int x = 0; x < XLength; x++) {
            for(int y = 0; y < YLength; y++) {
                for(int z = 0; z < ZLength; z++) {
                    if(_grid[x, y, z].IsLava) {
                        yield return new(x, y, z);
                    }
                }
            }
        }
    }

    public bool IsWater(ReadOnlyPositionV3<int> position) => !TryGet(position, out var element) || (element?.IsWater ?? true);
    public bool IsLava(ReadOnlyPositionV3<int> position) => TryGet(position, out var element) && (element?.IsLava ?? false);
    public bool IsAir(ReadOnlyPositionV3<int> position) => TryGet(position, out var element) && (element?.IsAir ?? false);

    public int CountNearAir(ReadOnlyPositionV3<int> position) {
        return position.EnumerateAdjacent()
            .Where(IsAir)
            .Count();
    }

    public int CountNearWater(ReadOnlyPositionV3<int> position) {
        return position.EnumerateAdjacent()
            .Where(IsWater)
            .Count();
    }

    public int CountNearLava(ReadOnlyPositionV3<int> position) {
        return position.EnumerateAdjacent()
            .Where(IsLava)
            .Count();
    }

    private void Flood() {

        Queue<ReadOnlyPositionV3<int>> toVisit = new();
        toVisit.Enqueue(new(0, 0, 0));

        while(toVisit.Count > 0) {

            var curr = toVisit.Dequeue();

            if(this[curr].IsWater) {
                continue;
            }

            this[curr] = new Water();

            curr.EnumerateAdjacent()
                .Where(IsAir)
                .ToList()
                .ForEach(toVisit.Enqueue);
        }
    }

}
