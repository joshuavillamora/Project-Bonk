using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 200.0f;
	public const float Acceleration = 1000.0f;
	public const float Deceleration = 1500.0f;
	public const float JumpVelocity = -250.0f;
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
		Velocity = velocity;
		Jump();

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		float direction = Input.GetAxis("move_left", "move_right");
		Vector2 dir = new Vector2(direction, 0);

		Walk(dir, (float)delta);
		MoveAndSlide();
	}

	private void Walk(Vector2 dir, float delta)
	{
		Vector2 velocity = Velocity;

		if (dir.X != 0)
		{
			animatedSprite.FlipH = dir.X < 0;
			velocity.X = Mathf.MoveToward(velocity.X, dir.X * Speed, Acceleration * delta);
		}
		else
		{
			velocity.X = Mathf.MoveToward(velocity.X, 0, Deceleration * delta);
		}

		Velocity = velocity;
	}

	private void Jump()
	{
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			Velocity = new Vector2(Velocity.X, JumpVelocity);
		}
	}
}
