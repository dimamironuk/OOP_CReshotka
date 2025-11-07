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
        Init(40, 15, 0.3f, 2.0f);
        currentMana = maxMana;
        Debug.Log($"Відьма ініціалізована. Здоров'я: {maxHealth}, Мана: {maxMana}");
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
            Debug.LogWarning($"{gameObject.name}: Недостатньо мани ({currentMana}/{spellCost}) для CastSpell!");
            return; // Виходимо з функції, якщо мани недостатньо
        }

        // 2. ВИТРАТА МАНИ
        currentMana -= spellCost;
        Debug.Log($"{gameObject.name}: Використано {spellCost} мани. Залишилось: {currentMana}.");

        float spellDamage = skill_baseDMG * 1.5f;
        if (Random.value <= skill_critChance)
        {
            spellDamage *= skill_critDMG;
            Debug.Log("КРИТИЧНИЙ УДАР ЗАКЛИНАННЯМ!");
        }
        Debug.Log($"{gameObject.name} атакувала за допомоги чорної магії");
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
}
