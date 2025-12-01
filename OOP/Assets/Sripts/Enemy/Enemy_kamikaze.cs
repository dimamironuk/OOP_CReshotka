using UnityEngine;

public class ExplosiveEnemy : EnemyBase
{
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float   damage = 40f;
    [SerializeField] private GameObject explosionEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            MainController playerController = collision.gameObject.GetComponent<MainController>();
            if(playerController != null )
            {
                playerController.TakeDMG(damage);
            }

            Destroy(gameObject);
        }
    }
    protected override void OnAttack() 
    {
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var hit in colliders)
        {
            if (hit.CompareTag("Player"))
            {
                MainController playerController = hit.GetComponent<MainController>();
                if (playerController != null)
                {
                    playerController.TakeDMG(damage);
                }
            }
        }

    }
}
