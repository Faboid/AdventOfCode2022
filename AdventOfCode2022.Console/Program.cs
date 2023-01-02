// See https://aka.ms/new-console-template for more information
using AdventOfCode2022.Console;
using AdventOfCode2022.Core;

Console.WriteLine("Hello, World!");

string line;
while((line = Console.ReadLine()) is not "exit") {

    var split = line.Split(' ');

    IProblem problem = ProblemPicker.GetProblem2022(int.Parse(split[0]));
    string result;

    string input = split.Length == 1 || split[1] == "test" ? problem.TestInput : problem.RealInput;

    Console.WriteLine("First Part:");
    result = problem.SolveFirst(input);
    Console.WriteLine(result);

    Console.WriteLine("Second Part:");
    result = problem.SolveSecond(input);
    Console.WriteLine(result);
    Console.WriteLine();

}


Console.WriteLine("Press a key to close the console.");
Console.ReadKey();
