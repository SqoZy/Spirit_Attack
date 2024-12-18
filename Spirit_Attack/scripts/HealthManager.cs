using Godot;
using System;

public partial class HealthManager : Node
{
    [Export]
    public int MaxHealth { get; set; } = 100;
    public int CurrentHealth { get; private set; }

    public override void _Ready()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        GD.Print("Health: " + CurrentHealth);
        if (CurrentHealth <= 0)
            Die();
    }

    private void Die() => GetParent().QueueFree(); // Destroy the parent node (Enemy)

}
