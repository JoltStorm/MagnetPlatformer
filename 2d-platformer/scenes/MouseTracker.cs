using Godot;
using System;
using System.ComponentModel;

public partial class MouseTracker : Node2D
{

	private Vector2 _mousePos;
	private Vector2 _lastClick;

	private CharacterBody2D player;
	private RayCast2D rayCast2D;
	private Line2D line2D;

    public override void _Ready()
    {
        Position = Vector2.Zero;
		player = GetParent<CharacterBody2D>();
		rayCast2D = player.GetNode<RayCast2D>("RayCast2D");
		line2D = rayCast2D.GetNode<Line2D>("Line2D");
    }

    public override void _Input(InputEvent @event)
    {
		if(@event is InputEventMouseButton eventMouseButton)
			_lastClick = eventMouseButton.Position;

		//track mouse position
		else if(@event is InputEventMouseMotion eventMouseMotion)
			_mousePos = eventMouseMotion.Position;
    }

    public override void _Process(double delta)
    {
		//use global position to set cursor position 
        GlobalPosition = _mousePos;

		rayCast2D.Position = Vector2.Zero; //raycast is a child of player, not cursor
		rayCast2D.TargetPosition = Position; //set targetpos to cursor

		line2D.Points = [rayCast2D.Position, Position];
		
    }

}
