using UnityEngine;

public class ExplosiveEnemy : EnemyBase
{
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float explosionDamage = 40f;
    [SerializeField] private GameObject explosionEffect;

    protected override void OnAttack()
    {
        // Ефект вибуху (опціонально)
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // Знаходимо всі об’єкти навколо
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var hit in colliders)
        {
            if (hit.CompareTag("Player"))
            {
                Mainch player = hit.GetComponent<Mainch>();
                if (player != null)
                {
                    player.TakeDMG(explosionDamage);
                }
            }
        }

        // Самознищення ворога
        Destroy(gameObject);
    }
}
