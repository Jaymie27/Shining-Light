using Godot;
using System;

public class Bat : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	
	private enum state{
		IDLE,
		WANDER,
		CHASE
	};
	
	private int ACCELERATION = 300;
	private int MAX_SPEED = 50;
	private int FRICTION = 200;
	private state currentState;
	
	public Vector2 Velocity;
	public Vector2 Direction;
	Area2D playerDetectionZone;
	KinematicBody2D player;
	AnimatedSprite aSprite;
	AudioStreamPlayer2D audioPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Velocity = Vector2.Zero;
		currentState = state.CHASE;
		playerDetectionZone = GetNode<Area2D>("PlayerDetectionZone");
		aSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		audioPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		audioPlayer.Stream = GD.Load<AudioStream>("res://");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _PhysicsProcess(float delta)
  {
	switch (currentState)
	{
	  case state.IDLE:
		Velocity = Velocity.MoveToward(Vector2.Zero, FRICTION * delta);
		seek_player();
		break;
	  case state.WANDER:
		break;
	  case state.CHASE:
		player = (GetNode("PlayerDetectionZone") as PlayerDetectionZone).player;
		if(player != null)
		{
			Direction = (player.GlobalPosition - this.GlobalPosition).Normalized();
			Velocity = Velocity.MoveToward(Direction * MAX_SPEED, ACCELERATION * delta);
			aSprite.FlipH = Velocity.x < 0;
		}
		else
		{
			currentState = state.IDLE;
		}
		break;
	}
	
	Velocity = MoveAndSlide(Velocity);
		
  }

	private void seek_player()
	{
		if((GetNode("PlayerDetectionZone") as PlayerDetectionZone).can_see_player())
		{
			currentState = state.CHASE;
		}
	}


	private void _on_Hurtbox_area_entered(object area)
	{
		if(!audioPlayer.Playing())
		{
			audioPlayer.play();
		}
		this.QueueFree();
	}
}


