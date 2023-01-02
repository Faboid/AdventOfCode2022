using System.Diagnostics;
using System.Text;

namespace AdventOfCode2022.Core.Days_10_19.Day_13;

public class PacketReader {

    private readonly Packet _packet;

    public PacketReader(Packet packet) {
        _packet = packet;
    }

    public bool HasRead => Index >= _packet.Value.Length;
    public int Index { get; private set; } = 1;
    public int Depth { get; private set; } = 1;

    public bool CheckNext() {
        if(Index >= _packet.Value.Length) {
            return false;
        }

        if(_packet[Index] is '[' or ']') {
            return false;
        }

        return true;
    }

    public BitResult GetNext() {

        BitResult value = null;
        while(!HasRead && !TryNext(out value)) {
            Index++;
        }

        return value;

    }

    /// <summary>
    /// Returns false when the depth changes or the packet ends.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryNext(out BitResult value) {
        value = new(null, Depth);

        if(Index >= _packet.Value.Length) {
            return false;
        }

        while(_packet[Index] is ' ' or ',') {
            Index++;
        }

        if(_packet[Index] is '[' or ']') {

            if(_packet[Index] is '[' && _packet[Index + 1] is ']') {
                Index++;
                value = new(null, Depth + 1);
                return true;
            }

            Depth += _packet[Index] switch {
                '[' => 1,
                ']' => -1,
                _ => throw new UnreachableException()
            };

            return false;
        }

        StringBuilder sb = new();
        while(int.TryParse(_packet[Index].ToString(), out var _)) {
            sb.Append(_packet[Index]);
            Index++;
        }

        value = new(int.Parse(sb.ToString()), Depth);
        return true;

    }

    /// <summary>
    /// Advances the Index enough times to reach one out.
    /// </summary>
    public void ExitDepth() {

        if(Depth < 1 || Index >= _packet.Value.Length) {
            throw new ArgumentOutOfRangeException();
        }

        while(_packet[Index] is not ']') {
            Index++;
        }

        Index++;
        Depth--;
    }

}