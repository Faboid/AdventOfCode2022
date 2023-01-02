using System.Collections;
using System.Reflection;

namespace AdventOfCode2022.Core;

public static class InputGetter {

    public static string GetYear2022Day(int day) {

        string resourceName = $"Day{day}Input.txt";
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Properties.{resourceName}");

        if(stream is null) {
            throw new ArgumentException($"The day {day} does not have a registered input. Please insert it in AdventOfCode2022.Core.Properties with the format 'DayNumberInput.txt' and set it as embedded resource.");
        }

        using var streamReader = new StreamReader(stream);
        string output = streamReader.ReadToEnd();
        return output;

    }

}

