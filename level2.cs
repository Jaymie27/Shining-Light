using Godot;
using System;

public class level2 : Node2D
{
	public override void _Ready()
	{
		
	}
	
	private void _on_ChangeLevelZone_area_entered(Area2D area)
	{
		GetTree().ChangeScene("res://EndScreen.tscn");
	}

}



