using UnityEngine;

public class FactorySpawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory currentFactory;
    
    [SerializeField] private float spawnInterval = 3f;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 1f, spawnInterval);
    }

    private void Spawn()
    {
        if (currentFactory != null)
        {
            currentFactory.CreateEnemy(transform.position);
        }
    }
}