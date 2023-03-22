using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitilizer : MonoBehaviour
{
    public GameObject[] levelList;
    public bool levelFinished;
    public int levelIndex = 0;

    void Update()
    {
        if (levelFinished)
        {
            Debug.Log("Level has finished");            
            Instantiate(levelList[levelIndex]);
            Debug.Log(levelIndex);
            levelIndex++;
            levelFinished = false;
        }
    }

}
