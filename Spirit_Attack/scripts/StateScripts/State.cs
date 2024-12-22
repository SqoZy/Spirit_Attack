using Godot;
using System;

public partial class State : Node
{
    [Signal]
    public delegate void TransitionedEventHandler();

    public virtual void Enter()
    {

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

    public void TriggerTransition()
    {
        EmitSignal(nameof(TransitionedEventHandler));
    }
}