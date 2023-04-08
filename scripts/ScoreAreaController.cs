using Godot;
using System;

public partial class ScoreAreaController : AreaHandler {
    public ScoreArea scoreArea;
    public Polygon2D poly;

    public override void _Ready() {
        base._Ready();
        poly = GetChild<Polygon2D>(1);
        collision.Polygon = scoreArea.shape;
        poly.Polygon = scoreArea.shape;
    }
}
