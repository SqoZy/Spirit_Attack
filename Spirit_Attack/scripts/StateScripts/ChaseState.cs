using Godot;


public partial class ChaseState : State
{
    public override void Enter()
    {
        base.Enter();
        GD.Print("Entering ChaseState");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Vector2 direction = (player.GlobalPosition - enemy.GlobalPosition).Normalized();
        enemy.Velocity = direction * speed;
        enemy.MoveAndSlide();
        //CheckAttackState();
        CheckWanderState();

    }

    //protected virtual void CheckAttackState() => (enemy.GlobalPosition.DistanceTo(player.GlobalPosition) < attackRange) && ChangeToAttack();

    protected virtual void CheckWanderState()
    {
        if (enemy.GlobalPosition.DistanceTo(player.GlobalPosition) < chaseRange)
            ChangeToWandering();
    }
}
