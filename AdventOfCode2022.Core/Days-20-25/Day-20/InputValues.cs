using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;

namespace AdventOfCode2022.Core.Days_20_25.Day_20;

public class InputValues {

    public const int Key = 811589153;
    public const string TestInput = "1\r\n2\r\n-3\r\n3\r\n-2\r\n0\r\n4";
    public static string Input => InputGetter.GetYear2022Day(20);

}