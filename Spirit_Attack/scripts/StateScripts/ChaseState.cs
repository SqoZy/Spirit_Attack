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
        base._PhysicsProcess(delta); // call the base physics process

        if (player != null)
        {
            Move();
            Attack();
        }
    }

    protected virtual void Attack()
    {
        if (enemy.GlobalPosition.DistanceTo(player.GlobalPosition) < attackRange)
        {
            GD.Print("Attacking player");
            //EmitSignal(nameof(Transitioned), "AttackState"); // Emit the signal to transition to AttackState
        }

    }

    protected virtual void Move()
    {

        Vector2 direction = (player.GlobalPosition - enemy.GlobalPosition).Normalized();
        enemy.Velocity = direction * speed;
        enemy.MoveAndSlide();
    }
}
