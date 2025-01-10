using Godot;
using System;
using System.Threading.Tasks;

public partial class AttackState : State
{
    [Export] private float attackCooldown;
    [Export] private float initiatingAttackTime;
    [Export] private float attackDamage;

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
        await Attack();
    }

    private async Task Attack()
    {
        // initiate attack animation towards the players location.
        await Task.Delay(TimeSpan.FromSeconds(initiatingAttackTime));
        if (enemy.GlobalPosition.DistanceTo(player.GlobalPosition) < attackRange)
        {
            player.GetNode<HealthManager>("HealthManager").TakeDamage((int)attackDamage);
        }
        else GD.Print("Player out of range"); // logic to go back to the chasing state
    }

    public override void Exit()
    {
        base.Exit();
    }
}
