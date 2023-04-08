using System.Collections.Generic;

public class Board {
    public ((int lower, int upper) X, (int lower, int upper) Y) bounds = ((0, 0), (0, 0));
    Dictionary<(int X, int Y), Tile> board = new Dictionary<(int X, int Y), Tile>();

    public Board() {
    }

    public void Set(int x, int y, Tile value) {
        if (bounds.X.lower > x)
            bounds.X.lower = x;
        if (bounds.X.upper < x)
            bounds.X.upper = x;
        if (bounds.Y.lower > y)
            bounds.Y.lower = y;
        if (bounds.Y.upper < y)
            bounds.Y.upper = y;

        board[(x, y)] = value;
    }
    public Tile Get(int x, int y) {
        try {
            return board[(x, y)];
        } catch (KeyNotFoundException) {
            return null;
        }
    }

    public int GetWidth() {
        return bounds.X.upper - bounds.X.lower + 1;
    }
    public int GetHeight() {
        return bounds.Y.upper - bounds.Y.lower + 1;
    }

    public Dictionary<(int X, int Y), Tile>.ValueCollection GetValues() {
        return board.Values;
    }

    public bool IsLegal(Tile tile) {
        if (Get(tile.pos.X, tile.pos.Y) != null) return false;
        
        var up = Get(tile.pos.X, tile.pos.Y + 1);
        var down = Get(tile.pos.X, tile.pos.Y - 1);
        var left = Get(tile.pos.X - 1, tile.pos.Y);
        var right = Get(tile.pos.X + 1, tile.pos.Y);

        if (up != null && up.sides.down != tile.sides.up) return false;
        if (down != null && down.sides.up != tile.sides.down) return false;
        if (left != null && left.sides.right != tile.sides.left) return false;
        if (right != null && right.sides.left != tile.sides.right) return false;
        
        return true;
    }
}