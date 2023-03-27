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
            if (transform.GetChild(i).gameObject.name == "SpawnPoint")
            {
                var manager = GameObject.Find("PlayerManager");
                manager.GetComponent<SpawnManager>().spawnPoint = transform.GetChild(i).gameObject;

            }
            levelContents[i].SetActive(true);
        }
        var player = GameObject.Find("Player");
        var playerDetails  = player.GetComponent<PlayerDetails>();
        var spawnpoint = GameObject.Find("PlayerManager");
        if(playerDetails != null)
        {
            if(playerDetails.playerID == 1)
            {
                player.transform.position = spawnpoint.GetComponent<SpawnManager>().spawnPoint.transform.position;
            }
        }
    }
}