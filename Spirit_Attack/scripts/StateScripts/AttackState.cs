using Godot;
using System;
using System.Threading.Tasks;

public partial class AttackState : State
{
    [Export] private float attackRange = 100;
    [Export] private float attackCooldown = 1.0f;

    public override void Enter()
    {
        base.Enter();
        GD.Print("Entering AttackState");
        player = GetNode<Node2D>("/root/Game/player");
    }

    public override void PhysicsUpdate(float delta)
    {
        base.PhysicsUpdate(delta);

        if (player == null) return;

            AttackPlayer();

    }

    private async void AttackPlayer()
    {
            GD.Print("Attacking player");
            // initate attack animation
            await Attack();
    }

    private async Task Attack()
    {
        await Task.Delay(TimeSpan.FromSeconds(attackCooldown));

    }

    public override void Exit()
    {
        base.Exit();
    }
}
