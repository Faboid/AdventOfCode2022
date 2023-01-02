namespace AdventOfCode2022.Core;

public interface IProblem {

    string TestInput { get; }
    string RealInput { get; }

    /// <summary>
    /// Solves the first problem using the pre-defined input.
    /// </summary>
    /// <returns></returns>
    public string SolveFirst();

    /// <summary>
    /// Solves the second problem using the pre-defined input.
    /// </summary>
    /// <returns></returns>
    public string SolveSecond();

    /// <summary>
    /// Solves the first problem using custom input. Might fail in case it depends on specific input layout.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public string SolveFirst(string input);

    /// <summary>
    /// Solves the second problem using custom input. Might fail in case it depends on specific input layout.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public string SolveSecond(string input);

}
