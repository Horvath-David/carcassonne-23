using System;
using System.Collections.Generic;
using Godot;
using Godot.NativeInterop;

public enum TileSide {
    Road,
    Field,
    City
}

public class Tile {
    public int rotation;
    public (int X, int Y) pos;

    public TileData type;
    public (TileSide up, TileSide right, TileSide down, TileSide left) sides;

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
    }
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

    public static TileData a1 = new TileData(prefix + "a1.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Field);
    public static TileData a2 = new TileData(prefix + "a2.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Field);

    public static TileData b1 = new TileData(prefix + "b1.jpg", TileSide.Field, TileSide.Field, TileSide.Field, TileSide.Field);
    public static TileData b2 = new TileData(prefix + "b2.jpg", TileSide.Field, TileSide.Field, TileSide.Field, TileSide.Field);
    public static TileData b3 = new TileData(prefix + "b3.jpg", TileSide.Field, TileSide.Field, TileSide.Field, TileSide.Field);
    public static TileData b4 = new TileData(prefix + "b4.jpg", TileSide.Field, TileSide.Field, TileSide.Field, TileSide.Field);

    public static TileData c =  new TileData(prefix +  "c.jpg", TileSide.City, TileSide.City, TileSide.City, TileSide.City);

    public static TileData d1 = new TileData(prefix + "d1.jpg", TileSide.City, TileSide.Road, TileSide.Field, TileSide.Road);
    public static TileData d2 = new TileData(prefix + "d2.jpg", TileSide.City, TileSide.Road, TileSide.Field, TileSide.Road);
    public static TileData d3 = new TileData(prefix + "d3.jpg", TileSide.City, TileSide.Road, TileSide.Field, TileSide.Road);
    public static TileData st = new TileData(prefix + "st.jpg", TileSide.City, TileSide.Road, TileSide.Field, TileSide.Road);

    public static TileData e1 = new TileData(prefix + "e1.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.Field);
    public static TileData e2 = new TileData(prefix + "e2.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.Field);
    public static TileData e3 = new TileData(prefix + "e3.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.Field);
    public static TileData e4 = new TileData(prefix + "e4.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.Field);
    public static TileData e5 = new TileData(prefix + "e5.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.Field);

    public static TileData f1 = new TileData(prefix + "f1.jpg", TileSide.Field, TileSide.City, TileSide.Field, TileSide.City);
    public static TileData f2 = new TileData(prefix + "f2.jpg", TileSide.Field, TileSide.City, TileSide.Field, TileSide.City);

    public static TileData g =  new TileData(prefix +  "g.jpg", TileSide.Field, TileSide.City, TileSide.Field, TileSide.City);

    public static TileData h1 = new TileData(prefix + "h1.jpg", TileSide.City, TileSide.Field, TileSide.City, TileSide.Field);
    public static TileData h2 = new TileData(prefix + "h2.jpg", TileSide.City, TileSide.Field, TileSide.City, TileSide.Field);
    public static TileData h3 = new TileData(prefix + "h3.jpg", TileSide.City, TileSide.Field, TileSide.City, TileSide.Field);

    public static TileData i1 = new TileData(prefix + "i1.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.City);
    public static TileData i2 = new TileData(prefix + "i2.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.City);

    public static TileData j1 = new TileData(prefix + "j1.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Field);
    public static TileData j2 = new TileData(prefix + "j2.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Field);
    public static TileData j3 = new TileData(prefix + "j3.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Field);

    public static TileData k1 = new TileData(prefix + "k1.jpg", TileSide.City, TileSide.Field, TileSide.Road, TileSide.Road);
    public static TileData k2 = new TileData(prefix + "k2.jpg", TileSide.City, TileSide.Field, TileSide.Road, TileSide.Road);
    public static TileData k3 = new TileData(prefix + "k3.jpg", TileSide.City, TileSide.Field, TileSide.Road, TileSide.Road);

    public static TileData l1 = new TileData(prefix + "l1.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Road);
    public static TileData l2 = new TileData(prefix + "l2.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Road);
    public static TileData l3 = new TileData(prefix + "l3.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Road);

    public static TileData m1 = new TileData(prefix + "m1.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.Field);
    public static TileData m2 = new TileData(prefix + "m2.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.Field);

    public static TileData n1 = new TileData(prefix + "n1.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.Field);
    public static TileData n2 = new TileData(prefix + "n2.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.Field);
    public static TileData n3 = new TileData(prefix + "n3.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.Field);

    public static TileData o1 = new TileData(prefix + "o1.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.City);
    public static TileData o2 = new TileData(prefix + "o2.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.City);

    public static TileData p1 = new TileData(prefix + "p1.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.City);
    public static TileData p2 = new TileData(prefix + "p2.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.City);
    public static TileData p3 = new TileData(prefix + "p3.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.City);

    public static TileData q =  new TileData(prefix +  "q.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.City);

    public static TileData r1 = new TileData(prefix + "r1.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.City);
    public static TileData r2 = new TileData(prefix + "r2.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.City);
    public static TileData r3 = new TileData(prefix + "r3.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.City);

    public static TileData s1 = new TileData(prefix + "s1.jpg", TileSide.City, TileSide.City, TileSide.Road, TileSide.City);
    public static TileData s2 = new TileData(prefix + "s2.jpg", TileSide.City, TileSide.City, TileSide.Road, TileSide.City);

    public static TileData t =  new TileData(prefix +  "t.jpg", TileSide.City, TileSide.City, TileSide.Road, TileSide.City);

    public static TileData u1 = new TileData(prefix + "u1.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field);
    public static TileData u2 = new TileData(prefix + "u2.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field);
    public static TileData u3 = new TileData(prefix + "u3.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field);
    public static TileData u4 = new TileData(prefix + "u4.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field);
    public static TileData u5 = new TileData(prefix + "u5.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field);
    public static TileData u6 = new TileData(prefix + "u6.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field);
    public static TileData u7 = new TileData(prefix + "u7.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field);
    public static TileData u8 = new TileData(prefix + "u8.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field);

    public static TileData v1 = new TileData(prefix + "v1.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road);
    public static TileData v2 = new TileData(prefix + "v2.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road);
    public static TileData v3 = new TileData(prefix + "v3.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road);
    public static TileData v4 = new TileData(prefix + "v4.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road);
    public static TileData v5 = new TileData(prefix + "v5.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road);
    public static TileData v6 = new TileData(prefix + "v6.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road);
    public static TileData v7 = new TileData(prefix + "v7.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road);
    public static TileData v8 = new TileData(prefix + "v8.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road);
    public static TileData v9 = new TileData(prefix + "v9.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road);

    public static TileData w1 = new TileData(prefix + "w1.jpg", TileSide.Field, TileSide.Road, TileSide.Road, TileSide.Road);
    public static TileData w2 = new TileData(prefix + "w2.jpg", TileSide.Field, TileSide.Road, TileSide.Road, TileSide.Road);
    public static TileData w3 = new TileData(prefix + "w3.jpg", TileSide.Field, TileSide.Road, TileSide.Road, TileSide.Road);
    public static TileData w4 = new TileData(prefix + "w4.jpg", TileSide.Field, TileSide.Road, TileSide.Road, TileSide.Road);

    public static TileData x =  new TileData(prefix +  "x.jpg", TileSide.Road, TileSide.Road, TileSide.Road, TileSide.Road);
    
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

public class TileData {
    public string path;
    public (TileSide up, TileSide right, TileSide down, TileSide left) sides;
    public TileData(string path, TileSide up, TileSide right, TileSide down, TileSide left) {
        this.path = path;
        this.sides = (up, right, down, left);
    }
}