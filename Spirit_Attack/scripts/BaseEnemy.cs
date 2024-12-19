using Godot;
using System;

public partial class BaseEnemy : CharacterBody2D
{
	protected int speed = 50;
	protected Node2D player;
	protected HealthManager healthManager;

	public override void _Ready()
	{
		player = GetNode<Node2D>("/root/Game/player");
		healthManager = GetNode<HealthManager>("HealthManager");
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta); // call the base physics process
		Move();
	}

	protected virtual void Move()
	{
		if (player == null)
			return;

		Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
		Velocity = direction * speed;
		MoveAndSlide();
	}
}
