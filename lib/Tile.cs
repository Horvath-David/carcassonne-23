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
    public static Vector2[] topCity = new[] {
        new Vector2(-512, -512), new Vector2(-370, -350), new Vector2(-170, -210), new Vector2(200, -220),
        new Vector2(400, -280), new Vector2(512, -460), new Vector2(512, -512)
    };
    public static Vector2[] wholeCity = new[] {
        new Vector2(-512, -512), new Vector2(-512, 512), new Vector2(512, 512), new Vector2(512, -512)
    };
    public static Vector2[] monastery1 = new[] {
        new Vector2(-320, -80), new Vector2(-310, 80), new Vector2(-180, 150), new Vector2(70, 200),
        new Vector2(250, 100), new Vector2(240, -60), new Vector2(150, -160), new Vector2(-30, -240),
        new Vector2(-30, -360), new Vector2(-130, -470), new Vector2(-240, -320), new Vector2(-230, -180)
    };
    public static Vector2[] bottomRoad1 = new[] {
        new Vector2(120, 190), new Vector2(220, 130), new Vector2(300, 190), new Vector2(300, 360),
        new Vector2(170, 440), new Vector2(50, 512), new Vector2(-50, 512), new Vector2(10, 400), 
        new Vector2(160, 330), new Vector2(200, 260)
    };
    public static Vector2[] monastery2 = new[] {
        new Vector2(-330, 140), new Vector2(-20, 320), new Vector2(270, 180), new Vector2(270, 10),
        new Vector2(60, -110), new Vector2(-50, -380), new Vector2(-170, -170), new Vector2(-330, -60)
    };
    public static Vector2[] topCity2 = new[] {
        new Vector2(-512, -512), new Vector2(-230, -210), new Vector2(10, -110), new Vector2(300, -110),
        new Vector2(512, -512)
    };
    public static Vector2[] topCity3 = new[] {
        new Vector2(-512, -512), new Vector2(-390, -260), new Vector2(-160, -130), new Vector2(80, -130),
        new Vector2(280, -170), new Vector2(460, -280), new Vector2(512, -512)
    };
    public static Vector2[] topCity4 = new[] {
        new Vector2(-512, -512), new Vector2(-300, -110), new Vector2(0, -110), new Vector2(230, -200),
        new Vector2(512, -512)
    };
    public static Vector2[] bridgeCity = new[] {
        new Vector2(-512, -512), new Vector2(-390, -430), new Vector2(-290, -320), new Vector2(-160, -230),
        new Vector2(100, -220), new Vector2(350, -390), new Vector2(512, -512), new Vector2(512, 512),
        new Vector2(310, 400), new Vector2(100, 370), new Vector2(-80, 330), new Vector2(-400, 430),
        new Vector2(-512, 512)
    };
    public static Vector2[] bottomCity = new[] {
        new Vector2(-512, 512), new Vector2(-360, 280), new Vector2(-170, 230), new Vector2(160, 220),
        new Vector2(410, 400), new Vector2(512, 512)
    };
    public static Vector2[] leftCity = new[] {
        new Vector2(-512, -512), new Vector2(-260, -280), new Vector2(-190, -110), new Vector2(-200, 140),
        new Vector2(-290, 380), new Vector2(-512, 512)
    };
    public static Vector2[] topToRightCity = new[] {
        new Vector2(-512, -512), new Vector2(-350, -160), new Vector2(140, 120), new Vector2(140, 240),
        new Vector2(320, 320), new Vector2(512, 512), new Vector2(512, -512)
    };
    public static Vector2[] topToLeftCity = new[] {
        new Vector2(-512, -512), new Vector2(512, -512), new Vector2(380, -260), new Vector2(320, -90),
        new Vector2(-20, 130), new Vector2(-310, 260), new Vector2(-320, 340), new Vector2(-430, 480),
        new Vector2(-512, 512)
    };
    public static Vector2[] noBottomCity = new[] {
        new Vector2(-512, -512), new Vector2(512, -512), new Vector2(512, 512), new Vector2(360, 430),
        new Vector2(240, 290), new Vector2(70, 210), new Vector2(-70, 210), new Vector2(-230, 300),
        new Vector2(-512, 512)
    };
    public static Vector2[] topRoad = new[] {
        new Vector2(-50, -512), new Vector2(50, -512), new Vector2(20, -350), new Vector2(-10, -240),
        new Vector2(-100, -260), new Vector2(-60, -380)
    };
    public static Vector2[] leftToRightRoad = new[] {
        new Vector2(-512, -50), new Vector2(-360, 0), new Vector2(-160, 90), new Vector2(-10, 70),
        new Vector2(70, 20), new Vector2(240, 20), new Vector2(380, -30), new Vector2(512, -50),
        new Vector2(512, 50), new Vector2(360, 80), new Vector2(230, 120), new Vector2(80, 130),
        new Vector2(-80, 180), new Vector2(-210, 180), new Vector2(-400, 80), new Vector2(-512, 50)
    };
    public static Vector2[] topToBottomRoad = new[] {
        new Vector2(-50, -512), new Vector2(-210, -270), new Vector2(-190, -80), new Vector2(20, 70),
        new Vector2(120, 210), new Vector2(-30, 410), new Vector2(-50, 512), new Vector2(50, 512),
        new Vector2(90, 420), new Vector2(230, 290), new Vector2(220, 160), new Vector2(90, -30),
        new Vector2(-80, -130), new Vector2(-110, -240), new Vector2(20, -410), new Vector2(50, -512)
    };
    public static Vector2[] leftToBottomRoad1 = new[] {
        new Vector2(-512, -50), new Vector2(-330, -130), new Vector2(-110, -90), new Vector2(160, 40),
        new Vector2(200, 180), new Vector2(70, 340), new Vector2(50, 512), new Vector2(-50, 512),
        new Vector2(-40, 290), new Vector2(80, 140), new Vector2(-60, 40), new Vector2(-310, -20),
        new Vector2(-512, 50)
    };
    public static Vector2[] leftToBottomRoad2 = new[] {
        new Vector2(-512, -50), new Vector2(-380, -160), new Vector2(-180, -170), new Vector2(-110, -70),
        new Vector2(-150, 80), new Vector2(-90, 170), new Vector2(110, 230), new Vector2(140, 340),
        new Vector2(50, 512), new Vector2(-50, 512), new Vector2(-50, 420), new Vector2(30, 320),
        new Vector2(-110, 270), new Vector2(-230, 170), new Vector2(-230, 30), new Vector2(-240, -80),
        new Vector2(-370, -60), new Vector2(-512, 50)
    };
    public static Vector2[] leftToBottomRoad3 = new[] {
        new Vector2(-512, -50), new Vector2(-210, -140), new Vector2(-50, -140), new Vector2(150, 0),
        new Vector2(220, 240), new Vector2(110, 380), new Vector2(50, 512), new Vector2(-50, 512),
        new Vector2(-20, 370), new Vector2(90, 200), new Vector2(30, 40), new Vector2(-120, -40),
        new Vector2(-512, 50)
    };
    public static Vector2[] bottomRoad2 = new[] {
        new Vector2(-30, 180), new Vector2(80, 180), new Vector2(60, 290), new Vector2(0, 370),
        new Vector2(50, 430), new Vector2(50, 512), new Vector2(-50, 512), new Vector2(-140, 350)
    };
    public static Vector2[] bottomRoad3 = new[] {
        new Vector2(-60, 200), new Vector2(50, 200), new Vector2(80, 290), new Vector2(40, 410),
        new Vector2(50, 512), new Vector2(-50, 512), new Vector2(-60, 370), new Vector2(-20, 290)
    };
    public static Vector2[] bottomRoad4 = new[] {
        new Vector2(-120, 200), new Vector2(30, 200), new Vector2(160, 300), new Vector2(170, 380),
        new Vector2(50, 512), new Vector2(-50, 512), new Vector2(-10, 400), new Vector2(50, 340)
    };
    public static Vector2[] bottomRoad5 = new[] {
        new Vector2(-80, 100), new Vector2(10, 100), new Vector2(30, 280), new Vector2(20, 390),
        new Vector2(50, 512), new Vector2(-50, 512), new Vector2(-60, 370), new Vector2(-50, 200)
    };
    public static Vector2[] leftRoad1 = new[] {
        new Vector2(-512, -50), new Vector2(-390, -40), new Vector2(-170, 20), new Vector2(-170, 120),
        new Vector2(-300, 100), new Vector2(-400, 40), new Vector2(-512, 50)
    };
    public static Vector2[] leftRoad2 = new[] {
        new Vector2(-512, -50), new Vector2(-370, -50), new Vector2(-220, -30), new Vector2(-210, 60),
        new Vector2(-380, 50), new Vector2(-512, 50)
    };
    public static Vector2[] rightRoad1 = new[] {
        new Vector2(512, -50), new Vector2(512, 50), new Vector2(340, 10), new Vector2(200, 20),
        new Vector2(200, -60), new Vector2(340, -80)
    };
    public static Vector2[] rightRoad2 = new[] {
        new Vector2(512, -50), new Vector2(512, 50), new Vector2(350, 90), new Vector2(200, 60),
        new Vector2(200, -40), new Vector2(350, 0)
    };
    
    public static Vector2[] rightRoad3 = new[] {
        new Vector2(512, -50), new Vector2(512, 50), new Vector2(200, 30), new Vector2(200, -60)
    };
    
    public static Vector2[] rightToBottomRoad1 = new[] {
        new Vector2(512, -50), new Vector2(512, 30), new Vector2(270, 30), new Vector2(230, 130),
        new Vector2(-50, 130), new Vector2(-60, 190), new Vector2(40, 300), new Vector2(50, 512),
        new Vector2(-50, 512), new Vector2(-60, 350), new Vector2(-160, 240), new Vector2(-190, 120),
        new Vector2(-90, 30), new Vector2(140, 30), new Vector2(190, -70)
    };
    public static Vector2[] rightToBottomRoad2 = new[] {
        new Vector2(512, -50), new Vector2(512, 30), new Vector2(260, -20), new Vector2(50, 40),
        new Vector2(-100, 130), new Vector2(30, 300), new Vector2(50, 512), new Vector2(-50, 512),
        new Vector2(-60, 350), new Vector2(-210, 160), new Vector2(-170, 50), new Vector2(100, -100),
        new Vector2(350, -120)
    };
    public static Vector2[] rightToBottomRoad3 = new[] {
        new Vector2(512, -50), new Vector2(512, 30), new Vector2(390, 60), new Vector2(300, 190),
        new Vector2(110, 280), new Vector2(110, 380), new Vector2(50, 512), new Vector2(-50, 512),
        new Vector2(0, 390), new Vector2(-10, 290), new Vector2(60, 200), new Vector2(240, 120),
        new Vector2(340, -20)
    };
    public static Vector2[] rightToBottomRoad4 = new[] {
        new Vector2(512, -50), new Vector2(512, 30), new Vector2(390, 70), new Vector2(230, 140),
        new Vector2(160, 260), new Vector2(0, 340), new Vector2(50, 512), new Vector2(-50, 512),
        new Vector2(-100, 370), new Vector2(-50, 240), new Vector2(80, 200), new Vector2(150, 90),
        new Vector2(320, -10)
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
    
    public static TileData a1 = new TileData(prefix + "a1.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.monastery1, AreaType.Monastery, (false, false, false, false)),
        new ScoreArea(RegionShapes.bottomRoad1, AreaType.Road, (false, false, true, false)),
    });
    public static TileData a2 = new TileData(prefix + "a2.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.monastery1, AreaType.Monastery, (false, false, false, false)),
        new ScoreArea(RegionShapes.bottomRoad1, AreaType.Road, (false, false, true, false)),
    });

    public static TileData b1 = new TileData(prefix + "b1.jpg", TileSide.Field, TileSide.Field, TileSide.Field, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.monastery2, AreaType.Monastery, (false, false, false, false)),
    });
    public static TileData b2 = new TileData(prefix + "b2.jpg", TileSide.Field, TileSide.Field, TileSide.Field, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.monastery2, AreaType.Monastery, (false, false, false, false)),
    });
    public static TileData b3 = new TileData(prefix + "b3.jpg", TileSide.Field, TileSide.Field, TileSide.Field, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.monastery2, AreaType.Monastery, (false, false, false, false)),
    });
    public static TileData b4 = new TileData(prefix + "b4.jpg", TileSide.Field, TileSide.Field, TileSide.Field, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.monastery2, AreaType.Monastery, (false, false, false, false)),
    });

    public static TileData c =  new TileData(prefix +  "c.jpg", TileSide.City, TileSide.City, TileSide.City, TileSide.City, new [] {
        new ScoreArea(RegionShapes.wholeCity, AreaType.City, (true, true, true, true), shield: true),
    });

    public static TileData d1 = new TileData(prefix + "d1.jpg", TileSide.City, TileSide.Road, TileSide.Field, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.topCity, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.leftToRightRoad, AreaType.Road, (false, true, false, true)),
    });
    public static TileData d2 = new TileData(prefix + "d2.jpg", TileSide.City, TileSide.Road, TileSide.Field, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.topCity, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.leftToRightRoad, AreaType.Road, (false, true, false, true)),
    });
    public static TileData d3 = new TileData(prefix + "d3.jpg", TileSide.City, TileSide.Road, TileSide.Field, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.topCity, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.leftToRightRoad, AreaType.Road, (false, true, false, true)),
    });
    public static TileData st = new TileData(prefix + "st.jpg", TileSide.City, TileSide.Road, TileSide.Field, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.topCity, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.leftToRightRoad, AreaType.Road, (false, true, false, true)),
    });

    public static TileData e1 = new TileData(prefix + "e1.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topCity2, AreaType.City, (true, false, false, false)),
    });
    public static TileData e2 = new TileData(prefix + "e2.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topCity2, AreaType.City, (true, false, false, false)),
    });
    public static TileData e3 = new TileData(prefix + "e3.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topCity2, AreaType.City, (true, false, false, false)),
    });
    public static TileData e4 = new TileData(prefix + "e4.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topCity2, AreaType.City, (true, false, false, false)),
    });
    public static TileData e5 = new TileData(prefix + "e5.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topCity2, AreaType.City, (true, false, false, false)),
    });

    public static TileData f1 = new TileData(prefix + "f1.jpg", TileSide.Field, TileSide.City, TileSide.Field, TileSide.City, new [] {
        new ScoreArea(RegionShapes.bridgeCity, AreaType.City, (false, true, false, true), shield: true),
    });
    public static TileData f2 = new TileData(prefix + "f2.jpg", TileSide.Field, TileSide.City, TileSide.Field, TileSide.City, new [] {
        new ScoreArea(RegionShapes.bridgeCity, AreaType.City, (false, true, false, true), shield: true),
    });

    public static TileData g =  new TileData(prefix +  "g.jpg", TileSide.Field, TileSide.City, TileSide.Field, TileSide.City, new [] {
        new ScoreArea(RegionShapes.bridgeCity, AreaType.City, (false, true, false, true)),
    });

    public static TileData h1 = new TileData(prefix + "h1.jpg", TileSide.City, TileSide.Field, TileSide.City, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topCity3, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.bottomCity, AreaType.City, (false, false, true, false)),
    });
    public static TileData h2 = new TileData(prefix + "h2.jpg", TileSide.City, TileSide.Field, TileSide.City, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topCity3, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.bottomCity, AreaType.City, (false, false, true, false)),
    });
    public static TileData h3 = new TileData(prefix + "h3.jpg", TileSide.City, TileSide.Field, TileSide.City, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topCity3, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.bottomCity, AreaType.City, (false, false, true, false)),
    });

    public static TileData i1 = new TileData(prefix + "i1.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.City, new [] {
        new ScoreArea(RegionShapes.topCity, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.leftCity, AreaType.City, (false, false, false, true)),
    });
    public static TileData i2 = new TileData(prefix + "i2.jpg", TileSide.City, TileSide.Field, TileSide.Field, TileSide.City, new [] {
        new ScoreArea(RegionShapes.topCity, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.leftCity, AreaType.City, (false, false, false, true)),
    });

    public static TileData j1 = new TileData(prefix + "j1.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topCity3, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.rightToBottomRoad1, AreaType.Road, (false, true, true, false)),
    });
    public static TileData j2 = new TileData(prefix + "j2.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topCity4, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.rightToBottomRoad2, AreaType.Road, (false, true, true, false)),
    });
    public static TileData j3 = new TileData(prefix + "j3.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topCity3, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.rightToBottomRoad1, AreaType.Road, (false, true, true, false)),
    });

    public static TileData k1 = new TileData(prefix + "k1.jpg", TileSide.City, TileSide.Field, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.topCity2, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.leftToBottomRoad1, AreaType.Road, (false, false, true, true)),
    });
    public static TileData k2 = new TileData(prefix + "k2.jpg", TileSide.City, TileSide.Field, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.topCity2, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.leftToBottomRoad1, AreaType.Road, (false, false, true, true)),
    });
    public static TileData k3 = new TileData(prefix + "k3.jpg", TileSide.City, TileSide.Field, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.topCity2, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.leftToBottomRoad1, AreaType.Road, (false, false, true, true)),
    });

    public static TileData l1 = new TileData(prefix + "l1.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.topCity, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.leftRoad1, AreaType.Road, (false, false, false, true)),
        new ScoreArea(RegionShapes.bottomRoad2, AreaType.Road, (false, false, true, false)),
        new ScoreArea(RegionShapes.rightRoad1, AreaType.Road, (false, true, false, false)),
    });
    public static TileData l2 = new TileData(prefix + "l2.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.topCity, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.leftRoad1, AreaType.Road, (false, false, false, true)),
        new ScoreArea(RegionShapes.bottomRoad2, AreaType.Road, (false, false, true, false)),
        new ScoreArea(RegionShapes.rightRoad1, AreaType.Road, (false, true, false, false)),
    });
    public static TileData l3 = new TileData(prefix + "l3.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.topCity, AreaType.City, (true, false, false, false)),
        new ScoreArea(RegionShapes.leftRoad1, AreaType.Road, (false, false, false, true)),
        new ScoreArea(RegionShapes.bottomRoad2, AreaType.Road, (false, false, true, false)),
        new ScoreArea(RegionShapes.rightRoad1, AreaType.Road, (false, true, false, false)),
    });

    public static TileData m1 = new TileData(prefix + "m1.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topToRightCity, AreaType.City, (true, true, false, false), shield: true),
    });
    public static TileData m2 = new TileData(prefix + "m2.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topToRightCity, AreaType.City, (true, true, false, false), shield: true),
    });

    public static TileData n1 = new TileData(prefix + "n1.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topToRightCity, AreaType.City, (true, true, false, false)),
    });
    public static TileData n2 = new TileData(prefix + "n2.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topToRightCity, AreaType.City, (true, true, false, false)),
    });
    public static TileData n3 = new TileData(prefix + "n3.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topToRightCity, AreaType.City, (true, true, false, false)),
    });

    public static TileData o1 = new TileData(prefix + "o1.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.City, new [] {
        new ScoreArea(RegionShapes.topToLeftCity, AreaType.City, (true, false, false, true), shield: true),
        new ScoreArea(RegionShapes.rightToBottomRoad3, AreaType.Road, (false, true, true, false)),
    });
    public static TileData o2 = new TileData(prefix + "o2.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.City, new [] {
        new ScoreArea(RegionShapes.topToLeftCity, AreaType.City, (true, false, false, true), shield: true),
        new ScoreArea(RegionShapes.rightToBottomRoad3, AreaType.Road, (false, true, true, false)),
    });

    public static TileData p1 = new TileData(prefix + "p1.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.City, new [] {
        new ScoreArea(RegionShapes.topToLeftCity, AreaType.City, (true, false, false, true)),
        new ScoreArea(RegionShapes.rightToBottomRoad3, AreaType.Road, (false, true, true, false)),
    });
    public static TileData p2 = new TileData(prefix + "p2.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.City, new [] {
        new ScoreArea(RegionShapes.topToLeftCity, AreaType.City, (true, false, false, true)),
        new ScoreArea(RegionShapes.rightToBottomRoad3, AreaType.Road, (false, true, true, false)),
    });
    public static TileData p3 = new TileData(prefix + "p3.jpg", TileSide.City, TileSide.Road, TileSide.Road, TileSide.City, new [] {
        new ScoreArea(RegionShapes.topToLeftCity, AreaType.City, (true, false, false, true)),
        new ScoreArea(RegionShapes.rightToBottomRoad4, AreaType.Road, (false, true, true, false)),
    });

    public static TileData q =  new TileData(prefix +  "q.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.City, new [] {
        new ScoreArea(RegionShapes.noBottomCity, AreaType.City, (true, true, false, true), shield: true),
    });

    public static TileData r1 = new TileData(prefix + "r1.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.City, new [] {
        new ScoreArea(RegionShapes.noBottomCity, AreaType.City, (true, true, false, true)),
    });
    public static TileData r2 = new TileData(prefix + "r2.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.City, new [] {
        new ScoreArea(RegionShapes.noBottomCity, AreaType.City, (true, true, false, true)),
    });
    public static TileData r3 = new TileData(prefix + "r3.jpg", TileSide.City, TileSide.City, TileSide.Field, TileSide.City, new [] {
        new ScoreArea(RegionShapes.noBottomCity, AreaType.City, (true, true, false, true)),
    });

    public static TileData s1 = new TileData(prefix + "s1.jpg", TileSide.City, TileSide.City, TileSide.Road, TileSide.City, new [] {
        new ScoreArea(RegionShapes.noBottomCity, AreaType.City, (true, true, false, true), shield: true),
        new ScoreArea(RegionShapes.bottomRoad3, AreaType.Road, (false, false, true, false)),
    });
    public static TileData s2 = new TileData(prefix + "s2.jpg", TileSide.City, TileSide.City, TileSide.Road, TileSide.City, new [] {
        new ScoreArea(RegionShapes.noBottomCity, AreaType.City, (true, true, false, true), shield: true),
        new ScoreArea(RegionShapes.bottomRoad3, AreaType.Road, (false, false, true, false)),
    });

    public static TileData t =  new TileData(prefix +  "t.jpg", TileSide.City, TileSide.City, TileSide.Road, TileSide.City, new [] {
        new ScoreArea(RegionShapes.noBottomCity, AreaType.City, (true, true, false, true)),
        new ScoreArea(RegionShapes.bottomRoad3, AreaType.Road, (false, false, true, false)),
    });

    public static TileData u1 = new TileData(prefix + "u1.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topToBottomRoad, AreaType.Road, (true, false, true, false)),
    });
    public static TileData u2 = new TileData(prefix + "u2.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topToBottomRoad, AreaType.Road, (true, false, true, false)),
    });
    public static TileData u3 = new TileData(prefix + "u3.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topToBottomRoad, AreaType.Road, (true, false, true, false)),
    });
    public static TileData u4 = new TileData(prefix + "u4.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topToBottomRoad, AreaType.Road, (true, false, true, false)),
    });
    public static TileData u5 = new TileData(prefix + "u5.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topToBottomRoad, AreaType.Road, (true, false, true, false)),
    });
    public static TileData u6 = new TileData(prefix + "u6.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topToBottomRoad, AreaType.Road, (true, false, true, false)),
    });
    public static TileData u7 = new TileData(prefix + "u7.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topToBottomRoad, AreaType.Road, (true, false, true, false)),
    });
    public static TileData u8 = new TileData(prefix + "u8.jpg", TileSide.Road, TileSide.Field, TileSide.Road, TileSide.Field, new [] {
        new ScoreArea(RegionShapes.topToBottomRoad, AreaType.Road, (true, false, true, false)),
    });

    public static TileData v1 = new TileData(prefix + "v1.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.leftToBottomRoad2, AreaType.Road, (false, false, true, true)),
    });
    public static TileData v2 = new TileData(prefix + "v2.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.leftToBottomRoad3, AreaType.Road, (false, false, true, true)),
    });
    public static TileData v3 = new TileData(prefix + "v3.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.leftToBottomRoad3, AreaType.Road, (false, false, true, true)),
    });
    public static TileData v4 = new TileData(prefix + "v4.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.leftToBottomRoad3, AreaType.Road, (false, false, true, true)),
    });
    public static TileData v5 = new TileData(prefix + "v5.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.leftToBottomRoad2, AreaType.Road, (false, false, true, true)),
    });
    public static TileData v6 = new TileData(prefix + "v6.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.leftToBottomRoad2, AreaType.Road, (false, false, true, true)),
    });
    public static TileData v7 = new TileData(prefix + "v7.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.leftToBottomRoad2, AreaType.Road, (false, false, true, true)),
    });
    public static TileData v8 = new TileData(prefix + "v8.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.leftToBottomRoad3, AreaType.Road, (false, false, true, true)),
    });
    public static TileData v9 = new TileData(prefix + "v9.jpg", TileSide.Field, TileSide.Field, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.leftToBottomRoad3, AreaType.Road, (false, false, true, true)),
    });

    public static TileData w1 = new TileData(prefix + "w1.jpg", TileSide.Field, TileSide.Road, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.bottomRoad4, AreaType.Road, (false, false, true, false)),
        new ScoreArea(RegionShapes.rightRoad2, AreaType.Road, (false, true, false, false)),
        new ScoreArea(RegionShapes.leftRoad2, AreaType.Road, (false, false, false, true)),
    });
    public static TileData w2 = new TileData(prefix + "w2.jpg", TileSide.Field, TileSide.Road, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.bottomRoad4, AreaType.Road, (false, false, true, false)),
        new ScoreArea(RegionShapes.rightRoad2, AreaType.Road, (false, true, false, false)),
        new ScoreArea(RegionShapes.leftRoad2, AreaType.Road, (false, false, false, true)),
    });
    public static TileData w3 = new TileData(prefix + "w3.jpg", TileSide.Field, TileSide.Road, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.bottomRoad4, AreaType.Road, (false, false, true, false)),
        new ScoreArea(RegionShapes.rightRoad2, AreaType.Road, (false, true, false, false)),
        new ScoreArea(RegionShapes.leftRoad2, AreaType.Road, (false, false, false, true)),
    });
    public static TileData w4 = new TileData(prefix + "w4.jpg", TileSide.Field, TileSide.Road, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.bottomRoad4, AreaType.Road, (false, false, true, false)),
        new ScoreArea(RegionShapes.rightRoad2, AreaType.Road, (false, true, false, false)),
        new ScoreArea(RegionShapes.leftRoad2, AreaType.Road, (false, false, false, true)),
    });

    public static TileData x =  new TileData(prefix +  "x.jpg", TileSide.Road, TileSide.Road, TileSide.Road, TileSide.Road, new [] {
        new ScoreArea(RegionShapes.topRoad, AreaType.Road, (true, false, false, false)),
        new ScoreArea(RegionShapes.bottomRoad5, AreaType.Road, (false, false, true, false)),
        new ScoreArea(RegionShapes.rightRoad3, AreaType.Road, (false, true, false, false)),
        new ScoreArea(RegionShapes.leftRoad1, AreaType.Road, (false, false, false, true)),
    });
    
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
    City, Road, Monastery, Garden
}
public class ScoreArea {
    public Vector2[] shape;
    public AreaType type;
    public (bool up, bool right, bool down, bool left) sides;
    public (int X, int Y) pos;
    public bool shield = false;

    public ScoreArea(Vector2[] shape, AreaType type, (bool up, bool right, bool down, bool left) sides, (int X, int Y)? pos = null, bool shield = false) {
        this.shape = shape;
        this.type = type;
        this.sides = sides;
        this.pos = pos ?? (0, 0);
        this.shield = shield;
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