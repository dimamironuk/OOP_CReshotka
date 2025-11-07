using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 100;
    public int currentHealth;
    public float Speed = 3.5f;

    [Header("Target")]
    public float sightRange = 8f;
    public float attackRange = 1.5f;

    [Header("Patrol")]
    public float radiusPatrol = 6f;
    public float timePatrol = 2f;

    protected Transform target;
    protected Rigidbody2D rb;

    protected enum State { Patrol, Chase, Attack, Dead }
    protected State currentState;

    private Vector2 patrolDirection = Vector2.zero;

    protected virtual void Awake()
    {
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
            InvokeRepeating(nameof(ChooseNewDirection), 0, 3f);

        rb.velocity = patrolDirection * Speed;
    }

    private void ChooseNewDirection()
    {
        patrolDirection = Random.insideUnitCircle.normalized;
    }

    protected virtual void HandleChase()
    {
        if (target == null) return;
        Vector2 dir = (target.position - transform.position).normalized;
        rb.velocity = dir * Speed;
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

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        currentState = State.Dead;
        rb.velocity = Vector2.zero;
        Destroy(gameObject, 0.2f);

        Debug.Log("Enemy is due!!!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            patrolDirection = new Vector2(patrolDirection.y, -patrolDirection.x);
        }
    }
}
