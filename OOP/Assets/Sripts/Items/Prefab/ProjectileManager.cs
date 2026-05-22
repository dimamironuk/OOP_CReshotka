using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
             DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("Projectile Prefabs")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject freezerPrefab;
    [SerializeField] private GameObject shurikenPrefab;
    [SerializeField] private GameObject archerArrowPrefab;

    [Header("Projectile Speeds")]
    [SerializeField] private float defaultSpeed = 15f;
    [SerializeField] private float shurikenSpeed = 20f;
    [SerializeField] private float shurikenRotationRate = 360f;

    public void SpawnProjectile(SkillExecutionType type, Transform casterTransform, Transform targetTransform, int damage, GameObject owner)
    {
        if (casterTransform == null) return;

        IMovementStrategy strategy = null;
        GameObject prefabToInstantiate = null;
        float currentSpeed = GetSpeedForType(type);

        switch (type)
        {
            case SkillExecutionType.fireball:
                if (targetTransform == null) return;
                prefabToInstantiate = fireballPrefab;
                strategy = new HomingMovementStrategy(targetTransform, currentSpeed);
                break;

            case SkillExecutionType.freezer:
                if (targetTransform == null) return;
                prefabToInstantiate = freezerPrefab;
                strategy = new HomingMovementStrategy(targetTransform, currentSpeed);
                break;

            case SkillExecutionType.multiArrows:
            case SkillExecutionType.poisonedArrows:
                if (targetTransform == null) return;
                prefabToInstantiate = archerArrowPrefab; 
                strategy = new HomingMovementStrategy(targetTransform, currentSpeed);
                break;
            case SkillExecutionType.shurikens:
                Vector3 initialDir = (targetTransform != null)
                                     ? (targetTransform.position - casterTransform.position).normalized
                                     : casterTransform.up;

                prefabToInstantiate = shurikenPrefab;
                strategy = new SpiralMovementStrategy(initialDir, shurikenSpeed, shurikenRotationRate);
                break;


            default:
                Debug.LogWarning($"Skill {type} should not be spawned as a projectile.");
                return;
        }

        if (prefabToInstantiate == null || strategy == null)
        {
            Debug.LogError($"Cannot spawn projectile for type {type}. Missing Prefab/Strategy.");
            return;
        }

        GameObject projectileGO = Instantiate(prefabToInstantiate, casterTransform.position, Quaternion.identity);
        SkillProjectileComponent projectileScript = projectileGO.GetComponent<SkillProjectileComponent>();
        if (projectileScript != null)
        {
            projectileScript.Initialize(strategy, damage, owner);
        }
        else
        {
            Debug.LogError($"Projectile prefab {prefabToInstantiate.name} is missing WitchSkillProjectile script!");
            Destroy(projectileGO);
        }
    }
    private float GetSpeedForType(SkillExecutionType type)
    {
        switch (type)
        {
            case SkillExecutionType.shurikens:
                return shurikenSpeed;
            default:
                return defaultSpeed;
        }
    }
}