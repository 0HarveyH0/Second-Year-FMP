using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowManyPlayers : MonoBehaviour
{
    public int PlayerCount;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
