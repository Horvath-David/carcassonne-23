using Godot;
using System;

public partial class MainMenu : Control
{
    [Export] public AudioStreamPlayer musicPlayer;
    [Export] public Button muteMusicButton;
	[Export] public Button startButton;
	public override void _Ready()
    {
        muteMusicButton.ButtonPressed = true;
    }
	async public void Start() {
		startButton.Text = "Please wait...";
		for (int i = 0; i < 40; i++) {
			musicPlayer.VolumeDb = i * -1;
			await ToSignal(GetTree().CreateTimer(0.00005f), "timeout");
		}
		GetTree().ChangeSceneToFile("res://scenes/main.tscn");
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
