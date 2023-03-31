using Godot;
using System;

public partial class StartGame : Control
{
	async public void Start() {
		var music = (AudioStreamPlayer)GetNode("Music");
		for (int i = 0; i < 40; i++) {
			music.VolumeDb = i * -1;
			await ToSignal(GetTree().CreateTimer(0.00005f), "timeout");
		}
		GetTree().ChangeSceneToFile("res://scenes/main.tscn");
	}
}
