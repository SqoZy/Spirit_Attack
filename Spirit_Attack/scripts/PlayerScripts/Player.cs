using Godot;
using System;
using System.Diagnostics;

public partial class Player : CharacterBody2D
{
	[Export]
	private int speed = 100;
	private Vector2 currentVelocity;
	private PackedScene bulletScene;
	private HealthManager healthManager;
	private Timer dashTimer;
	private Timer shootCooldownTimer;

	private bool isSprinting = false;
	private float sprintMultiplier = 1.5f;
	private float dashSpeedMultiplier = 10.0f;
	private float dashDuration = 0.2f;
	private float dashCooldown = 0.5f;
	private float lastDashTime = -1.0f;
	private float lastSprintPressTime = -1.0f;
	private float doubleTapTime = 0.3f;
	private float shootCooldown = 0.5f; // Cooldown duration for shooting
	private float lastShootTime = -1.0f;

	public override void _Ready()
	{
		bulletScene = GD.Load<PackedScene>("res://scenes/Bullet.tscn");
		healthManager = GetNode<HealthManager>("HealthManager");

		// Initialize the dash timer
		dashTimer = new Timer();
		dashTimer.WaitTime = dashDuration;
		dashTimer.OneShot = true;
		dashTimer.Connect("timeout", new Callable(this, nameof(EndDash)));
		AddChild(dashTimer);

		// Initialize the shoot cooldown timer
		shootCooldownTimer = new Timer();
		shootCooldownTimer.WaitTime = shootCooldown;
		shootCooldownTimer.OneShot = true;
		AddChild(shootCooldownTimer);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		HandleInput();

		Velocity = currentVelocity;
		MoveAndSlide();
	}

	private void HandleInput() // handle the input of the player
	{
		currentVelocity = Input.GetVector("movementLeftA", "movementRightD", "movementUpW", "movementDownS") * speed;

		if (Input.IsActionJustPressed("projectileFire") && shootCooldownTimer.IsStopped())
			Shoot(); // shoot

		if (Input.IsActionJustPressed("sprint-dash"))
		{
			float currentTime = Time.GetTicksMsec() / 1000.0f;
			if (currentTime - lastSprintPressTime < doubleTapTime)
				Dash(); // dash if double-tap detected
			else
				isSprinting = true;

			lastSprintPressTime = currentTime;
		}

		if (Input.IsActionJustReleased("sprint-dash"))
			isSprinting = false;

		if (isSprinting)
			currentVelocity *= sprintMultiplier;
	}

	private void Shoot()
	{
		float currentTime = Time.GetTicksMsec() / 1000.0f;
		if (currentTime - lastShootTime >= shootCooldown)
		{
			Bullet bullet = (Bullet)bulletScene.Instantiate();
			bullet.GlobalPosition = GlobalPosition;
			bullet.Rotation = GetLocalMousePosition().Angle();
			GetParent().AddChild(bullet);

			lastShootTime = currentTime;
			shootCooldownTimer.Start();
		}
	}

	private void Dash()
	{
		float currentTime = Time.GetTicksMsec() / 1000.0f;
		if (currentTime - lastDashTime > dashCooldown)
		{
			currentVelocity = currentVelocity.Normalized() * speed * dashSpeedMultiplier;
			lastDashTime = currentTime;
			dashTimer.Start();
		}
	}

	private void EndDash() => currentVelocity = Input.GetVector("movementLeftA", "movementRightD", "movementUpW", "movementDownS") * speed;
}
