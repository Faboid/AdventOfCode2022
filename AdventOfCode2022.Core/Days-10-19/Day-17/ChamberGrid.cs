using AdventOfCode2022.Core.Shared;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCode2022.Core.Days_10_19.Day_17;

public class ChamberGrid {

    //0 == floor, _grid.Length == open ceiling
    //bool == notempty
    private readonly List<bool[]> _grid;
    private readonly Downgrader<long> _downGrader = new(0);
    private long _floorHeight = 0;
    public int ChamberWidth { get; init; }

    public ChamberGrid(int chamberWidth) {
        ChamberWidth = chamberWidth;

        //set up floor
        _grid = new() {
            new bool[ChamberWidth].Select(x => true).ToArray()
        };
    }

    public IEnumerable<ReadOnlyPosition<long>> EnumerateRocksAbove(long height) {
        height -= _floorHeight;
        height = Math.Max(height, 0); //prevent asking under 0
        for(int y = (int)height; y < _grid.Count; y++) {
            for(int x = 0; x < ChamberWidth; x++) {
                if(_grid[y][x]) {
                    yield return new(x, y + _floorHeight);
                }
            }
        }
    }

    public long[] TopRocks() {
        long[] top = new long[ChamberWidth];

        for(int x = 0; x < ChamberWidth; x++) {

            for(int y = _grid.Count - 1; y >= 0; y--) {

                if(GetOrAdd(y)[x]) {
                    top[x] = y;
                    break;
                }
            }
        } 

        return top;
    }


    public long GetTowerHeight() => _grid.FindLastIndex(x => x.Any(x => x)) + _floorHeight;
    public bool HasRock(ReadOnlyPosition<long> pos) => GetOrAdd((int)_downGrader.Evaluate(pos.Y))[pos.X];
    public void AddRock(ReadOnlyPosition<long> pos) => GetOrAdd((int)_downGrader.Evaluate(pos.Y))[pos.X] = true;

    public bool[] GetOrAdd(int height) {
        if(height < 0) {
            throw new ArgumentOutOfRangeException("Height cannot be less than 0.");
        }

        while(_grid.Count <= height) {
            _grid.Add(new bool[ChamberWidth]);
        }

        return _grid[height];
    }

    /// <summary>
    /// Searches the highest fully-formed floor and deletes everything underneath.
    /// </summary>
    public void RefreshFloor() {

        var height = _grid.FindLastIndex(x => x.All(x => x));
        if(height <= 0) {
            //not found
            return;
        }

        var listCopy = _grid.Take(height).ToArray();
        foreach(var copy in listCopy) {
            _grid.Remove(copy);
        }

        _floorHeight += height;
        _downGrader.StartIndex = _floorHeight;

    }

    public string GetUpperCycleString() {
        var floor = TopRocks().Min();
        StringBuilder sb = new();

        for(int y = (int)floor; y < _grid.Count; y++) {
            for(int x = 0; x < ChamberWidth; x++) {
                if(_grid[y][x]) {
                    sb.Append('#');
                } else {
                    sb.Append('.');
                }
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    public override string ToString() {
        StringBuilder sb = new();

        for(int y = _grid.Count - 1; y >= 0; y--) {
            _grid[y].Select(x => x ? '#' : '.').Aggregate(sb, (a, b) => a.Append(b));
            sb.AppendLine();
        }

        return sb.ToString();
    }

}
