namespace AdventOfCode2022.Core.Days_20_25.Day_22;

public static class MapNavigatorBuilder {

    public const char OpenTile = '.';
    public const char WallTile = '#';
    public const char SpaceTile = ' ';

    /// <summary>
    /// Generates a map and return the root position. The root is always the first top-left open tile.
    /// </summary>
    /// <param name="mapAsString"></param>
    /// <returns></returns>
    public static MapNavigator BuildAsGrid(this IList<string> mapAsString) {
        var tileList = BuildTileList(mapAsString);
        var root = tileList[0].First(x => x is not null);
        return new MapNavigator(root);

    }

    public static List<List<Tile?>> BuildTileList(this IList<string> mapAsString) {
        var lengthY = mapAsString.Count;
        var lengthX = mapAsString.Max(x => x.Length);

        var tileList = CreateUnconnectedTiles(mapAsString, lengthY, lengthX);
        tileList.SetUpRightLeftConnections();
        tileList.SetUpUpDownConnections(lengthX);

        return tileList;
    }

    private static List<List<Tile?>> CreateUnconnectedTiles(this IList<string> mapAsString, int lengthY, int lengthX) {
        var tileList = new List<List<Tile?>>();
        //create all tiles with their positions
        for(int y = 0; y < lengthY; y++) {

            tileList.Add(new List<Tile?>());
            var row = mapAsString[y];
            for(int x = 0; x < lengthX; x++) {

                if(x >= mapAsString[y].Length) {
                    while(x < lengthX) {
                        tileList[y].Add(null);
                        x++;
                    }
                    break;
                }

                if(row[x] == SpaceTile) {
                    tileList[y].Add(null); //keep empty spaces as null
                    continue;
                }

                var tileType = row[x] switch {
                    OpenTile => TileType.Empty,
                    WallTile => TileType.Wall,
                    _ => throw new ArgumentException()
                };

                //index starts at 1, 1
                tileList[y].Add(new Tile(y + 1, x + 1, tileType));

            }
        }

        return tileList;
    }

    private static void SetUpRightLeftConnections(this List<List<Tile?>> tileList) {

        for(int y = 0; y < tileList.Count; y++) {

            //start by connecting first and last
            var first = tileList[y].First(x => x is not null);
            var last = tileList[y].Last(x => x is not null);

            first.Left = last;
            last.Right = first;

            var curr = first;
            for(int x = curr.Column; x < tileList[y].Count; x++) {
                var next = tileList[y][x];

                if(next is null) {
                    continue;
                }

                curr.Right = next;
                next.Left = curr;
                curr = next;
            }

        }
    }

    private static void SetUpUpDownConnections(this List<List<Tile?>> tileList, int lengthX) {

        for(int x = 0; x < lengthX; x++) {

            //start by connecting first and last
            var first = tileList.First(y => y[x] is not null)[x];
            var last = tileList.Last(y => y[x] is not null)[x];

            first.Up = last;
            last.Down = first;

            var curr = first;
            for(int y = first.Row; y < tileList.Count; y++) {
                var next = tileList[y][x];

                if(next is null) {
                    continue;
                }

                curr.Down = next;
                next.Up = curr;
                curr = next;
            }

        }

    }

}
