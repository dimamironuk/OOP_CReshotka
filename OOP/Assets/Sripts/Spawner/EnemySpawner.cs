using UnityEngine;

public class EnemySpawner : SpawnerController
{
    public int enemiesPerWave = 5;
    private int waveIndex = 0;

    protected override void Spawn()
    {
        aliveCount = 0;
        waveIndex++;

        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEntity(
                transform.position,
                $"Enemy_W{waveIndex}_{i}"
            );
        }
    }
}
