using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [HideInInspector] protected int maxHealth;
    [HideInInspector] protected float speed;
    
    [HideInInspector] protected float sightRange;
    [HideInInspector] protected float attackRange;
    
    [HideInInspector] protected float radiusPatrol;
    [HideInInspector] protected float timePatrol;

    [HideInInspector] public int currentHealth;
    protected Transform target;
    protected Rigidbody2D rb;

    protected enum State { Patrol, Chase, Attack, Dead }
    protected State currentState;
    private Vector2 patrolDirection = Vector2.zero;

    protected abstract void SetStats(); 

    protected virtual void Awake()
    {
        SetStats(); 
        
        currentHealth = maxHealth;
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentState = State.Patrol;
    }

    protected virtual void Update()
    {
        if (currentState == State.Dead) return;

        switch (currentState)
        {
            case State.Patrol:
                HandlePatrol();
                break;
            case State.Chase:
                HandleChase();
                break;
            case State.Attack:
                HandleAttack();
                break;
        }

        UpdateState();
    }

    private void UpdateState()
    {
        if (target == null)
        {
            currentState = State.Patrol;
            return;
        }

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= attackRange)
            currentState = State.Attack;
        else if (distance <= sightRange)
            currentState = State.Chase;
        else
            currentState = State.Patrol;
    }

    protected virtual void HandlePatrol()
    {
        if (!IsInvoking(nameof(ChooseNewDirection)))
            InvokeRepeating(nameof(ChooseNewDirection), 0, timePatrol);

        rb.velocity = patrolDirection * speed; 
    }

    private void ChooseNewDirection()
    {
        patrolDirection = Random.insideUnitCircle.normalized;
    }

    protected virtual void HandleChase()
    {
        if (target == null) return;
        Vector2 dir = (target.position - transform.position).normalized;
        rb.velocity = dir * speed;
    }

    protected virtual void HandleAttack()
    {
        rb.velocity = Vector2.zero;
        OnAttack();
    }

    protected abstract void OnAttack();

    public void TakeDMG(float damageAmount)
    {
        currentHealth -= (int)damageAmount;
        if (currentHealth <= 0) Die();
    }

    [Header("Loot Settings")]
    [SerializeField] protected GameObject heartPrefab; 
    [SerializeField] protected float heartDropChance = 0.5f;

    public void Die()
    {
        if (currentHealth <= 0)
        {
            TryDropHeart();
            Destroy(gameObject);
        }
    }

    private void TryDropHeart()
    {
        if (heartPrefab != null && Random.value <= heartDropChance)
        {
            Instantiate(heartPrefab, transform.position, Quaternion.identity);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            patrolDirection = new Vector2(patrolDirection.y, -patrolDirection.x);
        }
    }
}