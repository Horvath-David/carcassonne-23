using System;
using System.Collections.Generic;

public enum Side {
    Up, Right, Down, Left
}

public abstract class ScoreRegion {
    public List<ScoreArea> areas = new List<ScoreArea>();
    public List<(int, int, Side)> openSides = new List<(int, int, Side)>();

    public Dictionary<int, int> meeples = new Dictionary<int, int>();

    public int GetScore() {
        throw new NotImplementedException();
    }

    public bool IsComplete() {
        return openSides.Count == 0;
    }

    public bool Connects(ScoreArea area) {
        if (areas.Find(a => a.type == area.type && a.pos == (area.pos.X + 1, area.pos.Y) && a.sides.left && area.sides.right) != null) {
            return true;
        }
        if (areas.Find(a => a.type == area.type && a.pos == (area.pos.X - 1, area.pos.Y) && a.sides.right && area.sides.left) != null) {
            return true;
        }
        if (areas.Find(a => a.type == area.type && a.pos == (area.pos.X, area.pos.Y + 1) && a.sides.down && area.sides.up) != null) {
            return true;
        }
        if (areas.Find(a => a.type == area.type && a.pos == (area.pos.X, area.pos.Y - 1) && a.sides.up && area.sides.down) != null) {
            return true;
        }

        return false;
    }
}