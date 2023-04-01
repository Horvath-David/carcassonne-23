using System;
using System.Collections.Generic;

public class GameState {
    private Random random = new Random();

    public bool gameOver = false;
    public bool normalGameOver = true;
    
    public List<PlayerState> players = new List<PlayerState>();

    public List<ScoreRegion> scoreRegions = new List<ScoreRegion>();
    public HashSet<(int X, int Y)> openPlaces = new HashSet<(int X, int Y)>();

    public List<TileData> tilesLeft;
    public TileData nextTile;

    public void ChooseNextTile() {
        if (tilesLeft.Count == 0) {
            gameOver = true;
            normalGameOver = true;
            return;
        }
        var i = random.Next(0, tilesLeft.Count);
        nextTile = tilesLeft[i];
        tilesLeft.RemoveAt(i);

        var over = true;
        foreach (var place in openPlaces) {
            //TODO: integrate board into gameState
            if (Manager.board.IsLegal(new Tile(place.X, place.Y, nextTile, 0)) ||
                Manager.board.IsLegal(new Tile(place.X, place.Y, nextTile, 1)) ||
                Manager.board.IsLegal(new Tile(place.X, place.Y, nextTile, 2)) ||
                Manager.board.IsLegal(new Tile(place.X, place.Y, nextTile, 3))) over = false;
        }

        if (over) {
            gameOver = true;
            normalGameOver = false;
            return;
        }
    }
}