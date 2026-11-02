using Godot;
using System;

public partial class Killzone : Area2D
{
	private Timer timer;

	public override void _Ready() {
		timer = GetNode<Timer>("Timer");
		
		BodyEntered += OnBodyEntered;
		timer.Timeout += OnTimerTimeout;
	}

	public void OnBodyEntered(Node2D body) {
		GD.Print("You died");
		timer.Start();
	} 
	
	public void OnTimerTimeout() {
		GetTree().ReloadCurrentScene();
	}
}
