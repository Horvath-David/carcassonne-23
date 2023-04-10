using Godot;
using System;

public partial class UIManager : Control {
    [Export] public Label scoreLabel;
    [Export] public Label tilesLeftLabel;
    [Export] public TextureRect nextTileRect;
    [Export] public AudioStreamPlayer musicPlayer;
    [Export] public Button muteMusicButton;
    [Export] public Label waitLabel;
    [Export] public Label gameOverLabel;
    [Export] public Control musicVolPopUp;
    [Export] public HSlider musicVolume;

    public int Volume = 0;
    public bool gameEnded = false;


    public async override void _Ready() {
        musicPlayer.VolumeDb = -40;
        musicPlayer.Play(); 
        muteMusicButton.ButtonPressed = true;
        for (int i = -40; i <= 0; i++) {
            musicPlayer.VolumeDb = i;
            await ToSignal(GetTree().CreateTimer(0.00005f), "timeout");
        }
        waitLabel.Hide();
    }

    public async void SetLeft(int left) {
        tilesLeftLabel.Text = "Tiles left: " + left.ToString();
    }

    public async void GameOver() {
        muteMusicButton.Disabled = true;
        musicVolume.Editable = false;
        gameEnded = true;
        waitLabel.Show();
        for (int i = Volume; i <= 40; i++) {
            musicPlayer.VolumeDb = i*(-1);
            await ToSignal(GetTree().CreateTimer(0.00005f), "timeout");
        }
        musicPlayer.Stop();
        musicPlayer.Stream = GD.Load<AudioStream>("res://assets/music/Directed by Robert B. Weide.mp3");
        musicPlayer.VolumeDb = 0;
        musicPlayer.Play();
        gameOverLabel.Show();
        waitLabel.Hide();
        await ToSignal(GetTree().CreateTimer(30.0f), "timeout");
        waitLabel.Show();
        for (int i = 0; i <= 40; i++) {
            musicPlayer.VolumeDb = i*(-1);
            await ToSignal(GetTree().CreateTimer(0.05f), "timeout");
        }
        waitLabel.Hide();
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

    public void ChangeMusicVol(bool value_changed) {
        double vol = musicVolume.Value;
        if ((float)vol == 0) {
            musicPlayer.VolumeDb = -40;
            return;
        }
        musicPlayer.VolumeDb = ((float) vol - 100) / 3;
    }
}
