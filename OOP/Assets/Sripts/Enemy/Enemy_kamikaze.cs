using UnityEngine;

public class ExplosiveEnemy : EnemyBase
{
    [Header("Kamikaze Unique Stats")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private int _maxHealth = 40;
    
    [Header("AI Settings")]
    [SerializeField] private float _sightRange = 10f;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _patrolRadius = 5f;
    [SerializeField] private float _patrolTime = 2f;

    [Header("Explosion Settings")]
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float damage = 40f;
    [SerializeField] private GameObject explosionEffect;

    protected override void SetStats()
    {
        speed = _moveSpeed;
        maxHealth = _maxHealth;
        sightRange = _sightRange;
        attackRange = _attackRange;
        radiusPatrol = _patrolRadius;
        timePatrol = _patrolTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
if(collision.gameObject.tag == "Player")
{
collision.gameObject.GetComponent<Mainch>().TakeDMG(damage);
Debug.Log("ok");

Destroy(gameObject);
}
}

    protected override void OnAttack() 
    {
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius); 

        foreach (var hit in colliders)
        {
            if (hit.CompareTag("Player"))
            {
                Mainch player = hit.GetComponent<Mainch>();
                if (player != null)
                {
                    player.TakeDMG(damage);
                }
            }
        }
        
        Die(); 
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}