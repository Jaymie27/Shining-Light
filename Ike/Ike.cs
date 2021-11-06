using Godot;
using System;

public class Ike : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	public Vector2 Velocity;
	
	const int MAXSPEED = 80;
	const int ACCELERATION = 500;
	private int FRICTION = 500;
	private int life = 10;
	bool facing_right = true;
	Sprite currentSprite;
	AnimationPlayer animationPlayer;
	AnimationTree animationTree;
	AnimationNodeStateMachinePlayback animationState;
	
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentSprite = GetNode<Sprite>("Sprite");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationTree = GetNode<AnimationTree>("AnimationTree");
		animationState = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
	}
	
	public Vector2 GetInput()
	{
		var input_vector = Vector2.Zero;
		
		input_vector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		input_vector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
		input_vector = input_vector.Normalized();
		return input_vector;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	  public override void _PhysicsProcess(float delta)
	  {
		  
		  var input_vector = GetInput();
		
		if (facing_right) {
			currentSprite.FlipH = false;
		} else {
			currentSprite.FlipH = true;
		}
		
		if (life == 0)
		{
			
		}
		
		if(input_vector != Vector2.Zero)
		{
			animationTree.Set("parameters/Idle/blend_position", input_vector);
			animationTree.Set("parameters/Run/blend_position", input_vector);
			animationTree.Set("parameters/Attack/blend_position", input_vector);
			animationState.Travel("Run");
			if(input_vector.x >= 0)
			{
				facing_right = true;
			}
			else
			{
				facing_right = false;
			}
			Velocity = Velocity.MoveToward(input_vector * MAXSPEED, ACCELERATION * delta);	
		}
			
		else{
			
			if(Input.IsActionJustReleased("ui_attack"))
			{
				animationState.Travel("Attack");
			}
			else
			{
				animationState.Travel("Idle");
			}
			Velocity = Velocity.MoveToward(Vector2.Zero, FRICTION * delta);
		}

		Velocity = MoveAndSlide(Velocity);
	  }
	
	
	private void _on_ChangeLevelZone_body_entered(object body)
	{
		GetTree().ChangeScene("res://level2.tscn");
	}

}

