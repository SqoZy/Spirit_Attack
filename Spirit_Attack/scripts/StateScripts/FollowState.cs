using Godot;
using System;

public partial class FollowState : State
{
    private bool chasing;
    private float floattocheck;
    public override void Enter()
    {
        base.Enter();
        GD.Print("enetering stae");
        StartChase();
    }

    protected virtual void StartChase()
    {
        while (true)
        {
            chasing = true;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        floattocheck++;
        GD.Print(floattocheck);
        base._PhysicsProcess(delta);
        if (chasing)
        {
            Vector2 direction = (player.GlobalPosition - enemy.GlobalPosition).Normalized();
            enemy.Velocity = direction * speed;
            enemy.MoveAndSlide();
        }
        floattocheck = 0;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Velocity = Vector2.Zero;
    }

    // protected virtual void CheckWanderState()
    // {
    //     if (enemy.GlobalPosition.DistanceTo(player.GlobalPosition) < chaseRange)
    //         ChangeToWandering();
    // }
}
