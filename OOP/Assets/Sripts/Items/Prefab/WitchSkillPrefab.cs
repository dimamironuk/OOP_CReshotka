using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkillProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damageAmount;
    public GameObject owner;

    private Transform targetTransform;
    public void Initialize(Transform target, int damage, GameObject spellOwner)
    {
        targetTransform = target;
        damageAmount = damage;
        owner = spellOwner;
    }
    void Update()
    {
        if (targetTransform == null || !targetTransform.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetTransform.position) < 0.5f)
        {
            ApplyDamageAndDestroy();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == targetTransform.gameObject && other.gameObject != owner)
        {
            ApplyDamageAndDestroy();
        }
    }
    private void ApplyDamageAndDestroy()
    {
        if (targetTransform != null)
        {
            EnemyBase enemy = targetTransform.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.TakeDMG(damageAmount);
            }
        }
        Destroy(gameObject);
    }
}
