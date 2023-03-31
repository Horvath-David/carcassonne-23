using System;
using Godot;
using System.Collections.Generic;

public enum GamePhase {
    Running,
    Ended
}

public partial class Manager : Node2D
{
    public GamePhase phase = GamePhase.Running;
    public static Board board = new Board();
    public static GameState gameState = new GameState();
    public static Dictionary<(int X, int Y), EmptyFrame> emptyFrames = new Dictionary<(int X, int Y), EmptyFrame>();
    
    public static List<Tile> addBuffer = new List<Tile>();
    public static List<(int X, int Y)> emptyPlaceBuffer = new List<(int X, int Y)>();

    public static int rotation = 0;

    PackedScene tilePrefab = GD.Load<PackedScene>("res://prefabs/tile.tscn");
    PackedScene emptyFrame;
    
    [Export]
    Node2D boardNode;
    
    public override void _Ready() {
        emptyFrame = GD.Load<PackedScene>("res://prefabs/frame.tscn");

        gameState.tilesLeft = new List<TileData>(Tiles.all);
        gameState.nextTile = Tiles.st;
        gameState.tilesLeft.Remove(Tiles.st);
        
        PlaceTile(new Tile(0, 0, gameState.nextTile));
    }

    public override void _Process(double delta)
    {
        switch (phase) {
            case GamePhase.Running: {
                AppyAddBuffer();
                ApplyEmptyPlaceBuffer();

                if (gameState.gameOver) EndGame();
                break;
            }
            case GamePhase.Ended: {
                break;
            }
        }
    }

    private void AppyAddBuffer() {
        foreach (var tile in addBuffer) {
            var instance = tilePrefab.Instantiate() as Sprite2D;
            instance.Set("position", new Vector2(tile.pos.X * 100, -tile.pos.Y * 100));
            instance.Texture = GD.Load<Texture2D>(tile.type.path);
            instance.Rotation = (float)(Math.PI / 180f) * tile.rotation * 90f;
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
        if (emptyPlaceBuffer.Count > 0) {
            GD.Print(emptyPlaceBuffer.Count);
            HideFrames();
        }
        emptyPlaceBuffer = new List<(int X, int Y)>();
    }

    public static void PlaceTile(Tile tile) {
        if (gameState.gameOver) return;
        
        board.Set(tile.pos.X, tile.pos.Y, tile);
        addBuffer.Add(board.Get(tile.pos.X, tile.pos.Y));

        gameState.openPlaces.Remove((tile.pos.X, tile.pos.Y));

        if (board.Get(tile.pos.X + 1, tile.pos.Y) == null) {
            gameState.openPlaces.Add((tile.pos.X + 1, tile.pos.Y));
            emptyPlaceBuffer.Add((tile.pos.X + 1, tile.pos.Y));
        }
        if (board.Get(tile.pos.X - 1, tile.pos.Y) == null) {
            gameState.openPlaces.Add((tile.pos.X - 1, tile.pos.Y));
            emptyPlaceBuffer.Add((tile.pos.X - 1, tile.pos.Y));
        }
        if (board.Get(tile.pos.X, tile.pos.Y + 1) == null) {
            gameState.openPlaces.Add((tile.pos.X, tile.pos.Y + 1));
            emptyPlaceBuffer.Add((tile.pos.X, tile.pos.Y + 1));
        }
        if (board.Get(tile.pos.X, tile.pos.Y - 1) == null) {
            gameState.openPlaces.Add((tile.pos.X, tile.pos.Y - 1));
            emptyPlaceBuffer.Add((tile.pos.X, tile.pos.Y - 1));
        }
        gameState.ChooseNextTile();

        rotation = 0;
    }

    public static void ChangeRotation(int rotation = -1) {
        if (rotation < 0 || rotation > 3) {
            Manager.rotation++;
            if (Manager.rotation == 4) Manager.rotation = 0;
        }
        else {
            Manager.rotation = rotation;
        }

        foreach (var frame in emptyFrames.Values) {
            frame.Rotate(Manager.rotation);
        }
    }

    public static void HideFrames() {
        foreach (var frame in emptyFrames.Values) {
            var shouldHide = true;
            
            var tile = new Tile(frame.X, frame.Y, Manager.gameState.nextTile);
            
            if (Manager.board.IsLegal(tile)) shouldHide = false;
            if (Manager.board.IsLegal(tile)) shouldHide = false;
            if (Manager.board.IsLegal(tile)) shouldHide = false;
            if (Manager.board.IsLegal(tile)) shouldHide = false;

            if (Manager.board.IsLegal(Tiles.Rotate(0,
                    new Tile(frame.X, frame.Y, Manager.gameState.nextTile)))) shouldHide = false;
            if (Manager.board.IsLegal(Tiles.Rotate(1,
                    new Tile(frame.X, frame.Y, Manager.gameState.nextTile)))) shouldHide = false;
            if (Manager.board.IsLegal(Tiles.Rotate(2,
                    new Tile(frame.X, frame.Y, Manager.gameState.nextTile)))) shouldHide = false;
            if (Manager.board.IsLegal(Tiles.Rotate(3,
                    new Tile(frame.X, frame.Y, Manager.gameState.nextTile)))) shouldHide = false;

            if (shouldHide) {
                frame.Hide();
            }
            else {
                frame.Show();
            }
        }
    }

    private void EndGame() {
        phase = GamePhase.Ended;
        GD.Print("Game Over");
        
        foreach (var node in emptyFrames.Values) {
            node.QueueFree();
        }
    }
}