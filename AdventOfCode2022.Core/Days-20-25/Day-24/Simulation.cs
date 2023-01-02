namespace AdventOfCode2022.Core.Days_20_25.Day_24;

public class Simulation {

    private readonly Map _map;
    private readonly List<Blizzard> _blizzards;

    public Simulation(Map map, List<Blizzard> blizzards) {
        _map = map;
        _blizzards = blizzards;
    }

    public int CalculateBackAndForth() {

        var entrance = _map.EntrancePosition;
        var exit = _map.ExitPosition;
        var blizzards = _blizzards.Select(x => x.Clone()).ToList();
        int totalSteps = 0;

        //entrance -> exist
        totalSteps += CalculateSteps(entrance, exit, blizzards);

        //exit -> entrance
        totalSteps += CalculateSteps(exit, entrance, blizzards);

        //entrance -> exit
        totalSteps += CalculateSteps(entrance, exit, blizzards);

        return totalSteps;

    }

    public int CalculateSteps(ReadOnlyPosition from, ReadOnlyPosition to, List<Blizzard> currBlizzards) {

        var travelers = new List<Traveler>() {
            new Traveler(from)
        };

        while(!travelers.Any(x => x.Position == to)) {
            travelers = CompleteStep(travelers, currBlizzards);

        }

        var minimumCount = travelers.Where(x => x.Position == to).Min(x => x.Steps);
        return minimumCount;

    }

    public int CalculateStepsToExit() {
        return CalculateSteps(_map.EntrancePosition, _map.ExitPosition, _blizzards.Select(x => x.Clone()).ToList());
    }

    private List<Traveler> CompleteStep(List<Traveler> travelers, List<Blizzard> blizzards) {

        //get all possible paths for every single traveler
        var newTravelerLocations = travelers.Select(x => {
            var directions = (Direction[])Enum.GetValues(typeof(Direction));
            return directions.Select(dir => x.Clone().Move(dir));
        }).SelectMany(x => x).ToArray();

        //before adding the taken paths, force the existing travelers to wait
        travelers.ForEach(x => x.Wait());
        travelers.AddRange(newTravelerLocations);

        //remove duplicates
        travelers = travelers.GroupBy(x => x.Position).Select(x => x.First()).ToList();

        //move all blizzards
        blizzards.ForEach(x => x.Move());
        blizzards.ForEach(_map.WallWarping);
        var killZones = blizzards.GroupBy(x => x.Position).Select(x => x.First()).Select(x => x.Position).ToHashSet();

        //kill off all travelers that stand in blizzards
        var deadTravelers = travelers.Where(x => killZones.Contains(x.Position) || _map.IsOutOfBoundsOrInWalls(x.Position)).ToList();
        deadTravelers.ForEach(x => travelers.Remove(x));

        if(travelers.Count == 0) {
            //everyone's dead
            throw new ArgumentException("Everyone's dead.");
        }

        return travelers;
    }

    public static Simulation Parse(string input) {

        var rows = input.Split("\r\n");

        var height = rows.Length - 1;
        var width = rows[0].Length - 1;
        var entry = new ReadOnlyPosition(1, 0);
        var exit = new ReadOnlyPosition(width - 1, height);

        var map = new Map(new(0, height), new(0, width), entry, exit);

        var blizzards = new List<Blizzard>();

        for(int y = 0; y <= height; y++) {
            for(int x = 0; x <= width; x++) {

                if(rows[y][x] is '#' or '.') {
                    continue;
                }

                var direction = (Direction)rows[y][x];
                blizzards.Add(new Blizzard(direction, new Position(x, y)));
            }
        }

        return new Simulation(map, blizzards);

    }

}
