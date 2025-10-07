using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{

    [Header("Stats")]
    public int maxHealth = 100;
    public float Speed = 3.5f;


    [Header("Target")]
    public float sightRange = 8f;
    public float attackRange = 1.5f;


    [Header("Patrol")]
    public float radiusPatrol = 6f;
    public float timePatrol = 2f;

    public void TakeDMG(float damageAmount)
{

}
    
}
