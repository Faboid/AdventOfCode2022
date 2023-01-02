namespace AdventOfCode2022.Core.Days_10_19.Day_19;

public class WalletHistory : Wallet {

    private record Time(History History, BotInfo? BotInfo);
    private enum History {
        MinutePasses,
        BuyBot
    }

    private readonly Stack<Time> _history = new();

    public void RerollMinute() {

        var pop = _history.Pop();
        if(pop.History is History.BuyBot) {
            DestroyBot(pop.BotInfo!);
            _ = _history.Pop();
        }

        MinuteBack();
    }

    public override void MinutePasses() {
        _history.Push(new (History.MinutePasses, null));
        base.MinutePasses();
    }

    public override void CreateBot(BotInfo robot) {
        _history.Push(new (History.BuyBot, robot));
        base.CreateBot(robot);
    }

}

public class WalletBuyHistory : Wallet {

    private record Info(BotInfo Robot, int MinutesWaited);
    private readonly Stack<Info> _boughts = new();

    public bool CanBuyBot(BotInfo robot) {
        
        if(robot.OreCost > 0 && OreBots == 0) {
            return false;
        }

        if(robot.ClayCost > 0 && ClayBots == 0) {
            return false;
        }

        if(robot.ObsidianCost > 0 && ObsidianBots == 0) {
            return false;
        }

        return true;
    }

    public int BuyBot(BotInfo robot, int maxMinutes) {
        var minutes = UntilBot(robot) + 1;

        if(minutes > maxMinutes) {
            return int.MaxValue;
        }

        MinutesPasses(minutes);
        CreateBot(robot);
        _boughts.Push(new(robot, minutes));
        return minutes;
    }

    public void SellLast() {
        var info = _boughts.Pop();
        DestroyBot(info.Robot);
        MinutesBack(info.MinutesWaited);
    }

}

public class Wallet {

    public int Ores { get; set; }
    public int Clay { get; set; }
    public int Obsidian { get; set; }
    public int Geodes { get; set; }

    public int OreBots { get; set; } = 1;
    public int ClayBots { get; set; }
    public int ObsidianBots { get; set; }
    public int GeodeBots { get; set; }

    public virtual void MinutesPasses(int minutes) {
        Ores += OreBots * minutes;
        Clay += ClayBots * minutes;
        Obsidian += ObsidianBots * minutes ;
        Geodes += GeodeBots * minutes;
    }

    public virtual void MinutePasses() {
        Ores += OreBots;
        Clay += ClayBots;
        Obsidian += ObsidianBots;
        Geodes += GeodeBots;
    }

    public virtual void MinutesBack(int minutes) {
        Ores -= OreBots * minutes;
        Clay -= ClayBots * minutes;
        Obsidian -= ObsidianBots * minutes;
        Geodes -= GeodeBots * minutes;
    }

    public virtual void MinuteBack() {
        Ores -= OreBots;
        Clay -= ClayBots;
        Obsidian -= ObsidianBots;
        Geodes -= GeodeBots;
    }

    public int UntilBot(BotInfo robot) {
        int max = 0;

        if(robot.OreCost > 0) {
            max = Math.Max(max, (Ores - robot.OreCost) / OreBots);
        }

        if(robot.ClayCost > 0) {
            max = Math.Max(max, (Clay - robot.ClayCost) / ClayBots);
        }

        if(robot.ObsidianCost > 0) {
            max = Math.Max(max, Obsidian - robot.ObsidianCost) / ObsidianBots;
        }

        return max;
    }

    public bool AffordBot(BotInfo robot) {
        return
            Ores >= robot.OreCost &&
            Clay >= robot.ClayCost &&
            Obsidian >= robot.ObsidianCost;
    }

    public virtual void CreateBot(BotInfo robot) {

        Ores -= robot.OreCost;
        Clay -= robot.ClayCost;
        Obsidian -= robot.ObsidianCost;

        switch(robot.ResourceType) {
            case ResourceType.Ore:
                OreBots++;
                break;
            case ResourceType.Clay:
                ClayBots++;
                break;
            case ResourceType.Obsidian:
                ObsidianBots++;
                break;
            case ResourceType.Geode:
                GeodeBots++;
                break;
        };

    }

    public virtual void DestroyBot(BotInfo robot) {

        Ores += robot.OreCost;
        Clay += robot.ClayCost;
        Obsidian += robot.ObsidianCost;

        switch(robot.ResourceType) {
            case ResourceType.Ore:
                OreBots--;
                break;
            case ResourceType.Clay:
                ClayBots--;
                break;
            case ResourceType.Obsidian:
                ObsidianBots--;
                break;
            case ResourceType.Geode:
                GeodeBots--;
                break;
        };

    }

    public Wallet Clone() {
        return new Wallet() {
            Ores = Ores,
            Clay = Clay,
            Obsidian = Obsidian,
            Geodes = Geodes,

            OreBots = OreBots,
            ClayBots = ClayBots,
            ObsidianBots = ObsidianBots,
            GeodeBots = GeodeBots,
        };
    }

    public override string ToString() {
        return $"{Ores}, {Clay}, {Obsidian}, {Geodes} - {OreBots}, {ClayBots}, {ObsidianBots}, {GeodeBots}";
    }
}