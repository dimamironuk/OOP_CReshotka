using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    private Animator animator;
    public DynamicJoystick joystick;
    public float speed = 5f;
    private MainCharacter characterLogic;
    private Rigidbody2D rb;
    private Vector2 direction;

    [Header("UI")]
    [SerializeField] private Slider _hpBar;

    [Header("Attack Settings (Unity Dependent)")]
    [SerializeField] private CharacterType selectedCharacterType = CharacterType.Witch;
    [SerializeField] private SkillExecutionType selectedSkillType = SkillExecutionType.fireball;
    [SerializeField] public float attackRadius = 10f;
    [SerializeField] private string EnemyTag = "Enemy";
    [SerializeField] private float attackCooldown = 1f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Controller: Rigidbody2D wasn`t find!");
            return;
        }

        gameObject.tag = "Player";

        try
        {
            characterLogic = CharacterFactory.CreateCharacter(selectedCharacterType, attackCooldown);

            characterLogic.OnHealthStatsChanged += UpdateHealthBar;
            characterLogic.OnDeathOccurred += HandleCharacterDeath;
            characterLogic.OnAttackRequested += PerformAttack;
            characterLogic.OnSkillRequested += PerformSkill;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error making character: {e.Message}");
        }
    }

    void FixedUpdate()
    {
        if (joystick == null || rb == null) return;
        Vector2 targetVelocity = direction.normalized * speed;
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, 5f*Time.fixedDeltaTime);
    }

    void Update()
    {
        direction = new Vector2(joystick.Horizontal, joystick.Vertical);

        if (direction != Vector2.zero)
        {
            float yRotation = (direction.x >= 0f) ? 0f : 180f;
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
            animator.SetBool("isWalk", true);
        }
        else animator.SetBool("isWalk", false);

    }

    public void PerformAttack_B()
    {
        if (characterLogic == null) return;

        characterLogic.TryPerformAttack(Time.time);
    }
    public void PerformSkill_B()
    {
        if (characterLogic == null) return;

        characterLogic.TryPerformSkill(Time.time);
    }
    private void PerformAttack(int damage)
    {
        GameObject closestEnemyObject = FindClosestEnemyObjectByTag();

        if (closestEnemyObject == null)
        {
            Debug.Log("Attack target not found.");
            return;
        }
        ApplyDamageToEnemy(closestEnemyObject, damage);
    }

    private void PerformSkill(int damage, SkillExecutionType type)
    {
        GameObject closestEnemyObject = FindClosestEnemyObjectByTag();
        Transform targetTransform = (closestEnemyObject != null) ? closestEnemyObject.transform : null;
        if (closestEnemyObject == null)
        {
            Debug.Log("Skill target not found.");
            return;
        }

        animator.SetBool("isUlta", true);
        switch (type) {
            case SkillExecutionType.fireball:
            case SkillExecutionType.freezer:
            case SkillExecutionType.multiArrows:
            case SkillExecutionType.poisonedArrows:
                ProjectileManager.Instance.SpawnProjectile(
                    type,
                    transform,
                    targetTransform,
                    damage,
                    gameObject
                );
                break;

            case SkillExecutionType.shurikens:
                ProjectileManager.Instance.SpawnProjectile(
                    type,
                    transform,
                    targetTransform,
                    damage,
                    gameObject
                );
                break;
            case SkillExecutionType.hammer:
                if (closestEnemyObject != null)
                {
                    ApplyDamageToEnemy(closestEnemyObject, damage);
                }
                break;
            case SkillExecutionType.lunge:
                PerformLungeAttack(damage);
                break;
            case SkillExecutionType.shield:
                break;
            default:
                Debug.LogError($"Unhandled SkillExecutionType: {type}. Перевірте, чи всі нащадки MainCharacter визначили коректний тип.");
                break;

        }
        //animator.SetBool("isUlta", false);
    }

    void PerformLungeAttack(int damage)
    {
        Debug.Log($"Lunge attack was performed");
    }


    private void ApplyDamageToEnemy(GameObject target, int damage)
    {
        EnemyBase enemyComponent = target.GetComponent<EnemyBase>();
        animator.SetBool("isAttack", true);
        if (enemyComponent != null)
        {
            enemyComponent.TakeDMG(damage);
            Debug.Log($"Hit {target.name} for {damage}.");
        }
        else
        {
            Debug.LogWarning($"Target {target.name} is not an Enemy.");
        }
        animator.SetBool("isAttack", false);
    }

    public void TakeDMG(float damage)
    {
        if (characterLogic == null) return;
        characterLogic.TakeDMG(damage);
    }
    public void AddHealth(int amount)
    {
        if (characterLogic == null) return;
        characterLogic.AddHealth(amount);
    }

    private void UpdateHealthBar(int currentHP, int maxHP)
    {
        if (_hpBar != null)
        {
            _hpBar.maxValue = maxHP;
            _hpBar.value = currentHP;
            Canvas.ForceUpdateCanvases();
        }
    }

    private void HandleCharacterDeath()
    {
        Debug.Log($"{gameObject.name} died. Reloading scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private GameObject FindClosestEnemyObjectByTag()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag(EnemyTag);

        GameObject closestEnemyObject = null;
        float minDistance = attackRadius + 1f;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemyObject in allEnemies)
        {
            float distanceToEnemy = Vector3.Distance(currentPosition, enemyObject.transform.position);

            if (distanceToEnemy <= attackRadius && distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                closestEnemyObject = enemyObject;
            }
        }
        return closestEnemyObject;
    }
}
