using System.Diagnostics;

namespace AdventOfCode2022.Core.Days_10_19.Day_17;

public class JetGenerator {

    private readonly string _directions;
    private int _current = 0;

    public JetGenerator(string directions) {
        _directions = directions;

        if(_directions.Any(x => x is not '<' and not '>')) {
            throw new ArgumentException("The given directions input contains invalid chars.");
        }
    }

    public int GetCurrentIndex() => _current;

    public int Next() {

        if(_current >= _directions.Length) {
            _current = 0;
        }

        var output = _directions[_current];
        _current++;
        return output switch {
            '<' => -1,
            '>' => 1,
            _ => throw new UnreachableException($"The given _directions should not contain char({output})")
        };
        ;

    }

}