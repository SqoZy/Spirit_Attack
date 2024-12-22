using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{
    [Export] private State initialState;

    private State currentState;
    private Dictionary<string, State> states = new Dictionary<string, State>();

    public override void _Ready()
    {
        for (int i = 0; i < GetChildCount(); i++)
        {
            Node child = GetChild(i);
            if (child is State state)
            {
                states.Add(child.Name.ToString().ToLower(), state);
                state.Connect(nameof(State.Transitioned), new Callable(this, nameof(OnChildTransition)));
            }
        }
        currentState = initialState ?? currentState; // If initialState is null, currentState will be null
        initialState?.Enter(); // If initialState is null, this will not be called
    }

    public void _Process(float delta)
    {
        currentState?.Update(delta);
    }

    public void _PhysicsProcess(float delta)
    {
        currentState?.PhysicsUpdate(delta);
    }

    private void OnChildTransition(State state, string newStateName)
    {
        if (state != currentState) return;
        if (!states.TryGetValue(newStateName.ToLower(), out State nextState)) return;

        currentState?.Exit();
        nextState.Enter();
        currentState = nextState;
    }
}
