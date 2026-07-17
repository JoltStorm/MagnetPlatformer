using Godot;
using System;
using System.Linq;

public partial class Magnet : Area2D
{
	[Export] public float radius = 600f;
	[Export] public float maxForce = 300f;

	private bool isReady = false;

    public override void _Ready()
    {
        var collider = new CollisionShape2D{Shape = new CircleShape2D { Radius = radius}};
		AddChild(collider);
    }


    public override void _PhysicsProcess(double delta)
    {
        foreach (CharacterBody2D body in GetOverlappingBodies().OfType<CharacterBody2D>())
		{
			GD.Print(body.Name);
			PushBody(body, (float)delta);
		}
    }

	private void PushBody(CharacterBody2D body, float delta)
	{
		Player player = (Player)body;

		Vector2 offset = player.GlobalPosition - GlobalPosition;
		float distance = offset.Length();

		float falloff = 1f - Mathf.Clamp(distance / radius, 0f, 1f);
		Vector2 force = offset.Normalized() * maxForce * falloff;

		player.AddExternalForce(force);
		
	}


}
