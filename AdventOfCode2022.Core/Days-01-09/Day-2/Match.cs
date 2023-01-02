namespace AdventOfCode2022.Core.Day_2;

public class Match {

    public Match(string input) {
        var choices = input.Split(' ');
        A_Choice = choices[0].ToChoice();
        B_Choice = choices[1].ToChoice();
    }

    public Choice A_Choice { get; set; }
    public Choice B_Choice { get; set; }

    public (int A, int B) Points() {

        int a = (int)A_Choice;
        int b = (int)B_Choice;

        if(a == b) {
            a += 3;
            b += 3;
            return (a, b);
        }

        if(A_Choice is Choice.Paper) {
            if(B_Choice is Choice.Rock) {
                a += 6;
            } else if(B_Choice is Choice.Scissors) {
                b += 6;
            }
        } else if(A_Choice is Choice.Rock) {
            if(B_Choice is Choice.Paper) {
                b += 6;
            } else if(B_Choice is Choice.Scissors) {
                a += 6;
            }
        } else if(A_Choice is Choice.Scissors) {
            if(B_Choice is Choice.Paper) {
                a += 6;
            } else if(B_Choice is Choice.Rock) {
                b += 6;
            }
        }

        return (a, b);

    }

}

public enum Choice {
    None = 0,
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

public static class StringToChoiceConverter {

    public static Choice ToChoice(this string value) {

        if(value is "A" or "X" or "a" or "x") {
            return Choice.Rock;
        }

        if(value is "B" or "b" or "Y" or "y") {
            return Choice.Paper;
        }

        if(value is "C" or "c" or "Z" or "z") {
            return Choice.Scissors;
        }

        throw new NotSupportedException();

    }
}