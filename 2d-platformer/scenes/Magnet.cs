using Godot;
using System;
using System.Linq;

public partial class Magnet : RigidBody2D
{
	[Export] public float radius = 600f;
	[Export] public float maxForce = 300f;

	public bool isMagnetized = false;

    public override void _Ready()
    {
		ContactMonitor = true;
		MaxContactsReported = 2;
		
        var collider = new CollisionShape2D{Shape = new CircleShape2D { Radius = radius}};
		AddChild(collider);
    }

	private void PushBody(CharacterBody2D body)
	{
		Player player = (Player)body;

		Vector2 offset = player.GlobalPosition - GlobalPosition;
		float distance = offset.Length();

		float falloff = 1f - Mathf.Clamp(distance / radius, 0f, 1f);
		Vector2 force = offset.Normalized() * maxForce * falloff;

		player.AddExternalForce(force);
		
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

	private void OnMagnetAreaEntered(Area2D source)
	{
		if (isMagnetized)
		{
			foreach (CharacterBody2D body in source.GetOverlappingBodies().OfType<CharacterBody2D>())
			{
				PushBody(body);
			}	
		}
	}

}