using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected Enemy config;

    protected Transform target;
    protected Rigidbody2D rb;

    protected enum State { Patrol, Chase, Attack, Dead }
    protected State currentState;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentState = State.Patrol;
    }

    protected virtual void Update()
    {
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

        if (distance <= config.attackRange)
            currentState = State.Attack;
        else if (distance <= config.sightRange)
            currentState = State.Chase;
        else
            currentState = State.Patrol;
    }

    protected virtual void HandlePatrol()
    {
        if (!IsInvoking(nameof(ChooseNewDirection)))
            InvokeRepeating(nameof(ChooseNewDirection), 0, 3f);

        rb.velocity = patrolDirection * config.Speed;
    }

    Vector2 patrolDirection = Vector2.zero;
    void ChooseNewDirection()
    {
        patrolDirection = Random.insideUnitCircle.normalized;
    }

    protected virtual void HandleChase()
    {
        if (target == null) return;
        Vector2 dir = (target.position - transform.position).normalized;
        rb.velocity = dir * config.Speed;
    }

    protected virtual void HandleAttack()
    {
        rb.velocity = Vector2.zero;
        OnAttack();
    }

    void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Wall"))
    {
        patrolDirection = new Vector2(patrolDirection.y, -patrolDirection.x);
    }
}


    protected abstract void OnAttack();
}
