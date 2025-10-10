using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mainch : MonoBehaviour, IDamagable
{
    public DynamicJoystick joystick;
    public float speed = 5f;
    Rigidbody2D rb;
    Vector2 direction;

    [Header("Base stats")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int baseDMG;
    [SerializeField, Range(0.0f, 1.0f)] protected float critChance;
    [SerializeField] protected float critDMG;
    public int currentHealth { get; protected set; }
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
    }
    public virtual void Init(int health, int damage, float critCh, float critDamage)
    {
        maxHealth = health;
        baseDMG = damage;
        critChance = critCh;
        critDMG = critDamage;

        currentHealth = maxHealth;
    }
    public virtual void Die()
    {
        if (IsDead)
        {
            currentHealth = 0;
            Debug.Log($"{gameObject.name} вмер!");
            OnDeath?.Invoke();
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
        int damage = baseDMG;
        if (Random.value <= critChance)
        {
            damage *= Mathf.RoundToInt(critDMG);
        }
        return damage;
    }
    public virtual void Attack(IDamagable target)
    {
        int damage = CalculateDamage();
        target.TakeDMG(damage);
    }
}
