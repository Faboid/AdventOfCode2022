using System.Diagnostics;

namespace AdventOfCode2022.Core.Days_10_19.Day_11;

[DebuggerDisplay("Inspected Items: {InspectedItems}")]
public class Monkey {

    public Monkey(MonkeyCage cage, int whenTrue_ThrowToMonkeyId, int whenFalse_ThrowToMonkeyId, Action<Item> applyWorryModifier, Func<Item, bool> test, Queue<Item> items) {
        ApplyWorryModifier = applyWorryModifier;
        Test = test;
        _whenTrue_ThrowToMonkeyId = whenTrue_ThrowToMonkeyId;
        _whenFalse_ThrowToMonkeyId = whenFalse_ThrowToMonkeyId;
        Items = items;
        Cage = cage;
    }

    public MonkeyCage Cage { get; }
    public int InspectedItems { get; private set; } = 0;
    public Id Id { get; init; } = new();
    public Queue<Item> Items { get; }
    public Action<Item> ApplyWorryModifier { get; private set; }
    public Func<Item, bool> Test { get; private set; }

    private readonly int _whenTrue_ThrowToMonkeyId;
    private readonly int _whenFalse_ThrowToMonkeyId;

    public void ThrowAll() {

        while(Items.TryDequeue(out var item)) {
            ApplyWorryModifier(item);
            InspectedItems++;
            item.CalmWorry();
            var success = Test(item);
            var throwToMonkey = success ? _whenTrue_ThrowToMonkeyId : _whenFalse_ThrowToMonkeyId;
            ThrowToMonkey(item, throwToMonkey);
        }

    }

    private void ThrowToMonkey(Item item, int throwToMonkey) => Cage.GetMonkey(throwToMonkey).Items.Enqueue(item);

    public static Monkey Parse(string input, MonkeyCage cage) {

        Queue<Item> items = new();
        Action<Item> applyWorryModifier;
        Func<Item, bool> test;
        int whenTrue_ThrowToMonkeyId;
        int whenFalse_ThrowToMonkeyId;

        var split = input.Split("\r\n").Select(x => x.Trim()).ToArray();

        //items setting
        var startingItems = split[1].Split(" ")
            .Select(x => x.Trim())
            .Select(x => x.Trim(','))
            .Select(x => (success: int.TryParse(x, out var result), result))
            .Where(x => x.success)
            .Select(x => x.result);

        foreach(var item in startingItems) {
            items.Enqueue(new Item(item));
        }
        //end items setting

        //start operation building
        var operation = split[2].Split(" ")
            .Skip(3).ToArray(); //skip "operation:", "new", and "="

        var operationType = operation[1] switch {
            "*" => OperationType.Multiply,
            "+" => OperationType.Add,
            _ => throw new NotImplementedException()
        };

        var secondElement = operation[2];

        if(uint.TryParse(secondElement, out var result)) {

            applyWorryModifier = operationType switch {
                OperationType.Add => (item) => item.AddWorry(result),
                OperationType.Multiply => (item) => item.MultiplyWorry(result),
                _ => throw new NotImplementedException(),
            };

        } else { //else assuming "old * old"

            applyWorryModifier = operationType switch {
                OperationType.Add => (item) => item.AddWorry(item.Worry),
                OperationType.Multiply => (item) => item.MultiplyWorry(item.Worry),
                _ => throw new NotImplementedException(),
            };

        }
        //end operation building

        //set up test
        var divisibleBy = uint.Parse(split[3].Split(" ").Last());
        test = (item) => item.Worry % divisibleBy == 0;

        //set up throwing values
        whenTrue_ThrowToMonkeyId = int.Parse(split[4].Split(" ").Last());
        whenFalse_ThrowToMonkeyId = int.Parse(split[5].Split(" ").Last());

        return new Monkey(cage, whenTrue_ThrowToMonkeyId, whenFalse_ThrowToMonkeyId, applyWorryModifier, test, items);

    }

}
