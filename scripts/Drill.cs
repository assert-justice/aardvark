using Godot;
using System;

public partial class Drill : Area2D
{
    bool powered = false;
    AnimatedSprite2D sprite;
    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }
    public override void _PhysicsProcess(double delta)
    {
        if (powered)
        {
            sprite.Play("drilling");
        }
        else
        {
            sprite.Play("default");
        }
    }
    public void SetPowered(bool val)
    {
        powered = val;
    }
}
