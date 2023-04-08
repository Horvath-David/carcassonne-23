using System;
using System.Collections.Generic;
using Godot;

public enum TileSide {
    Road,
    Field,
    City
}

public class Tile {
    public int rotation;
    public (int X, int Y) pos;

    public bool canPlaceMeeple = true;

    public TileData type;
    public (TileSide up, TileSide right, TileSide down, TileSide left) sides;
    public List<ScoreArea> areas = new List<ScoreArea>();

    public Tile(int x, int y, TileData type, int rotation = 0) {
        this.pos = (x, y);
        this.type = type;
        if (rotation < 0 || rotation > 3) rotation = 0;
        this.rotation = rotation;
        var sides = new List<TileSide> { type.sides.up, type.sides.right, type.sides.down, type.sides.left };
        for (var i = 0; i < rotation; i++) {
            sides.Insert(0, sides[^1]);
            sides.RemoveAt(sides.Count - 1);
        }
        this.sides = (sides[0], sides[1], sides[2], sides[3]);
        foreach (var area in type.areas) {
            area.pos = pos;
            areas.Add(area);
        }
    }
}

public static class RegionShapes {
    //TODO: data entry
    public static Vector2[] topCity = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] bigCity = new[] {
        new Vector2(-512, -512), new Vector2(-512, 512), new Vector2(512, 512), new Vector2(512, -512)
    };
    
    public static Vector2[] monastery1 = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] monastery2 = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] wholeCity = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] middleTopCity = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] bridgeCity = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] bottomCity = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] leftCity = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] topToRightCity = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] topToLeftCity = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] noBottomCity = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] bottomRoad1 = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] bottomRoad2 = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] bottomRoad3 = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] bottomRoad4 = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] bottomRoad5 = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] leftRoad1 = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] leftRoad2 = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] rightRoad1 = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] rightRoad2 = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] topRoad = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] leftToRightRoad = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] topToBottomRoad = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] leftToBottomRoad1 = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] leftToBottomRoad2 = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] rightToBottomRoad1 = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] rightToBottomRoad2 = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
}

public static class Tiles {
    public static Tile Rotate(int rotation, Tile tile) {
        var sides = new List<TileSide> { tile.sides.up, tile.sides.right, tile.sides.down, tile.sides.left };
        for (var i = 0; i < rotation; i++) {
            sides.Insert(0, sides[^1]);
            sides.RemoveAt(sides.Count - 1);
        }
        tile.sides = (sides[0], sides[1], sides[2], sides[3]);
        return tile;
    }

    public static TileData Rotate(int rotation, TileData tile) {
        var sides = new List<TileSide> { tile.sides.up, tile.sides.right, tile.sides.down, tile.sides.left };
        for (var i = 0; i < rotation; i++) {
            sides.Insert(0, sides[^1]);
            sides.RemoveAt(sides.Count - 1);
        }
        tile.sides = (sides[0], sides[1], sides[2], sides[3]);
        return tile;
    }
    
    private static string prefix = "res://assets/tiles/";

