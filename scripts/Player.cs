using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] float Speed = 300;
	Node2D arm;
	RayCast2D ray;
	public override void _Ready()
	{
		base._Ready();
		arm = GetNode<Node2D>("Arm");
		ray = GetNode<RayCast2D>("Arm/RayCast2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		// float dt = (float)delta;
		var velocity = Velocity;
		var move = Input.GetVector("left", "right", "up", "down");
		if (move.Length() > 0 && (move.X == 0 || move.Y == 0))
		{
			float aim = move.Angle();
			arm.Rotation = aim;
		}
		velocity = move * Speed;
		Velocity = velocity;
		// arm.Visible = false;
		if (ray.IsColliding())
		{
			// arm.Visible = true;
			// GD.Print(ray.GetCollider());
			var target = ray.GetCollider();
			if (target is RigidBody2D rb && Input.IsActionPressed("grab"))
			{
				rb.LinearVelocity = velocity;
			}
		}
		MoveAndSlide();
	}
}
