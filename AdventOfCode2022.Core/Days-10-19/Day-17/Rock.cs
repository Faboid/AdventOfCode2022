using AdventOfCode2022.Core.Shared;

namespace AdventOfCode2022.Core.Days_10_19.Day_17;

public class Rock {

    public Position<long> Position { get; set; }
    public long RightEdge => Position.X + _rightEdgeOffset;
    public long LeftEdge => Position.X + _leftEdgeOffset;

    private readonly RockShape _rockShape;
    private readonly int _rightEdgeOffset;
    private readonly int _leftEdgeOffset;

    public Rock(Position<long> position, RockShape rockShape) {
        Position = position;
        _rockShape = rockShape;

        _rightEdgeOffset = rockShape.Positions.Max(x => x.X);
        _leftEdgeOffset = rockShape.Positions.Min(x => x.X);
    }

    public bool CollidesWith(ReadOnlyPosition<long> position) => EnumerateCurrentPositions().Any(x => x == position);
    public bool CollidesWithPillar(ReadOnlyPosition<long> pillarTop) => EnumerateCurrentPositions().Where(x => x.X == pillarTop.X).Any(x => x.Y <= pillarTop.Y);

    public IEnumerable<ReadOnlyPosition<long>> EnumerateCurrentPositions() {
        foreach(var position in _rockShape.Positions) {
            yield return new(Position.X + position.X, Position.Y + position.Y);
        }
    }

}
