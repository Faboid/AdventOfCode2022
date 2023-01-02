namespace AdventOfCode2022.Core.Days_20_25.Day_20;

public class Swapper {

    private static bool _shouldDebug = false;
    private static void Debug(string line) {
        if(_shouldDebug) {
            Console.WriteLine(line);
        }
    }

    public static void Decrypt(ElementCollection input) {
        
        for(int i = input.Min; i <= input.Max; i++) {

            Debug(input.ToString());

            var currIndex = input.FindIndexById(i);
            var isPositive = input[currIndex].Value >= 0;
            var count = Math.Abs(input[currIndex].Value);

            Debug($"{input[currIndex].Value} (");
            if(isPositive) {

                while(count != 0) {
                    input.SwapForward(currIndex);
                    currIndex++;
                    count--;
                    Debug($"    {input}");
                }

            } else {

                while(count != 0) {
                    input.SwapBack(currIndex);
                    currIndex--;
                    count--;
                    Debug($"    {input}");
                }

            }
            Debug(")");

        }

        Debug(input.ToString());

    }

}
