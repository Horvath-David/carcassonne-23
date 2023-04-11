using System;
using System.Collections.Generic;

public class GameState {
    private Random random = new Random();

    public bool gameOver = false;
    public bool normalGameOver = true;
    
    public List<PlayerState> players = new List<PlayerState>();
    public int currentPlayer = 0;

    public Board board = new Board();

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
            if (board.IsLegal(new Tile(place.X, place.Y, nextTile, 0)) ||
                board.IsLegal(new Tile(place.X, place.Y, nextTile, 1)) ||
                board.IsLegal(new Tile(place.X, place.Y, nextTile, 2)) ||
                board.IsLegal(new Tile(place.X, place.Y, nextTile, 3))) over = false;
        }

        if (over) {
            gameOver = true;
            normalGameOver = false;
            return;
        }
    }

    public void MergeRegions(int index1, int index2) {
        throw new NotImplementedException();
    }

    public void NextPlayer() {
        throw new NotImplementedException();
    }

    public bool CanPlaceMeeple(ScoreArea area) {
        return scoreRegions.Find(r => r.Connects(area)) == null;
    }

    public (bool, bool, bool, bool) Move(Tile tile) {
        throw new NotImplementedException();
    }

    public void PlaceMeeple(int X, int Y, int idx, int player) {
        throw new NotImplementedException();
    }

    // When placing tiles and searching for regions, go over all, Connects(), if one found add, if two found merge and add
    // If Connects() to any region, don't allow placing meeples
}