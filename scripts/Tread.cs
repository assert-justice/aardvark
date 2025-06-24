using Godot;
using System;

public partial class Tread : AnimatedSprite2D
{
    enum TreadState
    {
        Forward,
        Stop,
        Back,
    }
    TreadState state;
    public override void _PhysicsProcess(double delta)
    {
        if (state == TreadState.Forward) Play("default");
        else if (state == TreadState.Back) PlayBackwards("default");
        else Stop();
    }
    public void Forward() { state = TreadState.Forward; }
    public void Back(){ state = TreadState.Back; }
    public void StopAnim(){ state = TreadState.Stop; }
}
