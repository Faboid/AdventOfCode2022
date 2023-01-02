namespace AdventOfCode2022.Core.Day_2;

public interface IChoice {

    public IChoice Win();
    public IChoice Draw();
    public IChoice Lose();

    public int Value { get; }

}

public class Rock : IChoice {
    public int Value => 1;

    public IChoice Draw() => new Rock();
    public IChoice Lose() => new Scissors();
    public IChoice Win() => new Paper();
}

public class Paper : IChoice {
    public int Value => 2;

    public IChoice Draw() => new Paper();
    public IChoice Lose() => new Rock();
    public IChoice Win() => new Scissors();
}

public class Scissors : IChoice {
    public int Value => 3;

    public IChoice Draw() => new Scissors();
    public IChoice Lose() => new Paper();
    public IChoice Win() => new Rock();
}

public class MatchEvaluator {

    private readonly IChoice _opponentChoice;
    private readonly Result _myResult;

    public MatchEvaluator(string opponentChoice, string myResult) {

        _opponentChoice = opponentChoice switch {
            "A" => new Rock(),
            "B" => new Paper(),
            "C" => new Scissors(),
            _ => throw new ArgumentOutOfRangeException()
        };

        _myResult = myResult switch {
            "X" => Result.Loss,
            "Y" => Result.Draw,
            "Z" => Result.Win,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public int Evaluate() {
        return (int)_myResult + _myResult switch {
            Result.Win => _opponentChoice.Win().Value,
            Result.Draw => _opponentChoice.Draw().Value,
            Result.Loss => _opponentChoice.Lose().Value,
            _ => throw new Exception(), //unreacheable exception, but it's .net6
        };
    }

}

public enum Result {
    Loss = 0,
    Draw = 3,
    Win = 6,
}