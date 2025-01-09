using Godot;
using System;

public partial class EnemySpawner : Node2D
{
    [Export]
    private PackedScene enemyScene;
    [Export]
    private PackedScene bossEnemyScene;

    private Node2D player;

    public override void _Ready()
    {
        player = GetNode<Node2D>("/root/Game/player");

        foreach (Node child in GetChildren())
        {
            if (child is Marker2D marker) // check if the child is a Marker2D
            {
                if (marker.Name == "bossEnemyLocation") SpawnBossEnemy(marker.GlobalPosition);
                else if (marker.Name == "enemyLocation") SpawnEnemy(marker.GlobalPosition);
                else GD.PrintErr("Marker2D name is not recognized.");
            }
        }
    }

    private void SpawnEnemy(Vector2 position)
    {
        if (enemyScene != null)
        {
            Enemy enemy = (Enemy)enemyScene.Instantiate();
            enemy.GlobalPosition = position;
            AddChild(enemy);
        }
        else GD.PrintErr("enemyScene is not assigned.");
    }

    private void SpawnBossEnemy(Vector2 position)
    {
        if (bossEnemyScene != null)
        {
            BossEnemy bossEnemy = (BossEnemy)bossEnemyScene.Instantiate();
            bossEnemy.GlobalPosition = position;
            AddChild(bossEnemy);
        }
        else GD.PrintErr("bossEnemyScene is not assigned.");
    }
}
