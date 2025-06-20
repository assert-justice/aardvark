using Godot;
using System;

public partial class Turret : Node2D
{
    bool powered = false;
    RayCast2D ray;
    [Export] float FireTime = 0.1f;
    [Export] float Damage = 10;
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
            GlobalRotation = (target.Position - GlobalPosition).Angle();
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
