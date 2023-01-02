namespace AdventOfCode2022.Core.Days_10_19.Day_12;

public class PathFinder {

    private readonly Map _map;

    public PathFinder(Map map) {
        _map = map;
    }

    public bool TryFindPath(Position starting, Position target, out Tile[]? path) {

        List<Node> activeNodes = new();
        List<Node> visitedNodes = new();

        var startTile = _map.Single(x => x.Position.Equals(starting));
        var endTile = _map.Single(x => x.Position.Equals(target));

        var startNode = new Node(startTile, endTile.Position);

        activeNodes.Add(startNode);

        while(activeNodes.Any()) {

            var curr = activeNodes.MinBy(x => x.CostDistance)!;

            //reached the end
            if(curr.Tile.Position == endTile.Position) {
                path = curr.UnWrap(x => x.Parent).Select(x => x.Tile).ToArray();
                return true;
            }

            activeNodes.Remove(curr);
            visitedNodes.Add(curr);
            //RefreshConsole(visitedNodes);

            var walkable = GetWalkableFrom(curr, endTile);

            foreach(var walkableNode in walkable) {

                //ignore if visited
                if(visitedNodes.Any(x => x.X == walkableNode.X && x.Y == walkableNode.Y)) {
                    continue;
                }

                if(activeNodes.TryFirst(x => x.X == walkableNode.X && x.Y == walkableNode.Y, out var activeNode)) {

                    if(activeNode?.Cost > curr.Cost) {

                        activeNodes.Remove(activeNode);
                        activeNodes.Add(walkableNode);

                    }

                } else {
                    activeNodes.Add(walkableNode);
                }

            }

        }

        path = null;
        return false;

    }

    private IEnumerable<Node> GetWalkableFrom(Node current, Tile target) {

        var possible = new Position[] {
            new Position(current.X, current.Y - 1),
            new Position(current.X, current.Y + 1),
            new Position(current.X - 1, current.Y),
            new Position(current.X + 1, current.Y)
        }.Where(x => x.X >= 0 && x.X < _map.MaxX && x.Y >= 0 && x.Y < _map.MaxY);

        var nodes = possible.Select(x => _map[x]).Select(x => new Node(x, current, current.Cost + 1, target.Position));

        return nodes.Where(x => x.Tile.Height <= current.Tile.Height + 1);
    }

}
