using Godot;
using System;

public class level1 : Node2D
{

	public override void _Ready()
	{
		
	}
	
	
	private void _on_ChangeLevelZone_area_entered(Area2D area)
	{
		   GetTree().ChangeScene("res://level2.tscn");
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventKey eventKey)
		{
			if (eventKey.Pressed && eventKey.Scancode == (int)KeyList.L)
			{
				 GetTree().ChangeScene("res://level2.tscn");
			}
	
		}
					
				
	}

}




