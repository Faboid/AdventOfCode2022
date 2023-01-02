namespace AdventOfCode2022.Core.Day_6;

public class Tuner {

    public static int FindMarkerIndex(string input, int requiredDistinct) {

        var markSeeker = new MarkSeeker(requiredDistinct);

        int index = 0;
        while(index < input.Length && !markSeeker.Add(input[index])) {
            index++;
        }

        if(!markSeeker.HasMark()) {
            throw new ArgumentException("The given input does not contain a marker.");
        }

        return index + 1; //the marker is 1 index based
    }



}
