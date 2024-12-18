using Godot;
using System;

public partial class Bullet : Node2D
{
    private float Range = 300; // The maximum range of the bullet
    private float speed = 500; // The speed of the bullet
    private float distanceTravelled = 0; // The distance the bullet has travelled

    public override void _Ready()
    {
        var area = GetNode<Area2D>("Area2D"); // Get the Area2D node
        area.Connect("area_entered", new Callable(this, nameof(OnCollision))); // Connect the area_entered signal to the OnCollision method
        area.Connect("body_entered", new Callable(this, nameof(OnCollision))); // Connect the area_entered signal to the OnCollision method
    }

    public override void _Process(double delta)
    {
        float moveAmount = speed * (float)delta; // Calculate the amount to move the bullet
        Position += Transform.X.Normalized() * moveAmount; // Move the bullet in the direction the mouse is pointing
        distanceTravelled += moveAmount; // Increment the distance travelled by the bullet
        if (distanceTravelled >= Range) // If the bullet has travelled the maximum range
            QueueFree(); // Destroy the bullet
    }

    private void OnCollision(Node colisionObject)
    {
        if (colisionObject is Player)
            return;
        else if (colisionObject is Enemy enemy)
            enemy.GetNode<HealthManager>("HealthManager").TakeDamage(10);

        GD.Print("Bullet collided colisionObject " + colisionObject.Name);
        QueueFree(); // Destroy the bullet
    }
}
