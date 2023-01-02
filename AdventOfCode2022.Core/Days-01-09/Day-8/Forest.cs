namespace AdventOfCode2022.Core.Day_8;

public class Forest {

    //x, y
    private readonly Tree[,] _grid;
    private readonly int _height;
    private readonly int _width;

    public IEnumerable<Tree> Trees() {
        for(int x = 0; x < _width; x++) {
            for(int y = 0; y < _height; y++) {
                yield return _grid[x, y];
            }
        }
    }

    public Forest(Tree[,] grid) {
        _grid = grid;
        _width = _grid.GetLength(0);
        _height = _grid.GetLength(1);
        SetVisibility();
    }

    public int TreeScenicScore(int x, int y) {

        var treeHeight = _grid[x, y].Height;

        var upTrees = new List<Tree>();
        for(int upIndex = y - 1; upIndex >= 0; upIndex--) {
            upTrees.Add(_grid[x, upIndex]);

            if(_grid[x, upIndex].Height >= treeHeight) {
                break;
            }
        }

        var downTrees = new List<Tree>();
        for(int downIndex = y + 1; downIndex < _height; downIndex++) {
            downTrees.Add(_grid[x, downIndex]);

            if(_grid[x, downIndex].Height >= treeHeight) {
                break;
            }
        }

        var leftTrees = new List<Tree>();
        for(int leftIndex = x - 1; leftIndex >= 0; leftIndex--) {
            leftTrees.Add(_grid[leftIndex, y]);

            if(_grid[leftIndex, y].Height >= treeHeight) {
                break;
            }
        }

        var rightTrees = new List<Tree>();
        for(int rightIndex = x + 1; rightIndex < _width; rightIndex++) {
            rightTrees.Add(_grid[rightIndex, y]);

            if(_grid[rightIndex, y].Height >= treeHeight) {
                break;
            }
        }

        return upTrees.Count * downTrees.Count * leftTrees.Count * rightTrees.Count;

    }

    private void SetVisibility() {

        //margins
        for(int x = 0; x < _width; x++) {
            _grid[x, 0].IsVisible = true;
            _grid[x, _height - 1].IsVisible = true;
        }
        for(int y = 0; y < _width; y++) {
            _grid[0, y].IsVisible = true;
            _grid[_width - 1, y].IsVisible = true;
        }

        //top-left to down-right
        for(int x = 1; x < _width; x++) {
            for(int y = 1; y < _height; y++) {

                var tree = _grid[x, y];

                var higherTrees = new List<Tree>();
                for(int innerY = y - 1; innerY >= 0; innerY--) {
                    higherTrees.Add(_grid[x, innerY]);
                }

                if(higherTrees.All(x => x.Height < tree.Height)) {
                    tree.IsVisible = true;
                    continue;
                }

                var leftTrees = new List<Tree>();
                for(int innerX = x - 1; innerX >= 0; innerX--) {
                    leftTrees.Add(_grid[innerX, y]);
                }

                if(leftTrees.All(x => x.Height < tree.Height)) {
                    tree.IsVisible = true;
                    continue;
                }

            }
        }

        //down-right to top-left
        for(int x = _width - 2; x >= 0; x--) {
            for(int y = _height - 2; y >= 0; y--) {

                var tree = _grid[x, y];

                var lowerTrees = new List<Tree>();
                for(int innerY = y + 1; innerY < _height; innerY++) {
                    lowerTrees.Add(_grid[x, innerY]);
                }

                if(lowerTrees.All(x => x.Height < tree.Height)) {
                    tree.IsVisible = true;
                    continue;
                }

                var rightTrees = new List<Tree>();
                for(int innerX = x + 1; innerX < _width; innerX++) {
                    rightTrees.Add(_grid[innerX, y]);
                }

                if(rightTrees.All(x => x.Height < tree.Height)) {
                    tree.IsVisible = true;
                    continue;
                }


            }
        }

    }

    public static Forest Parse(string input) {

        var rows = input.Split("\r\n");

        int width = rows[0].Length;
        int height = rows.Length;
        var grid = new Tree[width, height];

        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                var h = rows[x][y] - '0';
                grid[x, y] = new Tree(h, x, y);
            }
        }

        var forest = new Forest(grid);
        return forest;

    }

}

