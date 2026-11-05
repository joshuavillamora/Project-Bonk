using Godot;
using System;

public partial class Chicken : Node2D
{
	public float speed = 50.0f;
	public int direction = 1;
	private AnimatedSprite2D animatedSprite;
	private RayCast2D rayCastRight;
	private RayCast2D rayCastLeft;
	
	public override void _Ready() {
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		rayCastRight = GetNode<RayCast2D>("RayCastRight");
		rayCastLeft = GetNode<RayCast2D>("RayCastLeft");
	}
		
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		if (rayCastRight.IsColliding()) {
			direction = -1;
			animatedSprite.FlipH = true;
		}  
		
		if (rayCastLeft.IsColliding()) {
			direction = 1;
			animatedSprite.FlipH = false;
		}
		
		Position += new Vector2(speed * (float)delta * direction, 0);
	}
}
