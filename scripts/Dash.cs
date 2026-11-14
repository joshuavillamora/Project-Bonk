using Godot;
using System;

public partial class Dash : Node2D {
	private Timer timer;

	public override void _Ready() {
		timer = GetNode<Timer>("DashTimer");
	}
	
	public void StartDash(float duration) {
		timer.WaitTime = duration;
		timer.Start();
	}
	
	public bool IsDashing() {
		return !timer.IsStopped();
	}
}
