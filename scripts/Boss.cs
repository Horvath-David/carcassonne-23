using Godot;
using System;

public partial class Boss : Node {
    [Export] public ColorRect player1;
    [Export] public ColorRect player2;
    [Export] public ColorRect player3;
    [Export] public ColorRect player4;
    
    [Export] public ColorRect colorPicker;
    private int activePicking = 1;

    [Export] public Button soloButton;
    [Export] public Button normalButton;

    private int players = 1;
    private bool lite = true;
    
    public void Quit() {
        GetTree().Quit();
    }

    public override void _Ready() {
        Manager.uiManager.MuteMusic(false);
    }

    public void DeletePlayer(int player) {
        var node = GetNode("Setup/Playerbox" + player);
        node.GetNode<ColorRect>("ColorRect2").Show();
        node.GetNode<Button>("Button").Hide();
        node.GetNode<LineEdit>("LineEdit").Text = "Player " + player;
        
        if (player != 4) {
            GetNode<ColorRect>("Setup/Playerbox" + (player + 1)).Hide();
        }

        players = Math.Max(1, player - 1);
    }

    public void AddPlayer(int player) {
        var node = GetNode("Setup/Playerbox" + player);
        node.GetNode<ColorRect>("ColorRect2").Hide();
        node.GetNode<Button>("Button").Show();

        if (player != 4) {
            GetNode<ColorRect>("Setup/Playerbox" + (player + 1)).Show();
        }

        players = player;
    }

    public void OpenColorPicker(int player) {
        colorPicker.Show();
        activePicking = player;
    }

    public void CloseColorPicker() {
        colorPicker.Hide();
    }

    public void ChangeColor(Color color) {
        var node = GetNode<ColorRect>("Setup/Playerbox" + activePicking + "/ColorRect");
        node.Color = color;
    }

    public void PressSolo() {
        normalButton.ButtonPressed = false;
        soloButton.ButtonPressed = true;
        
        player1.Position = new Vector2(351, 238);
        
        DeletePlayer(4);
        DeletePlayer(3);
        DeletePlayer(2);
        player2.Hide();

        players = 1;
        lite = true;
    }

    public void PressNormal() {
        normalButton.ButtonPressed = true;
        soloButton.ButtonPressed = false;

        player1.Position = new Vector2(218, 174);
        
        player2.Show();

        lite = false;
    }

    public void Play() {
        for (int i = 1; i <= players; i++) {
            var node = GetNode("Setup/Playerbox" + i);
            var data = new PlayerState();
            data.name = node.GetNode<LineEdit>("LineEdit").Text;
            data.color = node.GetNode<ColorRect>("ColorRect").Color;
            Manager.gameState.players.Add(data);

            var scoreNode = GetNode<ColorRect>("Main/UIScene/Scores/Player" + i);
            scoreNode.Show();
            scoreNode.GetNode<ColorRect>("ColorRect").Color = data.color;
            scoreNode.GetNode<Label>("Name").Text = data.name;
            
            if (lite) {
                scoreNode.GetNode<Label>("Name").Position += new Vector2(0, 7);
                scoreNode.GetNode<Label>("Meep").Hide();
            }
            Manager.uiManager.ChangePlayer(1);
        }

        Manager.gameState.lite = lite;

        GetNode<Control>("Setup").QueueFree();
    }
}
