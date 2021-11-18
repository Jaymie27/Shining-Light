using Godot;
using System;

public class Interface : Control
{
	
	ColorRect pause_overlay;

	public override void _Ready()
	{
		pause_overlay = GetNode<ColorRect>("ColorRect");
	}
	
	private void _on_ResumeButton_pressed()
	{
		GetTree().Paused = false;
		pause_overlay.Visible = false;
	}
		
	private void _on_MainMenuButton_pressed()
	{
		GetTree().Paused = false;
		GetTree().ChangeScene("res://TitleScreen/TitleScreen.tscn");
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
	

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventKey eventKey)
			if (eventKey.Pressed && eventKey.Scancode == (int)KeyList.P)
			{
				if(GetTree().Paused == false)
				{
					GetTree().Paused = true;
					pause_overlay.Visible = true;
					GetNode<Button>("ColorRect/VBoxContainer/ResumeButton").GrabFocus();
				}
				else
				{
					GetTree().Paused = false;
					pause_overlay.Visible = false;
				}
			}
				
	}

}
