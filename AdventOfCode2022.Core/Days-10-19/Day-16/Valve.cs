using System.Diagnostics;

namespace AdventOfCode2022.Core.Days_10_19.Day_16;

[DebuggerDisplay("Id: {Id}, FlowRate: {FlowRate}")]
public readonly struct Valve {

    public required string Id { get; init; }
    public required int FlowRate { get; init; }
    public required IReadOnlyList<string> Connections { get; init; }

    //example: Valve QP has flow rate=0; tunnels lead to valves IS, DG
    public static Valve Parse(string input) {

        var split = input.Split(new string[] { " ", "Valve", "has flow rate", "=", ";", "tunnels lead to valves", "," }, StringSplitOptions.RemoveEmptyEntries);

        var id = split[0];
        var flowRate = int.Parse(split[1]);
        var connections = split[2..].Where(x => x.Length == 2 && x.All(x => x >= 'A' && x <= 'Z')).ToArray();

        return new Valve {
            Id = id,
            FlowRate = flowRate,
            Connections = connections
        };

    }

}