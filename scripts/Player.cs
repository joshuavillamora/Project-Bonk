using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float NormalSpeed = 300.0f;
	public const float Acceleration = 1000.0f;
	public const float Deceleration = 1500.0f;
	public const float JumpVelocity = -400.0f;
	public const float JumpCutMultiplier = 0.2f;
	public const float DashSpeed = 2400.0f;
	public const float DashLength = 1.0f;
	public float Speed = NormalSpeed;
	private AnimatedSprite2D animatedSprite;
	private Dash dash;
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		dash = GetNode<Dash>("Dash");

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}
		
		if (Input.IsActionJustPressed("dash")) {
			dash.StartDash(DashLength);
		}
		
		if (dash.IsDashing()) {
			Speed = DashSpeed;
		} else {
			Speed = NormalSpeed;
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
		if (Input.IsActionJustReleased("jump") && Velocity.Y < 0)
		{
			Velocity = new Vector2(Velocity.X, Velocity.Y * JumpCutMultiplier);
		}
	}
}
