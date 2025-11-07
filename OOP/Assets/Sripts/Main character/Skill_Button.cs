using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Button : MonoBehaviour
{
    // Посилання на Відьму, яка буде виконувати атаку. Встановлюється в Інспекторі.
    [Tooltip("Перетягніть сюди об'єкт з компонентом Witch.")]
    public Witch playerWitch;

    // Радіус, в межах якого буде шукатися ворог.
    [SerializeField] private float searchRadius = 15f;

    // Важливо: Переконайтеся, що всі вороги мають тег "Enemy".
    private const string EnemyTag = "Enemy";

    void Start()
    {
        if (playerWitch == null)
        {
            Debug.LogError("ПОМИЛКА: Компонент Witch не встановлено для кнопки навичок! Атака неможлива.");
        }
    }

    // Ця функція викликається при натисканні на кнопку
    public void OnSkillButtonClick()
    {
        if (playerWitch == null) return;

        // 1. Знаходимо найближчого ворога в радіусі
        EnemyBase closestTarget = FindClosestEnemy();

        if (closestTarget != null)
        {
            // 2. Викликаємо навичку Відьми, передаючи знайденого ворога.
            // Ми використовуємо метод CastSpell, який був визначений у Witch.
            playerWitch.CastSpell(closestTarget);
        }
        else
        {
            Debug.Log("Немає ворогів у радіусі дії для використання навички.");
        }
    }

    // --- Допоміжний метод для пошуку найближчого ворога за тегом ---
    private EnemyBase FindClosestEnemy()
    {
        // 1. Знаходимо всі об'єкти з тегом "Enemy"
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag(EnemyTag);

        EnemyBase closestEnemyComponent = null;
        float minDistance = searchRadius + 1f; // Починаємо з дистанції, більшої за радіус
        Vector3 playerPosition = playerWitch.transform.position;

        foreach (GameObject enemyObject in allEnemies)
        {
            // Перевіряємо, чи ворог живий та чи має компонент Enemy
            EnemyBase enemyComponent = enemyObject.GetComponent<EnemyBase>();
            if (enemyComponent == null) continue;

            // 2. Обчислюємо дистанцію
            float distanceToEnemy = Vector3.Distance(playerPosition, enemyObject.transform.position);

            // 3. Перевіряємо умови: чи в радіусі І чи ближчий за попередньо знайденого
            if (distanceToEnemy <= searchRadius && distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                closestEnemyComponent = enemyComponent;
            }
        }

        return closestEnemyComponent;
    }
}
