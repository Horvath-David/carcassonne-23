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
}