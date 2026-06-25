using Godot;
using System;

public partial class Magnet : RigidBody2D
{

	public bool isMagnetized = false;

    public override void _Ready()
    {
        ContactMonitor = true;
		MaxContactsReported = 2;
    }

	private void OnScreenNotifierScreenExited()
	{
		QueueFree();
	}

	public void OnBodyEntered(Node2D body)
	{
		GD.Print("collided with " + body.Name);
		LinearVelocity = Vector2.Zero;
		SetDeferred("Freeze", true);
		isMagnetized = true;
	}
}
