using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject GoblinPrefab;
    [SerializeField]
    private GameObject BigGoblinPrefab;

    [SerializeField]
    private float GoblinInterval = 4f;
    [SerializeField]
    private float BigGoblinInterval = 10f;

    [SerializeField]
    private float timeToDecreaseInterval = 60f; 
    [SerializeField]
    private float intervalDecreaseAmount = 0.1f;

    void Start()
    {
        StartCoroutine(SpawnEnemy(GoblinInterval, GoblinPrefab));
        StartCoroutine(SpawnEnemy(BigGoblinInterval, BigGoblinPrefab));
        StartCoroutine(DifficultyScaler());
    }
    
    private IEnumerator SpawnEnemy(float initialInterval, GameObject enemy)
    {
        while (true)
        {
            float currentInterval;

            if (enemy == GoblinPrefab)
            {
                currentInterval = GoblinInterval;
            }
            else
            {
                currentInterval = BigGoblinInterval;
            }

            yield return new WaitForSeconds(currentInterval);
            Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
        }
    }

    private IEnumerator DifficultyScaler()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToDecreaseInterval);
            if (GoblinInterval > 0.5f)
            {
                GoblinInterval -= intervalDecreaseAmount;
            }
            if (BigGoblinInterval > 1.0f)
            {
                BigGoblinInterval -= intervalDecreaseAmount;
            }
        }
    }
}