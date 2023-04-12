using System;
using Godot;
using System.Collections.Generic;
using System.Linq;

public enum GamePhase {
    Running,
    Ended
}

public partial class Manager : Node2D {
    public GamePhase phase = GamePhase.Running;
    public static GameState gameState = new GameState();
    public static Dictionary<(int X, int Y), EmptyFrame> emptyFrames = new Dictionary<(int X, int Y), EmptyFrame>();

    public static List<Tile> addBuffer = new List<Tile>();
    public static List<(int X, int Y)> emptyPlaceBuffer = new List<(int X, int Y)>();

    public static Dictionary<(int X, int Y, int idx), ScoreAreaController> areaControllers =
        new Dictionary<(int X, int Y, int idx), ScoreAreaController>();

    public static List<ScoreAreaController> pendingAreas = new List<ScoreAreaController>();

    public static int rotation = 0;
    public static bool shouldCheck = false;
    public static bool preview = true;

    public static PackedScene scoreArea = GD.Load<PackedScene>("res://prefabs/score_area.tscn");
    PackedScene tilePrefab = GD.Load<PackedScene>("res://prefabs/tile.tscn");
    PackedScene emptyFrame = GD.Load<PackedScene>("res://prefabs/frame.tscn");

    [Export] public Node2D boardNode;
    public static UIManager uiManager;

    public override void _Ready() {
        uiManager = GetParent().GetNode("UIScene") as UIManager;

        gameState.tilesLeft = new List<TileData>(Tiles.all);
        gameState.nextTile = Tiles.st;
        gameState.tilesLeft.Remove(Tiles.st);

        PlaceTile(new Tile(0, 0, gameState.nextTile));
        
    }

    public override void _Process(double delta) {
        switch (phase) {
            case GamePhase.Running: {
                AppyAddBuffer();
                ApplyEmptyPlaceBuffer();

                if (gameState.gameOver && preview) EndGame(gameState.normalGameOver);
                break;
            }
            case GamePhase.Ended: {
                break;
            }
        }
    }

    private void AppyAddBuffer() {
        foreach (var tile in addBuffer) {
            var instance = tilePrefab.Instantiate() as TileController;
            instance.Set("position", new Vector2(tile.pos.X * 100, -tile.pos.Y * 100));
            instance.Texture = GD.Load<Texture2D>(tile.type.path);
            instance.Rotation = (float)(Math.PI / 180f) * tile.rotation * 90f;
            instance.tile = tile;
            if (tile.type == Tiles.st) instance.canPlaceMeeple = false;
            boardNode.AddChild(instance);
        }

        addBuffer = new List<Tile>();
    }

    private void ApplyEmptyPlaceBuffer() {
        foreach ((int X, int Y) emptyPlace in emptyPlaceBuffer) {
            if (emptyFrames.GetValueOrDefault(emptyPlace, null) != null) continue;
            var instance = emptyFrame.Instantiate() as EmptyFrame;
            instance.Position = new Vector2(emptyPlace.X * 100, -emptyPlace.Y * 100);
            instance.X = emptyPlace.X;
            instance.Y = emptyPlace.Y;
            boardNode.AddChild(instance);
            emptyFrames.Add(emptyPlace, instance);
        }

        if (emptyPlaceBuffer.Count > 0 || shouldCheck) {
            HideFrames();
            shouldCheck = false;
            var score = gameState.regions.Select(r => r.GetScore(true)).Aggregate(0, (a, b) => a + b);
            //uiManager.SetScore(score);
        }

        emptyPlaceBuffer = new List<(int X, int Y)>();
    }

    public static void PlaceTile(Tile tile) {
        if (gameState.gameOver) return;

        var sides = gameState.PlaceTile(tile);
        addBuffer.Add(gameState.board.Get(tile.pos.X, tile.pos.Y));

        if (sides.up) emptyPlaceBuffer.Add((tile.pos.X, tile.pos.Y + 1));
        if (sides.right) emptyPlaceBuffer.Add((tile.pos.X + 1, tile.pos.Y));
        if (sides.down) emptyPlaceBuffer.Add((tile.pos.X, tile.pos.Y - 1));
        if (sides.left) emptyPlaceBuffer.Add((tile.pos.X - 1, tile.pos.Y));

        if (!gameState.lite && tile.type != Tiles.st) {
            uiManager.placeMeeple.Show();
            preview = false;
        }

        uiManager.nextTileRect.Texture = GD.Load<CompressedTexture2D>(gameState.nextTile.path);
        uiManager.SetLeft(gameState.tilesLeft.Count + 1);

        ChangeRotation(0);

        shouldCheck = true;
    }

