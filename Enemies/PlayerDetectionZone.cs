using Godot;
using System;

public class PlayerDetectionZone : Area2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	public KinematicBody2D player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = null;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public bool can_see_player()
	{
		return player != null;
	}

	private void _on_PlayerDetectionZone_body_entered(object body)
	{
		player = (KinematicBody2D)body;
	}
	
	
	private void _on_PlayerDetectionZone_body_exited(object body)
	{
		player = null;
	}
}





