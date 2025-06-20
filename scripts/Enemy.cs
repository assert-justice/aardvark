using Godot;
using System;

public partial class Enemy : Node2D
{
    [Export] float Health = 100;
    public void Damage(float val)
    {
        Health -= val;
        if (Health < 0) Die();
    }
    void Die()
    {
        // QueueFree();
    }
}
