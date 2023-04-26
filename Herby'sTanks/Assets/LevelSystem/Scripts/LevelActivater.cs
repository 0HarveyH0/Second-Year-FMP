using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelActivater : MonoBehaviour
{
    public GameObject LevelInit;
    public List<GameObject> levelContents;
    public List<GameObject> Enemies;
    public int enemyCount;
    public int currentEnemyCount;
    void Start()
    {
        LevelInit = GameObject.Find("LevelManager");
        for(int i = 0; i < transform.childCount; i++)
        {
            levelContents.Add(transform.GetChild(i).gameObject);
            if (transform.GetChild(i).gameObject.name == "P1SpawnPoint")
            {
                var manager = GameObject.Find("PlayerManager");
                manager.GetComponent<SpawnManager>().P1SpawnPoint = transform.GetChild(i).gameObject;
            }
            levelContents[i].SetActive(true);
        }
        var player = GameObject.Find("Player");
        var spawnpoint = GameObject.Find("PlayerManager");
        foreach(GameObject enemy in levelContents)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Enemies.Add(enemy);
            }
        }
        enemyCount = Enemies.Count;
        currentEnemyCount = enemyCount;
    }
    void checkEnemyState()
    {
        for(int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i] == null)
            {
                currentEnemyCount--;
                Enemies.RemoveAt(i);
            }
        }
    }
    private void Update()
    {
        if(currentEnemyCount == 0)
        {
            Debug.Log("EnemyAlldead");
            var levelInitilizer = LevelInit.GetComponent<LevelInitilizer>();
            levelInitilizer.levelFinished = true;
        }
        else
        {
            checkEnemyState();
        }
    }
}