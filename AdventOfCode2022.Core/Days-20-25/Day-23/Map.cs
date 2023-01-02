using System.Text;

namespace AdventOfCode2022.Core.Days_20_25.Day_23;

public class Map {

    private readonly List<Elf> _elves;
    private readonly MovementGenerator _movementGenerator = new();

    public Map(IEnumerable<Elf> elves) {

        //initializes empty tiles grid
        _elves = elves.ToList();

    }

    public override string ToString() {
        
        var minY = _elves.Min(x => x.Position.Y);
        var maxY = _elves.Max(x => x.Position.Y);
        var minX = _elves.Min(x => x.Position.X);
        var maxX = _elves.Max(x => x.Position.X);

        StringBuilder sb = new();

        for(int y = minY; y <= maxY; y++) {
            for(int x = minX; x <= maxX; x++) {
                if(_elves.Any(elf => elf.Position.X == x && elf.Position.Y == y)) {
                    sb.Append("#");
                } else {
                    sb.Append('.');
                }
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    public int CountTiles() {

        var minY = _elves.Min(x => x.Position.Y);
        var maxY = _elves.Max(x => x.Position.Y);
        var minX = _elves.Min(x => x.Position.X);
        var maxX = _elves.Max(x => x.Position.X);

        var square = (maxX - minX + 1) * (maxY - minY + 1);
        return square - _elves.Count;

    }

    public void ActTurns(int turns) {
        while(turns > 0) {
            ActTurn();
            turns--;
        }
    }

    public bool ActTurn() {

        //propose phase
        ProposeAll();

        //check conflicts and set direction to none for them
        CheckConflicts();

        var anyMoves = _elves.Any(x => x.Proposed is not null);

        //execute movements
        ExecuteMovements();

        return anyMoves;

    }

    private void ProposeAll() {
        _movementGenerator.MoveNext();
        
        foreach(var elf in _elves) {
            if(HasAdjacentElf(elf.Position)) {
                var proposedDirection = _movementGenerator.ProposeNext(elf.Position, this);
                elf.SetProposed(proposedDirection);
            }
        }
    }

    private void CheckConflicts() {

        foreach(var firstElf in _elves) {

            if(firstElf.Proposed is null) {
                continue;
            }

            var matching = _elves.Where(x => x.Proposed is not null && x != firstElf && x.Proposed == firstElf.Proposed).ToArray();
            if(matching.Any()) {
                firstElf.Proposed = null;
            }

            foreach(var elf in matching) {
                elf.Proposed = null;
            }

        }

    }

    private void ExecuteMovements() {

        foreach(var elf in _elves.Where(x => x.Proposed is not null)) {
            elf.Position = elf.Proposed!;
            elf.Proposed = null;
        }

    }

    private bool HasAdjacentElf(ReadOnlyPosition position) {
        return position.EnumerateAdjacentDiagonal().Any(x => _elves.Any(elf => elf.Position == x));
    }

    public bool HasElfIn(ReadOnlyPosition position) => _elves.Any(x => x.Position == position);

    //public ITile this[ReadOnlyPosition position] => _elves[position.Y][position.X];

}
