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
    [SerializeField] private int inactive;
    public int round;
    private float timer = 10.07f;
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
                During();
                break;
            case roundStates.end:
                End();
                break;
            
        }
    }
    void SpawnEnemies()
    {          
        inactive = 0;
        SpawnEnemiesRoutine();
        if (enemyList.Count == spawnCount)
        {
            currentState = roundStates.during;
        }
    }
    void During()
    {
        if(inactive == spawnCount)
        {
            currentState = roundStates.end;
        }
        else
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] == null)
                {
                    enemyList.RemoveAt(i);
                    inactive++;
                }
            }
        }
    }

    void End()
    {
        round++;
        currentState = roundStates.start;
    }
    public void GetSpawnCount(int r)
    {
        float beforeRound = (float)(0.000058 * Mathf.Pow(r , 3) + 0.074032 * (Mathf.Pow(r,2) + 0.718119) * (r + 14.738699));
        spawnCount = Mathf.RoundToInt(beforeRound);
    }

    void SpawnEnemiesRoutine()
    {      
        for (int i = 0; i < spawnCount; i++)
        {
            timer -= 1 * Time.deltaTime;
            if (timer <= 0)
            {
                int pointToSpawnAt = Random.Range(0, spawnPoints.Length);
                var enemyObj = Instantiate(enemy, spawnPoints[pointToSpawnAt]);
                enemyList.Add(enemyObj);
                timer = 5f;
            }
        }       
    }
}
