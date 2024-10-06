using Godot;
using System;

public partial class MainMenu : Control {
    [Export] public AudioStreamPlayer musicPlayer;
    [Export] public Button muteMusicButton;
    [Export] public Button startButton;
    [Export] public Control musicVolPopUp;
    [Export] public HSlider musicVolume;

    public int Volume = 0;
    
    public override void _Ready() {
        muteMusicButton.ButtonPressed = true;
        ChangeMusicVol((float)musicVolume.Value);
    }
	async public void Start() {
		startButton.Disabled = true;
		startButton.Text = "Please wait...";
		for (int i = Volume * (-1); i < 40; i++) {
			musicPlayer.VolumeDb = i * -1;
			await ToSignal(GetTree().CreateTimer(0.00005f), "timeout");
		}
		GetTree().ChangeSceneToFile("res://scenes/main.tscn");
	}

	public void Exit() {
		GetTree().Quit();
	}
	public void MusicPopup(bool button_pressed) {
		if (button_pressed) {
			musicVolPopUp.Show();
		}
		else {
			musicVolPopUp.Hide();
		}
	}
    
	public void MuteMusic(bool button_pressed) {
		if (button_pressed)
		{
			musicPlayer.StreamPaused = false;
			muteMusicButton.Text = "Stop";
		}
		else
		{
			musicPlayer.StreamPaused = true;
			muteMusicButton.Text = "Play";
		}
	}

	public void ChangeMusicVol(float vol) {
		musicPlayer.VolumeDb = (float)(10.0 * Math.Log(vol));
	}
}
