namespace AdventOfCode2022.Core.Days_10_19.Day_13;

public class PacketsComparer : IComparer<Packet> {
    public int Compare(Packet? x, Packet? y) {

        if(x is null || y is null) {
            throw new NotImplementedException();
        }

        var readerA = new PacketReader(x);
        var readerB = new PacketReader(y);

        while(!readerA.HasRead && !readerB.HasRead) {
            var result = CompareNext(readerA, readerB);
            if(result != 0) {
                return result;
            }
        }

        if(!readerA.HasRead) {
            return 1;
        }

        if(!readerB.HasRead) {
            return -1;
        }

        return 0;
    }

    private static int CompareNext(PacketReader a, PacketReader b) {

        var resultA = a.GetNext();
        var resultB = b.GetNext();
        if(resultA.Value is null && resultB.Value is null) {
            return resultA.Depth - resultB.Depth;
        }

        if(resultA.Value is null || resultB.Value is null) {

            if(resultA.Value is null) {
                return -1;
            } else {
                return 1;
            }

        }

        while(a.Depth != b.Depth) {

            if(a.Depth < b.Depth) {
                b.ExitDepth();
            } else {
                a.ExitDepth();
            }

        }

        var result = (int)resultA.Value - (int)resultB.Value;

        if(resultA.Depth != resultB.Depth && result == 0) {
            return resultA.Depth - resultB.Depth;
        }

        return result;

    }

}
