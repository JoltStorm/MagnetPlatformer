using Godot;
using System;

public partial class MagnetSpawner : Node2D
{

	private PackedScene magnet = ResourceLoader.Load<PackedScene>("res://scenes/magnet.tscn");

	[Export] float Speed { get; set; } = 800f;

	public void ShootMagnet(RayCast2D magnetRay, Vector2 playerPos)
	{
		RigidBody2D newMagnet = (RigidBody2D)magnet.Instantiate();
		

		newMagnet.Position = playerPos;
		var magnetVector = magnetRay.TargetPosition.Normalized();
		
		newMagnet.LinearVelocity = magnetVector * Speed;
		newMagnet.LookAt(magnetRay.TargetPosition);
		
		AddChild(newMagnet);
	}

}
