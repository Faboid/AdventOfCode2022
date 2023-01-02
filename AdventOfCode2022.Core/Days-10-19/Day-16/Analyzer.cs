using System.Text;

namespace AdventOfCode2022.Core.Days_10_19.Day_16;

public class Analyzer {

    private readonly ValveNetwork _valveNetwork;
    private readonly Dictionary<string, int> _dp = new();

    public Analyzer(ValveNetwork valveNetwork) {
        _valveNetwork = valveNetwork;
    }
    
    public int GetMaxPressure(int remainingMinutes, params string[] startingLocations) {
        int workerId = 0;
        var workers = startingLocations.Select(x => new Worker(_valveNetwork, (workerId++).ToString(), x)).ToList();
        var searchers = new Searchers(remainingMinutes, workers, _valveNetwork);
        return GetMaxPressureInternal(searchers);
    }

    private int GetMaxPressureInternal(Searchers curr) {

        if(_dp.Count > 10000000) {
            _dp.Clear();
        }

        if(curr.MinutesLeft < 18 && curr.FlowRate < 50) {
            return 0;
        }

        while(curr.AllBusy() && curr.MinutesLeft > 0) {
            curr.MinutePass();
        }

        if(curr.MinutesLeft <= 0) {
            return curr.Pressure;
        }

        if(_dp.TryGetValue(curr.Key, out var value)) {
            return value;
        }

        int maxPressure = 0;

        //start set up open tasks region
        HashSet<string> _workersThatOpen = new();
        foreach(var searcher in curr.Workers) {
            if(searcher.IsBusy() || curr.HasOpened(searcher.Location) || _valveNetwork[searcher.Location].FlowRate == 0) {
                continue;
            }

            _workersThatOpen.Add(searcher.Location);
        }

        if(_workersThatOpen.Count > 0) {

            var clone = curr.Clone();

            foreach(var toOpen in _workersThatOpen) {
                var worker = clone.Workers.First(x => !x.IsBusy() && x.IsIn(toOpen));
                clone.Open(worker.Id, toOpen);
            }

            maxPressure = Math.Max(maxPressure, GetMaxPressureInternal(clone));
        }
        //end set up open tasks region

        foreach(var destination in _valveNetwork.ValuableValves.Where(valve => curr.Workers.All(x => !x.IsIn(valve.Id)) && !curr.HasOpened(valve.Id))) {

            foreach(var worker in curr.Workers) {

                if(worker.IsBusy()) {
                    continue;
                }

                var clone = curr.Clone();
                clone.MoveTo(worker.Id, destination.Id);
                maxPressure = Math.Max(maxPressure, GetMaxPressureInternal(clone));

            }

        }

        if(maxPressure == 0) {
            _dp[curr.Key] = curr.Pressure;
            curr.WaitAndComplete();
            _dp[curr.Key] = curr.Pressure;
            return _dp[curr.Key];
        }

        _dp[curr.Key] = maxPressure;
        return _dp[curr.Key];
    }

    private class Searchers {

        private readonly ValveNetwork _valveNetwork;

        public Searchers(int minutesLeft, List<Worker> searchers, ValveNetwork valveNetwork) {
            MinutesLeft = minutesLeft;
            Workers = searchers;
            _valveNetwork = valveNetwork;
        }

        public List<Worker> Workers;
        public int MinutesLeft { get; private set; } = 0;
        public int Pressure { get; private set; } = 0;
        public int FlowRate { get; private set; } = 0;
        public string OpenValves { get; private set; } = "";

        private int _extraFlowFromNextMinute = 0;

        public void WaitAndComplete() {
            while(MinutesLeft > 0) {
                MinutePass();
            }
        }

        public bool HasOpened(string id) => OpenValves.Contains(id);

        public void Open(string workerId, string valveId) {

            if(HasOpened(valveId)) {
                throw new ArgumentException("This valve is already open.");
            }

            OpenValves += ($",{valveId},");
            Workers.Single(x => x.Id == workerId).OpenBusy();
            _extraFlowFromNextMinute += _valveNetwork[valveId].FlowRate;

        }

        public void MoveTo(string workerId, string valveId) {
            Workers.First(x => x.Id == workerId).MoveTo(valveId);
        }

        public void MinutePass() {
            MinutesLeft--;
            Pressure += FlowRate;
            FlowRate += _extraFlowFromNextMinute;
            _extraFlowFromNextMinute = 0;
            Workers.ForEach(x => x.OnMinutePasses());
        }

        public bool AllBusy() => Workers.All(x => x.IsBusy());
        
        public string Key => $"({Workers.Select(x => x.Key).OrderBy(x => x).Aggregate(new StringBuilder(), (a, b) => a.Append(b))} - {MinutesLeft}), {OpenValves}";

        public Searchers Clone() {
            return new Searchers(MinutesLeft, Workers.Select(x => x.Clone()).ToList(), _valveNetwork) {
                Pressure = Pressure,
                FlowRate = FlowRate,
                OpenValves = OpenValves
            };
        }
    }

    private class Worker {

        private readonly ValveNetwork _valveNetwork;

        public string Id { get; init; }
        public string Location { get; private set; }

        public Worker(ValveNetwork valveNetwork, string id, string location) {
            _valveNetwork = valveNetwork;
            Id = id;
            Location = location;
        }

        public string Key => $"[{Location}, {BusyFor}]";
        public bool CanMove => BusyFor == 0;

        private int BusyFor = 0;
        private bool _walking = false;
        private string _isGoingTo = "";

        public string IsGoingTo() => _isGoingTo;
        public int DistanceFrom(string valveId) => _valveNetwork.MoveTime(Location, valveId); 
        public bool IsIn(string location) => !_walking && location == Location;
        public bool IsBusy() => !CanMove;

        public void OpenBusy() {

            if(BusyFor > 0) {
                throw new Exception("This worker is busy!");
            }

            BusyFor = 1;
        }

        public void MoveTo(string id) {
            var minutes = _valveNetwork.MoveTime(Location, id);
            BusyFor += minutes;
            Location = id;
            _walking = true;
            _isGoingTo = id;
        }

        public void OnMinutePasses() {
            if(BusyFor > 0) {
                BusyFor--;
            }

            if(BusyFor == 0) {
                _walking = false;
                _isGoingTo = "";
            }
        }

        public Worker Clone() {
            return new Worker(_valveNetwork, Id, Location) { 
                BusyFor = BusyFor, 
                _isGoingTo = _isGoingTo,
                _walking = _walking
            };
        }
    }

}