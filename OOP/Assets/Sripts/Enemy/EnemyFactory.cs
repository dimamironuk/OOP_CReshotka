using UnityEngine;

public abstract class EnemyFactory : MonoBehaviour
{
    public abstract void CreateEnemy(Vector3 position);
}