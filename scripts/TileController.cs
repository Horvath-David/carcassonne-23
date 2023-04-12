using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;

public partial class TileController : Sprite2D, ClickableArea {
    public Tile tile;
    public List<ScoreAreaController> areaControllers = new List<ScoreAreaController>();
    public bool canPlaceMeeple = true;

    public override void _Ready() {
        foreach (var area in tile.areas) {
            var instance = Manager.scoreArea.Instantiate() as ScoreAreaController;
            instance.scoreArea = area;
            instance.pos = tile.pos;
            instance.index = tile.areas.IndexOf(area);
            areaControllers.Add(instance);
            Manager.areaControllers.Add((tile.pos.X, tile.pos.Y, tile.areas.IndexOf(area)), instance);
            AddChild(instance);
        }

        foreach (var controller in areaControllers.FindAll(a => Manager.gameState.GetRegion(a.scoreArea).canPlaceMeeples)) {
            if (Manager.gameState.lite) break;
            controller.poly.Color = new Color(r:0.3f, g:0.3f, b:0.3f, a:0.75f);
            Manager.pendingAreas.Add(controller);
        }

        if (!canPlaceMeeple) {
            foreach (var area in areaControllers) {
                area.poly.Color = Colors.Transparent;
            }
        }
    }

    public void OnClick() {
        GD.Print(tile.type.path);
    }

    public void PlaceMeeple(int index) {
        if (!canPlaceMeeple) return;

        Manager.PlaceMeeple(tile.pos.X, tile.pos.Y, index);

        Manager.gameState.GetRegion(tile.areas[index]).canPlaceMeeples = false;
        canPlaceMeeple = false;
    }
}