    public static void PlaceMeeple(int x, int y, int idx) {
        uiManager.placeMeeple.Hide();
        gameState.PlaceMeeple(x, y, idx);
        preview = true;
        UpdateColors();
        pendingAreas = new List<ScoreAreaController>();
        uiManager.SetScore(gameState.players[0].score);
    }

    public static void SkipMeeple() {
        uiManager.placeMeeple.Hide();
        gameState.CheckComplete();
        preview = true;
        UpdateColors();
        pendingAreas = new List<ScoreAreaController>();
        uiManager.SetScore(gameState.players[0].score);
    }

    public static void UpdateColors() {
        foreach (var area in pendingAreas) {
            area.poly.Color = Colors.Transparent;
        }

        foreach (var region in gameState.regions) {
            Color color = Colors.Transparent;
            if (region.meeples.Count == 0) {
                color = Colors.Transparent;
            }
            if (region.meeples.Count == 1) {
                color = new Color(gameState.players[region.meeples.Keys.First()].color, 0.5f);
            }
            else if (region.meeples.Count > 1) {
                var sorted = region.meeples.OrderByDescending(e => e.Value).ToList();
                if (sorted[0].Value == sorted[1].Value) {
                    color = new Color(Colors.SlateGray, 0.5f);
                }
                else {
                    color = new Color(gameState.players[sorted[0].Key].color, 0.5f);
                }
            }
            
            foreach (var area in areaControllers.Values) {
                if (region.areas.Find(a => a == area.scoreArea) != null) area.poly.Color = color;
            }

        }
    }

    public static void ChangeRotation(int rotation = -1) {
        if (rotation < 0 || rotation > 3) {
            Manager.rotation++;
            if (Manager.rotation == 4) Manager.rotation = 0;
        } else {
            Manager.rotation = rotation;
        }

        uiManager.nextTileRect.Rotation = (float)(Math.PI / 180f) * Manager.rotation * 90f;
        foreach (var frame in emptyFrames.Values) {
            frame.Rotate(Manager.rotation);
        }
    }

    public static void HideFrames() {
        foreach (var frame in emptyFrames.Values) {
            var shouldHide = true;
            
            var tile = new Tile(frame.X, frame.Y, Manager.gameState.nextTile);
            
            if (Manager.gameState.board.IsLegal(tile)) shouldHide = false;
            if (Manager.gameState.board.IsLegal(tile)) shouldHide = false;
            if (Manager.gameState.board.IsLegal(tile)) shouldHide = false;
            if (Manager.gameState.board.IsLegal(tile)) shouldHide = false;

            if (Manager.gameState.board.IsLegal(Tiles.Rotate(0,
                    new Tile(frame.X, frame.Y, Manager.gameState.nextTile)))) shouldHide = false;
            if (Manager.gameState.board.IsLegal(Tiles.Rotate(1,
                    new Tile(frame.X, frame.Y, Manager.gameState.nextTile)))) shouldHide = false;
            if (Manager.gameState.board.IsLegal(Tiles.Rotate(2,
                    new Tile(frame.X, frame.Y, Manager.gameState.nextTile)))) shouldHide = false;
            if (Manager.gameState.board.IsLegal(Tiles.Rotate(3,
                    new Tile(frame.X, frame.Y, Manager.gameState.nextTile)))) shouldHide = false;

            if (shouldHide) {
                frame.Hide();
            } else {
                frame.Show();
            }
        }
    }
    
    public void CheckRegions() {
        throw new NotImplementedException();
    }

    private void EndGame(bool normal) {
        phase = GamePhase.Ended;
        if (normal) {
            uiManager.SetLeft(0);
            uiManager.nextTileRect.Modulate = Colors.Transparent;
        }
        GD.Print("Game Over");
        uiManager.GameOver();
        
        foreach (var node in emptyFrames.Values) {
            node.QueueFree();
        }
    }
}