using System.Text;

namespace AdventOfCode2022.Core.Days_10_19.Day_15;

public class SensorGrid {

    private readonly List<Sensor> _sensors;
    private readonly LateGridArray<SensorSign> _grid;
    private readonly int _minY;

    public SensorGrid(IEnumerable<Sensor> sensors) {
        _sensors = sensors.ToList();

        var minX = _sensors.Min(x => x.Position.X);
        var minY = _sensors.Min(x => x.Position.Y);
        var maxX = _sensors.Max(x => x.Position.X);
        var maxY = _sensors.Max(x => x.Position.Y);

        //add the limits of positions detected
        var maxDetection = _sensors.Max(x => x.DetectionRadius);
        minX -= maxDetection;
        minY -= maxDetection;
        maxX += maxDetection;
        maxY += maxDetection;

        var width = maxX - minX;
        var height = maxY - minY;

        _grid = new(minX, minY, width + 1, height + 1, SensorSign.Empty);

        foreach(var sensor in _sensors) {

            _grid[sensor.Position] = SensorSign.Sensor;
            _grid[sensor.FoundBeacon.Position] = SensorSign.Beacon;

            foreach(var position in sensor.EnumerateDetectedPositions()) {
                if(_grid[position] is SensorSign.Empty) {
                    _grid[position] = SensorSign.Detected;
                }
            }
        }

    }

    public int GetDetectedEmptyInRow(int row) {

        if(!_grid.HasRow(row)) {
            return 0;
        }

        return _grid[row].Count(x => x is SensorSign.Detected);

    }

    public override string ToString() {
        StringBuilder sb = new();

        int rowCount = _minY;
        foreach(var row in _grid.GetRows()) {
            sb.Append($"{rowCount:000}");

            foreach(var cell in row) {
                sb.Append((char)cell);
            }

            rowCount++;
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
