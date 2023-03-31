using Godot;
using System;

public partial class StartGame : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	async public void Start(){
		AudioStreamPlayer music = (AudioStreamPlayer)GetNode("Music");
		for (int i = 0; i < 40; i++)
		{
			music.VolumeDb = i*(-1);	
			GD.Print(i*(-1));
			await ToSignal(GetTree().CreateTimer(0.00005f), "timeout");
		}
		GetTree().ChangeSceneToFile("res://scenes/main.tscn");
	}
}
