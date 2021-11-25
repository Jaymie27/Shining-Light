using Godot;
using System;

public class Ike : KinematicBody2D
{

	[Export]
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
	TextureProgress lifeBar;

	CollisionShape2D hitbox;
	
	public static float DirectionX;
	public static float DirectionY;
	


	private enum State{
		MOVE,
		ATTACK,
		DEAD
	};

	State state;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Randomize();
		currentSprite = GetNode<Sprite>("Sprite");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationTree = GetNode<AnimationTree>("AnimationTree");
		animationState = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
		lifeBar = GetNode<TextureProgress>("Camera2D/CanvasLayer/Interface/Bar/TextureProgress");
		hitbox = GetNode<CollisionShape2D>("HitboxPivot/SwordHitbox/CollisionShape2D");
		hitbox.Disabled = true;
		DirectionX = Velocity.x;
		DirectionY = Velocity.y;
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
		  	checkDeath();
		switch(state){
			case State.MOVE:
				move_state(delta);
				break;
			case State.ATTACK:
				attack_state(delta);
				break;
			case State.DEAD:
				death_state(delta);
				break;

		}
		 
	  }
	

	private void move_state(float delta)
	{
		var input_vector = GetInput();
		
		if (facing_right) {
			currentSprite.FlipH = false;
		} else {
			currentSprite.FlipH = true;
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
				state = State.ATTACK;
			
			}
			else
			{
				animationState.Travel("Idle");
			}
			Velocity = Velocity.MoveToward(Vector2.Zero, FRICTION * delta);
		}
		DirectionX = Velocity.x;
		DirectionY = Velocity.y;
		Velocity = MoveAndSlide(Velocity);
	}

	private void attack_state(float delta)
	{
		Velocity = Vector2.Zero;
		animationState.Travel("Attack");
	}

	public void attack_animation_finished()
	{
		state = State.MOVE;
	}

	private void checkDeath()
	{
		if (life == 0)
		{
			state = State.DEAD;
		}
	}
		private void death_state(float delta)
	{
		animationState.Travel("Death");
	}

	private void death_animation_finished()
	{
		GetTree().ChangeScene("res://DeathScreen/DeathScreen.tscn");
	}

	private void _on_Hurtbox_area_entered(Area2D area)
	{
		
	   if(area.IsInGroup("enemyAttack"))
			{
				life--;
				((TextureProgress)(lifeBar)).Value = life;
			}
	}
}





