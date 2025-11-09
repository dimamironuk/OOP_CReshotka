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

        GameObject closestTarget = playerWitch.FindClosestEnemyObjectByTag();
        EnemyBase target = closestTarget.GetComponent<EnemyBase>();

        if (closestTarget != null)
        {
            playerWitch.CastSpell(target);
        }
        else
        {
            Debug.Log("There are no enemies within range to use the skill.");
        }
    }
}
