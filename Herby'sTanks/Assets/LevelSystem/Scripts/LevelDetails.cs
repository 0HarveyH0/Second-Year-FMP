using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDetails : MonoBehaviour
{
    public string missionName;
    public int missionLevel;
    public int enemyCount;
    public bool isPvP;
    public bool isCampaign;

    private void Start()
    {
        isPvP = GetComponent<LevelActivater>().isPvp;
        isCampaign = GetComponent<LevelActivater>().isCampaign;
    }
}
