using Godot;
using System;

public partial class Magnet : RigidBody2D
{
	private void OnScreenNotifierScreenExited()
	{
		QueueFree();
	}

	private void OnBodyEntered()
	{
		LinearVelocity = Vector2.Zero;
	}
}
