using System;
using System.Collections.Generic;

public class GameState {
    private Random random = new Random();

    public bool gameOver = false;
    
    public List<PlayerState> players = new List<PlayerState>();

    public List<ScoreRegion> scoreRegions = new List<ScoreRegion>();
    public HashSet<(int X, int Y)> openPlaces = new HashSet<(int X, int Y)>();

    public List<TileData> tilesLeft;
    public TileData nextTile;

    public void ChooseNextTile() {
        if (tilesLeft.Count == 0) {
            gameOver = true;
            return;
        }
        var i = random.Next(0, tilesLeft.Count);
        nextTile = tilesLeft[i];
        tilesLeft.RemoveAt(i);
    }
}