using Godot;
using System;

public partial class Bullet : WeaponManager
{
	private float Range = 300; // The maximum range of the bullet
	private float speed = 500; // The speed of the bullet
	private float distanceTravelled; // The distance the bullet has travelled

	public override void _Process(double delta)
	{
		float moveAmount = speed * (float)delta; // Calculate the amount to move the bullet
		Position += Transform.X.Normalized() * moveAmount; // Move the bullet in the direction the mouse is pointing
		distanceTravelled += moveAmount; // Increment the distance travelled by the bullet
		if (distanceTravelled >= Range) QueueFree();
	}
}
