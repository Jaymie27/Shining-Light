using Godot;
using System;

public class EndScreen : Control
{

	public override void _Ready()
	{
		GetNode<Button>("ColorRect/VBoxContainer/MainMenuButton").GrabFocus();
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