    //TODO: data entry
    public static TileData a1 = new TileData(prefix + "a1.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Field, new []{new ScoreArea(RegionShapes.monastery1, AreaType.Monastery, (false, false, false, false))});
    public static TileData a2 = new TileData(prefix + "a2.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Field, new ScoreArea[] {});

    public static TileData b1 = new TileData(prefix + "b1.jpg", TileSide.Field, TileSide.Field, TileSide.Field, TileSide.Field, new ScoreArea[] {});
    public static TileData b2 = new TileData(prefix + "b2.jpg", TileSide.Field, TileSide.Field, TileSide.Field, TileSide.Field, new ScoreArea[] {});
    public static TileData b3 = new TileData(prefix + "b3.jpg", TileSide.Field, TileSide.Field, TileSide.Field, TileSide.Field, new ScoreArea[] {});
    public static TileData b4 = new TileData(prefix + "b4.jpg", TileSide.Field, TileSide.Field, TileSide.Field, TileSide.Field, new ScoreArea[] {});

    public static TileData c =  new TileData(prefix +  "c.jpg", TileSide.City, TileSide.City, TileSide.City, TileSide.City, new ScoreArea[] {});

    public static TileData d1 = new TileData(prefix + "d1.jpg", TileSide.City, TileSide.Road, TileSide.Field, TileSide.Road, new ScoreArea[] {});
    public static TileData d2 = new TileData(prefix + "d2.jpg", TileSide.City, TileSide.Road, TileSide.Field, TileSide.Road, new ScoreArea[] {});
    public static TileData d3 = new TileData(prefix + "d3.jpg", TileSide.City, TileSide.Road, TileSide.Field, TileSide.Road, new ScoreArea[] {});
    public static TileData st = new TileData(prefix + "st.jpg", TileSide.City, TileSide.Road, TileSide.Field, TileSide.Road, new ScoreArea[] {});

    public static TileData e1 = new TileData(prefix + "e1.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.Field, new ScoreArea[] {});
    public static TileData e2 = new TileData(prefix + "e2.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.Field, new ScoreArea[] {});
    public static TileData e3 = new TileData(prefix + "e3.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.Field, new ScoreArea[] {});
    public static TileData e4 = new TileData(prefix + "e4.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.Field, new ScoreArea[] {});
    public static TileData e5 = new TileData(prefix + "e5.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.Field, new ScoreArea[] {});

    public static TileData f1 = new TileData(prefix + "f1.jpg", TileSide.Field, TileSide.City, TileSide.Field, TileSide.City, new ScoreArea[] {});
    public static TileData f2 = new TileData(prefix + "f2.jpg", TileSide.Field, TileSide.City, TileSide.Field, TileSide.City, new ScoreArea[] {});

    public static TileData g =  new TileData(prefix +  "g.jpg", TileSide.Field, TileSide.City, TileSide.Field, TileSide.City, new ScoreArea[] {});

    public static TileData h1 = new TileData(prefix + "h1.jpg", TileSide.City, TileSide.Field, TileSide.City, TileSide.Field, new ScoreArea[] {});
    public static TileData h2 = new TileData(prefix + "h2.jpg", TileSide.City, TileSide.Field, TileSide.City, TileSide.Field, new ScoreArea[] {});
    public static TileData h3 = new TileData(prefix + "h3.jpg", TileSide.City, TileSide.Field, TileSide.City, TileSide.Field, new ScoreArea[] {});

    public static TileData i1 = new TileData(prefix + "i1.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.City, new ScoreArea[] {});
    public static TileData i2 = new TileData(prefix + "i2.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.City, new ScoreArea[] {});

    public static TileData j1 = new TileData(prefix + "j1.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Field, new ScoreArea[] {});
    public static TileData j2 = new TileData(prefix + "j2.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Field, new ScoreArea[] {});
    public static TileData j3 = new TileData(prefix + "j3.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Field, new ScoreArea[] {});

    public static TileData k1 = new TileData(prefix + "k1.jpg", TileSide.City, TileSide.Field, TileSide.Road, TileSide.Road, new ScoreArea[] {});
    public static TileData k2 = new TileData(prefix + "k2.jpg", TileSide.City, TileSide.Field, TileSide.Road, TileSide.Road, new ScoreArea[] {});
    public static TileData k3 = new TileData(prefix + "k3.jpg", TileSide.City, TileSide.Field, TileSide.Road, TileSide.Road, new ScoreArea[] {});

    public static TileData l1 = new TileData(prefix + "l1.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Road, new ScoreArea[] {});
    public static TileData l2 = new TileData(prefix + "l2.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Road, new ScoreArea[] {});
    public static TileData l3 = new TileData(prefix + "l3.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Road, new ScoreArea[] {});

    public static TileData m1 = new TileData(prefix + "m1.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.Field, new ScoreArea[] {});
    public static TileData m2 = new TileData(prefix + "m2.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.Field, new ScoreArea[] {});

    public static TileData n1 = new TileData(prefix + "n1.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.Field, new ScoreArea[] {});
    public static TileData n2 = new TileData(prefix + "n2.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.Field, new ScoreArea[] {});
    public static TileData n3 = new TileData(prefix + "n3.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.Field, new ScoreArea[] {});

    public static TileData o1 = new TileData(prefix + "o1.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.City, new ScoreArea[] {});
    public static TileData o2 = new TileData(prefix + "o2.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.City, new ScoreArea[] {});

    public static TileData p1 = new TileData(prefix + "p1.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.City, new ScoreArea[] {});
    public static TileData p2 = new TileData(prefix + "p2.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.City, new ScoreArea[] {});
    public static TileData p3 = new TileData(prefix + "p3.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.City, new ScoreArea[] {});

    public static TileData q =  new TileData(prefix +  "q.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.City, new ScoreArea[] {});

    public static TileData r1 = new TileData(prefix + "r1.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.City, new ScoreArea[] {});
    public static TileData r2 = new TileData(prefix + "r2.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.City, new ScoreArea[] {});
    public static TileData r3 = new TileData(prefix + "r3.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.City, new ScoreArea[] {});

    public static TileData s1 = new TileData(prefix + "s1.jpg", TileSide.City, TileSide.City, TileSide.Road, TileSide.City, new ScoreArea[] {});
    public static TileData s2 = new TileData(prefix + "s2.jpg", TileSide.City, TileSide.City, TileSide.Road, TileSide.City, new ScoreArea[] {});

    public static TileData t =  new TileData(prefix +  "t.jpg", TileSide.City, TileSide.City, TileSide.Road, TileSide.City, new ScoreArea[] {});

    public static TileData u1 = new TileData(prefix + "u1.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field, new ScoreArea[] {});
    public static TileData u2 = new TileData(prefix + "u2.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field, new ScoreArea[] {});
    public static TileData u3 = new TileData(prefix + "u3.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field, new ScoreArea[] {});
    public static TileData u4 = new TileData(prefix + "u4.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field, new ScoreArea[] {});
    public static TileData u5 = new TileData(prefix + "u5.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field, new ScoreArea[] {});
    public static TileData u6 = new TileData(prefix + "u6.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field, new ScoreArea[] {});
    public static TileData u7 = new TileData(prefix + "u7.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field, new ScoreArea[] {});
    public static TileData u8 = new TileData(prefix + "u8.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field, new ScoreArea[] {});

    public static TileData v1 = new TileData(prefix + "v1.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new ScoreArea[] {});
    public static TileData v2 = new TileData(prefix + "v2.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new ScoreArea[] {});
    public static TileData v3 = new TileData(prefix + "v3.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new ScoreArea[] {});
    public static TileData v4 = new TileData(prefix + "v4.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new ScoreArea[] {});
    public static TileData v5 = new TileData(prefix + "v5.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new ScoreArea[] {});
    public static TileData v6 = new TileData(prefix + "v6.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new ScoreArea[] {});
    public static TileData v7 = new TileData(prefix + "v7.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new ScoreArea[] {});
    public static TileData v8 = new TileData(prefix + "v8.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new ScoreArea[] {});
    public static TileData v9 = new TileData(prefix + "v9.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new ScoreArea[] {});

    public static TileData w1 = new TileData(prefix + "w1.jpg", TileSide.Field, TileSide.Road, TileSide.Road, TileSide.Road, new ScoreArea[] {});
    public static TileData w2 = new TileData(prefix + "w2.jpg", TileSide.Field, TileSide.Road, TileSide.Road, TileSide.Road, new ScoreArea[] {});
    public static TileData w3 = new TileData(prefix + "w3.jpg", TileSide.Field, TileSide.Road, TileSide.Road, TileSide.Road, new ScoreArea[] {});
    public static TileData w4 = new TileData(prefix + "w4.jpg", TileSide.Field, TileSide.Road, TileSide.Road, TileSide.Road, new ScoreArea[] {});

    public static TileData x =  new TileData(prefix +  "x.jpg", TileSide.Road, TileSide.Road, TileSide.Road, TileSide.Road, new ScoreArea[] {});
    
    public static List<TileData> all = new List<TileData> {
        a1, a2, b1,
        b2, b3, b4,
        c,
        d1, d2, d3,
        st,
        e1, e2, e3, e4, e5,
        f1, f2,
        g,
        h1, h2, h3,
        i1, i2,
        j1, j2, j3,
        k1, k2, k3,
        l1, l2, l3,
        m1, m2,
        n1, n2, n3,
        o1, o2,
        p1, p2, p3,
        q,
        r1, r2, r3,
        s1, s2,
        t,
        u1, u2, u3, u4, u5, u6, u7, u8,
        v1, v2, v3, v4, v5, v6, v7, v8, v9,
        w1, w2, w3, w4,
        x
    };
}

public enum AreaType {
    City, ShieldCity, Road, Monastery, Garden
}
public class ScoreArea {
    public Vector2[] shape;
    public AreaType type;
    public (bool up, bool right, bool down, bool left) sides;
    public (int X, int Y) pos;

    public ScoreArea(Vector2[] shape, AreaType type, (bool up, bool right, bool down, bool left) sides, (int X, int Y)? pos = null) {
        this.shape = shape;
        this.type = type;
        this.sides = sides;
        this.pos = pos ?? (0, 0);
    }

    public bool IsClosing() {
        var sides = new List<bool> { this.sides.up, this.sides.right, this.sides.down, this.sides.left };
        return sides.FindAll(b => b).Count == 1;
    }
    
    public void Rotate(int rotation) {
        var sides = new List<bool> { this.sides.up, this.sides.right, this.sides.down, this.sides.left };
        for (var i = 0; i < rotation; i++) {
            sides.Insert(0, sides[^1]);
            sides.RemoveAt(sides.Count - 1);
        }
        this.sides = (sides[0], sides[1], sides[2], sides[3]);
    }
}

public class TileData {
    public string path;
    public (TileSide up, TileSide right, TileSide down, TileSide left) sides;
    public List<ScoreArea> areas;
    public TileData(string path, TileSide up, TileSide right, TileSide down, TileSide left, ScoreArea[] areas) {
        this.path = path;
        this.sides = (up, right, down, left);
        this.areas = new List<ScoreArea>(areas);
    }
}