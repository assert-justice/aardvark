using Godot;
using System;
using System.Collections.Generic;

public partial class Mech : CharacterBody2D
{
    [Export] float TurnSpeed = 0.3f;
    [Export] float Speed = 300;
    Dictionary<string, Socket> sockets = [];
    Turret ulTurret;
    Turret blTurret;
    Turret urTurret;
    Turret brTurret;
    Drill drill;
    Tread leftTread;
    Tread rightTread;
    public override void _Ready()
    {
        foreach (var node in GetTree().GetNodesInGroup("Socket"))
        {
            if (node is Socket socket)
            {
                sockets.Add(socket.SocketName, socket);
            }
        }
        ulTurret = GetNode<Turret>("ULTurret");
        blTurret = GetNode<Turret>("BLTurret");
        urTurret = GetNode<Turret>("URTurret");
        brTurret = GetNode<Turret>("BRTurret");
        drill = GetNode<Drill>("Drill");
        leftTread = GetNode<Tread>("LeftTread");
        rightTread = GetNode<Tread>("RightTread");
    }
    public override void _PhysicsProcess(double delta)
    {
        float dt = (float)delta;
        float turn = 0;
        if (IsPowered("Left")) turn -= 1;
        if (IsPowered("Right")) turn += 1;
        Rotation += turn * TurnSpeed * dt;
        float move = 0;
        if (IsPowered("Forward")) move -= 1;
        if (IsPowered("Back")) move += 1;
        var dp = Transform.BasisXform(new(0, move));
        Velocity = dp * Speed;
        MoveAndSlide();
        if (turn < 0)
        {
            leftTread.Back();
            rightTread.Forward();
        }
        else if (turn > 0)
        {
            leftTread.Forward();
            rightTread.Back();
        }
        else if (move > 0)
        {
            leftTread.Back();
            rightTread.Back();
        }
        else if (move < 0)
        {
            leftTread.Forward();
            rightTread.Forward();
        }
        else
        {
            leftTread.StopAnim();
            rightTread.StopAnim();
        }
        ulTurret.SetPowered(IsPowered("ULTurret"));
        urTurret.SetPowered(IsPowered("URTurret"));
        blTurret.SetPowered(IsPowered("BLTurret"));
        brTurret.SetPowered(IsPowered("BRTurret"));
        drill.SetPowered(IsPowered("Drill"));
    }
    bool IsPowered(string name)
    {
        return sockets[name].IsPowered();
    }
}
