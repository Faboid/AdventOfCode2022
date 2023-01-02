namespace AdventOfCode2022.Core.Days_10_19.Day_19;

public class BotInfo {

    public ResourceType ResourceType { get; init; }

    public BotInfo(ResourceType resourceType) {
        ResourceType = resourceType;
    }

    public int OreCost { get; init; }
    public int ClayCost { get; init; }
    public int ObsidianCost { get; init; }

}