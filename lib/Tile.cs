
using System.Collections.Generic;

public class Tile {
    public int rotation = 0;
    public (int X, int Y) pos;

    public TileData type;

    public Tile(int x, int y, TileData type) {
        this.pos = (x, y);
        this.type = type;
    }
}

public static class Tiles {
    private static string prefix = "res://assets/tiles/";

    public static TileData a1 = new TileData(prefix + "a1.jpg");
    public static TileData a2 = new TileData(prefix + "a2.jpg");

    public static TileData b1 = new TileData(prefix + "b1.jpg");
    public static TileData b2 = new TileData(prefix + "b2.jpg");
    public static TileData b3 = new TileData(prefix + "b3.jpg");
    public static TileData b4 = new TileData(prefix + "b4.jpg");

    public static TileData c =  new TileData(prefix +  "c.jpg");

    public static TileData d1 = new TileData(prefix + "d1.jpg");
    public static TileData d2 = new TileData(prefix + "d2.jpg");
    public static TileData d3 = new TileData(prefix + "d3.jpg");
    public static TileData st = new TileData(prefix + "st.jpg");

    public static TileData e1 = new TileData(prefix + "e1.jpg");
    public static TileData e2 = new TileData(prefix + "e2.jpg");
    public static TileData e3 = new TileData(prefix + "e3.jpg");
    public static TileData e4 = new TileData(prefix + "e4.jpg");
    public static TileData e5 = new TileData(prefix + "e5.jpg");

    public static TileData f1 = new TileData(prefix + "f1.jpg");
    public static TileData f2 = new TileData(prefix + "f2.jpg");

    public static TileData g =  new TileData(prefix +  "g.jpg");

    public static TileData h1 = new TileData(prefix + "h1.jpg");
    public static TileData h2 = new TileData(prefix + "h2.jpg");
    public static TileData h3 = new TileData(prefix + "h3.jpg");

    public static TileData i1 = new TileData(prefix + "i1.jpg");
    public static TileData i2 = new TileData(prefix + "i2.jpg");

    public static TileData j1 = new TileData(prefix + "j1.jpg");
    public static TileData j2 = new TileData(prefix + "j2.jpg");
    public static TileData j3 = new TileData(prefix + "j3.jpg");

    public static TileData k1 = new TileData(prefix + "k1.jpg");
    public static TileData k2 = new TileData(prefix + "k2.jpg");
    public static TileData k3 = new TileData(prefix + "k3.jpg");

    public static TileData l1 = new TileData(prefix + "l1.jpg");
    public static TileData l2 = new TileData(prefix + "l2.jpg");
    public static TileData l3 = new TileData(prefix + "l3.jpg");

    public static TileData m1 = new TileData(prefix + "m1.jpg");
    public static TileData m2 = new TileData(prefix + "m2.jpg");

    public static TileData n1 = new TileData(prefix + "n1.jpg");
    public static TileData n2 = new TileData(prefix + "n2.jpg");
    public static TileData n3 = new TileData(prefix + "n3.jpg");

    public static TileData o1 = new TileData(prefix + "o1.jpg");
    public static TileData o2 = new TileData(prefix + "o2.jpg");

    public static TileData p1 = new TileData(prefix + "p1.jpg");
    public static TileData p2 = new TileData(prefix + "p2.jpg");
    public static TileData p3 = new TileData(prefix + "p3.jpg");

    public static TileData q =  new TileData(prefix +  "q.jpg");

    public static TileData r1 = new TileData(prefix + "r1.jpg");
    public static TileData r2 = new TileData(prefix + "r2.jpg");
    public static TileData r3 = new TileData(prefix + "r3.jpg");

    public static TileData s1 = new TileData(prefix + "s1.jpg");
    public static TileData s2 = new TileData(prefix + "s2.jpg");

    public static TileData t =  new TileData(prefix +  "t.jpg");

    public static TileData u1 = new TileData(prefix + "u1.jpg");
    public static TileData u2 = new TileData(prefix + "u2.jpg");
    public static TileData u3 = new TileData(prefix + "u3.jpg");
    public static TileData u4 = new TileData(prefix + "u4.jpg");
    public static TileData u5 = new TileData(prefix + "u5.jpg");
    public static TileData u6 = new TileData(prefix + "u6.jpg");
    public static TileData u7 = new TileData(prefix + "u7.jpg");
    public static TileData u8 = new TileData(prefix + "u8.jpg");

    public static TileData v1 = new TileData(prefix + "v1.jpg");
    public static TileData v2 = new TileData(prefix + "v2.jpg");
    public static TileData v3 = new TileData(prefix + "v3.jpg");
    public static TileData v4 = new TileData(prefix + "v4.jpg");
    public static TileData v5 = new TileData(prefix + "v5.jpg");
    public static TileData v6 = new TileData(prefix + "v6.jpg");
    public static TileData v7 = new TileData(prefix + "v7.jpg");
    public static TileData v8 = new TileData(prefix + "v8.jpg");
    public static TileData v9 = new TileData(prefix + "v9.jpg");

    public static TileData w1 = new TileData(prefix + "w1.jpg");
    public static TileData w2 = new TileData(prefix + "w2.jpg");
    public static TileData w3 = new TileData(prefix + "w3.jpg");
    public static TileData w4 = new TileData(prefix + "w4.jpg");

    public static TileData x =  new TileData(prefix +  "x.jpg");
    
    public static List<TileData> all = new List<TileData> {
        a1,
        a2,
        b1,
        b2,
        b3,
        b4,
        c ,
        d1,
        d2,
        d3,
        st,
        e1,
        e2,
        e3,
        e4,
        e5,
        f1,
        f2,
        g ,
        h1,
        h2,
        h3,
        i1,
        i2,
        j1,
        j2,
        j3,
        k1,
        k2,
        k3,
        l1,
        l2,
        l3,
        m1,
        m2,
        n1,
        n2,
        n3,
        o1,
        o2,
        p1,
        p2,
        p3,
        q ,
        r1,
        r2,
        r3,
        s1,
        s2,
        t ,
        u1,
        u2,
        u3,
        u4,
        u5,
        u6,
        u7,
        u8,
        v1,
        v2,
        v3,
        v4,
        v5,
        v6,
        v7,
        v8,
        v9,
        w1,
        w2,
        w3,
        w4,
        x
    };
}

public class TileData {
    public string path;
    public TileData(string path) {
        this.path = path;
    }
}