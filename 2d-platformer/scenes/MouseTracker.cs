using Godot;
using System;
using System.ComponentModel;

public partial class MouseTracker : Node2D
{

	private Vector2 _mousePos;
	private Vector2 _lastClick;

	private CharacterBody2D player;
	private RayCast2D magnetRay;
	private Line2D line2D;

	private MagnetSpawner MagnetSpawner;

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.ConfinedHidden;

		Position = Vector2.Zero;
		player = GetParent<CharacterBody2D>();
		// MagnetSpawner is a sibling node; you have to get it from the scene root 
		MagnetSpawner = GetNode<MagnetSpawner>("/root/Main/MagnetSpawner");
		magnetRay = player.GetNode<RayCast2D>("MagnetRaycast");
		line2D = player.GetNode<Line2D>("MagnetRaycast/RayLine");

		magnetRay.CollideWithAreas = true;
		magnetRay.CollideWithBodies = false;
	}

	public override void _Input(InputEvent @event)
	{
		//track mouse click position
		if(@event.IsActionPressed("shoot"))
		{
			MagnetSpawner.ShootMagnet(magnetRay, player.Position);
		}

		//track mouse movement position
		else if(@event is InputEventMouseMotion eventMouseMotion)
			_mousePos = eventMouseMotion.Position;
	}

	public override void _Process(double delta)
	{
		//use global position to set cursor position 
		GlobalPosition = _mousePos;

		magnetRay.Position = Vector2.Zero; //raycast is a child of player, not cursor
		magnetRay.TargetPosition = Position; //set targetpos to cursor

		line2D.Points = [magnetRay.Position, Position];
		
	}

}
