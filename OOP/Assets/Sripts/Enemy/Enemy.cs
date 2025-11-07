using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{

    [Header("Stats")]
    public int maxHealth = 100;
    [HideInInspector] public int currentHealth;
    public float Speed = 3.5f;


    [Header("Target")]
    public float sightRange = 8f;
    public float attackRange = 1.5f;


    [Header("Patrol")]
    public float radiusPatrol = 6f;
    public float timePatrol = 2f;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDMG(float damageAmount)
    {
        currentHealth = -(int)damageAmount;

    if (currentHealth == 0)
        {
            Die();
    }

    }

public void Die()
    {
        EnemyBase baseScript = GetComponent<EnemyBase>();
        if (baseScript != null)
        {
            Destroy(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
