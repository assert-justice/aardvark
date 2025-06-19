using Godot;
using System;

public partial class Socket : Area2D
{
	[Export] public string SocketName;
	int Batteries = 0;
	public override void _Ready()
	{
		BodyEntered += (body) =>
		{
			if (body is RigidBody2D rb)
			{
				Batteries++;
			}
		};
		BodyExited += (body) =>
		{
			if (body is RigidBody2D rb)
			{
				Batteries--;
			}
		};
	}
	public bool IsPowered()
	{
		return Batteries > 0;
	}
}
