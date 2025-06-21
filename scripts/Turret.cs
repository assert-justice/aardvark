using Godot;
using System;

public partial class Turret : Node2D
{
	bool powered = false;
	RayCast2D ray;
	[Export] float FireTime = 0.1f;
	[Export] float Damage = 10;
	[Export] float TurnSpeed = 1;
	float FireClock = 0;
	public override void _Ready()
	{
		ray = GetNode<RayCast2D>("RayCast2D");
	}
	public override void _PhysicsProcess(double delta)
	{
		float dt = (float)delta;
		if (FireClock > 0)
		{
			FireClock -= dt;
		}
		if (!powered) return;
		// Turn toward closest target
		Node2D target = null;
		float range = Mathf.Inf;
		foreach (var node in GetTree().GetNodesInGroup("Enemy"))
		{
			if (node is Node2D n)
			{
				float len = (n.Position - Position).Length();
				if (len < range)
				{
					target = n;
					range = len;
				}
			}
		}
		if (target != null)
		{
			// Rotate toward target
			float targetAngle = (target.Position - GlobalPosition).Angle();
			float angleDiff = Mathf.Abs(Mathf.AngleDifference(GlobalRotation, targetAngle));
			float da = TurnSpeed * dt;
			if (angleDiff < da) GlobalRotation = targetAngle;
			else {
				float weight = 1 - (angleDiff - da) / angleDiff;
				GlobalRotation = Mathf.LerpAngle(GlobalRotation, targetAngle, weight);
			}
		}
		if (ray.IsColliding())
		{
			if (ray.GetCollider() is Enemy enemy)
			{
				// Try to fire
				if (FireClock <= 0)
				{
					FireClock = FireTime;
					enemy.Damage(Damage);
				}
			}
		}
	}

	public void SetPowered(bool val)
	{
		powered = val;
	}
}
