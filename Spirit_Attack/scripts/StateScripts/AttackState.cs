using Godot;
using System;
using System.Threading.Tasks;

public partial class AttackState : State
{
    protected Node2D player;
    private float range;

    public override void Enter()
    {
        base.Enter();
        range = 100;
        GD.Print("Entering AttackState");
        InitializePlayerAsync();
    }

    private async void InitializePlayerAsync()
    {
        await Task.Delay(2000); // Wait for 2 seconds
        player = GetNode<Node2D>("/root/Game/player");
        if (player == null)
        {
            GD.PrintErr("Player node not found after delay!");
        }
        else
        {
            GD.Print("Player node found after delay.");
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta); // call the base physics process

        Move();
        Attack();
    }

    protected virtual void Attack()
    {
        if (enemy.GlobalPosition.DistanceTo(player.GlobalPosition) < range)
        {
            GD.Print("Attacking player");
        }
    }

    protected virtual void Move()
    {
        if (player == null) return;

        Vector2 direction = (player.GlobalPosition - enemy.GlobalPosition).Normalized();
        enemy.Velocity = direction * speed;
        enemy.MoveAndSlide();
    }

}
