namespace AdventOfCode2022.Core.Days_10_19.Day_15;

public class Sensor {
    public Sensor(ReadOnlyPosition position, Beacon foundBeacon) {
        Position = position;
        FoundBeacon = foundBeacon;
    }

    public ReadOnlyPosition Position { get; init; }
    public Beacon FoundBeacon { get; init; }
    public int DetectionRadius => Math.Abs(Position.X - FoundBeacon.Position.X) + Math.Abs(Position.Y - FoundBeacon.Position.Y);

    public Range GetDetectedRowRange(int row) {

        var minY = Position.Y - DetectionRadius;
        var maxY = Position.Y + DetectionRadius;

        if(minY > row || row > maxY) {
            return Range.Empty();
        }

        var distanceFromSensor = Math.Abs(row - Position.Y);
        var xOffSet = Math.Abs(DetectionRadius - distanceFromSensor);

        return new Range(Position.X - xOffSet, Position.X + xOffSet);

        //return Enumerable.Range(xOffSet, (distanceFromSensor * 2) + 1);

    }

    public IEnumerable<int> EnumerateDetectedColumnsInRow(int row) {

        var minY = Position.Y - DetectionRadius;
        var maxY = Position.Y + DetectionRadius;

        if(minY > row || row > maxY) {
            yield break;
            
            //return Array.Empty<int>();
        }

        foreach(var pos in EnumerateDetectedPositions()) {
            if(pos.Y == row) {
                yield return pos.X;
            }
        }

        //return EnumerateDetectedPositions().Where(x => x.Y == row).Select(x => x.X);

        //var distanceFromSensor = Math.Abs(row - Position.Y);
        //var xOffSet = Math.Abs(Position.X - distanceFromSensor);

        //return Enumerable.Range(xOffSet, (distanceFromSensor * 2) + 1);

    } 

    /// <summary>
    /// Returns all positions detected by the sensor. Includes the sensor's and beacon's positions.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ReadOnlyPosition> EnumerateDetectedPositions() {

        //return all positions in the middle axis
        for(int y = -DetectionRadius; y <= DetectionRadius; y++) {
            yield return new ReadOnlyPosition(Position.X, Position.Y + y);
        }

        int xOffSet = 1;
        int yOffSet = 1;

        while(xOffSet < DetectionRadius && yOffSet < DetectionRadius) {

            var height = DetectionRadius - yOffSet;

            for(int y = -height; y <= height; y++) {
                yield return new ReadOnlyPosition(Position.X - xOffSet, Position.Y + y);
                yield return new ReadOnlyPosition(Position.X + xOffSet, Position.Y + y);
            }

            xOffSet++;
            yOffSet++;

        }

    }

    //input example:
    //Sensor at x=2, y=18: closest beacon is at x=-2, y=15
    public static Sensor Parse(string input) {

        var values = input
            .Split(new char[] { '=', ',', ' ', ':' }, StringSplitOptions.RemoveEmptyEntries)
            .Where(x => int.TryParse(x, out var _)) //skip x= and y=
            .Select(int.Parse)
            .ToArray();

        var sensorPosition = new ReadOnlyPosition(values[0], values[1]);
        var beaconPosition = new ReadOnlyPosition(values[2], values[3]);

        return new(sensorPosition, new Beacon(beaconPosition));

    }

}
