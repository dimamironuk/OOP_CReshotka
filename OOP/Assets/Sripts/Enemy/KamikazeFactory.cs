using UnityEngine;

public class KamikazeFactory : EnemyFactory
{
    [SerializeField] private GameObject kamikazePrefab;

    public override void CreateEnemy(Vector3 position)
    {
        Instantiate(kamikazePrefab, position, Quaternion.identity);
    }
}