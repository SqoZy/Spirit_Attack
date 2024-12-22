using Godot;
using System;

public partial class BaseEnemy : CharacterBody2D
{
    protected int speed = 50;
    protected Node2D player;
    protected HealthManager healthManager;
    protected StateMachine stateMachine;

    public override void _Ready()
    {
        player = GetNode<Node2D>("/root/Game/player");
        healthManager = GetNode<HealthManager>("HealthManager");
        stateMachine = GetNode<StateMachine>("StateMachine");
    }

    public override void _Process(double delta)
    {
        stateMachine?._Process((float)delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        stateMachine?._PhysicsProcess((float)delta);
    }
}