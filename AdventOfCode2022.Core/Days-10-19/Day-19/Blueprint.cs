namespace AdventOfCode2022.Core.Days_10_19.Day_19;

public class Balancer {

    private readonly Blueprint _blueprint;

    private readonly int _maxOreCost;
    private readonly int _maxClayCost;

    public Balancer(Blueprint blueprint) {
        _blueprint = blueprint;

        _maxOreCost = _blueprint.EnumerateBots().Max(x => x.OreCost);
        _maxClayCost = _blueprint.EnumerateBots().Max(x => x.ClayCost);
    }

    public bool ShouldMakeClay(Wallet wallet, int timeLeft) {
        return ShouldMake(wallet.ClayBots, timeLeft, wallet.Clay, _maxClayCost);
    }

    public bool ShouldMakeOre(Wallet wallet, int timeLeft) {
        return ShouldMake(wallet.OreBots, timeLeft, wallet.Ores, _maxOreCost);
    }

    public bool ShouldMake(int robots, int time, int stock, int maxCost) {
        var shouldNot = (robots * time) + stock >= time * maxCost;
        return !shouldNot;
    }

}

public class Blueprint {
    public Blueprint(int id, BotInfo oreBot, BotInfo clayBot, BotInfo obsidianBot, BotInfo geodeBot) {
        Id = id;
        OreBot = oreBot;
        ClayBot = clayBot;
        ObsidianBot = obsidianBot;
        GeodeBot = geodeBot;

        ExpectedOreBots = 20;//(30 / oreBot.OreCost) + 1;
        ExpectedClayBots = 20;//(30 / clayBot.OreCost) + 1;
        ExpectedObsidianBots = 20;//(30 / obsidianBot.OreCost) + 1;
    }

    public int Id { get; set; }
    public BotInfo OreBot { get; init; }
    public BotInfo ClayBot { get; init; }
    public BotInfo ObsidianBot { get; init; }
    public BotInfo GeodeBot { get; init; }

    public int ExpectedOreBots { get; init; }
    public int ExpectedClayBots { get; init; }
    public int ExpectedObsidianBots { get; init; }

    public IEnumerable<BotInfo> EnumerateBots() {
        yield return OreBot;
        yield return ClayBot; 
        yield return ObsidianBot;
        yield return GeodeBot;
    }

    public static Blueprint Parse(string input) {

        var split = input
            .Split(new char[] { ' ', ':' })
            .Select(x => (Success: int.TryParse(x, out var value), Value: value))
            .Where(x => x.Success)
            .Select(x => x.Value)
            .ToArray();

        var id = split[0];
        var oreBot = new BotInfo(ResourceType.Ore) { OreCost = split[1] };
        var clayBot = new BotInfo(ResourceType.Clay) { OreCost = split[2] };
        var obsidianBot = new BotInfo(ResourceType.Obsidian) { OreCost = split[3], ClayCost = split[4] };
        var geodeBot = new BotInfo(ResourceType.Geode) { OreCost = split[5], ObsidianCost = split[6] };

        return new(id, oreBot, clayBot, obsidianBot, geodeBot);

    }

}