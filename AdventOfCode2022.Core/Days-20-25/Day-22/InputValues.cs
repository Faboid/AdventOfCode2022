using AdventOfCode2022.Core.Days_10_19.Day_12;
using AdventOfCode2022.Core.Shared;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2022.Core.Days_20_25.Day_22;

public class InputValues {

    public const string TestInput = "        ...#\r\n        .#..\r\n        #...\r\n        ....\r\n...#.......#\r\n........#...\r\n..#....#....\r\n..........#.\r\n        ...#....\r\n        .....#..\r\n        .#......\r\n        ......#.\r\n\r\n10R5L5R10L4R5L5";
    public static string Input => InputGetter.GetYear2022Day(22);

    public static string ExtractMap(string input) => input.Split("\r\n\r\n")[0];
    public static string ExtractCommands(string input) => input.Split("\r\n\r\n")[1];

}