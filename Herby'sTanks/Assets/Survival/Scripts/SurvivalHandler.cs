using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum roundStates
{
    start,
    during,
    end
}
public class SurvivalHandler : MonoBehaviour
{
    public Transform[] spawnPoints;
    [SerializeField] private bool spawnEnemyBool;
    [SerializeField] private bool spawnEnemyCo;
    [SerializeField] private GameObject enemy;
    [SerializeField] private List<GameObject> enemyList;
    [SerializeField] private bool atStartOfRound;
    [SerializeField] private roundStates currentState;
    public int round;
    [SerializeField]private int spawnCount;
    private void Update()
    {
        switch (currentState)
        {
            case roundStates.start:
                GetSpawnCount(round);
                SpawnEnemies();
                break;
            case roundStates.during:
                break;
            case roundStates.end:
                break;
        }
    }
    void SpawnEnemies()
    {
                 
             StartCoroutine(SpawnEnemiesRoutine());           
        
        currentState = roundStates.during;
    }

    public void GetSpawnCount(int r)
    {
        float beforeRound = (float)(0.000058 * Mathf.Pow(r , 3) + 0.074032 * (Mathf.Pow(r,2) + 0.718119) * (r + 14.738699));
        spawnCount = Mathf.RoundToInt(beforeRound);
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        for(int i = 0; i < spawnCount; i++)
        {
            int pointToSpawnAt = Random.Range(0, spawnPoints.Length);
            var enemyObj = Instantiate(enemy, spawnPoints[pointToSpawnAt]);
            enemyList.Add(enemyObj);
            yield return new WaitForSecondsRealtime(5f);
        }
    }
}
