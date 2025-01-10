using Godot;
using System;

public partial class State : Node
{
    [Export] protected CharacterBody2D enemy;
    [Export] protected float speed;
    [Export] protected float chaseRange;
    [Export] protected float attackRange;
    protected Node2D player;

    [Signal]
    public delegate void TransitionedEventHandler();

    public virtual void Enter()
    {
        player = GetNode<Node2D>("/root/Game/player");
    }

    public virtual void Exit()
    {

    }

    public virtual void Update(float delta)
    {
        // Your update logic here
    }

    public virtual void PhysicsUpdate(float delta)
    {
        // Your physics update logic here
    }
}
