using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 300.0f;
	[Export]
	public float JumpVelocity {get; set; } = -400.0f;

	private float gravityMod = 1.25f;

	private Vector2 externalForce = Vector2.Zero;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// add the gravity
		if (!IsOnFloor())
		{
			velocity += GetGravity() * gravityMod * (float)delta;
		}

		// handle jump
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// get the input direction and handle movement/deceleration
		Vector2 direction = Input.GetVector("moveLeft", "moveRight", "moveDown", "moveUp");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		velocity += externalForce; //add any force from external sources to velocity
		externalForce = Vector2.Zero;

		Velocity = velocity;
		MoveAndSlide();
	}

	//for applying magnet forces/other external forces to the player
	public void AddExternalForce(Vector2 force)
	{
		externalForce += force;
	}

}
