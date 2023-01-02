namespace AdventOfCode2022.Core.Days_10_19.Day_12;

public class Node {

    public Node(Tile tile, Position target) {
        Tile = tile;
        Cost = 0;
        SetDistance(target);
    }

    public Node(Tile tile, Node parent, int cost, Position target) {
        Tile = tile;
        Parent = parent;
        Cost = cost;
        SetDistance(target);
    }

    public Tile Tile { get; init; }
    public Node? Parent { get; set; }
    public int Cost { get; }
    public int Distance { get; private set; }
    public int CostDistance => Cost + Distance;

    public int X => Tile.X;
    public int Y => Tile.Y;

    private void SetDistance(Position position) => SetDistance(position.X, position.Y);
    private void SetDistance(int targetX, int targetY) {
        Distance = Math.Abs(targetX - Tile.X) + Math.Abs(targetY - Tile.Y);
    }

}
