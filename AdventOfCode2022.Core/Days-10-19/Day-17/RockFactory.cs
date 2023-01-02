using AdventOfCode2022.Core.Shared;
using System.Text;

namespace AdventOfCode2022.Core.Days_10_19.Day_17;

public class RockFactory {

    private readonly IReadOnlyList<RockShape> _shapes;

    public RockFactory(IReadOnlyList<RockShape> shapes) {
        _shapes = shapes;
    }

    private int _current = 0;

    public Rock GenerateNext(Position<long> position) {

        if(_current >= _shapes.Count) {
            _current = 0;
        }

        var shape = _shapes[_current];
        _current++;

        return new Rock(position, shape);

    }

    public int GetCurrentRotationNumber() => _current;

    public static RockFactory Parse(string[] input) {
        var shapes = input.Select(RockShape.Parse).ToList();
        return new RockFactory(shapes);
    }

    public override string ToString() {
        StringBuilder sb = new();

        for(int i = 0; i < _shapes.Count; i++) {
            var rock = GenerateNext(new(0, 0));
            var positions = rock.EnumerateCurrentPositions();

            for(int y = 5; y >= -5; y--) {
                for(int x = -5; x < 5; x++) {
                    
                    if(x == 0 && y == 0) {
                        sb.Append('@');
                        continue;
                    }

                    if(positions.Any(pos => pos.X == x && pos.Y == y)) {
                        sb.Append('#');
                        continue;
                    }

                    sb.Append(".");

                }

                sb.AppendLine();
            }

        }
        
        return sb.ToString();
    }
}
