using Godot;
using System;

public partial class Socket : Area2D
{
	[Export] string SocketName;
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
}
