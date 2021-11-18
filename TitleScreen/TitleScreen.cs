using Godot;
using System;

public class TitleScreen : Control
{

	public override void _Ready()
	{
		GetNode<Button>("ColorRect/VBoxContainer/StartButton").GrabFocus();
	}

	private void _on_StartButton_pressed()
	{
		GetTree().ChangeScene("res://level1.tscn");
	}


	private void _on_OptionsButton_pressed()
	{
		var options = GD.Load<PackedScene>("res://optionsMenu.tscn").Instance();
		GetTree().CurrentScene.AddChild(options);
	}
	
	
	private void _on_QuitButton_pressed()
	{
		GetTree().Quit();
	}

}





