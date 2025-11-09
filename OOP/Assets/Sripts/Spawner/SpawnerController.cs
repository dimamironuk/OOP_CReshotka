using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class SpawnerController : MonoBehaviour
{
    [Header("Enemy prefab (has EnemyBase/ExplosiveEnemy)")]
    public GameObject enemyPrefab;

    [Header("Wave settings")]
    [Min(1)] public int enemiesPerWave = 5;
    [Min(0f)] public float waveDelay = 2f;

    private readonly HashSet<EnemyDeathRelay> _alive = new();
    private bool _waveScheduled;
    private int _waveIndex;

    private void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("[Spawner] Enemy Prefab not assigned!");
            enabled = false; return;
        }

        SpawnWave();
    }

    private void SpawnWave()
    {
        _waveIndex++;
        _waveScheduled = false;
        _alive.Clear();

        for (int i = 0; i < enemiesPerWave; i++)
        {
            // вороги народжуються з позиції спавнера
            var pos = transform.position;
            var enemyGO = Instantiate(enemyPrefab, pos, Quaternion.identity);
            enemyGO.name = $"Enemy_W{_waveIndex:D2}_{i:D2}";

            var relay = enemyGO.GetComponent<EnemyDeathRelay>();
            if (relay == null) relay = enemyGO.AddComponent<EnemyDeathRelay>();

            relay.Destroyed += OnEnemyDestroyed;
            _alive.Add(relay);
        }

        Debug.Log($"[Spawner] Wave #{_waveIndex} spawned: {_alive.Count} enemies");
    }

    private void OnEnemyDestroyed(EnemyDeathRelay relay)
    {
        if (relay == null) return;
        relay.Destroyed -= OnEnemyDestroyed;
        _alive.Remove(relay);

        if (_alive.Count == 0 && !_waveScheduled)
        {
            _waveScheduled = true;
            Invoke(nameof(SpawnWave), waveDelay);
            Debug.Log($"[Spawner] Wave cleared. Next in {waveDelay:0.00}s");
        }
    }
}
