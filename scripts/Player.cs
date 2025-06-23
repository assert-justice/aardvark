using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] float Speed = 300;
	[Export] float DragSpeed = 100;
	Node2D arm;
	RayCast2D ray;
	RigidBody2D held = null;
	AnimatedSprite2D sprite;
	int dir;

	public override void _Ready()
	{
		base._Ready();
		arm = GetNode<Node2D>("Arm");
		ray = GetNode<RayCast2D>("Arm/RayCast2D");
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		var move = Input.GetVector("left", "right", "up", "down");
		if (held == null)
		{
			if (move.Length() > 0 && (move.X == 0 || move.Y == 0))
			{
				float aim = move.Angle();
				dir = Mathf.FloorToInt(aim * 4 / Mathf.Tau);
				if (dir == -1) dir = 3;
				arm.Rotation = dir * Mathf.Pi / 2;
			}
			if (move.Length() > 0)
			{
				sprite.Play("walk");
			}
			else
			{
				sprite.Play("default");
			}
			Velocity = move * Speed;
			if (Input.IsActionPressed("grab") && ray.IsColliding())
			{
				if (ray.GetCollider() is RigidBody2D rb)
				{
					held = rb;
					held.LinearDamp = 0;
					string[] strings = ["right", "down", "left", "up"];
					sprite.Play(strings[dir]);
				}
			}
		}
		else
		{
			Velocity = move * DragSpeed;
			held.LinearVelocity = Velocity;
			if (!Input.IsActionPressed("grab") || !ray.IsColliding())
			{
				held.LinearVelocity *= 0;
				held.LinearDamp = 10;
				held = null;
			}
		}
		MoveAndSlide();
	}
}
