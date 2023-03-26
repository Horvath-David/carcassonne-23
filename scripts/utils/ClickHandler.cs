using Godot;

interface Clickable {
    void OnClick();
}

public partial class ClickHandler : Button
{
    public override void _Pressed()
    {
        GetParent<Clickable>().OnClick();
    }
}
