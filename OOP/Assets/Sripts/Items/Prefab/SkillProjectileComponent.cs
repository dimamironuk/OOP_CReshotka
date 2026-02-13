using UnityEngine;

public class SkillProjectileComponent : MonoBehaviour
{
    protected IMovementStrategy movementStrategy;
    protected int damageAmount;
    protected GameObject owner;

    [Header("Life Cycle")]
    [SerializeField] private float maxLifeTime = 5f;

    private float launchTime;

    public virtual void Initialize(IMovementStrategy strategy, int damage, GameObject owner)
    {
        this.movementStrategy = strategy;
        this.damageAmount = damage;
        this.owner = owner;
        this.launchTime = Time.time;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    protected virtual void Update()
    {
        movementStrategy?.Move(transform, Time.deltaTime);

        if (Time.time >= launchTime + maxLifeTime)
        {
            DestroyProjectile();
            return;
        }

        if (movementStrategy != null && !movementStrategy.IsTargetValid())
        {
            DestroyProjectile();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner) return;

        if (collision.CompareTag("Wall"))
        {
            DestroyProjectile();
        }

        if (collision.CompareTag("Enemy"))
        {
            ApplyDamageToTarget(collision.gameObject);
            DestroyProjectile();
        }

        
    }

    protected virtual void ApplyDamageToTarget(GameObject target)
    {
        EnemyBase enemy = target.GetComponent<EnemyBase>();

        if (enemy != null)
        {
            enemy.TakeDMG(damageAmount);
            Debug.Log($"{gameObject.name} hit {target.name} for {damageAmount}.");
        }
    }

    protected virtual void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}