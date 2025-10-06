using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mainch : MonoBehaviour, IDamagable
{
    [Header("Base stats")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int baseDMG;
    [SerializeField, Range(0.0f, 1.0f)] protected float critChance;
    [SerializeField] protected float critDMG;
    public float currentHealth { get; protected set; }

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
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log($"{gameObject.name} גלונ!");
        }
    }
    public virtual void TakeDMG(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= Mathf.RoundToInt(damage);

        if (currentHealth <= 0) Die();
    }
    protected virtual float CalculateDamage()
    {
        float Damage = baseDMG;

        float randomRoll = Random.value;

        if (randomRoll <= critChance)
        {
            Damage *= critDMG;
        }
        return Damage;
    }
    public virtual void Attack(IDamagable target)
    {
        float damage = CalculateDamage();
        target.TakeDMG(damage);
    }
}
