using System.Text;

namespace AdventOfCode2022.Core.Day_7;

public class InputValues {

    public const string TestInput = "$ cd /\r\n$ ls\r\ndir a\r\n14848514 b.txt\r\n8504156 c.dat\r\ndir d\r\n$ cd a\r\n$ ls\r\ndir e\r\n29116 f\r\n2557 g\r\n62596 h.lst\r\n$ cd e\r\n$ ls\r\n584 i\r\n$ cd ..\r\n$ cd ..\r\n$ cd d\r\n$ ls\r\n4060174 j\r\n8033020 d.log\r\n5626152 d.ext\r\n7214296 k";
    public static string Input => InputGetter.GetYear2022Day(7);
    public const int MaxFileSystemSize = 70000000;
    public const int RequiredSize = 30000000;

}