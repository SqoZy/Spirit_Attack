using Godot;
using System;

public partial class WeaponManager : Node2D
{
	protected float Range = 300;
	protected float speed = 500; // The speed of the bullet

	public override void _Ready()
	{
		var area = GetNode<Area2D>("Area2D"); // Get the Area2D node
		area.Connect("area_entered", new Callable(this, nameof(OnCollision)));
		area.Connect("body_entered", new Callable(this, nameof(OnCollision)));
	}

	public override void _Process(double delta)
	{

	}

	private void OnCollision(Node colisionObject)
	{
		if (colisionObject is Enemy enemy) enemy.GetNode<HealthManager>("HealthManager").TakeDamage(10);
		else if (colisionObject is BossEnemy bossEnemy) bossEnemy.GetNode<HealthManager>("HealthManager").TakeDamage(10);
		else if (colisionObject is Bullet) return;
		else if (colisionObject is Player) return;
		else GD.Print("Bullet collided colisionObject " + colisionObject.Name);
		QueueFree(); // Destroy the bullet
	}
}
