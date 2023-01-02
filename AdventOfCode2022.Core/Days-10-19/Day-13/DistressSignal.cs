namespace AdventOfCode2022.Core.Days_10_19.Day_13;

public class DistressSignal : IProblem {

    public string TestInput => InputValues.TestInput;
    public string RealInput => InputValues.Input;

    public void Test() {

        var test1 = "[[1],[4,3,4]]\r\n[[1],4]";
        TestOne(test1);

        void TestOne(string test) {

            var split = test.Split("\r\n");
            var pair = new Pair(split[0], split[1]);
            var result = pair.IsOrdered();
            Console.WriteLine(test);
            Console.WriteLine(result);

        }
    }

    public string SolveFirst() {
        return SolveFirst(InputValues.Input);
    }

    public string SolveSecond() {
        return SolveSecond(InputValues.Input);
    }

    public string SolveFirst(string input) {

        var pairs = input.SplitDoubleReturn().Select(x => x.Split("\r\n")).Select(x => new Pair(x[0], x[1])).ToList();
        var count = pairs.Select((x, i) => (ordered: x.IsOrdered(), index: i)).Where(x => x.ordered).Sum(x => x.index + 1);
        return count.ToString();

    }

    public string SolveSecond(string input) {

        var newPackets = new Packet[] { new("[[2]]"), new("[[6]]") };
        var packets = input.SplitDoubleReturn().Select(x => x.Split("\r\n")).SelectMany(x => x).Select(x => new Packet(x)).Concat(newPackets).ToList();
        packets.Sort(new PacketsComparer());

        int index = 1;
        int count = 1;
        foreach(var packet in packets) {
            if(newPackets.Any(x => x.Value.Equals(packet.Value))) {
                count *= index;
            }
            index++;
        }

        return count.ToString();
    }
}