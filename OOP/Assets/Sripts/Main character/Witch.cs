using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Mainch
{
    [Header("Witch Stats")]

    [SerializeField] private int maxMana = 100;
    [SerializeField] public int currentMana;
    [SerializeField] private int spellCost = 25;
    protected override void Start()
    {
        base.Start();
        Init(400, 15, 0.3f, 2.0f);
        currentMana = maxMana;
        Debug.Log($"Witch initialized. Health: {maxHealth}, Mana: {maxMana}");
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
        currentMana -= spellCost;
        Debug.Log($"{gameObject.name}: Used {spellCost} Remaining: {currentMana}.");

        float spellDamage = skill_baseDMG * 1.5f;
        if (Random.value <= skill_critChance)
        {
            spellDamage *= skill_critDMG;
            Debug.Log("CRITICAL HIT WITH SPELLS!");
        }
        Debug.Log($"{gameObject.name} attacked with the help of black magic");

        target.TakeDMG(spellDamage);
    }
    public void RestoreMana(int amount)
    {
        currentMana += amount;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }


     public void AddHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Debug.Log($"Player healed by {amount}. HP: {currentHealth}/{maxHealth}");
    }
}
