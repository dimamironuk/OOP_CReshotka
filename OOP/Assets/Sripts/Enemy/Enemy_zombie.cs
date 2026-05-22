using UnityEngine;

public class Enemy_zombie : EnemyBase
{
    [Header("Зомбі:")]
    [SerializeField] private float damage = 10f;      
    [SerializeField] private float attackSpeed = 1f;  

    private float _lastAttackTime;

    protected override void SetStats()
    {
        speed = 2.5f;           
        maxHealth = 50;         
        
        sightRange = 8f;        
        attackRange = 2.7f;     

        currentHealth = maxHealth;

        radiusPatrol = 4f;
        timePatrol = 3f;
    }

    protected override void OnAttack()
    {
        if (Time.time >= _lastAttackTime + attackSpeed)
        {
            Bite();
            _lastAttackTime = Time.time;
        }
    }

    private void Bite()
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
