namespace AdventOfCode2022.Core.Days_10_19.Day_12;

public static class ConsoleHelper {

    public static void DrawMap(Map map, IEnumerable<Tile> path, Position starting, Position ending) {

        for(int i = 0; i < map.MaxY; i++) {
            foreach(var tile in map.GetRow(i)) {
                var isInPath = path.TryFirst(vis => vis.X == tile.X && vis.Y == tile.Y, out var node);

                if(isInPath) {
                    Console.ForegroundColor = ConsoleColor.Green;
                } else if(tile.Height == 1) {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                } else if(tile.Height == 2) {
                    Console.ForegroundColor = ConsoleColor.Red;
                } else if(tile.Height == 3) {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                if(tile.Position == starting) {
                    Console.Write('S');
                } else if(tile.Position == ending) {
                    Console.Write('E');
                } else {
                    Console.Write((char)(tile.Height + 'a' - 1));
                }

                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine();
        }
    }

}
