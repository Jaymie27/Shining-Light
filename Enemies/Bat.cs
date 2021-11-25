using Godot;
using System;
using System.Collections.Generic;

public class Bat : KinematicBody2D
{
	private enum state{
		IDLE,
		WANDER,
		CHASE
	};
	
	private int ACCELERATION = 300;
	private int MAX_SPEED = 50;
	private int FRICTION = 200;

	private int wander_target_range = 4;

	private state currentState;
	
	public Vector2 Velocity;
	public Vector2 Direction;
	Area2D playerDetectionZone;
	KinematicBody2D player;
	AnimatedSprite aSprite;
	WanderController wanderController;

	List<state> states = new List<state>{state.IDLE, state.WANDER};


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Velocity = Vector2.Zero;
		currentState = pick_random_state(states);
		playerDetectionZone = GetNode<Area2D>("PlayerDetectionZone");
		aSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		wanderController = GetNode<WanderController>("WanderController");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _PhysicsProcess(float delta)
  {
	switch (currentState)
	{
	  case state.IDLE:
		Velocity = Velocity.MoveToward(Vector2.Zero, FRICTION * delta);
		seek_player();
		if(wanderController.getTimeLeft() == 0)
		{
			update_wander();
		}

		break;

	  case state.WANDER:
		seek_player();
		if(wanderController.getTimeLeft() == 0)
		{
			update_wander();
		}

		accelerate_towards_point(wanderController.get_target_position(), delta);

		if(this.GlobalPosition.DistanceTo(wanderController.get_target_position()) <= wander_target_range)
		{
			update_wander();
		}

		break;
		
	  case state.CHASE:
		player = (GetNode("PlayerDetectionZone") as PlayerDetectionZone).player;
		if(player != null)
		{
			accelerate_towards_point(player.GlobalPosition, delta);
		}
		else
		{
			currentState = state.IDLE;
		}
		break;
	}
	
	Velocity = MoveAndSlide(Velocity);
		
  }


	private void update_wander()
	{
		currentState = pick_random_state(states);
		Random rand = new Random();
		wanderController.start_wander_timer(rand.Next(1, 3));
	}
	private void accelerate_towards_point(Vector2 point, float delta)
	{
		Direction = this.GlobalPosition.DirectionTo(point);
		Velocity = Velocity.MoveToward(Direction * MAX_SPEED, ACCELERATION * delta);
		aSprite.FlipH = Velocity.x < 0;
	}
	private void seek_player()
	{
		if((GetNode("PlayerDetectionZone") as PlayerDetectionZone).can_see_player())
		{
			currentState = state.CHASE;
		}
	}

	private state pick_random_state(List<state> state_list)
	{
		Random rand = new Random();
		state_list = Shuffle<state> (state_list, rand);
		return state_list[0];
	}

	private List<T> Shuffle<T>(List<T> list, Random rnd)
	{
		for(var i=list.Count; i > 0; i--)
		{
			Swap(list, 0, rnd.Next(0, i));
		}
		return list;
	}

	private void Swap<T>( List<T> list, int i, int j)
	{
		var temp = list[i];
		list[i] = list[j];
		list[j] = temp;
	}


	private void _on_Hurtbox_area_entered(object area)
	{
		this.QueueFree();
	}
}


