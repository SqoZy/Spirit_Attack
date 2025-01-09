using Godot;


public partial class ChaseState : State
{
    private float range = 100;
    public override void Enter()
    {
        base.Enter();
        GD.Print("Entering ChageState");
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
        if (enemy.GlobalPosition.DistanceTo(player.GlobalPosition) < range)
        {
            GD.Print("Attacking player");
        }
    }

    protected virtual void Move()
    {

        Vector2 direction = (player.GlobalPosition - enemy.GlobalPosition).Normalized();
        enemy.Velocity = direction * speed;
        enemy.MoveAndSlide();
    }
}
