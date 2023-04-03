using Godot;
using System;

public partial class BoardZoom : Node2D {
    [Export]
    private float ZoomStep = 0.2f;
    [Export]
    private float MinZoom = 0.5f;
    [Export]
    private float MaxZoom = 5f;
    
    public override void _UnhandledInput(InputEvent @event) {
        var zoom = (Vector2)Get("scale");
        if (@event is InputEventMouseButton) {
            var button = (InputEventMouseButton)@event;
            if (!button.Pressed) return;

            var pivot = this.GetParent() as Node2D;

            if (button.ButtonIndex == MouseButton.WheelUp && zoom.X + ZoomStep <= MaxZoom) {
                
                this.Set("scale", new Vector2(zoom.X + ZoomStep, zoom.Y + ZoomStep));
            }
            if (button.ButtonIndex == MouseButton.WheelDown && zoom.X - ZoomStep >= MinZoom) {
                this.Set("scale", new Vector2(zoom.X - ZoomStep, zoom.Y - ZoomStep));
            }
            if (button.ButtonIndex == MouseButton.Middle) {

            }
        } 
        if (@event is InputEventMouseMotion) {
            var motion = (InputEventMouseMotion)@event;
            if (motion.ButtonMask == MouseButtonMask.Middle) {
                GD.Print("Middle mask");
                var pos = Position;
                pos -= motion.Relative * zoom;
                Position = pos;
            }
        }
    
    }
}
