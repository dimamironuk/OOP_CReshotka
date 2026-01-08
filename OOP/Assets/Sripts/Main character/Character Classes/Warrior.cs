using UnityEngine;

public class WarriorCharacter : MainCharacter
{
    private const int W_health = 75;
    private const int W_attackDamage = 20;
    private const int W_skillDamage = 50;
    private const float W_critChance = 0.3f;
    private const float W_critDamage = 2.0f;
    private const float W_skillCD = 5.0f;

    public WarriorCharacter(float attackCooldownDuration)
        : base(attackCooldownDuration, attackCooldownDuration)
    {

    }

    protected override CharacterStats GetInitialStats()
    {
        return new CharacterStats
        {
            maxHealth = W_health,
            attackDamage = W_attackDamage,
            skillDamage = W_skillDamage,
            critChance = W_critChance,
            critDamage = W_critDamage,
            skillCooldownDuration = W_skillCD
        };
    }
    public override void PerformSkillAttack(float currentTime)
    {
        if (currentTime < nextSkillAvailableTime)
        {
            float remainingTime = nextSkillAvailableTime - currentTime;
            Debug.LogWarning($"Warrior Skill on cooldown! Remain: {remainingTime:F2} sec.");
            return;
        }

        nextSkillAvailableTime = currentTime + this.Stats.skillCooldownDuration;

        int damage = CalculateSkillDamage();

        OnSkillRequested?.Invoke(damage,SkillExecutionType.hammer);

        Debug.Log("Warrior uses Skill: shield!");
    }
}
