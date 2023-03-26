using Godot;
using System.Collections.Generic;

public enum GamePhase {
    Running,
    Ended
}

public partial class Manager : Node2D
{
    public static Board board = new Board();
    public static GameState gameState = new GameState();
    public static Dictionary<(int X, int Y), EmptyFrame> emptyFrames = new Dictionary<(int X, int Y), EmptyFrame>();
    
    public static List<Tile> addBuffer = new List<Tile>();
    public static List<(int X, int Y)> emptyPlaceBuffer = new List<(int X, int Y)>();
    
    public GamePhase phase = GamePhase.Running;

    PackedScene tilePrefab = GD.Load<PackedScene>("res://prefabs/tile.tscn");
    PackedScene emptyFrame;
    
    [Export]
    Node2D boardNode;
    
    
    public override void _Ready() {
        emptyFrame = GD.Load<PackedScene>("res://prefabs/frame.tscn");
        
        var matrix = new BoardMatrix();
        var dict = new Board();

        GD.Print("### Matrix ###");

        var matrixPopulation = new System.Diagnostics.Stopwatch();
        matrixPopulation.Start();
        for (int i = 0; i < 15; i++) {
            for (int j = 0; j < 15; j++) {
                matrix.Set(i, j, new Tile(i, j, Tiles.a1));
            }
        }
        matrixPopulation.Stop();
        GD.Print($"Population took {(float)matrixPopulation.ElapsedTicks / (float)System.Diagnostics.Stopwatch.Frequency * 1000} ms");

        var matrixTraversing = new System.Diagnostics.Stopwatch();
        matrixTraversing.Start();
        for (int i = 0; i < 15; i++) {
            for (int j = 0; j < 15; j++) {
                matrix.Get(i, j);
            }
        }
        matrixTraversing.Stop();
        GD.Print($"Traversing took {(float)matrixTraversing.ElapsedTicks / (float)System.Diagnostics.Stopwatch.Frequency * 1000} ms");

        var matrixSetOne = new System.Diagnostics.Stopwatch();
        matrixSetOne.Start();
        matrix.Set(5, 5, new Tile(5, 5, Tiles.a1));
        matrixSetOne.Stop();
        GD.Print($"Setting one took {(float)matrixSetOne.ElapsedTicks / (float)System.Diagnostics.Stopwatch.Frequency * 1000} ms");

        var matrixOne = new System.Diagnostics.Stopwatch();
        matrixOne.Start();
        matrix.Get(5, 5);
        matrixOne.Stop();
        GD.Print($"Getting one took {(float)matrixOne.ElapsedTicks / (float)System.Diagnostics.Stopwatch.Frequency * 1000} ms");

        GD.Print("\n### Dict ###");
        
        var dictPopulation = new System.Diagnostics.Stopwatch();
        dictPopulation.Start();
        for (var i = 0; i < 15; i++) {
            for (var j = 0; j < 15; j++) {
                dict.Set(i, j, new Tile(i, j, Tiles.a1));
            }
        }
        dictPopulation.Stop();
        GD.Print($"Population took {(float)dictPopulation.ElapsedTicks / (float)System.Diagnostics.Stopwatch.Frequency * 1000} ms");

        var dictTraversing = new System.Diagnostics.Stopwatch();
        dictTraversing.Start();
        for (var i = 0; i < 15; i++) {
            for (int j = 0; j < 15; j++) {
                dict.Get(i, j);
            }
        }
        dictTraversing.Stop();
        GD.Print($"Traversing took {(float)dictTraversing.ElapsedTicks / (float)System.Diagnostics.Stopwatch.Frequency * 1000} ms");

        var dictSetOne = new System.Diagnostics.Stopwatch();
        dictSetOne.Start();
        dict.Set(5, 5, new Tile(5, 5, Tiles.a1));
        dictSetOne.Stop();
        GD.Print($"Setting one took {(float)dictSetOne.ElapsedTicks / (float)System.Diagnostics.Stopwatch.Frequency * 1000} ms");

        var dictOne = new System.Diagnostics.Stopwatch();
        dictOne.Start();
        dict.Get(5, 5);
        dictOne.Stop();
        GD.Print($"Getting one took {(float)dictOne.ElapsedTicks / (float)System.Diagnostics.Stopwatch.Frequency * 1000} ms");

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
            boardNode.AddChild(instance);
        }
        addBuffer = new List<Tile>();
    }
    private void ApplyEmptyPlaceBuffer() {
        foreach ((int X, int Y) emptyPlace in emptyPlaceBuffer) {
            if (emptyFrames.GetValueOrDefault(emptyPlace, null) != null) continue;
            var instance = emptyFrame.Instantiate() as EmptyFrame;
            instance.Set("position", new Vector2(emptyPlace.X * 100, -emptyPlace.Y * 100));
            instance.X = emptyPlace.X;
            instance.Y = emptyPlace.Y;
            boardNode.AddChild(instance);
            emptyFrames.Add(emptyPlace, instance);
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
    }

    private void EndGame() {
        phase = GamePhase.Ended;
        GD.Print("Game Over");

        GD.Print(emptyFrames.Count);
        foreach (var node in emptyFrames.Values) {
            try {
                node.QueueFree();
            } catch {}
        }
    }
}