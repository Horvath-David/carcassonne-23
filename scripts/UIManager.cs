using Godot;
using System;

public partial class UIManager : Control {
    [Export] public Label scoreLabel;
    [Export] public Label tilesLeftLabel;
    [Export] public TextureRect nextTileRect;
    [Export] public AudioStreamPlayer musicPlayer;
    [Export] public Button muteMusicButton;
    [Export] public Label waitLabel;

    public async override void _Ready()
    {
        musicPlayer.VolumeDb = -40;
        musicPlayer.Play();
        muteMusicButton.ButtonPressed = true;
        for (int i = -40; i <= 0; i++) {
			musicPlayer.VolumeDb = i;
			await ToSignal(GetTree().CreateTimer(0.00005f), "timeout");
		}
        waitLabel.Hide();
    }

    public void SetLeft(int left) {
        tilesLeftLabel.Text = "Tiles left: " + left.ToString();
    }
    public void Exit() {
        GetTree().Quit();
    }
    public void MuteMusic(bool button_pressed){
        if (button_pressed){ 
			musicPlayer.StreamPaused = false;
			muteMusicButton.Text = "♫";
			}
        else if (! button_pressed) {
			musicPlayer.StreamPaused = true;
			muteMusicButton.Text = "♪";
			}
    }
}
