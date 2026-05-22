using UnityEngine;

public class Enemy_boss_strange : EnemyBase
{
    [Header("Бос:")]
    [SerializeField] private float damage = 50f;     
    [SerializeField] private float attackSpeed = 4f; 

    private float _lastAttackTime;

    protected override void SetStats()
    {
        speed = 1.5f;          
        maxHealth = 150;        
        
        sightRange = 10f;      
        attackRange = 5f;     

        currentHealth = maxHealth;
        
        radiusPatrol = 5f;
        timePatrol = 4f; 
    }

    protected override void OnAttack()
    {
        if (Time.time >= _lastAttackTime + attackSpeed)
        {
            Attack();
            _lastAttackTime = Time.time;
        }
    }

    private void Attack()
    {
        if (target != null)
        {
            MainController player = target.GetComponent<MainController>();
            
            if (player != null)
            {
                player.TakeDMG(damage); 
            }
        }
    }
}