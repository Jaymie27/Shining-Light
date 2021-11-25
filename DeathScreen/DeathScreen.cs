using Godot;
using System;

public class DeathScreen : Control
{
	AudioStreamPlayer audioStreamPlayer;

	public static bool soundDisabled = false;
	
	public override void _Ready()
	{
		GetNode<Button>("ColorRect/VBoxContainer/RestartButton").GrabFocus();
		audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		if(!soundDisabled)
		{
			audioStreamPlayer.Play();
		}
	}

	private void _on_RestartButton_pressed()
	{
	   	GetTree().ChangeScene("res://level1.tscn");
	}


	private void _on_MainMenuButton_pressed()
	{
		GetTree().ChangeScene("res://TitleScreen/TitleScreen.tscn");
	}


	private void _on_QuitButton_pressed()
	{
		GetTree().Quit();
	}
}





