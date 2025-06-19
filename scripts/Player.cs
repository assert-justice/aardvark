using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] float Speed = 300;
    [Export] float DragSpeed = 100;
    Node2D arm;
    RayCast2D ray;
    RigidBody2D held = null;
    public override void _Ready()
    {
        base._Ready();
        arm = GetNode<Node2D>("Arm");
        ray = GetNode<RayCast2D>("Arm/RayCast2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        var move = Input.GetVector("left", "right", "up", "down");
        if (held == null)
        {
            if (move.Length() > 0 && (move.X == 0 || move.Y == 0))
            {
                float aim = move.Angle();
                arm.Rotation = aim;
            }
            Velocity = move * Speed;
            if (Input.IsActionPressed("grab") && ray.IsColliding())
            {
                if (ray.GetCollider() is RigidBody2D rb)
                {
                    held = rb;
                    held.LinearDamp = 0;
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
