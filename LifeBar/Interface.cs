using Godot;
using System;

public class Interface : Control
{
	
	ColorRect pause_overlay;
	Label DirXLabel, DirXStat, DirYLabel, DirYStat, FPSLabel, FPSStat;

	public override void _Ready()
	{
		pause_overlay = GetNode<ColorRect>("ColorRect");
		DirXLabel = GetNode<Label>("DirXLabel");
		DirXStat = GetNode<Label>("DirXStat");
		DirYLabel = GetNode<Label>("DirYLabel");
		DirYStat = GetNode<Label>("DirYStat");
		FPSLabel = GetNode<Label>("FPSLabel");
		FPSStat = GetNode<Label>("FPSStat");
	}
	
	public override void _PhysicsProcess(float delta)
	{
		DirXStat.Text = Ike.DirectionX.ToString();
		DirYStat.Text = Ike.DirectionY.ToString();
		FPSStat.Text = Engine.GetFramesPerSecond().ToString();
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
		{
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

			else if (eventKey.Pressed && eventKey.Scancode == (int)KeyList.I)
			{
				if(!DirXStat.Visible)
				{
					DirXLabel.Visible = true;
					DirXStat.Visible = true;
					DirYLabel.Visible = true;
					DirYStat.Visible = true;
					FPSLabel.Visible = true;
					FPSStat.Visible = true;
				}

				else
				{
					DirXLabel.Visible = false;
					DirXStat.Visible = false;
					DirYLabel.Visible = false;
					DirYStat.Visible = false;
					FPSLabel.Visible = false;
					FPSStat.Visible = false;
				}
				
			}
		}
					
				
	}
	

}
