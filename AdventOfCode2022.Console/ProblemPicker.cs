using AdventOfCode2022.Core;
using AdventOfCode2022.Core.Day_1;
using AdventOfCode2022.Core.Day_2;
using AdventOfCode2022.Core.Day_3;
using AdventOfCode2022.Core.Day_4;
using AdventOfCode2022.Core.Day_5;
using AdventOfCode2022.Core.Day_6;
using AdventOfCode2022.Core.Day_7;
using AdventOfCode2022.Core.Day_8;
using AdventOfCode2022.Core.Day_9;
using AdventOfCode2022.Core.Days_10_19.Day_10;
using AdventOfCode2022.Core.Days_10_19.Day_11;
using AdventOfCode2022.Core.Days_10_19.Day_12;
using AdventOfCode2022.Core.Days_10_19.Day_13;
using AdventOfCode2022.Core.Days_10_19.Day_14;
using AdventOfCode2022.Core.Days_10_19.Day_15;
using AdventOfCode2022.Core.Days_10_19.Day_16;
using AdventOfCode2022.Core.Days_10_19.Day_17;
using AdventOfCode2022.Core.Days_10_19.Day_18;
using AdventOfCode2022.Core.Days_10_19.Day_19;
using AdventOfCode2022.Core.Days_20_25.Day_20;
using AdventOfCode2022.Core.Days_20_25.Day_21;
using AdventOfCode2022.Core.Days_20_25.Day_22;
using AdventOfCode2022.Core.Days_20_25.Day_23;
using AdventOfCode2022.Core.Days_20_25.Day_24;
using AdventOfCode2022.Core.Days_20_25.Day_25;

namespace AdventOfCode2022.Console;

public class ProblemPicker {

    private static readonly Dictionary<int, Dictionary<int, IProblem>> _problemPicker;

    static ProblemPicker() {

        _problemPicker = new();
        var dict2022 = new Dictionary<int, IProblem>();
        _problemPicker.Add(2022, dict2022);

        dict2022.Add(1, new CalorieCounting());
        dict2022.Add(2, new RockPaperScissors());
        dict2022.Add(3, new RucksackReorganization());
        dict2022.Add(4, new CampCleanup());
        dict2022.Add(5, new SupplyStacks());
        dict2022.Add(6, new TuningTrouble());
        dict2022.Add(7, new NoSpaceLeftOnDevice());
        dict2022.Add(8, new TreeTopTreeHouse());
        dict2022.Add(9, new RopeBridge());
        dict2022.Add(10, new CathodeRayTube());
        dict2022.Add(11, new MonkeyInTheMiddle());
        dict2022.Add(12, new HillClimbingAlgorithm());
        dict2022.Add(13, new DistressSignal());
        dict2022.Add(14, new RegolithReservoir());
        dict2022.Add(15, new BeaconExclusionZone());
        dict2022.Add(16, new ProboscideaVolcanium());
        dict2022.Add(17, new PyroclasticFlow());
        dict2022.Add(18, new BoilingBoulders());
        dict2022.Add(19, new NotEnoughMinerals());
        dict2022.Add(20, new GrovePositioningSystem());
        dict2022.Add(21, new MonkeyMath());
        dict2022.Add(22, new MonkeyMap());
        dict2022.Add(23, new UnstableDiffusion());
        dict2022.Add(24, new BlizzardBasin());
        dict2022.Add(25, new FullOfHotAir());

    }

    public static IProblem GetProblem() {

        System.Console.WriteLine("Please choose a year.");
        var year = System.Console.ReadLine();
        var yearAsInt = int.Parse(year!);
        var chosenYear = _problemPicker[yearAsInt];
        System.Console.WriteLine("Please choose a day.");
        var day = System.Console.ReadLine();
        var dayAsInt = int.Parse(day!);
        return chosenYear[dayAsInt];

    }

    public static IProblem GetProblem2022() {

        var chosenYear = _problemPicker[2022];
        System.Console.WriteLine("Please choose a day.");
        var day = System.Console.ReadLine();
        var dayAsInt = int.Parse(day!);
        return chosenYear[dayAsInt];

    }

    public static IProblem GetProblem2022(int day) {
        return _problemPicker[2022][day];
    }

}

