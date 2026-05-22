using UnityEngine;

public class BossSpawner : SpawnerController
{
    protected override void Spawn()
    {
        aliveCount = 0;
        SpawnEntity(transform.position, "Boss");
    }
}
