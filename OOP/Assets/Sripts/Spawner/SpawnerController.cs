using UnityEngine;

public abstract class SpawnerController : MonoBehaviour, EnemyObserver
{
    public GameObject enemyPrefab;
    protected int aliveCount;

    protected virtual void Start()
    {
        Spawn();
    }

    protected abstract void Spawn();

    protected void SpawnEntity(Vector3 position, string name)
    {
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        enemy.name = name;

        EnemyDeathRelay relay = enemy.GetComponent<EnemyDeathRelay>();
        if (relay == null)
            relay = enemy.AddComponent<EnemyDeathRelay>();

        relay.Subscribe(this);
        aliveCount++;
    }

    public void OnEnemyDead()
    {
        aliveCount--;

        if (aliveCount <= 0)
        {
            Spawn();
        }
    }
}