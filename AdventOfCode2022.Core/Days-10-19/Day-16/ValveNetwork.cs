using System.Diagnostics;

namespace AdventOfCode2022.Core.Days_10_19.Day_16;

public class ValveNetwork {

    public ValveNetwork(IEnumerable<Valve> valves) {
        Valves = valves.ToDictionary(x => x.Id, y => y);
        ValuableValves = Valves.Values.Where(x => x.FlowRate > 0).ToList();
        CalculateTimes();
    }

    private record WalkTime(Dictionary<string, int> Times);

    public Dictionary<string, Valve> Valves { get; init; } = new();
    public List<Valve> ValuableValves { get; init; }
    public Valve this[string id] => Valves[id];

    private readonly Dictionary<string, WalkTime> _walkTimes = new();

    public int MoveTime(string from, string to) => _walkTimes[from].Times[to];

    private void CalculateTimes() {

        //to speed up the process, calculate only valuable valves' paths
        foreach(var fromValve in ValuableValves.Concat(new Valve[] { Valves["AA"]})) {

            var dictionary = new Dictionary<string, int>();

            foreach(var toValve in ValuableValves) {

                if(fromValve.Id == toValve.Id) {
                    continue;
                }

                dictionary.Add(toValve.Id, CalculateWalkTime(fromValve, toValve, new()) ?? throw new UnreachableException());

            }

            _walkTimes.Add(fromValve.Id, new WalkTime(dictionary));

        }

    }

    private int? CalculateWalkTime(Valve from, Valve to, HashSet<string> path) {

        var times = new List<int?>();
        foreach(var connectedValve in from.Connections) {

            var valve = Valves[connectedValve];
            if(valve.Id == to.Id) {
                times.Add(1);
                continue;
            }

            var pathCopy = path.ToHashSet();
            if(pathCopy.Add(valve.Id)) {
                var time = CalculateWalkTime(valve, to, pathCopy);
                times.Add(time + 1);
            }

        }

        times = times.Where(x => x is not null).ToList();

        if(times.Count == 0) {
            return null;
        }

        return times.Min();

    }

}
