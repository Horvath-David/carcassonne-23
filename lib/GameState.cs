using System;
using System.Collections.Generic;
using Godot;

public class GameState {
    private Random random = new Random();

    public bool gameOver = false;
    public bool normalGameOver = true;
    public bool lite = false;
    
    public List<PlayerState> players = new List<PlayerState>();
    public int currentPlayer = 0;

    public Board board = new Board();

    public List<ScoreRegion> regions = new List<ScoreRegion>();
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

    public ScoreRegion MergeRegions(ScoreRegion r1, ScoreRegion r2) {
        foreach (var area in r2.areas) {
            if (r1.areas.FindAll(a => a == area).Count == 0) r1.areas.Add(area);
        }
        foreach (var side in r2.openSides) {
            if (r1.openSides.FindAll(s => s == side).Count == 0) r1.openSides.Add(side);
        }
        foreach (var meep in r2.meeples) {
            r1.meeples[meep.Key] += meep.Value;
        }
        return r1;
    }

    public void NextPlayer() {
        throw new NotImplementedException();
    }

    public ScoreRegion GetRegion(ScoreArea area) {
        return regions.Find(r => r.areas.Find(a => a == area) != null);
    }

    public (bool up, bool right, bool down, bool left) PlaceTile(Tile tile) {
        var result = (false, false, false, false);
        
        board.Set(tile.pos.X, tile.pos.Y, tile);
        openPlaces.Remove((tile.pos.X, tile.pos.Y));

        if (board.Get(tile.pos.X, tile.pos.Y + 1) == null) {
            openPlaces.Add((tile.pos.X, tile.pos.Y + 1));
            result.Item1 = true;
        }
        if (board.Get(tile.pos.X + 1, tile.pos.Y) == null) {
            openPlaces.Add((tile.pos.X + 1, tile.pos.Y));
            result.Item2 = true;
        }
        if (board.Get(tile.pos.X, tile.pos.Y - 1) == null) {
            openPlaces.Add((tile.pos.X, tile.pos.Y - 1));
            result.Item3 = true;
        }
        if (board.Get(tile.pos.X - 1, tile.pos.Y) == null) {
            openPlaces.Add((tile.pos.X - 1, tile.pos.Y));
            result.Item4 = true;
        }
        ChooseNextTile();

        GD.Print("-----");
        foreach (var area in tile.areas) {
            area.Rotate(tile.rotation);
            var connects = new List<ScoreRegion>(regions.FindAll(r => r.Connects(area)));
            
            GD.Print(area.type + " " + connects.Count);
            while (connects.Count > 1) {
                int i1 = 0; 
                int i2 = 1;
                var merged = MergeRegions(connects[i1], connects[i2]);
                regions.Remove(connects[i1]);
                regions.Remove(connects[i2]);
                regions.Add(merged);
                connects.RemoveAt(0);
                connects.RemoveAt(0);
                connects.Add(merged);
            }

            int ix = 0;
            if (connects.Count == 1) {
                ix = regions.IndexOf(connects[0]);

                if (regions[ix].meeples.Values.Count != 0) {
                    regions[ix].canPlaceMeeples = false;
                }
            }
            if (connects.Count == 0) {
                var region = ScoreRegion.FromType(this, area.type);
                regions.Add(region);
                ix = regions.IndexOf(region);
            }

            regions[ix].areas.Add(area);
            
            if (area.sides.up && board.Get(area.pos.X, area.pos.Y + 1) == null) regions[ix].openSides.Add((area.pos.X, area.pos.Y, Side.Up));
            if (area.sides.right && board.Get(area.pos.X + 1, area.pos.Y) == null) regions[ix].openSides.Add((area.pos.X, area.pos.Y, Side.Right));
            if (area.sides.down && board.Get(area.pos.X, area.pos.Y - 1) == null) regions[ix].openSides.Add((area.pos.X, area.pos.Y, Side.Down));
            if (area.sides.left && board.Get(area.pos.X - 1, area.pos.Y) == null) regions[ix].openSides.Add((area.pos.X, area.pos.Y, Side.Left));

            regions[ix].openSides.Remove((area.pos.X, area.pos.Y - 1, Side.Up));
            regions[ix].openSides.Remove((area.pos.X - 1, area.pos.Y, Side.Right));
            regions[ix].openSides.Remove((area.pos.X, area.pos.Y + 1, Side.Down));
            regions[ix].openSides.Remove((area.pos.X + 1, area.pos.Y, Side.Left));
        }

        return result;
    }

    public void PlaceMeeple(int X, int Y, int idx) {
        throw new NotImplementedException();
    }

    public void ApplyMove(Move move) {
        if (move.tile != null) PlaceTile(move.tile);
    }

    // When placing tiles and searching for regions, go over all, Connects(), if one found add, if two found merge and add
    // If Connects() to any region, don't allow placing meeples
}