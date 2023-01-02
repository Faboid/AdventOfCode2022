namespace AdventOfCode2022.Core.Day_5;

public static class StackExtensions {

    public static void PushAll<T>(this Stack<T> stack, IEnumerable<T> enumerable) {
        foreach(var item in enumerable)
            stack.Push(item);
    }

    public static List<T> PopRange<T>(this Stack<T> stack, int amount) {
        var list = new List<T>();
        while(amount > 0) {
            list.Add(stack.Pop());
            amount--;
        }
        return list;
    }

}
