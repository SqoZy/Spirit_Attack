using Godot;
using System;
using System.Threading.Tasks;

public partial class WanderingState : State
{
    [Export] private CharacterBody2D enemy;
    [Export] private float speed = 50;
    [Export] private float wanderRadius = 200; // Maximum distance from the central point
    [Export] private float stopDuration = 2.0f; // Duration to stop before wandering again

    private Vector2 moveDirection;
    private Random random = new Random();
    private Vector2 centralPoint;
    private bool isWandering = false;
    private bool isStopping = false;

    private void RandomizeWander()
    {
        float angle = (float)(random.NextDouble() * Math.PI * 2);
        moveDirection = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

        // Adjust the direction if the enemy is near the edge of the radius
        if (enemy.GlobalPosition.DistanceTo(centralPoint) > wanderRadius * 0.9f)
        {
            moveDirection = (centralPoint - enemy.GlobalPosition).Normalized();
        }
    }

    public override void Enter()
    {
        base.Enter();
        centralPoint = enemy.GlobalPosition; // Set the central point to the current position of the enemy
        StartWandering();
    }

    private async void StartWandering()
    {
        while (true)
        {
            if (!isStopping)
            {
                RandomizeWander();
                isWandering = true;
                await Task.Delay(TimeSpan.FromSeconds(random.NextDouble() * 4 + 1)); // Wander for a random duration between 1 and 5 seconds
                isWandering = false;
            }

            await Task.Delay(TimeSpan.FromSeconds(stopDuration)); // Stop for a few seconds before wandering again
            isStopping = false;
        }
    }

    public override void PhysicsUpdate(float delta)
    {
        if (enemy != null && isWandering && !isStopping)
        {
            Vector2 newPosition = enemy.GlobalPosition + moveDirection * speed * delta;

            // Check if the new position is within the wander radius
            if (newPosition.DistanceTo(centralPoint) > wanderRadius)
            {
                // If the new position is outside the radius, stop the enemy and wait
                isStopping = true;
                isWandering = false;
            }
            else
            {
                enemy.Velocity = moveDirection * speed;
                enemy.MoveAndSlide();
            }
        }
        else
        {
            enemy.Velocity = Vector2.Zero;
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Velocity = Vector2.Zero;
    }
}
