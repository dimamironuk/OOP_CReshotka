using UnityEngine;

public class ExplosiveEnemy : EnemyBase
{
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float explosionDamage = 40f;
    [SerializeField] private GameObject explosionEffect;

    protected override void OnAttack()
    {
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

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

        Destroy(gameObject);
    }
}
