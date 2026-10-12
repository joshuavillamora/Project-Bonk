using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	private AnimatedSprite2D animatedSprite;
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		float direction = Input.GetAxis("move_left", "move_right");
		Vector2 dir = new Vector2(direction, 0);

		Velocity = velocity;
		Walk(dir);
		MoveAndSlide();
	}

	private void Walk(Vector2 dir)
	{
		Vector2 velocity = Velocity;

		if (dir.X != 0)
		{
			animatedSprite.FlipH = dir.X < 0;
			velocity.X = dir.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(velocity.X, 0, Speed);
		}

		Velocity = velocity;
	}
}
