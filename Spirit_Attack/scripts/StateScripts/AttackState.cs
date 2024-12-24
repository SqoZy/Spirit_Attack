using Godot;
using System;

public partial class AttackState : State
{
    protected Node2D player;
    private float range;

    public override void _Ready()
    {
        player = GetNode<Node2D>("/root/Game/player");

        range = 100;
    }

    public virtual void Enter() => base.Enter();

    public virtual void Exit() => base.Enter();

    public virtual void Update(float delta)
    {
        // Your update logic here
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta); // call the base physics process
        if (Move == null)
        {
            GD.Print("nomove");
            return;
        }
        Move();
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
        if (enemy == null) return;


        Vector2 direction = (player.GlobalPosition - enemy.GlobalPosition).Normalized();
        enemy.Velocity = direction * speed;
        enemy.MoveAndSlide();
    }

}
