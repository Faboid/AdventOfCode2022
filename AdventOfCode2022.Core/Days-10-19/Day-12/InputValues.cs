using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

namespace AdventOfCode2022.Core.Days_10_19.Day_12;

public class InputValues {

    public const string TestInput = "Sabqponm\r\nabcryxxl\r\naccszExk\r\nacctuvwj\r\nabdefghi";
    public static string Input => InputGetter.GetYear2022Day(12);
    public static char[][] MapAsGrid(string input) => input.Split("\r\n").Select(x => x.ToCharArray()).ToArray();

}