using System;
using UnityEngine;

public struct CharacterStats
{
    public int maxHealth;
    public int attackDamage;
    public int skillDamage;
    public float critChance;
    public float critDamage;
    public float skillCooldownDuration;
    public SkillExecutionType executionType;
}

public abstract class MainCharacter
{
    public Action<int, int> OnHealthStatsChanged;
    public Action OnDeathOccurred;
    public Action<int> OnAttackRequested;
    public Action<int, SkillExecutionType> OnSkillRequested;
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public bool IsDead => CurrentHealth <= 0;

    protected CharacterStats Stats { get; private set; }

    private float attackCooldownDuration;
    private float nextAttackAvailableTime = 0f;

    private float skillCooldownDuration;
    protected float nextSkillAvailableTime = 0f;

    protected int attack_baseDamage;
    protected int skill_baseDamage;
    protected float critChance;
    protected float critDamage;

    protected abstract CharacterStats GetInitialStats();

    public MainCharacter(float cooldownAttackDuration, float cooldownSkillDuration)
    {
        this.attackCooldownDuration = cooldownAttackDuration;
        this.skillCooldownDuration = cooldownSkillDuration;

        this.Stats = GetInitialStats();

        this.MaxHealth = this.Stats.maxHealth;
        this.CurrentHealth = this.Stats.maxHealth;
        this.skill_baseDamage = this.Stats.skillDamage;
        this.attack_baseDamage = this.Stats.attackDamage;
        this.critChance = this.Stats.critChance;
        this.critDamage = this.Stats.critDamage;

        NotifyHealthStatsChanged();
    }

    public virtual void TakeDMG(float damage)
    {
        if (IsDead) return;

        CurrentHealth -= Mathf.RoundToInt(damage);

        NotifyHealthStatsChanged();

        if (CurrentHealth <= 0) Die();
    }

    public virtual int CalculateAttackDamage()
    {
        int damage = this.Stats.attackDamage;

        if (UnityEngine.Random.value <= this.Stats.critChance)
        {
            damage = Mathf.RoundToInt(damage * this.Stats.critDamage);
            Debug.Log($"CRITICAL HIT! Damage: {damage}");
        }
        return damage;
    }
    //          #################################################################33          ÏÅÐÅÄÅËÀÒÜ îäíî è òî æå áóêâàëüíî 
    public virtual int CalculateSkillDamage()
    {
        int damage = this.Stats.skillDamage;

        if (UnityEngine.Random.value <= this.Stats.critChance)
        {
            damage = Mathf.RoundToInt(damage * this.Stats.critDamage);
            Debug.Log($"CRITICAL HIT! Damage: {damage}");
        }
        return damage;
    }

    public void AddHealth(int amount)
    {
        if (amount <= 0) return;

        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;

        NotifyHealthStatsChanged();
        Debug.Log($"Player healed by {amount}. HP: {CurrentHealth}/{MaxHealth}");
    }
    public virtual void Die()
    {
        if (IsDead)
        {
            CurrentHealth = 0;
            Debug.Log($"Character died!");
            NotifyHealthStatsChanged();

            OnDeathOccurred?.Invoke();
        }
    }

    public void TryPerformAttack(float currentTime)
    {
        if (currentTime < nextAttackAvailableTime)
        {
            float remainingTime = nextAttackAvailableTime - currentTime;
            Debug.LogWarning($"Cooldown ATTACK. Remaining time: {remainingTime:F2} sec.");
            return;
        }

        nextAttackAvailableTime = currentTime + attackCooldownDuration;

        OnAttackRequested?.Invoke(CalculateAttackDamage());
    }
    public void TryPerformSkill(float currentTime)
    {
        if (currentTime < nextSkillAvailableTime)
        {
            float remainingTime = nextSkillAvailableTime - currentTime;
            Debug.LogWarning($"Cooldown SKKILL. Remaining time: {remainingTime:F2} sec.");
            return;
        }

        nextSkillAvailableTime = currentTime + skillCooldownDuration;

        OnSkillRequested?.Invoke(CalculateSkillDamage(), this.Stats.executionType);
    }
    public abstract void PerformSkillAttack(float currentTime);

    private void NotifyHealthStatsChanged()
    {
        OnHealthStatsChanged?.Invoke(CurrentHealth, MaxHealth);
    }
}
