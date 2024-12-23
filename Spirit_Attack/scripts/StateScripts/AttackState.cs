using Godot;
using System;

public partial class AttackState : Node
{
    private float range;

    public override void _Ready()
    {
        base._Ready();
        range = 100;
    }
    protected virtual void Attack()
    {
        if (enemy.GlobalPosition.DistanceTo(player.GlobalPosition) < range)
        {
            GD.Print("Attacking player");
        }
    }
}
