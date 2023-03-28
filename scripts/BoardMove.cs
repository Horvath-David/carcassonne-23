using Godot;
using System;

public partial class BoardMove : Node2D
{
    [Export]
    private float ZoomStep = 0.2f;
    [Export]
    private float MinZoom = 0.5f;
    [Export]
    private float MaxZoom = 5f;
    public override void _Input(InputEvent @event)
    {
        var zoom = (Vector2)this.GetParent().Get("scale");
        if (@event is InputEventMouseMotion) {
            var motion = (InputEventMouseMotion)@event;

            if (motion.ButtonMask == MouseButtonMask.Middle) {
                var pos = Position;
                pos += motion.Relative / zoom;
                Position = pos;
            }
        } else if (@event is InputEventMouseButton) {
            var button = (InputEventMouseButton)@event;
            if (button.ButtonIndex == MouseButton.Right && button.Pressed) Manager.ChangeRotation();
        }
    }
}
