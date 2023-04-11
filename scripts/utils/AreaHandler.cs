using Godot;
using System;

interface ClickableArea {
    void OnClick();

    public void OnMouseDown() {
        return;
    }
    public void OnMouseUp() {
        return;
    }
}

interface HoverableArea {
    void OnMouseEnter();
    void OnMouseExit();
}

public partial class AreaHandler : Area2D {
    public CollisionPolygon2D collision;
    
    public override void _Ready() {
        collision = GetChild<CollisionPolygon2D>(0);
    }

    public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx) {
        if (@event is InputEventMouseButton) {
            var buttonEvent = (InputEventMouseButton)@event;
            if (buttonEvent.ButtonIndex == MouseButton.Left && buttonEvent.Pressed) {
                GetParent<ClickableArea>().OnMouseDown();
            }
            if (buttonEvent.ButtonIndex == MouseButton.Left && !buttonEvent.Pressed) {
                GetParent<ClickableArea>().OnMouseUp();
                OnClick();
            }
        }
    }

    public virtual void OnClick() {
        GetParent<ClickableArea>().OnClick();
    }
}
