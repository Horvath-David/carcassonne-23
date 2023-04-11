using Godot;
using System;
using System.Collections.Generic;

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
    [Export] public Control nowPlayingFrame;
    [Export] public Label nowPlaying;
    [Export] public Button nextMusic;

    public int Volume = 0;
    public bool gameEnded = false;
    public bool paused = false;
    public bool nowPlayingAnim = false;
    
    public List<string> musicList = new List<string>{
        "Sharks - Shiver [NCS Release]", 
        "Akacia - Electric [NCS Release]",
        "Cartoon - Why We Lose (feat. Coleman Trapp) [NCS Release]", 
        "Syn Cole - Feel Good [NCS Release]", 
        "Lost Sky - Dreams pt. II (feat. Sara Skinner) [NCS Release]", 
        "Culture Code - Make Me Move (feat. Karra) [NCS Release]",
        "Alan Walker - Dreamer [NCS Release]",
        "Lost Sky - Fearless pt.II (feat. Chris Linton) [NCS Release]",
        "Prismo - Stronger [NCS Release]",
        "Prismo - Weakness [NCS Release]",
        "Valence - Infinite [NCS Release]",
        "Different Heaven - Nekozilla [NCS Release]",
        "Different Heaven & EH!DE - My Heart [NCS Release]",
        "Electro-Light - Symbolism [NCS Release]",
        "JPB - High [NCS Release]",
        "Elektronomia - Limitless [NCS Release]",
        "Elektronomia - Energy [NCS Release]",
        "Disfigure - Blank [NCS Release]"
    };
    public Random random = new Random();
    public int previousIndex = -1; // initialize previous index to an invalid value
    public string selectedString;
    public int index;


    public async override void _Ready() {
        nowPlayingAnim = true;
        nextMusic.Disabled = true;
        nextMusic.Text = "Wait";
        // selects the first music randomly
        int index = random.Next(musicList.Count);
        previousIndex = index;
        string selectedString = musicList[index];
        GD.Print(selectedString);
        musicPlayer.Stream = GD.Load<AudioStream>("res://assets/music/"+selectedString+".mp3");
        musicPlayer.Play(); 
        muteMusicButton.ButtonPressed = true;
        waitLabel.Hide();
        nowPlaying.Text = selectedString;
        for (float i = 0; i <= 10; i++) {
            nowPlayingFrame.Modulate = new Color(1, 1, 1, i/10);
            await ToSignal(GetTree().CreateTimer(0.005f), "timeout");
        }
        await ToSignal(GetTree().CreateTimer(3f), "timeout");
        for (float i = 0; i <= 10; i++) {
            nowPlayingFrame.Modulate = new Color(1, 1, 1, 1 - (i / 10));
            await ToSignal(GetTree().CreateTimer(0.005f), "timeout");
        }

        if (!paused) nextMusic.Disabled = false;
        nextMusic.Text = "Next";
        nowPlayingAnim = false;
    }

    public async void ChangeMusic() {
        nowPlayingAnim = true;
        musicPlayer.Stop();
        nextMusic.Disabled = true;
        nextMusic.Text = "Wait";
        index = random.Next(musicList.Count);
        do {
            index = random.Next(musicList.Count);
        } while (index == previousIndex);

        if (index == previousIndex) {
            ChangeMusic();
            return;
        }
        string selectedString = musicList[index];
        index = previousIndex;
        musicPlayer.Stream = GD.Load<AudioStream>("res://assets/music/"+selectedString+".mp3");
        musicPlayer.Play(); 
        nowPlaying.Text = selectedString;
        for (float i = 0; i <= 10; i++) {
            nowPlayingFrame.Modulate = new Color(1, 1, 1, i/10);
            await ToSignal(GetTree().CreateTimer(0.005f), "timeout");
        }
        await ToSignal(GetTree().CreateTimer(3f), "timeout");
        for (float i = 0; i <= 10; i++) {
            nowPlayingFrame.Modulate = new Color(1, 1, 1, 1 - (i / 10));
            await ToSignal(GetTree().CreateTimer(0.005f), "timeout");
        }
        if (!paused) nextMusic.Disabled = false;
        nextMusic.Text = "Next";
        nowPlayingAnim = false;
    }

    public async void ShowMusic() {
        if (!nowPlayingAnim) {
            for (float i = 0; i <= 10; i++)
            {
                nowPlayingFrame.Modulate = new Color(1, 1, 1, i / 10);
                await ToSignal(GetTree().CreateTimer(0.005f), "timeout");
            }
        }
    }
    public async void HideMusic() {
        if (!nowPlayingAnim) {
            for (float i = 0; i <= 10; i++) {
                nowPlayingFrame.Modulate = new Color(1, 1, 1, 1 - (i / 10));
                await ToSignal(GetTree().CreateTimer(0.005f), "timeout");
            }
        }
    }

    public async void SetLeft(int left) {
        tilesLeftLabel.Text = "Tiles left: " + left.ToString();
    }

    public async void GameOver() {
        muteMusicButton.Disabled = true;
        musicVolume.Editable = false;
        nextMusic.Disabled = true;
        gameEnded = true;
        waitLabel.Show();
        for (int i = Volume * (-1); i <= 40; i++) {
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
            nextMusic.Disabled = false;
            musicPlayer.StreamPaused = false;
            paused = false;
            muteMusicButton.Text = "Stop";
        }
        else
        {
            nextMusic.Disabled = true;
            musicPlayer.StreamPaused = true;
            paused = true;
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