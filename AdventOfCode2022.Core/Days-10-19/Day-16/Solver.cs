namespace AdventOfCode2022.Core.Days_10_19.Day_16;

public class Solver {

    private readonly ValveNetwork _valveNetwork;
    private readonly Dictionary<string, int> _dp = new();

    public Solver(ValveNetwork valveNetwork) {
        _valveNetwork = valveNetwork;
    }

    //Find(30, "", "AA", 0);
    public int Find(int remainingMinutes, string opened, string current, int currPressure) {

        if(_dp.TryGetValue(Build(remainingMinutes, opened, current), out var value)) {
            return value;
        }

        if(_dp.Count > 1000000) {
            _dp.Clear();
        }

        int max = currPressure;
        foreach(var valve in _valveNetwork.ValuableValves.Where(x => !opened.Contains(x.Id))) {

            var walkingTime = _valveNetwork.MoveTime(current, valve.Id);
            if ((walkingTime + 1) > remainingMinutes) {
                continue;
            }

            var newRemMinutes = remainingMinutes - (walkingTime + 1);
            var newOpened = $"{opened}|{valve.Id}{newRemMinutes}|";
            var newPressure = currPressure + (newRemMinutes * valve.FlowRate);
            max = Math.Max(max, Find(newRemMinutes, newOpened, valve.Id, newPressure));
        }

        _dp[Build(remainingMinutes, opened, current)] = max;
        return max;
    }

    //FindMany(26, "", 0, "AA");
    public int FindMany(int remainingMinutes, string opened, Worker first, Worker second, int currPressure) {

        if(_dp.TryGetValue(Build(remainingMinutes, opened, first, second), out var value)) {
            return value;
        }

        if(_dp.Count > 1000000) {
            _dp.Clear();
        }

        if(remainingMinutes < 15 && currPressure < 1000) {
            return 0;
        }

        while(first.TimeToAction != remainingMinutes && second.TimeToAction != remainingMinutes) {
            remainingMinutes--;
        }

        int max = currPressure;
        var unopenedValves = _valveNetwork.ValuableValves.Where(x => !opened.Contains(x.Id));

        foreach(var valve in unopenedValves) {
            
            if(first.TimeToAction == remainingMinutes && second.Location != valve.Id) {

                var walkingTime = _valveNetwork.MoveTime(first.Location, valve.Id);
                if((walkingTime + 1) > remainingMinutes) {
                    continue;
                }

                var newRemMinutes = remainingMinutes - (walkingTime + 1);
                var newOpened = $"{opened}|{valve.Id}{newRemMinutes}|";
                var newPressure = currPressure + (newRemMinutes * valve.FlowRate);
                var newFirstWorker = new Worker(newRemMinutes, valve.Id);
                max = Math.Max(max, FindMany(remainingMinutes, newOpened, newFirstWorker, second, newPressure));
            }

            if(second.TimeToAction == remainingMinutes && first.Location != valve.Id) {

                var walkingTime = _valveNetwork.MoveTime(second.Location, valve.Id);
                if((walkingTime + 1) > remainingMinutes) {
                    continue;
                }

                var newRemMinutes = remainingMinutes - (walkingTime + 1);
                var newOpened = $"{opened}|{valve.Id}{newRemMinutes}|";
                var newPressure = currPressure + (newRemMinutes * valve.FlowRate);
                var newSecondWorker = new Worker(newRemMinutes, valve.Id);
                max = Math.Max(max, FindMany(remainingMinutes, newOpened, first, newSecondWorker, newPressure));

            }

        }

        _dp[Build(remainingMinutes, opened, first, second)] = max;
        return max;
    }

    public struct Worker {
        public Worker(int distanceToValve, string location) {
            TimeToAction = distanceToValve;
            Location = location;
        }

        public int TimeToAction { get; set; }
        public string Location { get; set; }

        public Worker Copy() => new Worker(TimeToAction, Location);
        public override string ToString() => $"{TimeToAction}, {Location}";
    }

    private static string Build(int remainingMinutes, string opened, string current) {
        return $"{remainingMinutes}, {current}, {opened}";
    }

    private static string Build(int remainingMinutes, string opened, Worker first, Worker second) {
        return $"{remainingMinutes}, {first}, {second}, {opened}";
    }

}