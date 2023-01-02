using System.Text;

namespace AdventOfCode2022.Core.Days_10_19.Day_10;

public class Display {

    private readonly Receiver _receiver;
    private readonly int _pixels;

    public Display(Receiver receiver, int pixels) {
        _receiver = receiver;
        _pixels = pixels;
        _receiver.CycleChanged += SetPixel;
        _display = Enumerable.Repeat(0, _pixels).Select(x => new Pixel()).ToArray();
    }

    private readonly Pixel[] _display;

    private void SetPixel(int cycle, int value) {

        var diff = Math.Abs(((cycle - 1) % 40) - value);

        //the sprite is three pixels wide, so the maximum allowed distance is one
        _display[cycle - 1].Active = diff < 2;

    }

    public override string ToString() {
        var sb = new StringBuilder();

        for(int i = 40; i <= _pixels; i += 40) {
            _display[(i - 40)..i].Select(x => x.ToString()).Aggregate(sb, (sb, x) => sb.Append(x)).ToString();
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
