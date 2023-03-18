using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelActivater : MonoBehaviour
{
    public List<GameObject> levelContents;
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            levelContents.Add(transform.GetChild(i).gameObject);
            levelContents[i].SetActive(true);
        }
    }
}