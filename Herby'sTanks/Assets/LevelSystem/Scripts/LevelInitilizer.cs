using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitilizer : MonoBehaviour
{
    public GameObject[] levelList;
    public GameObject Level;
    public bool levelFinished;
    public int levelIndex = 0;

    private void Start()
    {
        Level = Instantiate(levelList[levelIndex]);
    }
    void Update()
    {
        if (levelFinished)
        {
            Debug.Log("Level has finished");
            DestroyImmediate(Level , true);
            levelIndex++;
            Level = Instantiate(levelList[levelIndex]);
            Debug.Log(levelIndex);
            levelFinished = false;
        }
    }

}
