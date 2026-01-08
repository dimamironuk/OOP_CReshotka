using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherCharacter : MainCharacter
{
    private const int A_health = 75;
    private const int A_attackDamage = 20;
    private const int A_skillDamage = 50;
    private const float A_critChance = 0.3f;
    private const float A_critDamage = 2.0f;
    private const float A_skillCD = 5.0f;

    public ArcherCharacter(float attackCooldownDuration)
        : base(attackCooldownDuration, attackCooldownDuration)
    {

    }

    protected override CharacterStats GetInitialStats()
    {
        return new CharacterStats
        {
            maxHealth = A_health,
            attackDamage = A_attackDamage,
            skillDamage = A_skillDamage,
            critChance = A_critChance,
            critDamage = A_critDamage,
            skillCooldownDuration = A_skillCD
        };
    }
    public override void PerformSkillAttack(float currentTime)
    {
        if (currentTime < nextSkillAvailableTime)
        {
            float remainingTime = nextSkillAvailableTime - currentTime;
            Debug.LogWarning($"Archer Skill on cooldown! Remain: {remainingTime:F2} sec.");
            return;
        }

        nextSkillAvailableTime = currentTime + this.Stats.skillCooldownDuration;

        int damage = CalculateSkillDamage();

        OnSkillRequested?.Invoke(damage, SkillExecutionType.poisonedArrows);

        Debug.Log("Archer uses Skill: Ice Bolt!");
    }
}
