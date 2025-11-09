using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mainch : MonoBehaviour
{
    public DynamicJoystick joystick;
    public float speed = 25f;
    Rigidbody2D rb;
    Vector2 direction;

    [SerializeField] private GameObject _sellerPanel;
    private bool isSeller = false;

    [Header("Tag Attack Settings")]
    [SerializeField] public float attackRadius = 10f;
    [SerializeField] private string EnemyTag = "Enemy";
    [SerializeField] private float attackCooldownDuration = 1f;
    private float nextAttackAvailableTime = 0f;

    [Header("Base stats")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int skill_baseDMG;
    [SerializeField, Range(0.0f, 1.0f)] protected float skill_critChance;
    [SerializeField] protected float skill_critDMG;
    [SerializeField] protected int currentHealth;

    public bool IsDead => currentHealth <= 0;
    public event System.Action OnDeath;
    void Awake()
    {
        gameObject.tag = "Player";
    }
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D не знайдений на об'єкті!");
        }
        if (joystick == null)
        {
            Debug.LogWarning("Joystick не встановлений в інспекторі!");
        }
        nextAttackAvailableTime = Time.time;    
    }
    protected virtual void FixedUpdate()
    {
        if (joystick == null || rb == null)
            return;
        direction = Vector2.up * joystick.Vertical + Vector2.right * joystick.Horizontal;
        rb.AddForce(direction * speed * Time.deltaTime, ForceMode2D.Impulse);
    }
    protected virtual void Update()
    {
        if (direction != Vector2.zero)
        {
            float yRotation = (direction.x >= 0f) ? 180f : 0f;
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        if (isSeller && Input.GetKeyDown(KeyCode.E))
        {
            _sellerPanel.SetActive(true);
        }

    }
    public virtual void Init(int health, int damage, float critCh, float critDamage)
    {
        maxHealth = health;
        skill_baseDMG = damage;
        skill_critChance = critCh;
        skill_critDMG = critDamage;

        currentHealth = maxHealth;
    }
    public virtual void Die()
    {
        if (IsDead)
        {
            currentHealth = 0;
            Debug.Log($"{gameObject.name} died!");
            OnDeath?.Invoke();
            Application.LoadLevel(Application.loadedLevel);
        }
    }
    public virtual void TakeDMG(float damage)
    {
        if (IsDead) return;

        currentHealth -= Mathf.RoundToInt(damage);

        if (currentHealth <= 0) Die();
    }
    protected virtual int CalculateDamage()
    {
        int damage = skill_baseDMG;
        if (Random.value <= skill_critChance)
        {
            damage *= Mathf.RoundToInt(skill_critDMG);
        }
        return damage;
    }
    public void PerformCooldownAttack()
    {
        if (Time.time < nextAttackAvailableTime)
        {
            float remainingTime = nextAttackAvailableTime - Time.time;
            Debug.LogWarning($"Cooldown. Remaining time: {remainingTime:F2} sec.");
            return;
        }
        nextAttackAvailableTime = Time.time + attackCooldownDuration;
        AttackClosestEnemyByTag();
    }
    protected void AttackClosestEnemyByTag()
    {
        GameObject closestEnemyObject = FindClosestEnemyObjectByTag();

        if (closestEnemyObject != null)
        {
            EnemyBase enemyComponent = closestEnemyObject.GetComponent<EnemyBase>();

            if (enemyComponent != null)
            {
                int damage = CalculateDamage();
                enemyComponent.TakeDMG(damage);
                Debug.Log($"Атаковано {closestEnemyObject.name} (тег '{EnemyTag}') з шкодою {damage}.");
            }
            else
            {
                Debug.LogWarning($"Знайдено об'єкт '{closestEnemyObject.name}' з тегом '{EnemyTag}', але на ньому немає компонента 'Enemy'.");
            }
        }
        else
        {
            Debug.Log($"Ціль з тегом '{EnemyTag}' в радіусі {attackRadius} не знайдена.");
        }
    }
    public GameObject FindClosestEnemyObjectByTag()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag(EnemyTag);

        GameObject closestEnemyObject = null;
        float minDistance = attackRadius + 1f;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemyObject in allEnemies)
        {
            float distanceToEnemy = Vector3.Distance(currentPosition, enemyObject.transform.position);

            if (distanceToEnemy <= attackRadius && distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                closestEnemyObject = enemyObject;
            }
        }
        return closestEnemyObject;
    }
    public virtual void GetHealth(int health)
    {
        if (health <= 0) return;
        if (currentHealth + health >= maxHealth)
            currentHealth = maxHealth;
        else currentHealth += health;
    }

    //Seller Logic
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Seller")
        {
            isSeller = true;    
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Seller")
        {
            isSeller = false;
        }
    }
}
