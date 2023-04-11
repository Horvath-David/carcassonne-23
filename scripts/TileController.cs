using Godot;
using System;
using System.Collections.Generic;

public partial class TileController : Sprite2D, ClickableArea {
    public Tile tile;
    public List<ScoreAreaController> areaControllers = new List<ScoreAreaController>();

    public override void _Ready() {
        if (tile.areas.Find(a => Manager.gameState.scoreRegions.Find(r => r.Connects(a) && r.meeples.Values.Count != 0) != null) != null) {
            tile.canPlaceMeeple = false;
        }

        if (tile.canPlaceMeeple) {
            foreach (var area in tile.areas) {
                var instance = Manager.scoreArea.Instantiate() as ScoreAreaController;
                instance.scoreArea = area;
                instance.pos = tile.pos;
                instance.index = tile.areas.IndexOf(area);
                AddChild(instance);
            }
        }
    }

    public void OnClick() {
        GD.Print(tile.type.path);
    }

    public void PlaceMeeple() {
        if (!tile.canPlaceMeeple) return;

        // do stuff
        
        tile.canPlaceMeeple = false;
    }
}
