using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Mainch
{
    [Header("Witch Stats")]

    [SerializeField] private int maxMana = 100;
    [SerializeField] public int currentMana;
    [SerializeField] private int spellCost = 25;
    [SerializeField] private float cooldown = 5f;
    private float timeCount = 0f;

    public GameObject spellProjectilePrefab;
    public Transform spellOriginPoint;
    protected override void Start()
    {
        base.Start();
        Init(400, 15, 0.3f, 2.0f);
        currentMana = maxMana;
        Debug.Log($"Witch initialized. Health: {maxHealth}, Mana: {maxMana}");

        if (spellProjectilePrefab == null)
        {
            Debug.LogError("Witch: Spell projectile prefab not installed!");
        }
        if (spellOriginPoint == null)
        {
            spellOriginPoint = transform;
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();
    }
    public void CastSpell(EnemyBase target)
    {
        if (currentMana < spellCost)
        {
            Debug.LogWarning($"{gameObject.name}: Not enough mana ({currentMana}/{spellCost}) for CastSpell!");
            return;
        }
        if (Time.time < timeCount)
        {
            float remainingTime = Time.time - timeCount;
            Debug.LogWarning($"Cooldown. Remaining time: {remainingTime:F1} sec.");
            return;
        }
        if (spellProjectilePrefab == null) 
            {
            Debug.LogError($"{gameObject.name}: Unable to cast spell, projectile prefab missing!");
            return;
        }
        if (target == null || target.currentHealth <= 0)
        {
            Debug.LogWarning($"{gameObject.name}:The target for the spell is invalid or dead.");
            return;
        }

        timeCount = Time.time + cooldown;
        currentMana -= spellCost;
        Debug.Log($"{gameObject.name}: Used {spellCost} Remaining: {currentMana}.");

        float spellDamage = skill_baseDMG * 1.5f;
        if (Random.value <= skill_critChance)
        {
            spellDamage *= skill_critDMG;
            Debug.Log("CRITICAL HIT WITH SPELLS!");
        }
        Debug.Log($"{gameObject.name} attacked with the help of black magic");

        GameObject newProjectile = Instantiate(spellProjectilePrefab, spellOriginPoint.position, Quaternion.identity);
        WitchSkillProjectile projectileScript = newProjectile.GetComponent<WitchSkillProjectile>();

        if (projectileScript != null)
        {
            projectileScript.Initialize(target.transform, Mathf.RoundToInt(spellDamage), gameObject);
            Debug.Log($"{gameObject.name} fired a projectile at {target.name} with potential harm {Mathf.RoundToInt(spellDamage)}.");
        }
        else
        {
            Debug.LogError("Witch: The created projectile has no component WitchSpellProjectile!");
            Destroy(newProjectile);
        }
    }
    public void RestoreMana(int amount)
    {
        currentMana += amount;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }
}
