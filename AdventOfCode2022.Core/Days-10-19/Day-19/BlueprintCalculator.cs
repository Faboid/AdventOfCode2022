namespace AdventOfCode2022.Core.Days_10_19.Day_19;

public class BlueprintCalculator {

    private readonly Blueprint _blueprint;
    private readonly Balancer _balancer;
    private readonly int _minutes;
    private int? _qualityLevel;
    private int? _maxGeodes;

    private readonly Dictionary<string, int> _maxGeodesPerPath = new();
    private readonly Dictionary<int, int> _maxGeodesPerMinute = new();

    public BlueprintCalculator(Blueprint blueprint, int minutes) {
        _blueprint = blueprint;
        _balancer = new(blueprint);
        _minutes = minutes;
    }

    public int GetQualityLevel() {

        if(_qualityLevel is not null) {
            return (int)_qualityLevel;
        }

        _maxGeodes = CalculateMaxGeodes(_minutes, new());
        _qualityLevel = _maxGeodes * _blueprint.Id;
        _maxGeodesPerPath.Clear();
        return (int)_qualityLevel;

    }

    public int GetMaxGeodes() {

        if(_maxGeodes is not null) {
            return (int)_maxGeodes;
        }

        _maxGeodes = CalculateMaxGeodes(_minutes, new());
        _maxGeodesPerMinute.Clear();
        _maxGeodesPerPath.Clear();
        return (int)_maxGeodes;

    }

    private int CalculateMaxGeodes(int remainingMinutes, WalletHistory wallet) {

        if(_maxGeodesPerMinute.TryGetValue(remainingMinutes, out var maxGeodes)) {
            if(wallet.Geodes < (maxGeodes / 2) - 5) {
                return wallet.Geodes;
            }
        }

        if(wallet.Geodes > maxGeodes) {
            _maxGeodesPerMinute[remainingMinutes] = wallet.Geodes;
        }

        if(remainingMinutes == 0) {
            return wallet.Geodes;
        }

        var dpKey = BuildDPKey(remainingMinutes, wallet);
        if(_maxGeodesPerPath.TryGetValue(dpKey, out var value)) {
            return value;
        }

        if(_maxGeodesPerPath.Count > 100000000) {
            _maxGeodesPerPath.Clear();
        }

        int max = wallet.Geodes;

        if(wallet.AffordBot(_blueprint.GeodeBot)) {
            wallet.MinutePasses();
            wallet.CreateBot(_blueprint.GeodeBot);
            max = Math.Max(max, CalculateMaxGeodes(remainingMinutes - 1, wallet));
            wallet.RerollMinute();
        }

        if(wallet.AffordBot(_blueprint.ObsidianBot)) {
            wallet.MinutePasses();
            wallet.CreateBot(_blueprint.ObsidianBot);
            max = Math.Max(max, CalculateMaxGeodes(remainingMinutes - 1, wallet));
            wallet.RerollMinute();
        }

        if(_balancer.ShouldMakeClay(wallet, remainingMinutes) && wallet.AffordBot(_blueprint.ClayBot)) {
            wallet.MinutePasses();
            wallet.CreateBot(_blueprint.ClayBot);
            max = Math.Max(max, CalculateMaxGeodes(remainingMinutes - 1, wallet));
            wallet.RerollMinute();
        }

        if(_balancer.ShouldMakeOre(wallet, remainingMinutes) && wallet.AffordBot(_blueprint.OreBot)) {
            wallet.MinutePasses();
            wallet.CreateBot(_blueprint.OreBot);
            max = Math.Max(max, CalculateMaxGeodes(remainingMinutes - 1, wallet));
            wallet.RerollMinute();
        }

        //wait
        wallet.MinutePasses();
        max = Math.Max(max, CalculateMaxGeodes(remainingMinutes - 1, wallet));
        wallet.RerollMinute();
        
        _maxGeodesPerPath[dpKey] = max;
        return max;

    }

    private static string BuildDPKey(int remainingMinutes, Wallet wallet) {
        return $"{remainingMinutes}, {wallet}";
    }

}