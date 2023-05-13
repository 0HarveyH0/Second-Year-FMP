using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalHandler : MonoBehaviour
{
    public Transform[] spawnPoints;
    [SerializeField] private bool spawnEnemyBool;
    [SerializeField] private bool spawnEnemyCo;
    [SerializeField] private GameObject enemy;
    [SerializeField] private bool atStartOfRound;
    public int round;
    [SerializeField]private int spawnCount;
    private void Update()
    {
        if (atStartOfRound)
        {
            GetSpawnCount(round);
        }
        if (spawnEnemyBool)
        {
            SpawnEnemies();
        }
    }
    void SpawnEnemies()
    {
        for(int i = 0; i < spawnCount; i++)
        {
            if (spawnEnemyCo)
            {
                StartCoroutine(SpawnEnemiesRoutine());
            }
        }
        spawnEnemyBool = false;
    }

    public void GetSpawnCount(int r)
    {
        float beforeRound = (float)(0.000058 * Mathf.Pow(r , 3) + 0.074032 * (Mathf.Pow(r,2) + 0.718119) * (r + 14.738699));
        spawnCount = Mathf.RoundToInt(beforeRound);
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        int pointToSpawnAt = Random.Range(0, spawnPoints.Length);
        spawnEnemyCo = false;
        Instantiate(enemy, spawnPoints[pointToSpawnAt]);
        yield return new WaitForSecondsRealtime(3f);
        spawnEnemyCo = true;
    }
}
