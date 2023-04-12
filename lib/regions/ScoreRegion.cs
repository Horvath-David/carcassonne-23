using System;
using System.Collections.Generic;
using System.Linq;

public enum Side {
    Up, Right, Down, Left
}

public abstract class ScoreRegion {
    public List<ScoreArea> areas = new List<ScoreArea>();
    public List<(int, int, Side)> openSides = new List<(int, int, Side)>();

    public Dictionary<int, int> meeples = new Dictionary<int, int>();
    public GameState state;
    public bool canPlaceMeeples = true;

    public ScoreRegion(GameState state) {
        this.state = state;
    }

    public static ScoreRegion FromType(GameState state, AreaType type) {
        switch (type) {
            case AreaType.City:
                return new CityRegion(state);
                break;
            case AreaType.Monastery:
                return new MonasteryRegion(state);
                break;
            case AreaType.Road:
                return new RoadRegion(state);
                break;
        }

        return null;
    }

    public int GetOwner() {
        var sorted = from entry in meeples orderby entry.Value descending select entry.Key;
        return sorted.First();
    }

    public virtual int GetScore(bool lite = false) {
        throw new NotImplementedException();
    }

    public virtual bool IsComplete() {
        return openSides.Count == 0;
    }

    public bool Connects(ScoreArea area) {
        var type = area.type;

        if (areas.Find(a => a.type == type && a.pos == (area.pos.X + 1, area.pos.Y) && a.sides.left && area.sides.right) != null) {
            return true;
        }
        if (areas.Find(a => a.type == type && a.pos == (area.pos.X - 1, area.pos.Y) && a.sides.right && area.sides.left) != null) {
            return true;
        }
        if (areas.Find(a => a.type == type && a.pos == (area.pos.X, area.pos.Y + 1) && a.sides.down && area.sides.up) != null) {
            return true;
        }
        if (areas.Find(a => a.type == type && a.pos == (area.pos.X, area.pos.Y - 1) && a.sides.up && area.sides.down) != null) {
            return true;
        }

        return false;
    }
}