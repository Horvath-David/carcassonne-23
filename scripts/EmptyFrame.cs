using Godot;

public partial class EmptyFrame : Sprite2D, Clickable
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
}
