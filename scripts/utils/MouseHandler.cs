using Godot;

interface Clickable {
    void OnClick();

    public void OnMouseDown() {
        return;
    }
    public void OnMouseUp() {
        return;
    }
}

interface Hoverable {
    void OnMouseEnter();
    void OnMouseExit();
}

public partial class MouseHandler : Button
{
    public override void _Ready() {
        Connect("mouse_entered", new Godot.Callable(this, "OnMouseEnter"));
        Connect("mouse_exited", new Godot.Callable(this, "OnMouseExit"));
        Connect("button_down", new Godot.Callable(this, "OnMouseDown"));
        Connect("button_up", new Godot.Callable(this, "OnMouseUp"));
    }

    public override void _Pressed()
    {
        try {
            GetParent<Clickable>().OnClick();
        } catch {}
    }
    private void OnMouseDown() {
        try {
            GetParent<Clickable>().OnMouseDown();
        } catch {}
    }
    private void OnMouseUp() {
        try {
            GetParent<Clickable>().OnMouseUp();
        } catch {}
    }
    private void OnMouseEnter() {
        try {
            GetParent<Hoverable>().OnMouseEnter();
        } catch {}
    }
    private void OnMouseExit() {
        try {
            GetParent<Hoverable>().OnMouseExit();
        } catch {}
    }
}
