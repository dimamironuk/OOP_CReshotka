using UnityEngine;

public class Enemy_shooting : EnemyBase
{
    [Header("Стрілець:")]
    [SerializeField] private GameObject arrowPrefab; 
    [SerializeField] private float arrowSpeed = 50f;
    [SerializeField] private float fireRate = 1.5f;   

    private float _nextFireTime; 

    protected override void SetStats()
    {
        speed = 3f;             
        maxHealth = 30;         
        sightRange = 12f;       
        attackRange = 8f;       
        
        currentHealth = maxHealth;
        radiusPatrol = 5f;
        timePatrol = 2f;
    }

    protected override void OnAttack()
    {
        if (Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (arrowPrefab != null)
        {
            GameObject newArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            
            Vector2 direction = (target.position - transform.position).normalized;

            Rigidbody2D rbArrow = newArrow.GetComponent<Rigidbody2D>();
            if (rbArrow != null)
            {
                rbArrow.velocity = direction * arrowSpeed;
            }
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            newArrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
