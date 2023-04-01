using Godot;
using System;

public partial class UIManager : Control {
    [Export] public Label scoreLabel;
    [Export] public Label tilesLeftLabel;
    [Export] public TextureRect nextTileRect;

    public void SetLeft(int left) {
        tilesLeftLabel.Text = "Tiles left: " + left.ToString();
    }
}
