using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Button : MonoBehaviour
{
    public Witch playerWitch;
    [SerializeField] private float searchRadius = 15f;

    private const string EnemyTag = "Enemy";

    void Start()
    {
        if (playerWitch == null)
        {
            Debug.LogError("ERROR: Witch component not installed for skill button! Attack is not possible.");
        }
    }
    public void OnSkillButtonClick()
    {
        if (playerWitch == null) return;

        EnemyBase closestTarget = FindClosestEnemy();

        if (closestTarget != null)
        {
            playerWitch.CastSpell(closestTarget);
        }
        else
        {
            Debug.Log("There are no enemies within range to use the skill.");
        }
    }
    private EnemyBase FindClosestEnemy()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag(EnemyTag);

        EnemyBase closestEnemyComponent = null;
        float minDistance = searchRadius + 1f; 
        Vector3 playerPosition = playerWitch.transform.position;

        foreach (GameObject enemyObject in allEnemies)
        {
            EnemyBase enemyComponent = enemyObject.GetComponent<EnemyBase>();
            if (enemyComponent == null) continue;

            float distanceToEnemy = Vector3.Distance(playerPosition, enemyObject.transform.position);

            if (distanceToEnemy <= searchRadius && distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                closestEnemyComponent = enemyComponent;
            }
        }

        return closestEnemyComponent;
    }
}
