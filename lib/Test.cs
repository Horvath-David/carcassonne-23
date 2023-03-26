using System.Collections.Generic;

public class BoardMatrix {
    public Tile[,] board = new Tile[15, 15];

    public BoardMatrix() {
    }

    public void Set(int x, int y, Tile value) {
        board[x, y] = value;
    }
    public Tile Get(int x, int y) {
        return board[x, y];
    }
}

public class BoardDict {
    public Dictionary<(int X, int Y), Tile> board = new Dictionary<(int X, int Y), Tile>();

    public BoardDict() {
    }

    public void Set(int x, int y, Tile value) {
        board[(x, y)] = value;
    }
    public Tile Get(int x, int y) {
        return board[(x, y)];
    }
}

/*public class Tile {

}*/