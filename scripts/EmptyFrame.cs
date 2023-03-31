using System;
using Godot;

public partial class EmptyFrame : Sprite2D, Clickable, Hoverable
{
    [Export]
    public int X = 0;
    [Export]
    public int Y = 0;

    public void OnClick() {
        Manager.PlaceTile(new Tile(X, Y, Manager.gameState.nextTile, rotation: Manager.rotation));
        Manager.emptyFrames.Remove((X, Y));
        QueueFree();
    }

    public void OnMouseEnter() {
        Texture = GD.Load<CompressedTexture2D>(Manager.gameState.nextTile.path);
        Rotation = (float)(Math.PI / 180f) * Manager.rotation * 90f;
    }

    public void OnMouseExit() {
        Texture = GD.Load<CompressedTexture2D>("res://assets/frame.png");
    }

    public void Rotate(int rotation) {
        Rotation = (float)(Math.PI / 180f) * rotation * 90f;
    }
}
