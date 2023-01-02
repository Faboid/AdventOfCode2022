using System.Text;

namespace AdventOfCode2022.Core.Days_10_19.Day_16;

public class Simulator {

    private readonly ValveNetwork _valveNetwork;
    private readonly Dictionary<string, int> _dp = new();

    public Simulator(ValveNetwork valveNetwork) {
        _valveNetwork = valveNetwork;
    }

    public string GetBestPath() => _dp.Keys.MaxBy(x => _dp[x]) ?? throw new Exception();

    public int GetMaxPressure(int remainingMinutes, string startingLocation) {

        if(!_valveNetwork.Valves.Values.Any(x => x.Id == startingLocation)) {
            throw new ArgumentException("The given starting location does not exist.");
        }

        var output = GetMaxPressureInternal(new Simulation(remainingMinutes, startingLocation, _valveNetwork));
        return output;
    }

    private int GetMaxPressureInternal(Simulation curr) {

        if(_dp.TryGetValue(curr.Key, out var value)) {
            return value;
        }

        if(curr.MinutesLeft <= 0) {
            return curr.Pressure;
        }

        List<int> maxPressure = new();

        //if the current valve is not open
        if(!curr.HasOpened(curr.Location) && _valveNetwork[curr.Location].FlowRate > 0) {
            var clone = curr.Clone();
            clone.Open(_valveNetwork[curr.Location]);
            maxPressure.Add(GetMaxPressureInternal(clone));
        }

        //visit all possible destinations
        foreach(var destination in _valveNetwork.ValuableValves.Where(x => x.Id != curr.Location && !curr.HasOpened(x.Id))) {
            var clone = curr.Clone();
            clone.MoveTo(destination.Id);
            maxPressure.Add(GetMaxPressureInternal(clone));
        }

        if(maxPressure.Count == 0) {
            curr.WaitAndComplete();
            _dp[curr.Key] = curr.Pressure;
            return _dp[curr.Key];
        }

        _dp[curr.Key] = maxPressure.Max();
        return _dp[curr.Key];

    }

    private class Simulation {

        private readonly ValveNetwork _valveNetwork;

        public Simulation(int minutesLeft, string currentLocation, ValveNetwork valveNetwork) {
            MinutesLeft = minutesLeft;
            Location = currentLocation;
            _valveNetwork = valveNetwork;
        }

        public int MinutesLeft { get; private set; } = 0;
        public int Pressure { get; private set; } = 0;
        public int FlowRate { get; private set; } = 0;
        public HashSet<string> OpenValves = new();
        public string Location { get; set; }

        public string Key => $"({Location} - {MinutesLeft}), {OpenValves.Aggregate(new StringBuilder(), (a, b) => a.Append(b))}";

        public bool HasOpened(string id) => OpenValves.Contains(id);

        public void WaitAndComplete() {
            OnMinutesPasses(MinutesLeft);
        }

        public void Open(Valve valve) {

            if(HasOpened(valve.Id)) {
                throw new ArgumentException("This valve is already open.");
            }

            OpenValves.Add(valve.Id);
            OnMinutesPasses(1);

            //flowrate increases after the minute ends
            FlowRate += valve.FlowRate;
        }

        public void MoveTo(string id) {
            var minutes = _valveNetwork.MoveTime(Location, id);
            OnMinutesPasses(minutes);
            Location = id;
        }

        public Simulation Clone() => new(MinutesLeft, Location, _valveNetwork) { Pressure = Pressure, FlowRate = FlowRate, OpenValves = OpenValves.ToHashSet() };

        private void OnMinutesPasses(int minutes = 1) {
            while(minutes > 0 && MinutesLeft > 0) {
                MinutesLeft--;
                minutes--;
                Pressure += FlowRate;
            }
        }

    }

}
